using FluentValidation;
using SelfWaiter.DealerAPI.Core.Application.Features.Commands.CityCommands;

namespace SelfWaiter.DealerAPI.Core.Application.Features.Rules.Validations.CityValidators
{
    public class CreateRangeCityCommandValidator: AbstractValidator<CreateRangeCityCommand>
    {
        public CreateRangeCityCommandValidator()
        {
            RuleFor(x => x.Cities)
                .NotEmpty()
                .WithMessage(ValidationMessages.CitiesCanNotBeEmpty);


            RuleForEach(x => x.Cities)
                .ChildRules(x =>
                {
                    x.RuleFor(x => x.Name)
                    .NotEmpty()
                    .WithMessage(ValidationMessages.CityNameCanNotBeEmpty);

                    x.RuleFor(x => x.CountryId)
                    .NotEmpty()
                    .WithMessage(ValidationMessages.City_CountryIdCanNotBeEmpty);
                });
        }
    }
}
