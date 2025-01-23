using FluentValidation;
using SelfWaiter.DealerAPI.Core.Application.Features.Queries.CountryQueries;

namespace SelfWaiter.DealerAPI.Core.Application.Features.Rules.Validations.CountryValidators
{
    public class GetCountryByIdQueryValidator: AbstractValidator<GetCountryByIdQuery>
    {
        public GetCountryByIdQueryValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage(ValidationMessages.CountryIdCanNotBeEmpty);
        }
    }
}
