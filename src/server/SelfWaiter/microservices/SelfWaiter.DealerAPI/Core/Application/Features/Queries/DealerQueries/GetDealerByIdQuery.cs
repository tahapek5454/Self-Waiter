using MediatR;
using Microsoft.Extensions.Caching.Hybrid;
using SelfWaiter.DealerAPI.Core.Application.Dtos;
using SelfWaiter.DealerAPI.Core.Application.Mapper;
using SelfWaiter.DealerAPI.Core.Application.Repositories;
using SelfWaiter.Shared.Core.Application.Utilities;
using SelfWaiter.Shared.Core.Application.Utilities.Consts;

namespace SelfWaiter.DealerAPI.Core.Application.Features.Queries.DealerQueries
{
    public class GetDealerByIdQuery: IRequest<DealerDto>
    {
        public Guid Id { get; set; }
        public class GetDealerByIdQueryHandler(IDealerRepository _dealerRepository, HybridCache _hybridCache) : IRequestHandler<GetDealerByIdQuery, DealerDto>
        {
            public async Task<DealerDto> Handle(GetDealerByIdQuery request, CancellationToken cancellationToken)
            {
                string cacheKey = $"dealer-${request.Id}";
                Guid state = request.Id;

                var dealerDto = await _hybridCache.GetOrCreateAsync(cacheKey, state, async (state, token) =>
                {
                    return await GetDealerDtoByIdAsync(state);

                }, tags: [CacheTags.Dealer], cancellationToken: cancellationToken);

                return dealerDto;
            }

            private async Task<DealerDto> GetDealerDtoByIdAsync(Guid id)
            {
                var dealer = await _dealerRepository.GetByIdAsync(id, false);

                if (dealer is null)
                    throw new SelfWaiterException(ExceptionMessages.DealerNotFound);

                var dealerDto = ObjectMapper.Mapper.Map<DealerDto>(dealer);

                return dealerDto;
            }
        }


    }
}
