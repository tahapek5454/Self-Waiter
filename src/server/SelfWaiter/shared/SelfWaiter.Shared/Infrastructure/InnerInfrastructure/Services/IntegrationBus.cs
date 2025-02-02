using MassTransit;
using SelfWaiter.Shared.Core.Application;
using SelfWaiter.Shared.Core.Application.IntegrationEvents.Abstractions;
using SelfWaiter.Shared.Core.Application.Services;

namespace SelfWaiter.Shared.Infrastructure.InnerInfrastructure.Services
{
    public class IntegrationBus(ISendEndpointProvider _sendEndpointProvider, IPublishEndpoint _publishEndpoint) : IIntegrationBus
    {
        public async Task SendAsync<T>(T message) where T : IIntegrationEvent
        {
            var queueName = SelfWaiterHelper.GetQueueName(message);
            if (!string.IsNullOrEmpty(queueName))
            {
                var sendEndpoint = await _sendEndpointProvider.GetSendEndpoint(new Uri($"queue:{queueName}"));
                await sendEndpoint.Send(message);
            }
            else
            {
                await _publishEndpoint.Publish(message);
            }
        }

        public async Task SendAsync(IIntegrationEvent message, Type type)
        {
            var queueName = SelfWaiterHelper.GetQueueName(message);
            if (!string.IsNullOrEmpty(queueName))
            {
                var sendEndpoint = await _sendEndpointProvider.GetSendEndpoint(new Uri($"queue:{queueName}"));
                await sendEndpoint.Send(message, type);
            }
            else
            {
                await _publishEndpoint.Publish(message, type); 
            }
        }

        
    }
}
