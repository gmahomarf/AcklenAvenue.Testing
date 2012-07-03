namespace AcklenAvenue.Data.Sample.Domain
{
    public interface IRepository
    {
        T Get<T>(object id) where T : IEntity;
    }
}