using System;
using NHibernate;
using NHibernate.Context;

namespace AcklenAvenue.Data.NHibernate
{
    public class NHibernateSessionContainer : ISessionContainer
    {
        readonly ISessionFactory _sessionFactory;

        public NHibernateSessionContainer(ISessionFactory sessionFactory)
        {
            _sessionFactory = sessionFactory;
        }

        #region ISessionContainer Members

        public ISession Session
        {
            get
            {
                try
                {
                    if(_sessionFactory.IsClosed)
                        OpenSession();

                    return _sessionFactory.GetCurrentSession();
                }
                catch (Exception ex)
                {
                    throw new Exception("No current session was bound to the context. It is possible that you have not executed OpenSession in the SessionContainer. Depending on your platform (ASP.NET, Console App, etc), you need to, first, open the session container. This places the current session in memory so that it's availble when you need it. See inner exception for more details.", ex);
                }
            }
        }

        public void OpenSession()
        {
            CurrentSessionContext.Bind(_sessionFactory.OpenSession());
        }

        public void CloseSession()
        {
            Dispose();
        }

        public void Dispose()
        {
            CurrentSessionContext.Unbind(_sessionFactory);
            _sessionFactory.Dispose();            
        }

        #endregion
    }
}