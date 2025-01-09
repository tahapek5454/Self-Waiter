using MediatR;
using SelfWaiter.DealerAPI.Core.Application.Repositories;
using SelfWaiter.Shared.Core.Application.Utilities;
using SelfWaiter.Shared.Core.Application.Utilities.Consts;

namespace SelfWaiter.DealerAPI.Core.Application.Features.Commands.CountryCommands
{
    public class DeleteRangeCountryCommand: IRequest<int>
    {
        public IEnumerable<Guid> Ids { get; set; }

        public class DeleteRangeCountryCommandHandler(ICountryRepository _countryRepository, IDealerUnitOfWork _dealerUnitOfWork) : IRequestHandler<DeleteRangeCountryCommand, int>
        {
            public async Task<int> Handle(DeleteRangeCountryCommand request, CancellationToken cancellationToken)
            {
                if (request.Ids is null || !request.Ids.Any()) return 0;


                var entities = _countryRepository.Where(x => request.Ids.Contains(x.Id));

                if (entities.Count() != request.Ids.Count())
                    throw new SelfWaiterException(ExceptionMessages.InconsistencyExceptionMessage);

                await _countryRepository.DeleteRangeAsync(entities);

                return await _dealerUnitOfWork.SaveChangesAsync();
            }
        }
    }
}
