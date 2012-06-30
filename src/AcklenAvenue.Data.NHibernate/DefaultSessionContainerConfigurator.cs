using System;
using NHibernate;

namespace AcklenAvenue.Data.NHibernate
{
    public class DefaultSessionContainerConfigurator : SessionContainerConfigurator
    {
        public DefaultSessionContainerConfigurator(Func<ISessionFactory, ISession> getCurrentSession, Action<ISessionFactory> openNewSession, Action<ISessionFactory> destroySession)
        {
            GetCurrentSession = getCurrentSession;
            OpenNewSession = openNewSession;
            DestroySession = destroySession;
        }
    }
}