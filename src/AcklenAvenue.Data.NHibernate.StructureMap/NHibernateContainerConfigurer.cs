using NHibernate;
using StructureMap;

namespace AcklenAvenue.Data.NHibernate.StructureMap
{
    public class NHibernateContainerConfigurer : IContainerConfigurer<IContainer>
    {
        readonly ISessionFactoryBuilder _sessionFactoryBuilder;

        public NHibernateContainerConfigurer(ISessionFactoryBuilder sessionFactoryBuilder)
        {
            _sessionFactoryBuilder = sessionFactoryBuilder;
        }

        public void Configure(IContainer container)
        {
            container.Configure(x =>
                {
                    x.For<ISessionFactory>()
                        .HybridHttpOrThreadLocalScoped()
                        .Use(_sessionFactoryBuilder.Build());

                    x.For<ISessionContainer>().Use<SessionContainer>();                    

                    x.For<ISessionContainerConfigurator>().Use<NHibernateContextSessionContainerConfigurator>();

                    x.For<IUnitOfWork<ISession>>().Use<UnitOfWork>();

                    x.For<ISessionFactoryBuilder>().Use(_sessionFactoryBuilder);
                });            
        }          
    }
}