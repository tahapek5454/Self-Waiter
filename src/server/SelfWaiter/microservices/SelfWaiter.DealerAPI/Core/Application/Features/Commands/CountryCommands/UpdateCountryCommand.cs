using MediatR;
using SelfWaiter.DealerAPI.Core.Application.Mapper;
using SelfWaiter.DealerAPI.Core.Application.Repositories;
using SelfWaiter.DealerAPI.Core.Domain.Entities;

namespace SelfWaiter.DealerAPI.Core.Application.Features.Commands.CountryCommands
{
    public class UpdateCountryCommand: IRequest<bool>
    {
        public string? Name { get; set; }

        public class UpdateCountryCommandHandler(ICountryRepository _countryRepository, IDealerUnitOfWork _dealerUnitOfWork) : IRequestHandler<UpdateCountryCommand, bool>
        {
            public async Task<bool> Handle(UpdateCountryCommand request, CancellationToken cancellationToken)
            {
                var entity = ObjectMapper.Mapper.Map<Country>(request);

                await _countryRepository.UpdateAsync(entity);

                return true;
            }
        }
    }
}
