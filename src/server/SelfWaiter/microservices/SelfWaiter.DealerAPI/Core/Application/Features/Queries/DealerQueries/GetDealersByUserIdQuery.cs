using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Hybrid;
using SelfWaiter.DealerAPI.Core.Application.Dtos;
using SelfWaiter.DealerAPI.Core.Application.Repositories;
using SelfWaiter.Shared.Core.Application.Utilities.Consts;

namespace SelfWaiter.DealerAPI.Core.Application.Features.Queries.DealerQueries
{
    public class GetDealersByUserIdQuery: IRequest<IEnumerable<DealerDto>>
    {
        public Guid? UserId { get; set; }
        public class GetDealersByUserIdQueryHandler(IDealerRepository _dealerRepository, HybridCache _hybridCache) : IRequestHandler<GetDealersByUserIdQuery, IEnumerable<DealerDto>>
        {
            public async Task<IEnumerable<DealerDto>> Handle(GetDealersByUserIdQuery request, CancellationToken cancellationToken)
            {
                string cacheKey = $"dealer-userId-{request.UserId}";
                var dealers = await _hybridCache.GetOrCreateAsync(cacheKey, async (ctoken) =>
                {
                    var dealers = _dealerRepository.Query()
                                                .AsNoTracking()
                                                .Where(x => x.UserProfileAndDealers.Any(y => y.UserProfileId.Equals(request.UserId)))
                                                .Select(x => new DealerDto(x))
                                                .ToList();

                    return await Task.FromResult(dealers);
                }, tags: [CacheTags.Dealer]);

                return dealers ?? Enumerable.Empty<DealerDto>();
            }
        }
    }
}
