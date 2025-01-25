using FluentValidation;
using SelfWaiter.DealerAPI.Core.Application.Features.Commands.DistrictCommands;

namespace SelfWaiter.DealerAPI.Core.Application.Features.Rules.Validations.DistrictValidators
{
    public class CreateRangeDistrictCommandValidator: AbstractValidator<CreateRangeDistrictCommand>
    {
        public CreateRangeDistrictCommandValidator()
        {
            RuleFor(x => x.Districts)
                .NotEmpty()
                .WithMessage(ValidationMessages.DistrictsCanNotBeEmpty);

            RuleForEach(x => x.Districts)
                .ChildRules(x =>
                {
                    x.RuleFor(x => x.Name)
                    .NotEmpty()
                    .WithMessage(ValidationMessages.DistrictNameCanNotBeEmpty);

                    x.RuleFor(x => x.CityId)
                        .NotEmpty()
                        .WithMessage(ValidationMessages.District_CityIdCanNotBeEmpty);
                });
        }
    }
}
