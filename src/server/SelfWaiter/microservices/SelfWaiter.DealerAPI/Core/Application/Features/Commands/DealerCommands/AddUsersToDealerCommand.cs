using MediatR;
using Microsoft.EntityFrameworkCore;
using SelfWaiter.DealerAPI.Core.Application.Features.Notifications.DomainEvents;
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
        public class AddUsersToDealerCommandHandler(IDealerRepository _dealerRepository, IUserProfileRepository _userProfileRepository, IUserProfileAndDealerRepository _userProfileAndDealerRepository, IMediator _mediator) : IRequestHandler<AddUsersToDealerCommand, bool>
        {
            public async Task<bool> Handle(AddUsersToDealerCommand request, CancellationToken cancellationToken)
            {
                var dealer = await  _dealerRepository
                                            .Query()
                                            .IgnoreQueryFilters()
                                            .Where(x => x.Id.Equals(request.DealerId) && x.IsValid.Equals(true))
                                            .Include(x => x.UserProfileAndDealers)
                                            .Select(x => new Dealer()
                                            {
                                                Id = x.Id,
                                                UserProfileAndDealers = x.UserProfileAndDealers
                                            })
                                            .FirstOrDefaultAsync();

                                            
                                          
                if (dealer is null)
                    throw new SelfWaiterException(ExceptionMessages.DealerNotFound);

                var userIds = _userProfileRepository
                                            .Query()
                                            .Where(x => request.UserIds.Contains(x.Id))
                                            .Select(x => x.Id)
                                            .ToList();

                if (userIds.Count() != request.UserIds.Count())
                    throw new SelfWaiterException(ExceptionMessages.InconsistencyExceptionMessage);


                var intersection = dealer.UserProfileAndDealers.Join(request.UserIds,
                        l1 => l1.UserProfileId,
                        l2 => l2,
                        (l1, l2) => l1
                    ).ToList();
                    
                if(intersection?.Any() == true) 
                {
                    intersection?.ForEach(x =>
                    {
                        x.IsValid = true;
                        _userProfileAndDealerRepository.UpdateAsync(x);
                    });
    
                    request.UserIds = request.UserIds.Except(intersection.Select(x => x.UserProfileId));
                }

                if(request.UserIds?.Any() == true)
                {
                    await _userProfileAndDealerRepository.CreateRangeAsync(request.UserIds.Select(x => new UserProfileAndDealer()
                    {
                        DealerId = request.DealerId,
                        UserProfileId = x
                    }));
                }

                await _mediator.Publish(new DealerChangedEvent()
                {
                    Tags = [CacheTags.Dealer]
                });

                return true;
            }
        }
    }
}
