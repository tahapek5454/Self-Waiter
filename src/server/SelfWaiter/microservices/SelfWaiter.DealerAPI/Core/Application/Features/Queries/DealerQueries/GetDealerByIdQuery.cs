using MediatR;
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
        public class GetDealerByIdQueryHandler(IDealerRepository _dealerRepository) : IRequestHandler<GetDealerByIdQuery, DealerDto>
        {
            public async Task<DealerDto> Handle(GetDealerByIdQuery request, CancellationToken cancellationToken)
            {
                var dealer = await _dealerRepository.GetByIdAsync(request.Id, false);

                if (dealer is null)
                    throw new SelfWaiterException(ExceptionMessages.DealerNotFound);

                var dealerDto = ObjectMapper.Mapper.Map<DealerDto>(dealer); 

                return dealerDto;
            }
        }
    }
}
