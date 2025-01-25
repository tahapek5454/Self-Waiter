using MediatR;
using SelfWaiter.DealerAPI.Core.Application.Mapper;
using SelfWaiter.DealerAPI.Core.Application.Repositories;
using SelfWaiter.DealerAPI.Core.Domain.Entities;
using SelfWaiter.Shared.Core.Application.Utilities;
using SelfWaiter.Shared.Core.Application.Utilities.Consts;

namespace SelfWaiter.DealerAPI.Core.Application.Features.Commands.DistrictCommands
{
    public class CreateDistrictCommand: IRequest<bool>
    {
        public string Name { get; set; }
        public Guid CityId { get; set; }
        public class CreateDistrictCommandHandler(IDistrictRepository _districtRepository) : IRequestHandler<CreateDistrictCommand, bool>
        {
            public async Task<bool> Handle(CreateDistrictCommand request, CancellationToken cancellationToken)
            {
                if (await _districtRepository.AnyAsync(x => x.Name.Equals(request.Name)))
                    throw new SelfWaiterException(ExceptionMessages.DistrictAlreadyExist);

                var district = ObjectMapper.Mapper.Map<District>(request);

                await _districtRepository.CreateAsync(district);

                return true;
            }
        }
    }
}
