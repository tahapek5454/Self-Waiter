using MediatR;
using Microsoft.EntityFrameworkCore;
using SelfWaiter.DealerAPI.Core.Application.Dtos;
using SelfWaiter.DealerAPI.Core.Application.Repositories;

namespace SelfWaiter.DealerAPI.Core.Application.Features.Queries.DealerQueries
{
    public class GetDealersByCreatorUserIdQuery: IRequest<IEnumerable<DealerDto>>
    {
        public Guid? CreatorUserId { get; set; }
        public class GetDealersByCreatorUserIdQueryHandler(IDealerRepository _dealerRepository) : IRequestHandler<GetDealersByCreatorUserIdQuery, IEnumerable<DealerDto>>
        {
            public async Task<IEnumerable<DealerDto>> Handle(GetDealersByCreatorUserIdQuery request, CancellationToken cancellationToken)
            {
                var dealers = _dealerRepository.Query()
                                                .AsNoTracking()
                                                .Where(x => x.CreatorUserId.Equals(request.CreatorUserId))
                                                .Select(x => new DealerDto(x))
                                                .ToList();

                return await Task.FromResult(dealers);
            }
        }
    }
}
