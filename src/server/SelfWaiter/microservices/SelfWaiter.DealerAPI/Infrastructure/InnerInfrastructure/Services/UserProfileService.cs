using SelfWaiter.DealerAPI.Core.Application.Dtos;
using SelfWaiter.DealerAPI.Core.Application.Services;
using SelfWaiter.DealerAPI.Core.Domain.Entities;
using SelfWaiter.Shared.Core.Application.Repositories;

namespace SelfWaiter.DealerAPI.Infrastructure.InnerInfrastructure.Services
{
    public class UserProfileService(IBaseRepository<UserProfile> _baseRepository) : BaseService<UserProfile, UserProfileDto>(_baseRepository), IUserProfileService
    {
    }
}
