using MediatR;
using SelfWaiter.DealerAPI.Core.Application.Mapper;
using SelfWaiter.DealerAPI.Core.Application.Repositories;
using SelfWaiter.DealerAPI.Core.Domain.Entities;

namespace SelfWaiter.DealerAPI.Core.Application.Features.Commands.CityCommands
{
    public class CreateRangeCityCommand: IRequest<bool>
    {
        public IEnumerable<CreateRangeCityRequest> Cities { get; set; }
        public class CreateRangeCityCommandHandler(ICityRepository _cityRepository) : IRequestHandler<CreateRangeCityCommand, bool>
        {
            public async Task<bool> Handle(CreateRangeCityCommand request, CancellationToken cancellationToken)
            {
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
