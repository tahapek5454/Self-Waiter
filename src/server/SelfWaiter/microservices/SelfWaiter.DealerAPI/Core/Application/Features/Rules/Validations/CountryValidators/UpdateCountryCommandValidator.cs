using FluentValidation;
using SelfWaiter.DealerAPI.Core.Application.Features.Commands.CountryCommands;

namespace SelfWaiter.DealerAPI.Core.Application.Features.Rules.Validations.CountryValidators
{
    public class UpdateCountryCommandValidator: AbstractValidator<UpdateCountryCommand>
    {
        public UpdateCountryCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage(ValidationMessages.CountryIdCanNotBeEmpty);
        }
    }
}
