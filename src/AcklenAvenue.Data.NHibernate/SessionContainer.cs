using NHibernate;

namespace AcklenAvenue.Data.NHibernate
{
    public class SessionContainer : ISessionContainer
    {
        readonly ISessionFactory _sessionFactory;
        readonly ISessionContainerConfigurator _sessionContainerConfigurator;

        public SessionContainer(ISessionFactory sessionFactory, ISessionContainerConfigurator sessionContainerConfigurator)
        {
            _sessionFactory = sessionFactory;
            _sessionContainerConfigurator = sessionContainerConfigurator;
        }

        public virtual ISession Session
        {
            get { return _sessionContainerConfigurator.GetCurrentSession(_sessionFactory); }
        }

        public virtual void OpenSession()
        {
            _sessionContainerConfigurator.OpenNewSession(_sessionFactory);
        }

        public virtual void CloseSession()
        {
            _sessionContainerConfigurator.DestroySession(_sessionFactory);
        }

        public virtual void Dispose()
        {
            CloseSession();
        }
    }
}