using MediatR;
using SelfWaiter.DealerAPI.Core.Application.Repositories;

namespace SelfWaiter.DealerAPI.Core.Application.Features.Commands.CityCommands
{
    public class DeleteCityCommand: IRequest<bool>
    {
        public Guid Id { get; set; }
        public class DeleteCityCommandHandler(ICityRepository _cityRepository) : IRequestHandler<DeleteCityCommand, bool>
        {
            public async Task<bool> Handle(DeleteCityCommand request, CancellationToken cancellationToken)
            {
                var city = await _cityRepository.GetByIdAsync(request.Id);

                if (city is null)
                {
                    return false;
                }

                await _cityRepository.DeleteAsync(city);

                return true;
            }
        }
    }
}
