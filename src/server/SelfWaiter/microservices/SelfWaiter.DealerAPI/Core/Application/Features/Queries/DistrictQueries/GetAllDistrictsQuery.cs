using MediatR;
using Microsoft.EntityFrameworkCore;
using SelfWaiter.DealerAPI.Core.Application.Dtos;
using SelfWaiter.DealerAPI.Core.Application.Mapper;
using SelfWaiter.DealerAPI.Core.Application.Repositories;

namespace SelfWaiter.DealerAPI.Core.Application.Features.Queries.DistrictQueries
{
    public class GetAllDistrictsQuery: IRequest<IEnumerable<DistrictDto>>
    {
        public class GetAllDistrictsQueryHandler(IDistrictRepository _districtRepository, ILogger<IDistrictRepository> _logger) : IRequestHandler<GetAllDistrictsQuery, IEnumerable<DistrictDto>>
        {
            public async Task<IEnumerable<DistrictDto>> Handle(GetAllDistrictsQuery request, CancellationToken cancellationToken)
            {
                _logger.LogWarning("All districts requested {requestName}", nameof(GetAllDistrictsQuery));

                var districtsQueryable =  _districtRepository.Query().AsNoTracking();
                var districtDtoQuertable = ObjectMapper.Mapper.ProjectTo<DistrictDto>(districtsQueryable);

                return await Task.FromResult(districtDtoQuertable?.ToList() ?? Enumerable.Empty<DistrictDto>());
            }
        }
    }
}
