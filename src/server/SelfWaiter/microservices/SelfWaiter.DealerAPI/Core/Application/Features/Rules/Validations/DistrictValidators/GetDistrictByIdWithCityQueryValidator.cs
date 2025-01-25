using FluentValidation;
using SelfWaiter.DealerAPI.Core.Application.Features.Queries.DistrictQueries;

namespace SelfWaiter.DealerAPI.Core.Application.Features.Rules.Validations.DistrictValidators
{
    public class GetDistrictByIdWithCityQueryValidator: AbstractValidator<GetDistrictByIdWithCityQuery>
    {
        public GetDistrictByIdWithCityQueryValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage(ValidationMessages.DistrictIdCanNotBeEmpty);
        }
    }
}
