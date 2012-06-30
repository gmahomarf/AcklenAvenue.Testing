using System;
using NHibernate;

namespace AcklenAvenue.Data.NHibernate
{
    public abstract class SessionContainerConfigurator
    {
        public Func<ISessionFactory, ISession> GetCurrentSession;
        public Action<ISessionFactory> OpenNewSession;
        public Action<ISessionFactory> DestroySession;        
    }
}