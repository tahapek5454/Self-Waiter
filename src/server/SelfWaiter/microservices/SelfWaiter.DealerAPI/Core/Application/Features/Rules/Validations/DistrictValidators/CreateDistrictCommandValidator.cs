using FluentValidation;
using SelfWaiter.DealerAPI.Core.Application.Features.Commands.DistrictCommands;

namespace SelfWaiter.DealerAPI.Core.Application.Features.Rules.Validations.DistrictValidators
{
    public class CreateDistrictCommandValidator: AbstractValidator<CreateDistrictCommand>
    {
        public CreateDistrictCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage(ValidationMessages.DistrictNameCanNotBeEmpty);

            RuleFor(x => x.CityId)
                .NotEmpty()
                .WithMessage(ValidationMessages.District_CityIdCanNotBeEmpty);
        }
    }
}
