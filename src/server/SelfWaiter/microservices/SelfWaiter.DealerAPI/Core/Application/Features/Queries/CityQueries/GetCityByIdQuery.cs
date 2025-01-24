using MediatR;
using SelfWaiter.DealerAPI.Core.Application.Dtos;
using SelfWaiter.DealerAPI.Core.Application.Mapper;
using SelfWaiter.DealerAPI.Core.Application.Repositories;
using SelfWaiter.Shared.Core.Application.Utilities;
using SelfWaiter.Shared.Core.Application.Utilities.Consts;

namespace SelfWaiter.DealerAPI.Core.Application.Features.Queries.CityQueries
{
    public class GetCityByIdQuery:IRequest<CityDto>
    {
        public Guid Id { get; set; }

        public class GetCityByIdQueryHandler(ICityRepository _cityRepository) : IRequestHandler<GetCityByIdQuery, CityDto>
        {
            public async Task<CityDto> Handle(GetCityByIdQuery request, CancellationToken cancellationToken)
            {
                var city = await _cityRepository
                                    .GetByIdAsync(request.Id, false);

                if (city is null)
                    throw new SelfWaiterException(ExceptionMessages.CityNotFound);

                var cityDto = ObjectMapper.Mapper.Map<CityDto>(city);

                return cityDto;
            }
        }
    }
}
