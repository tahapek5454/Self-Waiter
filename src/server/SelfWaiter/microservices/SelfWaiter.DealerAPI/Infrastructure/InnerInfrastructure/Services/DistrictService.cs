using SelfWaiter.DealerAPI.Core.Application.Dtos;
using SelfWaiter.DealerAPI.Core.Application.Services;
using SelfWaiter.DealerAPI.Core.Domain.Entities;
using SelfWaiter.Shared.Core.Application.Repositories;

namespace SelfWaiter.DealerAPI.Infrastructure.InnerInfrastructure.Services
{
    public class DistrictService(IBaseRepository<District> _baseRepository) : BaseService<District, DistrictDto>(_baseRepository), IDistrictService
    {
    }
}
