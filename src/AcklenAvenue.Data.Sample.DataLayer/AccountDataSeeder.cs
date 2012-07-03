using AcklenAvenue.Data.Sample.Domain;
using NHibernate;

namespace AcklenAvenue.Data.Sample.DataLayer
{
    public class AccountDataSeeder : IDataSeeder
    {
        readonly ISession _session;

        public AccountDataSeeder(ISession session)
        {
            _session = session;
        }

        #region IDataSeeder Members

        public void Seed()
        {
            using(var tx = _session.BeginTransaction())
            {
                _session.Save(new Account
                {
                    Name = "Byron",
                });

                _session.Save(new Account
                {
                    Name = "Colin",
                });

                _session.Save(new Account
                {
                    Name = "David",
                });

                tx.Commit();
            }

        }

        #endregion
    }
}