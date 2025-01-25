using FluentValidation;
using SelfWaiter.DealerAPI.Core.Application.Features.Commands.DealerCommands;

namespace SelfWaiter.DealerAPI.Core.Application.Features.Rules.Validations.DealerValidators
{
    public class CreateDealerCommandValidator: AbstractValidator<CreateDealerCommand>
    {
        public CreateDealerCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage(ValidationMessages.DealerNameCanNotBeEmpty)
                .MaximumLength(75)
                .WithMessage(ValidationMessages.DealerNameMaxLength);

            RuleFor(x => x.DistrictId)
                .NotEmpty()
                .WithMessage(ValidationMessages.Dealer_DistrictIdCanNotBeEmpty);

            RuleFor(x => x.CreatorUserId)
                .NotEmpty()
                .WithMessage(ValidationMessages.Dealer_CreatorUserIdCanNotBeEmpty);

            RuleFor(x => x.Adress)
                .MaximumLength(250)
                .WithMessage(ValidationMessages.Dealer_AdressMaxLength);

            RuleFor(x => x.PhoneNumber)
                .MaximumLength(25)
                .WithMessage(ValidationMessages.Dealer_AdressMaxLength);
        }
    }
}
