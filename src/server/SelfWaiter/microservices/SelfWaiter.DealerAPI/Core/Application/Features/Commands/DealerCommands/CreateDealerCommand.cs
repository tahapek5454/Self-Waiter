using MediatR;
using SelfWaiter.DealerAPI.Core.Application.Mapper;
using SelfWaiter.DealerAPI.Core.Application.Repositories;
using SelfWaiter.DealerAPI.Core.Domain.Entities;
using SelfWaiter.Shared.Core.Application.Utilities;
using SelfWaiter.Shared.Core.Application.Utilities.Consts;

namespace SelfWaiter.DealerAPI.Core.Application.Features.Commands.DealerCommands
{
    public class CreateDealerCommand: IRequest<bool>
    {
        public string Name { get; set; }
        public string? Adress { get; set; }
        public string? PhoneNumber { get; set; }
        public Guid DistrictId { get; set; }
        public Guid? CreatorUserId { get; set; }

        public class CreateDealerCommandHandler(IDealerRepository _dealerRepository) : IRequestHandler<CreateDealerCommand, bool>
        {
            public async Task<bool> Handle(CreateDealerCommand request, CancellationToken cancellationToken)
            {
                if (await _dealerRepository.AnyAsync(x => x.Name.Equals(request.Name)))
                    throw new SelfWaiterException(ExceptionMessages.DealerAlreadyExist);

                var dealer = ObjectMapper.Mapper.Map<Dealer>(request);

                await _dealerRepository.CreateAsync(dealer);

                return true;
            }
        }
    }
}
