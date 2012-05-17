using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;

namespace AcklenAvenue.Data.NHibernate
{
    public class DatabaseDeployer : IDatabaseDeployer
    {
        readonly Configuration _nhibernateConfiguration;

        public DatabaseDeployer(Configuration nhibernateConfiguration)
        {
            _nhibernateConfiguration = nhibernateConfiguration;
        }

        public void Drop()
        {
            DropAllForeignKeysFromDatabase();
            var schemaExport = new SchemaExport(_nhibernateConfiguration);

            schemaExport.Drop(true, true);
        }

        public void Create()
        {
            var schemaExport = new SchemaExport(_nhibernateConfiguration);
            schemaExport.Create(true, true);
        }

        public void Seed(List<IDataSeeder> seeders)
        {
            seeders.ForEach(x => x.Seed());
        }

        void DropAllForeignKeysFromDatabase()
        {
            IEnumerable<string> tableNamesFromMappings = _nhibernateConfiguration.ClassMappings.Select(x => x.Table.Name);

            string dropAllForeignKeysSql =
                @"
                  DECLARE @cmd nvarchar(1000)
                  DECLARE @fk_table_name nvarchar(1000)
                  DECLARE @fk_name nvarchar(1000)

                  DECLARE cursor_fkeys CURSOR FOR
                  SELECT  OBJECT_NAME(fk.parent_object_id) AS fk_table_name,
                          fk.name as fk_name
                  FROM    sys.foreign_keys fk  JOIN
                          sys.tables tbl ON tbl.OBJECT_ID = fk.referenced_object_id
                  WHERE OBJECT_NAME(fk.parent_object_id) in ('" +
                String.Join("','", tableNamesFromMappings) +
                @"')

                  OPEN cursor_fkeys
                  FETCH NEXT FROM cursor_fkeys
                  INTO @fk_table_name, @fk_name

                  WHILE @@FETCH_STATUS=0
                  BEGIN
                    SET @cmd = 'ALTER TABLE [' + @fk_table_name + '] DROP CONSTRAINT [' + @fk_name + ']'
                    exec dbo.sp_executesql @cmd

                    FETCH NEXT FROM cursor_fkeys
                    INTO @fk_table_name, @fk_name
                  END
                  CLOSE cursor_fkeys
                  DEALLOCATE cursor_fkeys
                ;";

            using (ISession session = _nhibernateConfiguration.BuildSessionFactory().OpenSession())
            {
                using (IDbConnection connection = session.Connection)
                {
                    IDbCommand command = connection.CreateCommand();
                    command.CommandText = dropAllForeignKeysSql;
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}