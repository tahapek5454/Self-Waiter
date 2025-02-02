using SelfWaiter.Shared.Core.Application.Attributes;
using SelfWaiter.Shared.Core.Application.IntegrationEvents.Abstractions;
using SelfWaiter.Shared.Core.Application.Utilities.Consts;
using SelfWaiter.Shared.Core.Domain.Enums;

namespace SelfWaiter.Shared.Core.Application.IntegrationEvents.DealerImageFileChangedEvents
{
    [QueueName(RabbitMQSettings.StateMachine_DealerImageFileChangedQueue)]
    public class DealerImageFileChangedStartedEvent: IIntegrationEvent
    {
        public Guid FileId { get; set; }
        public Guid RelationId { get; set; }
        public string FileName { get; set; }
        public string Path { get; set; }
        public string Storage { get; set; }
        public OpeartionTypeEnum OperationType { get; set; }
    }
}
