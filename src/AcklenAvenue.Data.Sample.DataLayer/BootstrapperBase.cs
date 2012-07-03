using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlServerCe;
using System.IO;
using AcklenAvenue.Data.NHibernate;
using AcklenAvenue.Data.NHibernate.StructureMap;
using FluentNHibernate.Cfg.Db;
using StructureMap;
using Configuration = NHibernate.Cfg.Configuration;

namespace AcklenAvenue.Data.Sample.DataLayer
{
    public abstract class BootstrapperBase
    {
        readonly Container _container;

        protected BootstrapperBase(Container container)
        {
            _container = container;
        }

        public virtual void Run()
        {
            CreateTheDatabaseFile();
            ConfigureDataLayer();
            PopulateDatabase();
        }

        void PopulateDatabase()
        {
            var sessionFactoryBuilder = _container.GetInstance<ISessionFactoryBuilder>();
            Configuration config = null;
            var sessionFactory = sessionFactoryBuilder.Build(configuration => config = configuration);
            var session = sessionFactory.OpenSession();
            var databaseDeployer = new DatabaseDeployer(config);
            databaseDeployer.Create();
            databaseDeployer.Seed(new List<IDataSeeder>
                {
                    new AccountDataSeeder(session)
                });
            sessionFactory.Close();
        }

        void ConfigureDataLayer()
        {
            MsSqlCeConfiguration databaseConfiguration = MsSqlCeConfiguration.Standard.ShowSql()
                .ConnectionString(x => x.FromConnectionStringWithKey("sampleDatabase"));

            var sessionFactoryBuilder = new SessionFactoryBuilder(new SampleMappingScheme(), databaseConfiguration);
            new NHibernateContainerConfigurer(sessionFactoryBuilder).Configure(_container);
        }

        void CreateTheDatabaseFile()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["sampleDatabase"].ConnectionString;
            
            DeleteFileDatabaseIfExists(connectionString);

            using (var engine = new SqlCeEngine(connectionString))
            {
                try
                {
                    engine.CreateDatabase();
                }
                catch
                {
                    //already exsits                    
                }
            }

        }

        void DeleteFileDatabaseIfExists(string connectionString)
        {
            var conStrParts = connectionString.Split(new[] {'='});
            var partWithDbName = conStrParts[1];
            var dbFilename = partWithDbName;

            if (File.Exists(dbFilename))
            {
                File.Delete(dbFilename);
            }
        }
    }
}