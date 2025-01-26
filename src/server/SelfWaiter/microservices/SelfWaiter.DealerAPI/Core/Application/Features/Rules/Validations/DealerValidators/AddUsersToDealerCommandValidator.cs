using FluentValidation;
using SelfWaiter.DealerAPI.Core.Application.Features.Commands.DealerCommands;

namespace SelfWaiter.DealerAPI.Core.Application.Features.Rules.Validations.DealerValidators
{
    public class AddUsersToDealerCommandValidator: AbstractValidator<AddUsersToDealerCommand>
    {
        public AddUsersToDealerCommandValidator()
        {
            RuleFor(x => x.DealerId)
               .NotEmpty()
               .WithMessage(ValidationMessages.DealerIdCanNotBeEmpty);

            RuleFor(x => x.UserIds)
                .NotEmpty()
                .WithMessage(ValidationMessages.Dealer_UserIdCanNotBeEmpty);

            RuleForEach(x => x.UserIds)
                .ChildRules(X =>
                {
                    X.RuleFor(X => X)
                    .NotEmpty()
                    .WithMessage(ValidationMessages.Dealer_UserIdCanNotBeEmpty);
                });
        }
    }
}
