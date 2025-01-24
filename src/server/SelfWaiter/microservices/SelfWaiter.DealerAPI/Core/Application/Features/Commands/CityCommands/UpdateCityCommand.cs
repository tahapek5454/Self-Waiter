using MediatR;
using SelfWaiter.DealerAPI.Core.Application.Repositories;
using SelfWaiter.Shared.Core.Application.Utilities;
using SelfWaiter.Shared.Core.Application.Utilities.Consts;

namespace SelfWaiter.DealerAPI.Core.Application.Features.Commands.CityCommands
{
    public class UpdateCityCommand:IRequest<bool>
    {
        public Guid Id { get; set; }
        public Guid? CountryId { get; set; }
        public string? Name { get; set; }

        public class UpdateCityCommandHandler(ICityRepository _cityRepository) : IRequestHandler<UpdateCityCommand, bool>
        {
            public async Task<bool> Handle(UpdateCityCommand request, CancellationToken cancellationToken)
            {
                var city = await _cityRepository.GetByIdAsync(request.Id);

                if(city is null)
                {
                    throw new SelfWaiterException(ExceptionMessages.CityNotFound);
                }

                _cityRepository.UpdateAdvance(city, request);

                return true;
            }
        }
    }
}
