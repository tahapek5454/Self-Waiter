using MassTransit;
using SelfWaiter.Shared.Core.Application.IntegrationEvents.Abstractions;

namespace SelfWaiter.Shared.Core.Application.IntegrationEvents.DealerImageFileChangedEvents
{
    public class DealerImageFileReceivedEvent : CorrelatedBy<Guid>, IIntegrationEvent
    {
        public Guid CorrelationId { get; }

        public DealerImageFileReceivedEvent(Guid correlationId)
        {
            CorrelationId = correlationId;
        }
    }
}
