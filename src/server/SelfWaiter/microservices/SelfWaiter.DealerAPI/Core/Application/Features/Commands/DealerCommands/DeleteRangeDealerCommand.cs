using MediatR;
using SelfWaiter.DealerAPI.Core.Application.Repositories;
using SelfWaiter.Shared.Core.Application.Utilities;
using SelfWaiter.Shared.Core.Application.Utilities.Consts;

namespace SelfWaiter.DealerAPI.Core.Application.Features.Commands.DealerCommands
{
    public class DeleteRangeDealerCommand: IRequest<bool>
    {
        public IEnumerable<Guid> Ids { get; set; }
        public class DeleteRangeDealerCommandHandler(IDealerRepository _dealerRepository) : IRequestHandler<DeleteRangeDealerCommand, bool>
        {
            public async Task<bool> Handle(DeleteRangeDealerCommand request, CancellationToken cancellationToken)
            {
                var dealers = _dealerRepository.Query().Where(x => request.Ids.Contains(x.Id)).ToList();

                if (dealers.Count() != request.Ids.Count())
                    throw new SelfWaiterException(ExceptionMessages.InconsistencyExceptionMessage);

                await _dealerRepository.DeleteRangeAsync(dealers);

                return true;
            }
        }
    }
}
