using MediatR;
using SelfWaiter.DealerAPI.Core.Application.Mapper;
using SelfWaiter.DealerAPI.Core.Application.Repositories;
using SelfWaiter.DealerAPI.Core.Domain.Entities;
using SelfWaiter.Shared.Core.Application.Utilities;
using SelfWaiter.Shared.Core.Application.Utilities.Consts;

namespace SelfWaiter.DealerAPI.Core.Application.Features.Commands.CountryCommands
{
    public class CreateCountryCommand: IRequest<bool>
    {
        public string Name { get; set; }

        public class CreateCountryCommandHandler(ICountryRepository _countryRepository) : IRequestHandler<CreateCountryCommand, bool>
        {      
            public async Task<bool> Handle(CreateCountryCommand request, CancellationToken cancellationToken)
            {
                if (await _countryRepository.AnyAsync(x => x.Name.Equals(request.Name)))
                    throw new SelfWaiterException(ExceptionMessages.CountryAlreadyExist);

                var country = ObjectMapper.Mapper.Map<Country>(request);
                await _countryRepository.CreateAsync(country);

                return true;
            }
        }
    }
}
