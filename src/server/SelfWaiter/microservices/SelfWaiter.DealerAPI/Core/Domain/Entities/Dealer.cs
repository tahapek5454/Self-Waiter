using SelfWaiter.DealerAPI.Core.Domain.Entities.Abstract;

namespace SelfWaiter.DealerAPI.Core.Domain.Entities
{
    public class Dealer: NotifiableEntity
    {
        public string Name { get; set; }
        public string? Adress { get; set; }
        public string? PhoneNumber { get; set; }
        public string? ImageUrl { get; set; }
        public Guid DistrictId { get; set; }
        public virtual District District { get; set; }
        public Guid? CreatorUserId { get; set; }
        public virtual UserProfile? CreatorUser { get; set; }
        public virtual List<UserProfileAndDealer> UserProfileAndDealers { get; set; }
    }
}
