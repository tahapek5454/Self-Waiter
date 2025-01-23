using MediatR;
using Microsoft.EntityFrameworkCore;
using SelfWaiter.DealerAPI.Core.Application.Dtos;
using SelfWaiter.DealerAPI.Core.Application.Mapper;
using SelfWaiter.DealerAPI.Core.Application.Repositories;

namespace SelfWaiter.DealerAPI.Core.Application.Features.Queries.CountryQueries
{
    public class GetAllCountriesQuery: IRequest<IEnumerable<CountryDto>>
    {
        public class GetAllCountriesQueryHandler(ICountryRepository _countryRepository ,ILogger<GetAllCountriesQuery> _logger) : IRequestHandler<GetAllCountriesQuery, IEnumerable<CountryDto>>
        {
            public async Task<IEnumerable<CountryDto>> Handle(GetAllCountriesQuery request, CancellationToken cancellationToken)
            {
                _logger.LogWarning("All countries requested {requestName}", nameof(GetAllCountriesQuery));

                var countriesQueryable = _countryRepository.Query().AsNoTracking();

                var countryDtosQueryable = ObjectMapper.Mapper.ProjectTo<CountryDto>(countriesQueryable);

                return await Task.FromResult(countryDtosQueryable.ToList());

            }
        }
    }
}
