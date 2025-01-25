using FluentValidation;
using SelfWaiter.DealerAPI.Core.Application.Features.Commands.DealerCommands;

namespace SelfWaiter.DealerAPI.Core.Application.Features.Rules.Validations.DealerValidators
{
    public class DeleteDealerCommandValidator: AbstractValidator<DeleteDealerCommand>
    {
        public DeleteDealerCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage(ValidationMessages.DealerIdCanNotBeEmpty);
        }
    }
}
