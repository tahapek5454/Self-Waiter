using MediatR;
using SelfWaiter.FileAPI.Core.Application.Behaviors.Dispatchers;
using SelfWaiter.FileAPI.Core.Application.Repositories;

namespace SelfWaiter.FileAPI.Core.Application.Behaviors
{
    public sealed class AfterHandlerBehavior<TRequest, TResponse>(ILogger<TRequest> _logger, IFileUnitOfWork _unitOfWork, IDomainEventsDispatcher _domainEventsDispatcher) : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var result = await next();

            var requestName = request.GetType().Name;
            _logger.LogInformation("after handler - request ended {requestName} before transaction save changes", requestName);

            await _domainEventsDispatcher.DispatchEventsAsync();
            var affectedCount = await _unitOfWork.SaveChangesAsync();

            _logger.LogInformation("after handler - request ended {requestName} after transaction save changes affectedCount {affectedCount}", requestName, affectedCount);

            return result;
        }
    }
}
