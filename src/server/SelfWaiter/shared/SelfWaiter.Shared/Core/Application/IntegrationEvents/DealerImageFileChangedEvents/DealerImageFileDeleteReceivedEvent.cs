using MassTransit;
using SelfWaiter.Shared.Core.Application.Attributes;
using SelfWaiter.Shared.Core.Application.IntegrationEvents.Abstractions;
using SelfWaiter.Shared.Core.Application.Utilities.Consts;
using SelfWaiter.Shared.Core.Domain.Enums;

namespace SelfWaiter.Shared.Core.Application.IntegrationEvents.DealerImageFileChangedEvents
{
    [QueueName(RabbitMQSettings.StateMachine_DealerImageFileChangedQueue)]
    public class DealerImageFileDeleteReceivedEvent: CorrelatedBy<Guid>, IIntegrationEvent
    {
        public Guid CorrelationId { get; }

        public DealerImageFileDeleteReceivedEvent(Guid correlationId, Guid fileId = default, Guid relationId = default, string fileName = null, string path = null, string storage = null, OpeartionTypeEnum operationType = default)
        {
            CorrelationId = correlationId;
            FileId = fileId;
            RelationId = relationId;
            FileName = fileName;
            Path = path;
            Storage = storage;
            OperationType = operationType;
        }

        public Guid FileId { get; set; }
        public Guid RelationId { get; set; }
        public string FileName { get; set; }
        public string Path { get; set; }
        public string Storage { get; set; }
        public OpeartionTypeEnum OperationType { get; set; }
    }
}
