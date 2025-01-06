using SelfWaiter.DealerAPI.Core.Application.Dtos;
using SelfWaiter.DealerAPI.Core.Domain.Entities;
using SelfWaiter.Shared.Core.Application.Services;

namespace SelfWaiter.DealerAPI.Core.Application.Services
{
    public interface ICityService: IBaseService<City, CityDto>
    {
    }
}
