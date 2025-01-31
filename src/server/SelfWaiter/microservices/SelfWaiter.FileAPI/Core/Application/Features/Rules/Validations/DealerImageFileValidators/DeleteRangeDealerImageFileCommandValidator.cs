using FluentValidation;
using SelfWaiter.FileAPI.Core.Application.Features.Commands.DealerImageFileCommands;

namespace SelfWaiter.FileAPI.Core.Application.Features.Rules.Validations.DealerImageFileValidators
{
    public class DeleteRangeDealerImageFileCommandValidator: AbstractValidator<DeleteRangeDealerImageFileCommand>
    {
        public DeleteRangeDealerImageFileCommandValidator()
        {
            RuleFor(x => x.FileNames)
                .NotEmpty()
                .WithMessage(ValidationMessages.FileNamesCanNotBeEmpty);
        }
    }
}
