using FluentValidation;
using SelfWaiter.DealerAPI.Core.Application.Features.Queries.CityQueries;

namespace SelfWaiter.DealerAPI.Core.Application.Features.Rules.Validations.CityValidators
{
    public class GetCityByIdQueryValidator: AbstractValidator<GetCityByIdQuery>
    {
        public GetCityByIdQueryValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage(ValidationMessages.CityIdCanNotBeEmpty);
        }
    }
}
