using MediatR;
using SelfWaiter.DealerAPI.Core.Application.Repositories;
using SelfWaiter.Shared.Core.Application.Utilities.Consts;
using SelfWaiter.Shared.Core.Application.Utilities;

namespace SelfWaiter.DealerAPI.Core.Application.Features.Commands.DistrictCommands
{
    public class UpdateDistrictCommand: IRequest<bool>
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public Guid? CityId { get; set; }
        public class UpdateDistrictCommandHandler(IDistrictRepository _districtRepository) : IRequestHandler<UpdateDistrictCommand, bool>
        {
            public async Task<bool> Handle(UpdateDistrictCommand request, CancellationToken cancellationToken)
            {
                var district = await _districtRepository.GetByIdAsync(request.Id);

                if (district is null)
                    throw new SelfWaiterException(ExceptionMessages.DistrictNotFound);

                _districtRepository.UpdateAdvance(district, request);

                return true;
            }
        }
    }
}
