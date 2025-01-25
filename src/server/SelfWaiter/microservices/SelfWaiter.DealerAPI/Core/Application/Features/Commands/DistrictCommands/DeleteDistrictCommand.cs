using MediatR;
using SelfWaiter.DealerAPI.Core.Application.Repositories;
using SelfWaiter.Shared.Core.Application.Utilities;
using SelfWaiter.Shared.Core.Application.Utilities.Consts;

namespace SelfWaiter.DealerAPI.Core.Application.Features.Commands.DistrictCommands
{
    public class DeleteDistrictCommand: IRequest<bool>
    {
        public Guid Id { get; set; }
        public class DeleteDistrictCommandHandler(IDistrictRepository _districtRepository) : IRequestHandler<DeleteDistrictCommand, bool>
        {
            public async Task<bool> Handle(DeleteDistrictCommand request, CancellationToken cancellationToken)
            {
                var district = await _districtRepository.GetByIdAsync(request.Id);

                if (district is null)
                    throw new SelfWaiterException(ExceptionMessages.DistrictNotFound);

                await _districtRepository.DeleteAsync(district);

                return true;
            }
        }
    }
}
