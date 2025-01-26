using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Hybrid;
using SelfWaiter.DealerAPI.Core.Application.Dtos;
using SelfWaiter.DealerAPI.Core.Application.Repositories;
using SelfWaiter.Shared.Core.Application.Utilities.Consts;

namespace SelfWaiter.DealerAPI.Core.Application.Features.Queries.DealerQueries
{
    public class GetDealersByCreatorUserIdQuery: IRequest<IEnumerable<DealerDto>>
    {
        public Guid? CreatorUserId { get; set; }
        public class GetDealersByCreatorUserIdQueryHandler(IDealerRepository _dealerRepository, HybridCache _hybridCache) : IRequestHandler<GetDealersByCreatorUserIdQuery, IEnumerable<DealerDto>>
        {
            public async Task<IEnumerable<DealerDto>> Handle(GetDealersByCreatorUserIdQuery request, CancellationToken cancellationToken)
            {
                string cacheKey = $"dealer-creatorUserId-{request.CreatorUserId}";

                var dealers = await _hybridCache.GetOrCreateAsync(cacheKey, async (cToken) =>
                {
                    var dealers = _dealerRepository.Query()
                                .AsNoTracking()
                                .Where(x => x.CreatorUserId.Equals(request.CreatorUserId))
                                .Select(x => new DealerDto(x))
                                .ToList();

                    return await Task.FromResult(dealers);
                }, tags: [CacheTags.Dealer]);

                return dealers ?? Enumerable.Empty<DealerDto>();
            }
        }
    }
}
