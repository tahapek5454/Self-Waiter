using FluentValidation;
using SelfWaiter.DealerAPI.Core.Application.Features.Queries.DealerQueries;

namespace SelfWaiter.DealerAPI.Core.Application.Features.Rules.Validations.DealerValidators
{
    public class GetDealersByUserIdQueryValidator: AbstractValidator<GetDealersByUserIdQuery>
    {
        public GetDealersByUserIdQueryValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty()
                .WithMessage(ValidationMessages.Dealer_UserIdCanNotBeEmpty);
        }
    }
}
