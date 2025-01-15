using MediatR;
using SelfWaiter.DealerAPI.Core.Application.Behaviors.Dispatchers;
using SelfWaiter.DealerAPI.Core.Application.Repositories;

namespace SelfWaiter.DealerAPI.Core.Application.Behaviors
{
    public sealed  class AfterHandlerBehavior<TRequest, TResponse>(ILogger<TRequest> _logger, IDealerUnitOfWork _dealerUnitOfWork, IDomainEventsDispatcher _domainEventsDispatcher) : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var result = await next();

            var requestName = request.GetType().Name;
            _logger.LogInformation("after handler - request ended {requestName} before transaction save changes", requestName);

            await _domainEventsDispatcher.DispatchEventsAsync();
            var affectedCount = await _dealerUnitOfWork.SaveChangesAsync();

            _logger.LogInformation("after handler - request ended {requestName} after transaction save changes affectedCount {affectedCount}", requestName, affectedCount);

            return result;
        }
    }
}
