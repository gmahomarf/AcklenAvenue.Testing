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
                        .Singleton()
                        .Use(_sessionFactoryBuilder.Build());

                    x.For<ISessionContainer>()
                        .Use<NHibernateSessionContainer>();

                    x.For<IUnitOfWork<ISession>>().Use<UnitOfWork>();

                });            
        }          
    }
}