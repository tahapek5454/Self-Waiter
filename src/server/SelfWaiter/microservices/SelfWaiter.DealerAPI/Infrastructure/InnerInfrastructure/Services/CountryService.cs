using SelfWaiter.DealerAPI.Core.Application.Dtos;
using SelfWaiter.DealerAPI.Core.Application.Services;
using SelfWaiter.DealerAPI.Core.Domain.Entities;
using SelfWaiter.Shared.Core.Application.Repositories;

namespace SelfWaiter.DealerAPI.Infrastructure.InnerInfrastructure.Services
{
    public class CountryService(IBaseRepository<Country> _baseRepository) : BaseService<Country, CountryDto>(_baseRepository), ICountryService
    {
    }
}
