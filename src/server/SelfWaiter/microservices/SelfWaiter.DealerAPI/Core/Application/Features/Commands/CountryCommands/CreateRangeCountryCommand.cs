using MediatR;
using SelfWaiter.DealerAPI.Core.Application.Dtos;
using SelfWaiter.DealerAPI.Core.Application.Mapper;
using SelfWaiter.DealerAPI.Core.Application.Repositories;
using SelfWaiter.DealerAPI.Core.Domain.Entities;

namespace SelfWaiter.DealerAPI.Core.Application.Features.Commands.CountryCommands
{
    public class CreateRangeCountryCommand: IRequest<bool>
    {
        public IEnumerable<CountryDto> Countries { get; set; }

        public class CreateRangeCountryCommandHandler(ICountryRepository _countryRepository, IDealerUnitOfWork _dealerUnitOfWork) : IRequestHandler<CreateRangeCountryCommand, bool>
        {
            public async Task<bool> Handle(CreateRangeCountryCommand request, CancellationToken cancellationToken)
            {
                if (request.Countries is null || !request.Countries.Any())
                    return false;

                var entities = ObjectMapper.Mapper.Map<List<Country>>(request.Countries);
                await _countryRepository.CreateRangeAsync(entities);

                return true;
            }
        }
    }
}
