namespace AcklenAvenue.DomainEvents.Specs
{
    public class Account : IDomainEventObservable
    {
        public string Name { get; private set; }

        #region IEventRaiser Members

        public event DomainEvent NotifyObservers;

        #endregion

        public void ChangeName(string newName)
        {
            Name = newName;
            var nameChanged = new NameChanged
                {
                    NewName = newName,
                    Account = this,
                };
            NotifyObservers(nameChanged);
        }
    }
}