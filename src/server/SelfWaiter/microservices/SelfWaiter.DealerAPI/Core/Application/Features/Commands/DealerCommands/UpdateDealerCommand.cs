using MediatR;
using SelfWaiter.DealerAPI.Core.Application.Features.Notifications.DomainEvents;
using SelfWaiter.DealerAPI.Core.Application.Repositories;
using SelfWaiter.Shared.Core.Application.Utilities;
using SelfWaiter.Shared.Core.Application.Utilities.Consts;

namespace SelfWaiter.DealerAPI.Core.Application.Features.Commands.DealerCommands
{
    public class UpdateDealerCommand: IRequest<bool>
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Adress { get; set; }
        public string? PhoneNumber { get; set; }
        public Guid? DistrictId { get; set; }
        public Guid? CreatorUserId { get; set; }
        public class UpdateDealerCommandHandler(IDealerRepository _dealerRepository) : IRequestHandler<UpdateDealerCommand, bool>
        {
            public async Task<bool> Handle(UpdateDealerCommand request, CancellationToken cancellationToken)
            {
                var dealer = await _dealerRepository.GetByIdAsync(request.Id);

                if (dealer is null)
                    throw new SelfWaiterException(ExceptionMessages.DealerNotFound);
                _dealerRepository.UpdateAdvance(dealer, request);

                dealer.AddDomainEvent(new DealerChangedEvent()
                {
                    Tags = [CacheTags.Dealer]
                });

                return true;
            }
        }
    }
}
