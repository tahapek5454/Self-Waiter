using FluentValidation;
using SelfWaiter.DealerAPI.Core.Application.Features.Queries.DistrictQueries;

namespace SelfWaiter.DealerAPI.Core.Application.Features.Rules.Validations.DistrictValidators
{
    public class GetDistrictByIdQueryValidator: AbstractValidator<GetDistrictByIdQuery>
    {
        public GetDistrictByIdQueryValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage(ValidationMessages.DistrictIdCanNotBeEmpty);
        }
    }
}
