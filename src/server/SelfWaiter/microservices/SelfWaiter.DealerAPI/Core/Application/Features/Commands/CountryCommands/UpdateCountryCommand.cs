using MediatR;
using SelfWaiter.DealerAPI.Core.Application.Repositories;

namespace SelfWaiter.DealerAPI.Core.Application.Features.Commands.CountryCommands
{
    public class UpdateCountryCommand: IRequest<bool>
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }

        public class UpdateCountryCommandHandler(ICountryRepository _countryRepository) : IRequestHandler<UpdateCountryCommand, bool>
        {
            public async Task<bool> Handle(UpdateCountryCommand request, CancellationToken cancellationToken)
            {
                var entity = await _countryRepository.GetByIdAsync(request.Id);

                if (entity == null) return false;

                _countryRepository.UpdateAdvance(entity, request);

                return true;
            }
        }
    }
}
