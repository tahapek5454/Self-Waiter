using MediatR;
using Microsoft.EntityFrameworkCore;
using SelfWaiter.DealerAPI.Core.Application.Dtos;
using SelfWaiter.DealerAPI.Core.Application.Mapper;
using SelfWaiter.DealerAPI.Core.Application.Repositories;
using SelfWaiter.Shared.Core.Application.Utilities;
using SelfWaiter.Shared.Core.Application.Utilities.Consts;

namespace SelfWaiter.DealerAPI.Core.Application.Features.Queries.DistrictQueries
{
    public class GetDistrictByIdWithCityQuery: IRequest<DistrictDto>
    {
        public Guid Id { get; set; }
        public class GetDistrictByIdWithCityQueryHandler(IDistrictRepository _districtRepository) : IRequestHandler<GetDistrictByIdWithCityQuery, DistrictDto>
        {
            public async Task<DistrictDto> Handle(GetDistrictByIdWithCityQuery request, CancellationToken cancellationToken)
            {
                var district = await _districtRepository.Query()
                                                  .AsNoTracking()
                                                  .Include(X => X.City)
                                                  .FirstOrDefaultAsync(x => x.Id.Equals(request.Id));

                if (district is null)
                    throw new SelfWaiterException(ExceptionMessages.DistrictNotFound);

                var districtDto = ObjectMapper.Mapper.Map<DistrictDto>(district);

                return districtDto;
            }
        }
    }
}
