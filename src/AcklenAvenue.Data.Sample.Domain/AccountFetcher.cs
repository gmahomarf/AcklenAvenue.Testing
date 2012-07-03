namespace AcklenAvenue.Data.Sample.Domain
{
    public class AccountFetcher : IAccountFetcher
    {
        readonly IRepository _repository;

        public AccountFetcher(IRepository repository)
        {
            _repository = repository;
        }

        #region IAccountFetcher Members

        public Account Get(long id)
        {
            return _repository.Get<Account>(id);
        }

        #endregion
    }
}