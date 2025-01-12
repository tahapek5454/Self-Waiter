using FluentValidation;
using SelfWaiter.DealerAPI.Core.Application.Features.Commands.CountryCommands;

namespace SelfWaiter.DealerAPI.Core.Application.Features.Rules.Validations.CountryValidators
{
    public class DeleteCountryCommandValidator: AbstractValidator<DeleteCountryCommand>
    {
        public DeleteCountryCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage(ValidationMessages.CountryIdCanNotBeEmpty);
        }
    }
}
