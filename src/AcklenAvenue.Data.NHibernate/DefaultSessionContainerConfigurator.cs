using System;
using NHibernate;

namespace AcklenAvenue.Data.NHibernate
{
    public class DefaultSessionContainerConfigurator : ISessionContainerConfigurator
    {
        public DefaultSessionContainerConfigurator(Func<ISessionFactory, ISession> getCurrentSession,
                                                   Action<ISessionFactory> openNewSession,
                                                   Action<ISessionFactory> destroySession)
        {
            GetCurrentSession = getCurrentSession;
            OpenNewSession = openNewSession;
            DestroySession = destroySession;
        }

        #region ISessionContainerConfigurator Members

        public Func<ISessionFactory, ISession> GetCurrentSession { get; private set; }

        public Action<ISessionFactory> OpenNewSession { get; private set; }

        public Action<ISessionFactory> DestroySession { get; private set; }

        #endregion
    }
}