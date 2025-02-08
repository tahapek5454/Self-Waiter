using System.Text.Json;
using MassTransit;
using SelfWaiter.Shared.Core.Application.IntegrationEvents.DealerImageFileChangedEvents;
using SelfWaiter.Shared.Core.Application.Utilities.Consts;
using SelfWaiter.Shared.Core.Domain.Enums;
using SelfWaiter.StateMachine.StateInstances;

namespace SelfWaiter.StateMachine.StateMachines
{
    public class DealerImageFileChangedStateMachine: MassTransitStateMachine<DealerImageFileChangedStateInstance>
    {
        public Event<DealerImageFileChangedStartedEvent> DealerImageFileChangedStartedEvent { get; set; }
        public Event<DealerImageFileReceivedEvent> DealerImageFileReceivedEvent { get; set; }
        public Event<DealerImageFileNotReceivedEvent> DealerImageFileNotReceivedEvent { get; set; }
        public Event<DealerImageFileRollbackReceivedEvent> DealerImageFileRollbackReceivedEvent { get; set; }
        public Event<DealerImageFileDeleteReceivedEvent> DealerImageFileDeleteReceivedEvent { get; set; }

        public State DealerImageFileChanged { get; set; }
        public State DealerImageFileReceived { get; set; }
        public State DealerImageFileNotReceived { get; set; }
        public State DealerImageFileRollbackReceived { get; set; }
        public State DealerImageFileDeleted { get; set; }


        private readonly ILogger<DealerImageFileChangedStateMachine> _logger;
        public DealerImageFileChangedStateMachine(ILogger<DealerImageFileChangedStateMachine> logger)
        {
            _logger = logger;

            InstanceState(instance => instance.CurrentState);

            Event(() => DealerImageFileChangedStartedEvent, (instance) =>
            {
                instance.CorrelateBy<Guid>(db => db.FileId, @event => @event.Message.FileId)
                .SelectId(e => Guid.NewGuid());
            });

            Event(() => DealerImageFileReceivedEvent, (instance) =>
            {
                instance.CorrelateById(@event => @event.Message.CorrelationId);
            });

            Event(() => DealerImageFileNotReceivedEvent, (instance) =>
            {
                instance.CorrelateById(@event => @event.Message.CorrelationId);
            });

            Event(() => DealerImageFileRollbackReceivedEvent, (instance) =>
            {
                instance.CorrelateById(@event => @event.Message.CorrelationId);
            });


            Initially(
                    When(DealerImageFileChangedStartedEvent)
                    .Then(SaveInstanceData)
                    .Then(LogInitialInstanceSaved)
                    .TransitionTo(DealerImageFileChanged)
                    .Send(context => new Uri($"queue:{RabbitMQSettings.Dealer_DealerImageFileChangedQueue}"), CreateDealerImageFileChangedEvent)
                    .Then(LogInitialEventSent)
            );



            During(
                DealerImageFileChanged,

                When(DealerImageFileReceivedEvent)
                    .IfElse(context => context.Message.OperationType == OpeartionTypeEnum.Delete,
                        thenBinder => thenBinder
                            .Send(context => new Uri($"queue:{RabbitMQSettings.File_DealerImageFileDeleteQueue}"), CreateDealerImageFileDeleteEvent)
                            .Then(LogDeleteOperation)
                            .TransitionTo(DealerImageFileDeleted),
                        elseBinder => elseBinder
                            .Then(LogSuccessfulCompletion)
                            .Finalize()),

                When(DealerImageFileNotReceivedEvent)
                .IfElse(context => context.Message.OperationType == OpeartionTypeEnum.Delete,
                        thenBinder => thenBinder
                            .Then(LogFileNotReceivedError)
                            .Finalize(),
                         elseBinder => elseBinder
                            .TransitionTo(DealerImageFileNotReceived)
                            .Send(context => new Uri($"queue:{RabbitMQSettings.File_DealerImageFileNotReceivedQueue}"),  CreateDealerImageFileRollbackEvent)
                            .Then(LogFileNotReceivedError))  
            );


            During(
                DealerImageFileNotReceived,

                When(DealerImageFileRollbackReceivedEvent)
                .TransitionTo(DealerImageFileRollbackReceived)
                .Then(LogSuccessfullyRollback)
                .Finalize()
            );


            During(
                DealerImageFileDeleted,

                When(DealerImageFileDeleteReceivedEvent)
                .TransitionTo(DealerImageFileRollbackReceived)
                .Then(LogSuccessfullyDelted)
                .Finalize()

            );

            SetCompletedWhenFinalized();
        }

        #region Initial 

        private void SaveInstanceData(BehaviorContext<DealerImageFileChangedStateInstance, DealerImageFileChangedStartedEvent> context)
        {
            context.Instance.Path = context.Message.Path;
            context.Instance.FileName = context.Message.FileName;
            context.Instance.FileId = context.Message.FileId;
            context.Instance.OperationType = context.Message.OperationType;
            context.Instance.Storage = context.Message.Storage;
            context.Instance.RelationId = context.Message.RelationId;
        }

        private void LogInitialInstanceSaved(BehaviorContext<DealerImageFileChangedStateInstance, DealerImageFileChangedStartedEvent> context)
        {
            var serializedEvent = JsonSerializer.Serialize(context.Message);
            _logger.LogInformation(
                "{stateMachineName} - instance saved {eventInstance} with correlationId : {correlationId}",
                nameof(DealerImageFileChangedStateMachine),
                serializedEvent,
                context.Instance.CorrelationId
            );
        }

          
        private  DealerImageFileChangedEvent CreateDealerImageFileChangedEvent(BehaviorContext<DealerImageFileChangedStateInstance, DealerImageFileChangedStartedEvent> context) =>
            new DealerImageFileChangedEvent(context.Instance.CorrelationId)
            {
                FileId = context.Message.FileId,
                FileName = context.Message.FileName,
                OperationType = context.Message.OperationType,
                Path = context.Message.Path,
                RelationId = context.Message.RelationId,
                Storage = context.Message.Storage
            };

        private void LogInitialEventSent(BehaviorContext<DealerImageFileChangedStateInstance, DealerImageFileChangedStartedEvent> context)
        {
            _logger.LogInformation(
                "{stateMachineName} - {stateEvent} sent to {queueName} with correlationId : {correlationId}",
                nameof(DealerImageFileChangedStateMachine),
                nameof(DealerImageFileChangedEvent),
                RabbitMQSettings.Dealer_DealerImageFileChangedQueue,
                context.Instance.CorrelationId
            );
        }

        #endregion

        #region DealerImageFileChanged -> DealerImageFileReceivedEvent -> Type Create
        private void LogSuccessfulCompletion(BehaviorContext<DealerImageFileChangedStateInstance, DealerImageFileReceivedEvent> context)
        {
            _logger.LogInformation(
                "{stateMachineName} - successfully finished with correlationId : {correlationId}",
                nameof(DealerImageFileChangedStateMachine),
                context.Instance.CorrelationId
            );
        }
        #endregion

        #region #region DealerImageFileChanged -> DealerImageFileReceivedEvent -> Type Delete
        private DealerImageFileDeleteEvent CreateDealerImageFileDeleteEvent(BehaviorContext<DealerImageFileChangedStateInstance, DealerImageFileReceivedEvent> context) =>
            new DealerImageFileDeleteEvent(context.Instance.CorrelationId)
            {
                FileId = context.Message.FileId,
                FileName = context.Message.FileName,
                OperationType = context.Message.OperationType,
                Path = context.Message.Path,
                RelationId = context.Message.RelationId,
                Storage = context.Message.Storage
            };

        private void LogDeleteOperation(BehaviorContext<DealerImageFileChangedStateInstance, DealerImageFileReceivedEvent> context)
        {
            _logger.LogInformation(
                "{stateMachineName} - delete operation triggered with correlationId : {correlationId}",
                nameof(DealerImageFileChangedStateMachine),
                context.Instance.CorrelationId
            );
        }
        #endregion

        #region DealerImageFileChanged -> DealerImageFileNotReceivedEvent

        private DealerImageFileRollbackEvent CreateDealerImageFileRollbackEvent(BehaviorContext<DealerImageFileChangedStateInstance, DealerImageFileNotReceivedEvent> context) =>
            new DealerImageFileRollbackEvent(context.Instance.CorrelationId)
            {
                FileId = context.Message.FileId,
                FileName = context.Message.FileName,
                OperationType = context.Message.OperationType,
                Path = context.Message.Path,
                RelationId = context.Message.RelationId,
                Storage = context.Message.Storage
            };

        private void LogFileNotReceivedError(BehaviorContext<DealerImageFileChangedStateInstance, DealerImageFileNotReceivedEvent> context)
        {
            var serializedEvent = JsonSerializer.Serialize(context.Instance);
            _logger.LogError(
                "{stateMachineName} - instance error DealerImageFileNotReceivedEvent {eventInstance} with correlationId : {correlationId}",
                nameof(DealerImageFileChangedStateMachine),
                serializedEvent,
                context.Instance.CorrelationId
            );
        }

        #endregion

        #region DealerImageFileNotReceived -> DealerImageFileRollbackReceivedEvent

        private void LogSuccessfullyRollback(BehaviorContext<DealerImageFileChangedStateInstance, DealerImageFileRollbackReceivedEvent> context)
        {
            _logger.LogInformation(
                "{stateMachineName} - successfully rollback with correlationId : {correlationId}",
                nameof(DealerImageFileChangedStateMachine),
                context.Instance.CorrelationId
            );
        }

        #endregion


        #region DealerImageFileDeleted -> DealerImageFileDeleteReceivedEvent

        private void LogSuccessfullyDelted(BehaviorContext<DealerImageFileChangedStateInstance, DealerImageFileDeleteReceivedEvent> context)
        {
            _logger.LogInformation(
                "{stateMachineName} - successfully deleted with correlationId : {correlationId}",
                nameof(DealerImageFileChangedStateMachine),
                context.Instance.CorrelationId
            );
        }

        #endregion

    }
}
