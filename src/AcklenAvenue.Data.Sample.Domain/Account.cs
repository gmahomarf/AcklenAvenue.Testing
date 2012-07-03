namespace AcklenAvenue.Data.Sample.Domain
{
    public class Account : IEntity
    {
        public virtual string Name { get; set; }

        #region IEntity Members

        public virtual long Id { get; set; }

        #endregion
    }
}