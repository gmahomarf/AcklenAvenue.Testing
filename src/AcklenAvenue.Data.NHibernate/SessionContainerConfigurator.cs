using System;
using NHibernate;

namespace AcklenAvenue.Data.NHibernate
{
    public interface ISessionContainerConfigurator
    {
        Func<ISessionFactory, ISession> GetCurrentSession { get; }
        Action<ISessionFactory> OpenNewSession { get; }
        Action<ISessionFactory> DestroySession { get; }
    }
}