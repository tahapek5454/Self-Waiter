using MediatR;
using SelfWaiter.Shared.Core.Domain.Entities;

namespace SelfWaiter.FileAPI.Core.Domain.Abstract
{
    public abstract class NotifiableEntity : BaseEntity
    {
        protected List<INotification>? _domainEvents;

        public int ClearDomainEvents()
        {
            if (_domainEvents == null)
                return 0;

            int quantity = _domainEvents.Count();

            _domainEvents = Enumerable.Empty<INotification>()
                                      .ToList();

            return quantity;
        }

        public void AddDomainEvent(INotification domainEvent)
        {
            _domainEvents ??= new List<INotification>();
            _domainEvents.Add(domainEvent);
        }

        public IEnumerable<INotification> GetDomainEvents()
            => _domainEvents == null ? Enumerable.Empty<INotification>() : _domainEvents;
        public bool HasDomainEvents()
            => _domainEvents?.Any() ?? false;
    }
}
