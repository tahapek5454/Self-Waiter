using MediatR;
using SelfWaiter.DealerAPI.Core.Application.Repositories;
using SelfWaiter.Shared.Core.Application.Utilities.Consts;
using SelfWaiter.Shared.Core.Application.Utilities;
using Microsoft.EntityFrameworkCore;
using SelfWaiter.Shared.Core.Application.Extension;

namespace SelfWaiter.DealerAPI.Core.Application.Features.Commands.DealerCommands
{
    public class RemoveUsersFromDealerCommand: IRequest<bool>
    {
        public IEnumerable<Guid> UserIds { get; set; }
        public Guid DealerId { get; set; }
        public class RemoveUsersFromDealerCommandHandler(IDealerRepository _dealerRepository, IUserProfileRepository _userProfileRepository) : IRequestHandler<RemoveUsersFromDealerCommand, bool>
        {
            public async Task<bool> Handle(RemoveUsersFromDealerCommand request, CancellationToken cancellationToken)
            {
                var dealer = await _dealerRepository.Query().Include(x => x.UserProfileAndDealers)
                                                            .FirstOrDefaultAsync(x => x.Id == request.DealerId);
                if (dealer is null)
                    throw new SelfWaiterException(ExceptionMessages.DealerNotFound);

                var users = _userProfileRepository.Query().Where(x => request.UserIds.Contains(x.Id)).ToList();

                if (users.Count() != request.UserIds.Count())
                    throw new SelfWaiterException(ExceptionMessages.InconsistencyExceptionMessage);

                var deletedUserProfileAndDealersDatas = dealer.UserProfileAndDealers?
                                                                .Where(x => request.UserIds.Contains(x.UserProfileId));

                if(deletedUserProfileAndDealersDatas?.Any() != true)
                    throw new SelfWaiterException(ExceptionMessages.InconsistencyExceptionMessage);

                deletedUserProfileAndDealersDatas.Foreach(x => x.IsValid = false);

                return true;
            }
        }
    }
}
