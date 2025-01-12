using FluentValidation;
using SelfWaiter.DealerAPI.Core.Application.Features.Commands.CountryCommands;

namespace SelfWaiter.DealerAPI.Core.Application.Features.Rules.Validations.CountryValidations
{
    public class CreateCountryCommandValidator: AbstractValidator<CreateCountryCommand>
    {
        public CreateCountryCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage(ValidationMessages.CountryNameCanNotBeEmpty);
        }
    }
}
