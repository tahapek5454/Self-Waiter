using SelfWaiter.Shared.Core.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace SelfWaiter.DealerAPI.Core.Domain.Entities
{
    public class District: BaseEntity
    {
        public string Name { get; set; }
        public Guid CityId { get; set; }
        public virtual City City { get; set; }
        public virtual IEnumerable<Dealer> Dealers { get; set; }

        [NotMapped]
        public override string? CreatorUserName { get => base.CreatorUserName; set => base.CreatorUserName = value; }
        [NotMapped]
        public override string? UpdatetorUserName { get => base.UpdatetorUserName; set => base.UpdatetorUserName = value; }
    }
}
