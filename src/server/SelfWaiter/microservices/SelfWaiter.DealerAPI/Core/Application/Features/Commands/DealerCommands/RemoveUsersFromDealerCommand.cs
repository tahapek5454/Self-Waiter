using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Hybrid;
using SelfWaiter.DealerAPI.Core.Application.Repositories;
using SelfWaiter.Shared.Core.Application.Extension;
using SelfWaiter.Shared.Core.Application.Utilities;
using SelfWaiter.Shared.Core.Application.Utilities.Consts;

namespace SelfWaiter.DealerAPI.Core.Application.Features.Commands.DealerCommands
{
    public class RemoveUsersFromDealerCommand: IRequest<bool>
    {
        public IEnumerable<Guid> UserIds { get; set; }
        public Guid DealerId { get; set; }
        public class RemoveUsersFromDealerCommandHandler(IDealerRepository _dealerRepository, IUserProfileRepository _userProfileRepository, HybridCache _hybridCache) : IRequestHandler<RemoveUsersFromDealerCommand, bool>
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

                await _hybridCache.RemoveByTagAsync([CacheTags.Dealer]);

                return true;
            }
        }
    }
}
