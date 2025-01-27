using MediatR;
using Microsoft.EntityFrameworkCore;
using SelfWaiter.DealerAPI.Core.Application.Dtos;
using SelfWaiter.DealerAPI.Core.Application.Features.Rules;
using SelfWaiter.DealerAPI.Core.Application.Mapper;
using SelfWaiter.DealerAPI.Core.Application.Repositories;
using SelfWaiter.Shared.Core.Application.Extension;
using SelfWaiter.Shared.Core.Domain.Dtos;

namespace SelfWaiter.DealerAPI.Core.Application.Features.Queries.DealerQueries
{
    public class GetDealersQuery: DynamicAndPaginationRequest<PaginationResult<DealerDto>>
    {
        public class GetDealersQueryHandler(IDealerRepository _dealerRepository) : IRequestHandler<GetDealersQuery, PaginationResult<DealerDto>>
        {
            public async Task<PaginationResult<DealerDto>> Handle(GetDealersQuery request, CancellationToken cancellationToken)
            {
                var query = _dealerRepository.Query()
                                            .AsNoTracking();

                if (request.DynamicRequest is not null)
                {
                    query = query.ToDynamic(request.DynamicRequest);
                }

                var queryDtos = ObjectMapper.Mapper.ProjectTo<DealerDto>(query);

                var r = queryDtos.GetPage(request.Page, request.Size);

                return await Task.FromResult(r);
            }
        }
    }
}
