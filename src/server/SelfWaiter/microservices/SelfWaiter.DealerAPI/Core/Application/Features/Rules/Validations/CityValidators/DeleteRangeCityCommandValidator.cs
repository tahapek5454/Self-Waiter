using FluentValidation;
using SelfWaiter.DealerAPI.Core.Application.Features.Commands.CityCommands;

namespace SelfWaiter.DealerAPI.Core.Application.Features.Rules.Validations.CityValidators
{
    public class DeleteRangeCityCommandValidator: AbstractValidator<DeleteRangeCityCommand>
    {
        public DeleteRangeCityCommandValidator()
        {
            RuleFor(x => x.Ids)
                .NotEmpty()
                .WithMessage(ValidationMessages.CityIdCanNotBeEmpty);

            RuleForEach(x => x.Ids)
                .ChildRules(x =>
                {
                    x.RuleFor(x => x)
                    .NotEmpty()
                    .WithMessage(ValidationMessages.CityIdCanNotBeEmpty);

                });
        }
    }
}
