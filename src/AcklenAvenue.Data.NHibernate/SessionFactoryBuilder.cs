using System;
using System.Configuration;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using Configuration = NHibernate.Cfg.Configuration;

namespace AcklenAvenue.Data.NHibernate
{
    public class SessionFactoryBuilder : ISessionFactoryBuilder
    {
        readonly IDatabaseMappingScheme<MappingConfiguration> _mappingScheme;
        readonly IPersistenceConfigurer _persistenceConfigurer;

        public SessionFactoryBuilder(IDatabaseMappingScheme<MappingConfiguration> mappingScheme, IPersistenceConfigurer persistenceConfigurer)
        {
            _mappingScheme = mappingScheme;
            _persistenceConfigurer = persistenceConfigurer;
        }

        public ISessionFactory Build(Action<Configuration> additionalConfiguration = null)
        {
            var sessionFactory = GetFluentConfiguration().BuildSessionFactory();
            if (additionalConfiguration != null)
                additionalConfiguration.Invoke(NHibernateConfiguration);
            return sessionFactory;
        }

        private Configuration NHibernateConfiguration;

        private FluentConfiguration GetFluentConfiguration()
        {
            var current_session_context_class = Convert.ToString((string)ConfigurationManager.AppSettings["current_session_context_class"]);

            return Fluently.Configure()
                .Database(_persistenceConfigurer)
                .Mappings(_mappingScheme.Mappings)
                .ExposeConfiguration(x =>
                    {
                        x.SetProperty("current_session_context_class", current_session_context_class);
                        NHibernateConfiguration = x;
                    });
        }     
    }
}