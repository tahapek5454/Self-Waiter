using SelfWaiter.DealerAPI.Core.Domain.Entities.Abstract;

namespace SelfWaiter.DealerAPI.Core.Domain.Entities
{
    public class UserProfile: NotifiableEntity
    {
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? ImageUrl { get; set; }
        public virtual IEnumerable<Dealer> CreatedDealers { get; set; }
        public virtual IEnumerable<UserProfileAndDealer> UserProfileAndDealers { get; set; }
    }
}
