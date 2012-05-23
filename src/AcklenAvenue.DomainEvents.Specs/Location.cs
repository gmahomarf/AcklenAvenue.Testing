namespace AcklenAvenue.DomainEvents.Specs
{
    public class Location
    {
        public string Address { get; private set; }

        public Account Account { get; set; }

        #region IDomainEventObservable Members

        public event DomainEvent RaiseEvent;

        #endregion

        public void ChangeLocation(string address)
        {
            Address = address;
            RaiseEvent(new LocationChanged());
        }
    }
}