using MediatR;
using Microsoft.EntityFrameworkCore;
using SelfWaiter.DealerAPI.Core.Application.Dtos;
using SelfWaiter.DealerAPI.Core.Application.Mapper;
using SelfWaiter.DealerAPI.Core.Application.Repositories;
using SelfWaiter.Shared.Core.Application.Utilities;
using SelfWaiter.Shared.Core.Application.Utilities.Consts;

namespace SelfWaiter.DealerAPI.Core.Application.Features.Queries.CityQueries
{
    public class GetCityByIdWithCountryQuery: IRequest<CityDto>
    {
        public Guid Id { get; set; }
        public class GetCityByIdWithCountryQueryHandler(ICityRepository _cityRepository) : IRequestHandler<GetCityByIdWithCountryQuery, CityDto>
        {
            public async Task<CityDto> Handle(GetCityByIdWithCountryQuery request, CancellationToken cancellationToken)
            {
                var city = await _cityRepository
                                     .Query()
                                     .AsNoTracking()
                                     .Include(x => x.Country)
                                     .FirstOrDefaultAsync(x => x.Id.Equals(request.Id));

                if (city is null)
                    throw new SelfWaiterException(ExceptionMessages.CityNotFound);


                var cityDto = ObjectMapper.Mapper.Map<CityDto>(city);

                return cityDto;
            }
        }
    }
}
