using SelfWaiter.Shared.Core.Domain.Entities;

namespace SelfWaiter.DealerAPI.Core.Domain.Entities
{
    public class UserProfileAndDealer: BaseEntity
    {
        public Guid DealerId { get; set; }
        public virtual Dealer Dealer { get; set; }
        public Guid UserProfileId { get; set; }
        public virtual UserProfile UserProfile { get; set; }

    }
}
