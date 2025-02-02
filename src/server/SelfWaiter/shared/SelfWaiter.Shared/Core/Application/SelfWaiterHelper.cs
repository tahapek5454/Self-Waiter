using SelfWaiter.Shared.Core.Application.Attributes;
using SelfWaiter.Shared.Core.Application.IntegrationEvents.Abstractions;
using System.Reflection;

namespace SelfWaiter.Shared.Core.Application
{
    public static class SelfWaiterHelper
    {
        public static string? GetQueueName<T>(T @event) where T : IIntegrationEvent
        {
            var attribute = @event.GetType().GetCustomAttribute<QueueNameAttribute>();
            return attribute?.QueueName;
        }
    }
}
