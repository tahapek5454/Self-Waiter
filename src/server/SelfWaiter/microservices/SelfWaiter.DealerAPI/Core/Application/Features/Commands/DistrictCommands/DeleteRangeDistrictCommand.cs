using MediatR;
using SelfWaiter.DealerAPI.Core.Application.Repositories;
using SelfWaiter.Shared.Core.Application.Utilities;
using SelfWaiter.Shared.Core.Application.Utilities.Consts;

namespace SelfWaiter.DealerAPI.Core.Application.Features.Commands.DistrictCommands
{
    public class DeleteRangeDistrictCommand: IRequest<bool>
    {
        public IEnumerable<Guid> Ids { get; set; }
        public class DeleteRangeDistrictCommandHandler(IDistrictRepository _districtRepository) : IRequestHandler<DeleteRangeDistrictCommand, bool>
        {
            public async Task<bool> Handle(DeleteRangeDistrictCommand request, CancellationToken cancellationToken)
            {
                var districts = _districtRepository.Query().Where(x => request.Ids.Contains(x.Id)).ToList();
                
                if(districts.Count() != request.Ids.Count())
                {
                    throw new SelfWaiterException(ExceptionMessages.InconsistencyExceptionMessage);
                }

                await _districtRepository.DeleteRangeAsync(districts);

                return true;
            }
        }
    }
}
