using System;
using NHibernate;

namespace AcklenAvenue.Data.NHibernate
{
    public class UnitOfWork : IUnitOfWork<ISession>
    {
        readonly ISessionContainer _sessionContainer;

        public UnitOfWork(ISessionContainer sessionContainer)
        {
            _sessionContainer = sessionContainer;            
        }

        #region IUnitOfWork Members

        public void Commit(Action<ISession> action)
        {
            Commit<object>(session =>
                {
                    action(session);
                    return null;
                });
        }

        public T Commit<T>(Func<ISession, T> func)
        {
            var _session = _sessionContainer.Session;
            using (ITransaction transaction = _session.BeginTransaction())
            {
                T result;
                try
                {
                    result = func(_session);
                    _session.Flush();
                }
                catch (HibernateException)
                {
                    if (!transaction.WasRolledBack && !transaction.WasCommitted)
                        transaction.Rollback();

                    throw;
                }
                finally
                {
                    if (!transaction.WasRolledBack && transaction.IsActive)
                    {
                        transaction.Commit();
                    }
                }

                return result;
            }
        }

        #endregion
    }
}