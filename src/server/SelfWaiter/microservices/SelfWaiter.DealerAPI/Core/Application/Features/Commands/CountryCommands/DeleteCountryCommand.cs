using MediatR;
using SelfWaiter.DealerAPI.Core.Application.Repositories;

namespace SelfWaiter.DealerAPI.Core.Application.Features.Commands.CountryCommands
{
    public class DeleteCountryCommand: IRequest<int>
    {
        public Guid Id { get; set; }

        public class DeleteCountryCommandHandler(ICountryRepository _countryRepository, IDealerUnitOfWork _dealerUnitOfWork) : IRequestHandler<DeleteCountryCommand, int>
        {
            public async Task<int> Handle(DeleteCountryCommand request, CancellationToken cancellationToken)
            {
                var entity = await _countryRepository.GetByIdAsync(request.Id);

                if (entity is null) return 0;

                await _countryRepository.DeleteAsync(entity);
                return await _dealerUnitOfWork.SaveChangesAsync();
            }
        }
    }
}
