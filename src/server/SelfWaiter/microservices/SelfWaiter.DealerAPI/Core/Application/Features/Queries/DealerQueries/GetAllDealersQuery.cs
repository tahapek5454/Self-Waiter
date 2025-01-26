using MediatR;
using Microsoft.EntityFrameworkCore;
using SelfWaiter.DealerAPI.Core.Application.Dtos;
using SelfWaiter.DealerAPI.Core.Application.Mapper;
using SelfWaiter.DealerAPI.Core.Application.Repositories;

namespace SelfWaiter.DealerAPI.Core.Application.Features.Queries.DealerQueries
{
    public class GetAllDealersQuery: IRequest<IEnumerable<DealerDto>>
    {
        public class GetAllDealersQueryHandler(IDealerRepository _dealerRepository, ILogger<GetAllDealersQuery> _logger) : IRequestHandler<GetAllDealersQuery, IEnumerable<DealerDto>>
        {
            public async Task<IEnumerable<DealerDto>> Handle(GetAllDealersQuery request, CancellationToken cancellationToken)
            {
                _logger.LogWarning("All dealer requested {requestName}", nameof(GetAllDealersQuery));

                var dealerQueryable = _dealerRepository.Query().AsNoTracking();
                var dealerDtoQueryable = ObjectMapper.Mapper.ProjectTo<DealerDto>(dealerQueryable);

                return await Task.FromResult(dealerDtoQueryable?.ToList()  ?? Enumerable.Empty<DealerDto>());   
            }
        }
    }
}
