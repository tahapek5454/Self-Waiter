using MediatR;

namespace SelfWaiter.DealerAPI.Core.Application.Features.Notifications.DomainEvents
{
    public struct DealerChangedEvent: INotification
    {
        public IEnumerable<string> Tags { get; set; }
    }
}
