using FluentValidation;
using SelfWaiter.DealerAPI.Core.Application.Features.Commands.DistrictCommands;

namespace SelfWaiter.DealerAPI.Core.Application.Features.Rules.Validations.DistrictValidators
{
    public class DeleteRangeDistrictCommandValidator: AbstractValidator<DeleteRangeDistrictCommand>
    {
        public DeleteRangeDistrictCommandValidator()
        {
            RuleFor(x => x.Ids)
                .NotEmpty()
                .WithMessage(ValidationMessages.DistrictIdCanNotBeEmpty);

            RuleForEach(x => x.Ids)
                .ChildRules(x =>
                {
                    x.RuleFor(x => x)
                    .NotEmpty()
                    .WithMessage(ValidationMessages.DistrictIdCanNotBeEmpty);
                });
        }
    }
}
