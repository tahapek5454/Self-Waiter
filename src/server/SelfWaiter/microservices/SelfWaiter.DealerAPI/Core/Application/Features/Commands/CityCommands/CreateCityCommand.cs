using MediatR;
using SelfWaiter.DealerAPI.Core.Application.Mapper;
using SelfWaiter.DealerAPI.Core.Application.Repositories;
using SelfWaiter.DealerAPI.Core.Domain.Entities;
using SelfWaiter.Shared.Core.Application.Utilities;
using SelfWaiter.Shared.Core.Application.Utilities.Consts;

namespace SelfWaiter.DealerAPI.Core.Application.Features.Commands.CityCommands
{
    public class CreateCityCommand: IRequest<bool>
    {
        public string Name { get; set; }
        public Guid CountryId { get; set; }

        public class CreateCityCommandHandler(ICityRepository _cityRepository) : IRequestHandler<CreateCityCommand, bool>
        {
            public async Task<bool> Handle(CreateCityCommand request, CancellationToken cancellationToken)
            {
                if (await _cityRepository.AnyAsync(x => x.Name.Equals(request.Name)))
                    throw new SelfWaiterException(ExceptionMessages.CityAlreadyExist);

                var city = ObjectMapper.Mapper.Map<City>(request);

                await _cityRepository.CreateAsync(city);

                return true;
            }
        }
    }
}
