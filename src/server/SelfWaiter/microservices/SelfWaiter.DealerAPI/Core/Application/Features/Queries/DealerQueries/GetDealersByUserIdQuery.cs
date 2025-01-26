using MediatR;
using Microsoft.EntityFrameworkCore;
using SelfWaiter.DealerAPI.Core.Application.Dtos;
using SelfWaiter.DealerAPI.Core.Application.Repositories;

namespace SelfWaiter.DealerAPI.Core.Application.Features.Queries.DealerQueries
{
    public class GetDealersByUserIdQuery: IRequest<IEnumerable<DealerDto>>
    {
        public Guid? UserId { get; set; }
        public class GetDealersByUserIdQueryHandler(IDealerRepository _dealerRepository) : IRequestHandler<GetDealersByUserIdQuery, IEnumerable<DealerDto>>
        {
            public async Task<IEnumerable<DealerDto>> Handle(GetDealersByUserIdQuery request, CancellationToken cancellationToken)
            {
                var dealers = _dealerRepository.Query()
                                                .AsNoTracking()
                                                .Where(x => x.UserProfileAndDealers.Any(y => y.UserProfileId.Equals(request.UserId)))
                                                .Select(x => new DealerDto(x))
                                                .ToList();

                return await Task.FromResult(dealers);
            }
        }
    }
}
