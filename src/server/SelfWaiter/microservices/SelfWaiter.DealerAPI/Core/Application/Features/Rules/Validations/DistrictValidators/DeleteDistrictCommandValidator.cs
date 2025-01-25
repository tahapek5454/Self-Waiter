using FluentValidation;
using SelfWaiter.DealerAPI.Core.Application.Features.Commands.DistrictCommands;

namespace SelfWaiter.DealerAPI.Core.Application.Features.Rules.Validations.DistrictValidators
{
    public class DeleteDistrictCommandValidator: AbstractValidator<DeleteDistrictCommand>
    {
        public DeleteDistrictCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage(ValidationMessages.DistrictIdCanNotBeEmpty);
        }
    }
}
