using FluentValidation;
using SelfWaiter.DealerAPI.Core.Application.Features.Commands.CityCommands;

namespace SelfWaiter.DealerAPI.Core.Application.Features.Rules.Validations.CityValidators
{
    public class DeleteCityCommandValidator: AbstractValidator<DeleteCityCommand>
    {
        public DeleteCityCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage(ValidationMessages.CityIdCanNotBeEmpty);
        }
    }
}
