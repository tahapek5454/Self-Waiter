using Azure;
using MediatR;

namespace SelfWaiter.DealerAPI.Core.Application.Behaviors
{
    public sealed class BeforeHandlerBehavior<TRequest, TResponse>(ILogger<TRequest> _logger) : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var requestName = request.GetType().Name;
            _logger.LogInformation("before handler - request started {requestName}", requestName);

            return await next();
        }
    }
}
