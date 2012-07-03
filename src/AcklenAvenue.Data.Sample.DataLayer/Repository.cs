using AcklenAvenue.Data.NHibernate;
using AcklenAvenue.Data.Sample.Domain;

namespace AcklenAvenue.Data.Sample.DataLayer
{
    public class Repository : IRepository
    {
        readonly ISessionContainer _sessionContainer;

        public Repository(ISessionContainer sessionContainer)
        {
            _sessionContainer = sessionContainer;
        }

        public T Get<T>(object id) where T : IEntity
        {
            using(_sessionContainer.Session.BeginTransaction())
            {
                return _sessionContainer.Session.Get<T>(id);
            }
        }
    }
}