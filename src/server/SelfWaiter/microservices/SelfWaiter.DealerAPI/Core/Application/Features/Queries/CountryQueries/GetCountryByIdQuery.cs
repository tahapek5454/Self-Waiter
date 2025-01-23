using MediatR;
using SelfWaiter.DealerAPI.Core.Application.Dtos;
using SelfWaiter.DealerAPI.Core.Application.Mapper;
using SelfWaiter.DealerAPI.Core.Application.Repositories;
using SelfWaiter.Shared.Core.Application.Utilities;
using SelfWaiter.Shared.Core.Application.Utilities.Consts;

namespace SelfWaiter.DealerAPI.Core.Application.Features.Queries.CountryQueries
{
    public class GetCountryByIdQuery: IRequest<CountryDto>
    {
        public Guid Id { get; set; }
        public class GetCountryByIdQueryHandler(ICountryRepository _countryRepository) : IRequestHandler<GetCountryByIdQuery, CountryDto>
        {
            public async Task<CountryDto> Handle(GetCountryByIdQuery request, CancellationToken cancellationToken)
            {
                var country = await _countryRepository.GetByIdAsync(request.Id, false);

                if (country is null)
                    throw new SelfWaiterException(ExceptionMessages.CountryNotFound);

                var countryDto = ObjectMapper.Mapper.Map<CountryDto>(country);

                return countryDto;
            }
        }
    }
}
