using FluentValidation;
using SelfWaiter.DealerAPI.Core.Application.Features.Commands.CountryCommands;

namespace SelfWaiter.DealerAPI.Core.Application.Features.Rules.Validations.CountryValidators
{
    public class CreateRangeCountryCommandValidator: AbstractValidator<CreateRangeCountryCommand>
    {
        public CreateRangeCountryCommandValidator()
        {
            RuleFor(x => x.Countries)
                .NotEmpty()
                .WithMessage(ValidationMessages.CountriesCanNotBeEmpty);

            RuleForEach(x => x.Countries)
                .ChildRules(x =>
                {
                    x.RuleFor(y => y.Name)
                        .NotEmpty()
                        .WithMessage(ValidationMessages.CountryNameCanNotBeEmpty);

                });

        }
    }
}
