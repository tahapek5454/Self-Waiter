using MediatR;
using SelfWaiter.DealerAPI.Core.Application.Dtos;
using SelfWaiter.DealerAPI.Core.Application.Mapper;
using SelfWaiter.DealerAPI.Core.Application.Repositories;
using SelfWaiter.Shared.Core.Application.Utilities;
using SelfWaiter.Shared.Core.Application.Utilities.Consts;

namespace SelfWaiter.DealerAPI.Core.Application.Features.Queries.DistrictQueries
{
    public class GetDistrictByIdQuery: IRequest<DistrictDto>
    {
        public Guid Id { get; set; }
        public class GetDistrictByIdQueryHandler(IDistrictRepository _districtRepository) : IRequestHandler<GetDistrictByIdQuery, DistrictDto>
        {
            public async Task<DistrictDto> Handle(GetDistrictByIdQuery request, CancellationToken cancellationToken)
            {
                var district = await _districtRepository.GetByIdAsync(request.Id, false);

                if (district is null)
                    throw new SelfWaiterException(ExceptionMessages.DistrictNotFound);

                var districtDto = ObjectMapper.Mapper.Map<DistrictDto>(district);

                return districtDto;
            }
        }
    }
}
