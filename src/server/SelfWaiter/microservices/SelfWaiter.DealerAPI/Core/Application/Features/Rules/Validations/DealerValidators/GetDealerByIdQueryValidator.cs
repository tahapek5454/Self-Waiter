using FluentValidation;
using SelfWaiter.DealerAPI.Core.Application.Features.Queries.DealerQueries;

namespace SelfWaiter.DealerAPI.Core.Application.Features.Rules.Validations.DealerValidators
{
    public class GetDealerByIdQueryValidator: AbstractValidator<GetDealerByIdQuery>
    {
        public GetDealerByIdQueryValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage(ValidationMessages.DealerIdCanNotBeEmpty);
        }
    }
}
