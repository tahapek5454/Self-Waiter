using MediatR;
using SelfWaiter.DealerAPI.Core.Application.Repositories;
using SelfWaiter.Shared.Core.Application.Utilities.Consts;
using SelfWaiter.Shared.Core.Application.Utilities;

namespace SelfWaiter.DealerAPI.Core.Application.Features.Commands.CityCommands
{
    public class DeleteRangeCityCommand:IRequest<bool>
    {
        public IEnumerable<Guid> Ids { get; set; }
        public class DeleteRangeCityCommandHandler(ICityRepository _cityRepository) : IRequestHandler<DeleteRangeCityCommand, bool>
        {
            public async Task<bool> Handle(DeleteRangeCityCommand request, CancellationToken cancellationToken)
            {
                var cities = _cityRepository.Query().Where(x=> request.Ids.Contains(x.Id)).ToList();

                if (cities?.Any() != true)
                {
                    return await Task.FromResult(false);
                }

                if(cities.Count() != request.Ids.Count())
                {
                    throw new SelfWaiterException(ExceptionMessages.InconsistencyExceptionMessage);
                }

                await _cityRepository.DeleteRangeAsync(cities);

                return true;
            }
        }
    }
}
