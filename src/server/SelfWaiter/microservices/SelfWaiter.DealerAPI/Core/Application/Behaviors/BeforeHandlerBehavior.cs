using FluentValidation;
using MediatR;
using SelfWaiter.Shared.Core.Application.Utilities;

namespace SelfWaiter.DealerAPI.Core.Application.Behaviors
{
    public sealed class BeforeHandlerBehavior<TRequest, TResponse>(ILogger<TRequest> _logger, IEnumerable<IValidator<TRequest>> _validators) : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var requestName = request.GetType().Name;
            _logger.LogInformation("before handler - request started {requestName} before handle validator", requestName);

            ValidationContext<object> context = new(request);

            IEnumerable<ValidationExceptionModel> errors = _validators
                .Select(validator => validator.Validate(context))
                .SelectMany(result => result.Errors)
                .Where(failure => failure != null)
                .GroupBy(
                   keySelector: p => p.PropertyName,
                   resultSelector: (propertyName, errors) =>
                      new ValidationExceptionModel { Property = propertyName, Errors = errors.Select(e => e.ErrorMessage) }
                ).ToList();

            if(errors?.Any() ?? false)
            {
                _logger.LogInformation("before handler - request started {requestName} after handle validator, has validation exception or exceptions", requestName);

                throw new SelfWaiterValidationException(errors);
            }

            return await next();
        }
    }
}
