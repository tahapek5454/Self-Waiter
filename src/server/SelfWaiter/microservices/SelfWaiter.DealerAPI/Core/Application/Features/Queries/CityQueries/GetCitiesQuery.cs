using MediatR;
using Microsoft.EntityFrameworkCore;
using SelfWaiter.DealerAPI.Core.Application.Dtos;
using SelfWaiter.DealerAPI.Core.Application.Features.Rules;
using SelfWaiter.DealerAPI.Core.Application.Mapper;
using SelfWaiter.DealerAPI.Core.Application.Repositories;
using SelfWaiter.Shared.Core.Application.Extension;
using SelfWaiter.Shared.Core.Application.Utilities;
using SelfWaiter.Shared.Core.Application.Utilities.Consts;
using SelfWaiter.Shared.Core.Domain.Dtos;

namespace SelfWaiter.DealerAPI.Core.Application.Features.Queries.CityQueries
{
    public class GetCitiesQuery: DynamicAndPaginationRequest<PaginationResult<CityDto>>
    {

        public class GetCitiesQueryHandler(ICityRepository _cityRepository) : IRequestHandler<GetCitiesQuery, PaginationResult<CityDto>>
        {
            public async Task<PaginationResult<CityDto>> Handle(GetCitiesQuery request, CancellationToken cancellationToken)
            {
                var query = _cityRepository.Query()
                                           .Include(x => x.Country)
                                           .AsNoTracking();

                if(request.DynamicRequest is not null)
                {
                    query = query.ToDynamic(request.DynamicRequest);
                }

                var queryDtos = ObjectMapper.Mapper.ProjectTo<CityDto>(query);

                if (queryDtos is null)
                    throw new SelfWaiterException(ExceptionMessages.InvalidQuery);

                var r = queryDtos.GetPage(request.Page, request.Size);

                return await Task.FromResult(r);
            }
        }
    }
}
