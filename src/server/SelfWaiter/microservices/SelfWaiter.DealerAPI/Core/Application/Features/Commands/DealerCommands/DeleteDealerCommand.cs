using MediatR;
using SelfWaiter.DealerAPI.Core.Application.Repositories;
using SelfWaiter.Shared.Core.Application.Utilities;
using SelfWaiter.Shared.Core.Application.Utilities.Consts;

namespace SelfWaiter.DealerAPI.Core.Application.Features.Commands.DealerCommands
{
    public class DeleteDealerCommand: IRequest<bool>
    {
        public Guid Id { get; set; }
        public class DeleteDealerCommandHandler(IDealerRepository _dealerRepository) : IRequestHandler<DeleteDealerCommand, bool>
        {
            public async Task<bool> Handle(DeleteDealerCommand request, CancellationToken cancellationToken)
            {
                var dealer = await _dealerRepository.GetByIdAsync(request.Id);

                if (dealer is null)
                    throw new SelfWaiterException(ExceptionMessages.DealerNotFound);

                await _dealerRepository.DeleteAsync(dealer);

                return true;
            }
        }
    }
}
