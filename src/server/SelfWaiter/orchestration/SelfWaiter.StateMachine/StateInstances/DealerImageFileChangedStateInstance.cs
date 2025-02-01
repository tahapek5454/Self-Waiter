using MassTransit;
using SelfWaiter.Shared.Core.Domain.Enums;

namespace SelfWaiter.StateMachine.StateInstances
{
    public class DealerImageFileChangedStateInstance : SagaStateMachineInstance
    {
        public Guid CorrelationId { get; set; }
        public string CurrentState { get; set; }

        public Guid FileId { get; set; }
        public Guid RelationId { get; set; }
        public string FileName { get; set; }
        public string Path { get; set; }
        public string Storage { get; set; }

        public OpeartionTypeEnum OperationType { get; set; }

    }
}
