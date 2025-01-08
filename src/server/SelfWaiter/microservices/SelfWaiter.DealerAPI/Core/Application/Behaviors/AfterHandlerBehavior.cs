using MediatR;

namespace SelfWaiter.DealerAPI.Core.Application.Behaviors
{
    public sealed  class AfterHandlerBehavior<TRequest, TResponse>(ILogger<TRequest> _logger) : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var result = await next();

            var requestName = request.GetType().Name;
            _logger.LogInformation("after handler - request ended {requestName}", requestName);

            return result;
        }
    }
}
