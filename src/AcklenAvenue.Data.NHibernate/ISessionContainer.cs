using System;
using NHibernate;

namespace AcklenAvenue.Data.NHibernate
{
    public interface ISessionContainer : IDisposable
    {
        ISession Session { get; }
        void OpenSession();
        void CloseSession();
    }
}