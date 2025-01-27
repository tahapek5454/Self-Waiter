using MediatR;
using Microsoft.EntityFrameworkCore;
using SelfWaiter.DealerAPI.Core.Application.Dtos;
using SelfWaiter.DealerAPI.Core.Application.Features.Rules;
using SelfWaiter.DealerAPI.Core.Application.Mapper;
using SelfWaiter.DealerAPI.Core.Application.Repositories;
using SelfWaiter.Shared.Core.Application.Extension;
using SelfWaiter.Shared.Core.Domain.Dtos;

namespace SelfWaiter.DealerAPI.Core.Application.Features.Queries.DistrictQueries
{
    public class GetDistrictsQuery: DynamicAndPaginationRequest<PaginationResult<DistrictDto>>
    {
        public class GetDistrictsQueryHandler(IDistrictRepository _districtRepository) : IRequestHandler<GetDistrictsQuery, PaginationResult<DistrictDto>>
        {
            public async Task<PaginationResult<DistrictDto>> Handle(GetDistrictsQuery request, CancellationToken cancellationToken)
            {
                var query = _districtRepository.Query()
                                                .Include(x => x.City)
                                                .AsNoTracking();

                if (request.DynamicRequest is not null)
                {
                    query = query.ToDynamic(request.DynamicRequest);
                }

                var queryDtos = ObjectMapper.Mapper.ProjectTo<DistrictDto>(query);


                var r = queryDtos.GetPage(request.Page, request.Size);

                return await Task.FromResult(r);
            }
        }
    }
}
