namespace AcklenAvenue.Data.Sample.Domain
{
    public interface IAccountFetcher
    {
        Account Get(long id);
    }
}