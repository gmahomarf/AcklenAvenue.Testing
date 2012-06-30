using System;
using NHibernate.Context;

namespace AcklenAvenue.Data.NHibernate
{
    public class NHibernateContextSessionContainerConfigurator : SessionContainerConfigurator
    {
        public NHibernateContextSessionContainerConfigurator()
        {
            GetCurrentSession = x =>
                {
                    try
                    {
                        if (x.IsClosed)
                            OpenNewSession(x);

                        return x.GetCurrentSession();
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("No current session was bound to the context. It is possible that you have not executed OpenSession in the SessionContainer. Depending on your platform (ASP.NET, Console App, etc), you need to, first, open the session container. This places the current session in memory so that it's availble when you need it. See inner exception for more details.", ex);
                    }
                };

            OpenNewSession = x => CurrentSessionContext.Bind(x.OpenSession());

            DestroySession = x => CurrentSessionContext.Unbind(x).Close();
        }
    }
}