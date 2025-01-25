using MediatR;
using SelfWaiter.DealerAPI.Core.Application.Mapper;
using SelfWaiter.DealerAPI.Core.Application.Repositories;
using SelfWaiter.DealerAPI.Core.Domain.Entities;
using SelfWaiter.Shared.Core.Application.Utilities;
using SelfWaiter.Shared.Core.Application.Utilities.Consts;

namespace SelfWaiter.DealerAPI.Core.Application.Features.Commands.DistrictCommands
{
    public class CreateRangeDistrictCommand: IRequest<bool>
    {
        public IEnumerable<CreateRangeDistrictRequest> Districts { get; set; }
        public class CreateRangeDistrictCommandHandler(IDistrictRepository _districtRepository) : IRequestHandler<CreateRangeDistrictCommand, bool>
        {
            public async Task<bool> Handle(CreateRangeDistrictCommand request, CancellationToken cancellationToken)
            {
                var names = request.Districts.Select(x => x.Name);

                if(await _districtRepository.AnyAsync(x => names.Contains(x.Name)))
                    throw new SelfWaiterException(ExceptionMessages.DistrictAlreadyExist);

                var districts = ObjectMapper.Mapper.Map<IEnumerable<District>>(request.Districts);

                await _districtRepository.CreateRangeAsync(districts);

                return true;
            }
        }
    }

    public class CreateRangeDistrictRequest
    {
        public string Name { get; set; }
        public Guid CityId { get; set; }
    }
}
