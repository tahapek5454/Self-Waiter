using FluentValidation;
using SelfWaiter.DealerAPI.Core.Application.Features.Queries.CityQueries;

namespace SelfWaiter.DealerAPI.Core.Application.Features.Rules.Validations.CityValidators
{
    public class GetCityByIdWithCountryQueryValidator:AbstractValidator<GetCityByIdWithCountryQuery>
    {
        public GetCityByIdWithCountryQueryValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage(ValidationMessages.CityIdCanNotBeEmpty);
        }
    }
}
