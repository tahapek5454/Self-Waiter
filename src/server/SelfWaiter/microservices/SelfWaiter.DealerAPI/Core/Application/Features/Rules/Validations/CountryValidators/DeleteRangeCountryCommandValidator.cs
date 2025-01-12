using FluentValidation;
using SelfWaiter.DealerAPI.Core.Application.Features.Commands.CountryCommands;

namespace SelfWaiter.DealerAPI.Core.Application.Features.Rules.Validations.CountryValidators
{
    public class DeleteRangeCountryCommandValidator: AbstractValidator<DeleteRangeCountryCommand>
    {
        public DeleteRangeCountryCommandValidator()
        {
            RuleFor(x => x.Ids)
                .NotEmpty()
                .WithMessage(ValidationMessages.CountryIdCanNotBeEmpty);

            RuleForEach(x => x.Ids)
                .ChildRules(x =>
                {
                    x.RuleFor(x => x)
                        .NotEmpty()
                        .WithMessage(ValidationMessages.CountryIdCanNotBeEmpty);
                });
        }
    }
}
