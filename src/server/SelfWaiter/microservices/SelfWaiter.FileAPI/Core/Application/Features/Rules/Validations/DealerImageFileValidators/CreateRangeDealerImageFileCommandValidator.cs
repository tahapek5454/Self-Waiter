using FluentValidation;
using SelfWaiter.FileAPI.Core.Application.Features.Commands.DealerImageFileCommands;

namespace SelfWaiter.FileAPI.Core.Application.Features.Rules.Validations.DealerImageFileValidators
{
    public class CreateRangeDealerImageFileCommandValidator: AbstractValidator<CreateRangeDealerImageFileCommand>
    {
        public CreateRangeDealerImageFileCommandValidator()
        {
            RuleFor(x => x.RelationId)
                .NotEmpty()
                .WithMessage(ValidationMessages.RelationIdCanNotBeEmpty);


            RuleFor(x => x.FormFileCollection)
                .NotEmpty()
                .WithMessage(ValidationMessages.FormFileCollectionCanNotBeEmpty);
        }
    }
}
