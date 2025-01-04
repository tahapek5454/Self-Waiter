using SelfWaiter.Shared.Core.Entities;

namespace SelfWaiter.DealerAPI.Core.Domain.Entities
{
    public class Dealer: BaseEntity
    {
        public string Name { get; set; }
        public string? Adress { get; set; }
        public string? PhoneNumber { get; set; }
        public string? ImageUrl { get; set; }
        public Guid DistrictId { get; set; }
        public virtual District District { get; set; }
        public virtual IEnumerable<UserProfileAndDealer> UserProfileAndDealers { get; set; }
    }
}
