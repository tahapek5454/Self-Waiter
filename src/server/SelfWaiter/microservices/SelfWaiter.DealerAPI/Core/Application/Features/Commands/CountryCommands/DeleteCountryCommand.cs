using MediatR;
using SelfWaiter.DealerAPI.Core.Application.Repositories;

namespace SelfWaiter.DealerAPI.Core.Application.Features.Commands.CountryCommands
{
    public class DeleteCountryCommand: IRequest<bool>
    {
        public Guid Id { get; set; }

        public class DeleteCountryCommandHandler(ICountryRepository _countryRepository, IDealerUnitOfWork _dealerUnitOfWork) : IRequestHandler<DeleteCountryCommand, bool>
        {
            public async Task<bool> Handle(DeleteCountryCommand request, CancellationToken cancellationToken)
            {
                var entity = await _countryRepository.GetByIdAsync(request.Id);

                if (entity is null) return false;

                await _countryRepository.DeleteAsync(entity);
                return true;
            }
        }
    }
}
