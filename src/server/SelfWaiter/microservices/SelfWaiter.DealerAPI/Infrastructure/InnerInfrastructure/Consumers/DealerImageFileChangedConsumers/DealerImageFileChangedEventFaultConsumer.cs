using MassTransit;
using SelfWaiter.DealerAPI.Core.Application.Mapper;
using SelfWaiter.Shared.Core.Application.IntegrationEvents.DealerImageFileChangedEvents;
using SelfWaiter.Shared.Core.Application.Services;

namespace SelfWaiter.DealerAPI.Infrastructure.InnerInfrastructure.Consumers.DealerImageFileChangedConsumers
{
    public class DealerImageFileChangedEventFaultConsumer(IIntegrationBus _bus, ILogger<DealerImageFileChangedEventFaultConsumer> _logger) : IConsumer<Fault<DealerImageFileChangedEvent>>
    {
        public async Task Consume(ConsumeContext<Fault<DealerImageFileChangedEvent>> context)
        {
            var errorMessages = context.Message.Exceptions.FirstOrDefault()?.Message ?? string.Empty;
            _logger.LogError("{requestName} - distributed event error - correlationId - {correlationId} - message: {message}", nameof(DealerImageFileChangedEventFaultConsumer), context.Message.Message.CorrelationId, errorMessages);
            var @event = ObjectMapper.Mapper.Map<DealerImageFileNotReceivedEvent>(context.Message.Message);
            await _bus.SendAsync(@event);
        }
    }
}
