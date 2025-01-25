using FluentValidation;
using SelfWaiter.DealerAPI.Core.Application.Features.Commands.CityCommands;

namespace SelfWaiter.DealerAPI.Core.Application.Features.Rules.Validations.DealerValidators
{
    public class DeleteRangeCityCommandValidator: AbstractValidator<DeleteRangeCityCommand>
    {
        public DeleteRangeCityCommandValidator()
        {
            RuleFor(x => x.Ids)
                .NotEmpty()
                .WithMessage(ValidationMessages.DealerIdCanNotBeEmpty);

            RuleForEach(x => x.Ids)
                .ChildRules(x =>
                {
                    x.RuleFor(x => x)
                    .NotEmpty()
                    .WithMessage(ValidationMessages.DealerIdCanNotBeEmpty);
                });
        }
    }
}
