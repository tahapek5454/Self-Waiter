using MediatR;
using SelfWaiter.DealerAPI.Core.Application.Mapper;
using SelfWaiter.DealerAPI.Core.Application.Repositories;
using SelfWaiter.DealerAPI.Core.Domain.Entities;
using SelfWaiter.Shared.Core.Application.Utilities.Consts;
using SelfWaiter.Shared.Core.Application.Utilities;

namespace SelfWaiter.DealerAPI.Core.Application.Features.Commands.CityCommands
{
    public class CreateRangeCityCommand: IRequest<bool>
    {
        public IEnumerable<CreateRangeCityRequest> Cities { get; set; }
        public class CreateRangeCityCommandHandler(ICityRepository _cityRepository) : IRequestHandler<CreateRangeCityCommand, bool>
        {
            public async Task<bool> Handle(CreateRangeCityCommand request, CancellationToken cancellationToken)
            {
                var names = request.Cities.Select(x => x.Name);
                if (await _cityRepository.AnyAsync(x => names.Contains(x.Name)))
                    throw new SelfWaiterException(ExceptionMessages.CityAlreadyExist);

                var cities = ObjectMapper.Mapper.Map<IEnumerable<City>>(request.Cities);

                await _cityRepository.CreateRangeAsync(cities);

                return true;
            }
        }
    }

    public class CreateRangeCityRequest
    {
        public Guid CountryId { get; set; }
        public string Name { get; set; }
    }
}
