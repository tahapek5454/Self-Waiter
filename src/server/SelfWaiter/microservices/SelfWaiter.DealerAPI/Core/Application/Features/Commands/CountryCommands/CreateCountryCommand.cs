using MediatR;
using SelfWaiter.DealerAPI.Core.Application.Mapper;
using SelfWaiter.DealerAPI.Core.Application.Repositories;
using SelfWaiter.DealerAPI.Core.Domain.Entities;

namespace SelfWaiter.DealerAPI.Core.Application.Features.Commands.CountryCommands
{
    public class CreateCountryCommand: IRequest<int>
    {
        public string Name { get; set; }

        public class CreateCountryCommandHandler(ICountryRepository _countryRepository, IDealerUnitOfWork _dealerUnitOfWork) : IRequestHandler<CreateCountryCommand, int>
        {      
            public async Task<int> Handle(CreateCountryCommand request, CancellationToken cancellationToken)
            {
                var country = ObjectMapper.Mapper.Map<Country>(request);
                await _countryRepository.CreateAsync(country);

                return await _dealerUnitOfWork.SaveChangesAsync();
            }
        }
    }
}
