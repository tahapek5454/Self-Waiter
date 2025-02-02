using SelfWaiter.Shared.Core.Application.IntegrationEvents.Abstractions;

namespace SelfWaiter.Shared.Core.Application.Services
{
    public interface IIntegrationBus
    {
        Task SendAsync<T>(T message) where T : IIntegrationEvent;
        Task SendAsync(IIntegrationEvent message, Type type);
    }
}
