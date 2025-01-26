using MediatR;
using SelfWaiter.DealerAPI.Core.Application.Repositories;
using SelfWaiter.DealerAPI.Core.Domain.Entities;
using SelfWaiter.Shared.Core.Application.Utilities;
using SelfWaiter.Shared.Core.Application.Utilities.Consts;

namespace SelfWaiter.DealerAPI.Core.Application.Features.Commands.DealerCommands
{
    public class AddUsersToDealerCommand: IRequest<bool>
    {
        public IEnumerable<Guid> UserIds { get; set; }
        public Guid DealerId { get; set; }
        public class AddUsersToDealerCommandHandler(IDealerRepository _dealerRepository, IUserProfileRepository _userProfileRepository) : IRequestHandler<AddUsersToDealerCommand, bool>
        {
            public async Task<bool> Handle(AddUsersToDealerCommand request, CancellationToken cancellationToken)
            {
                var dealer = await _dealerRepository.GetByIdAsync(request.DealerId);

                if (dealer is null)
                    throw new SelfWaiterException(ExceptionMessages.DealerNotFound);

                var users = _userProfileRepository.Query().Where(x => request.UserIds.Contains(x.Id)).ToList();

                if (users.Count() != request.UserIds.Count())
                    throw new SelfWaiterException(ExceptionMessages.InconsistencyExceptionMessage);

                var userProfileAndDealerDatas = users.Select(x => new UserProfileAndDealer()
                {
                    UserProfileId = x.Id,
                    DealerId = request.DealerId
                });
                dealer.UserProfileAndDealers ??= new();
                dealer.UserProfileAndDealers.AddRange(userProfileAndDealerDatas);

                return true;
            }
        }
    }
}
