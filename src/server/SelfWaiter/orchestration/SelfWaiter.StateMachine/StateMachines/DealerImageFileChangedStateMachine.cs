using System.Text.Json;
using MassTransit;
using SelfWaiter.Shared.Core.Application.IntegrationEvents.DealerImageFileChangedEvents;
using SelfWaiter.Shared.Core.Application.Utilities.Consts;
using SelfWaiter.StateMachine.StateInstances;

namespace SelfWaiter.StateMachine.StateMachines
{
    public class DealerImageFileChangedStateMachine: MassTransitStateMachine<DealerImageFileChangedStateInstance>
    {
        public Event<DealerImageFileChangedStartedEvent> DealerImageFileChangedStartedEvent { get; set; }
        public Event<DealerImageFileReceivedEvent> DealerImageFileReceivedEvent { get; set; }
        public Event<DealerImageFileNotReceivedEvent> DealerImageFileNotReceivedEvent { get; set; }

        public State DealerImageFileChanged { get; set; }
        public State DealerImageFileReceived { get; set; }
        public State DealerImageFileNotReceived { get; set; }


        private readonly ILogger<DealerImageFileChangedStateMachine> _logger;
        public DealerImageFileChangedStateMachine(ILogger<DealerImageFileChangedStateMachine> logger)
        {
            _logger = logger;

            InstanceState(instance => instance.CurrentState);

            Event(() => DealerImageFileChangedStartedEvent, (instance) =>
            {
                instance.CorrelateBy(db => db.FileName, @event => @event.Message.FileName)
                .SelectId(e => Guid.NewGuid());
            });

            Event(() => DealerImageFileReceivedEvent, (instance) =>
            {
                instance.CorrelateById(@event => @event.Message.CorrelationId);
            });

            Event(() => DealerImageFileReceivedEvent, (instance) =>
            {
                instance.CorrelateById(@event => @event.Message.CorrelationId);
            });


            Initially(
                    When(DealerImageFileChangedStartedEvent)
                    .Then(context =>
                        {
                            context.Instance.Path = context.Message.Path;
                            context.Instance.FileName = context.Message.FileName;
                            context.Instance.FileId = context.Message.FileId;
                            context.Instance.OperationType = context.Message.OperationType; 
                            context.Instance.Storage = context.Message.Storage;
                            context.Instance.RelationId = context.Message.RelationId;
                        }
                    )
                    .Then(context =>
                    {
                        var serializedevent = JsonSerializer.Serialize(context.Message);
                        _logger.LogInformation("{stateMachineName} - instance saved {eventInstance} with correlationId : {correlationId}", nameof(DealerImageFileChangedStateMachine), serializedevent, context.Instance.CorrelationId);
                    })
                    .TransitionTo(DealerImageFileChanged)
                    .Send(new Uri($"queue:{RabbitMQSettings.Dealer_DealerImageFileChangedQueue}"), context => new DealerImageFileChangedEvent(context.Instance.CorrelationId)
                        {
                            FileId = context.Message.FileId,
                            FileName = context.Message.FileName,
                            OperationType = context.Message.OperationType,
                            Path = context.Message.Path,
                            RelationId = context.Message.RelationId,
                            Storage = context.Message.Storage
                        }
                    )
                    .Then(context =>
                    {
                        _logger.LogInformation("{stateMachineName} - {stateEvent} send to {queueName} with correlationId : {correlationId}", nameof(DealerImageFileChangedStateMachine), nameof(DealerImageFileChangedEvent) , RabbitMQSettings.Dealer_DealerImageFileChangedQueue, context.Instance.CorrelationId);
                    })
            );



            During(
                DealerImageFileChanged,
                When(DealerImageFileReceivedEvent)
                .TransitionTo(DealerImageFileReceived)
                .Then((context) =>
                {
                    _logger.LogInformation("{stateMachineName} - successfully finished with correlationId : {correlationId}", nameof(DealerImageFileChangedStateMachine), context.Instance.CorrelationId);
                })
                .Finalize(),

                When(DealerImageFileNotReceivedEvent)
                .TransitionTo(DealerImageFileNotReceived)
                .Send(new Uri($"queue:{RabbitMQSettings.Dealer_DealerImageFileChangedQueue}"), context => new DealerImageFileNotReceivedEvent(context.Instance.CorrelationId)
                    {
                        FileId= context.Message.FileId,
                        FileName= context.Message.FileName,
                        OperationType= context.Message.OperationType,
                        Path= context.Message.Path,
                        RelationId= context.Message.RelationId,
                        Storage = context.Message.Storage
                    }
                )
                .Then(context =>
                {
                    var serializedevent = JsonSerializer.Serialize(context.Instance);
                    _logger.LogError("{stateMachineName} - instance error DealerImageFileNotReceivedEvent {eventInstance} with correlationId : {correlationId}", nameof(DealerImageFileChangedStateMachine), serializedevent, context.Instance.CorrelationId);
                })
            );


            SetCompletedWhenFinalized();
        }
    }
}
