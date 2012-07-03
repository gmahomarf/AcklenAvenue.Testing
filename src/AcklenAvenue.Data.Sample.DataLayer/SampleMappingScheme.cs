using System;
using AcklenAvenue.Data.Sample.Domain;
using FluentNHibernate.Automapping;
using FluentNHibernate.Cfg;

namespace AcklenAvenue.Data.Sample.DataLayer
{
    public class SampleMappingScheme : IDatabaseMappingScheme<MappingConfiguration>
    {
        #region IDatabaseMappingScheme<MappingConfiguration> Members

        public Action<MappingConfiguration> Mappings
        {
            get
            {
                AutoPersistenceModel autoPersistenceModel = AutoMap.Assemblies(typeof (IEntity).Assembly)
                    .Where(t => typeof (IEntity).IsAssignableFrom(t));

                return
                    x =>
                        {
                            x.HbmMappings.AddFromAssemblyOf<SampleMappingScheme>();
                            x.AutoMappings.Add(autoPersistenceModel);
                        };
            }
        }

        #endregion
    }
}