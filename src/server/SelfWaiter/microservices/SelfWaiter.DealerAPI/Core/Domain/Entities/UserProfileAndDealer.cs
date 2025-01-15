using SelfWaiter.DealerAPI.Core.Domain.Entities.Abstract;

namespace SelfWaiter.DealerAPI.Core.Domain.Entities
{
    public class UserProfileAndDealer: NotifiableEntity
    {
        public Guid DealerId { get; set; }
        public virtual Dealer Dealer { get; set; }
        public Guid UserProfileId { get; set; }
        public virtual UserProfile UserProfile { get; set; }

    }
}
