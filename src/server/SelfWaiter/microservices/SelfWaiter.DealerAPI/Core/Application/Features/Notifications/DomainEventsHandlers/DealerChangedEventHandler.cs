using MediatR;
using Microsoft.Extensions.Caching.Hybrid;
using SelfWaiter.DealerAPI.Core.Application.Features.Notifications.DomainEvents;

namespace SelfWaiter.DealerAPI.Core.Application.Features.Notifications.DomainEventsHandlers
{
    public class DealerChangedEventHandler(HybridCache _hybridCache) : INotificationHandler<DealerChangedEvent>
    {
        public async Task Handle(DealerChangedEvent notification, CancellationToken cancellationToken)
        {
            await _hybridCache.RemoveByTagAsync(notification.Tags);
        }
    }
}
