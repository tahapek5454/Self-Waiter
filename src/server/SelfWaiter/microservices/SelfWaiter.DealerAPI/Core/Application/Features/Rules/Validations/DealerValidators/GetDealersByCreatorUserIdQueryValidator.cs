using FluentValidation;
using SelfWaiter.DealerAPI.Core.Application.Features.Queries.DealerQueries;

namespace SelfWaiter.DealerAPI.Core.Application.Features.Rules.Validations.DealerValidators
{
    public class GetDealersByCreatorUserIdQueryValidator:AbstractValidator<GetDealersByCreatorUserIdQuery>
    {

        public GetDealersByCreatorUserIdQueryValidator()
        {
            RuleFor(x => x.CreatorUserId)
                .NotEmpty()
                .WithMessage(ValidationMessages.DealerCreatorUserIdCanNotBeEmpty);
        }

    }
}
