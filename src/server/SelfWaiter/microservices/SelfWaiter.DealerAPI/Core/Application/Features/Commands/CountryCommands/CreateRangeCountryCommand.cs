using MediatR;
using SelfWaiter.DealerAPI.Core.Application.Mapper;
using SelfWaiter.DealerAPI.Core.Application.Repositories;
using SelfWaiter.DealerAPI.Core.Domain.Entities;
using SelfWaiter.Shared.Core.Application.Utilities.Consts;
using SelfWaiter.Shared.Core.Application.Utilities;

namespace SelfWaiter.DealerAPI.Core.Application.Features.Commands.CountryCommands
{
    public class CreateRangeCountryCommand: IRequest<bool>
    {
        public IEnumerable<CreateRangeCountryRequest> Countries { get; set; }

        public class CreateRangeCountryCommandHandler(ICountryRepository _countryRepository) : IRequestHandler<CreateRangeCountryCommand, bool>
        {
            public async Task<bool> Handle(CreateRangeCountryCommand request, CancellationToken cancellationToken)
            {
                var names = request.Countries.Select(x => x.Name);
                if(await _countryRepository.AnyAsync(x => names.Contains(x.Name)))
                    throw new SelfWaiterException(ExceptionMessages.CountryAlreadyExist);

                var entities = ObjectMapper.Mapper.Map<List<Country>>(request.Countries);
                await _countryRepository.CreateRangeAsync(entities);

                return true;
            }
        }

        
    }

    public class CreateRangeCountryRequest
    {
        public string Name { get; set; }
    }


}
