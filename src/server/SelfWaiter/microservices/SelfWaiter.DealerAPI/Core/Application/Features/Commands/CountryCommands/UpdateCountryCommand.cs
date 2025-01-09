using MediatR;
using SelfWaiter.DealerAPI.Core.Application.Mapper;
using SelfWaiter.DealerAPI.Core.Application.Repositories;
using SelfWaiter.DealerAPI.Core.Domain.Entities;

namespace SelfWaiter.DealerAPI.Core.Application.Features.Commands.CountryCommands
{
    public class UpdateCountryCommand: IRequest<int>
    {
        public string? Name { get; set; }

        public class UpdateCountryCommandHandler(ICountryRepository _countryRepository, IDealerUnitOfWork _dealerUnitOfWork) : IRequestHandler<UpdateCountryCommand, int>
        {
            public async Task<int> Handle(UpdateCountryCommand request, CancellationToken cancellationToken)
            {
                var entity = ObjectMapper.Mapper.Map<Country>(request);

                await _countryRepository.UpdateAsync(entity);

                return await _dealerUnitOfWork.SaveChangesAsync();
            }
        }
    }
}
