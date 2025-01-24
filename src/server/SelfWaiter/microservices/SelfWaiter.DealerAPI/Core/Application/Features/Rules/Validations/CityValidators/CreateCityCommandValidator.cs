using FluentValidation;
using SelfWaiter.DealerAPI.Core.Application.Features.Commands.CityCommands;

namespace SelfWaiter.DealerAPI.Core.Application.Features.Rules.Validations.CityValidators
{
    public class CreateCityCommandValidator: AbstractValidator<CreateCityCommand>
    {
        public CreateCityCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage(ValidationMessages.CityNameCanNotBeEmpty);

            RuleFor(x => x.CountryId)
                .NotEmpty()
                .WithMessage(ValidationMessages.City_CountryIdCanNotBeEmpty);
        }
    }
}
