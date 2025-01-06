using SelfWaiter.Shared.Core.Domain.Entities;

namespace SelfWaiter.DealerAPI.Core.Domain.Entities
{
    public class UserProfile: BaseEntity
    {
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? ImageUrl { get; set; }
        public virtual IEnumerable<UserProfileAndDealer> UserProfileAndDealers { get; set; }
    }
}
