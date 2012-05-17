using System;
using NHibernate;
using NHibernate.Cfg;

namespace AcklenAvenue.Data.NHibernate
{
    public interface ISessionFactoryBuilder
    {
        ISessionFactory Build(Action<Configuration> additionalConfiguration = null);
    }
}