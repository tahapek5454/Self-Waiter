using MediatR;
using Microsoft.EntityFrameworkCore;
using SelfWaiter.DealerAPI.Core.Application.Dtos;
using SelfWaiter.DealerAPI.Core.Application.Mapper;
using SelfWaiter.DealerAPI.Core.Application.Repositories;

namespace SelfWaiter.DealerAPI.Core.Application.Features.Queries.CityQueries
{
    public class GetAllCitiesQuery:IRequest<IEnumerable<CityDto>>
    {
        public class GetAllCityQueryHandler(ICityRepository _cityRepository, ILogger<GetAllCitiesQuery> _logger) : IRequestHandler<GetAllCitiesQuery, IEnumerable<CityDto>>
        {
            public async Task<IEnumerable<CityDto>> Handle(GetAllCitiesQuery request, CancellationToken cancellationToken)
            {
                _logger.LogWarning("All cities requested {requestName}", nameof(GetAllCitiesQuery));

                var cityQuerayble = _cityRepository.Query().AsNoTracking();
                var cityDtoQueryable = ObjectMapper.Mapper.ProjectTo<CityDto>(cityQuerayble);
                                                        
                return await Task.FromResult(cityDtoQueryable?.ToList() ?? Enumerable.Empty<CityDto>());
            }
        }
    }
}
