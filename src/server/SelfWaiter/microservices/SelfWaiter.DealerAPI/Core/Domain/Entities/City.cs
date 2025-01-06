using SelfWaiter.Shared.Core.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace SelfWaiter.DealerAPI.Core.Domain.Entities
{
    public class City: BaseEntity
    {
        public string Name { get; set; }
        public Guid CountryId { get; set; }
        public virtual Country Country { get; set; }
        public virtual IEnumerable<District> Districts { get; set; }

        [NotMapped]
        public override string? CreatorUserName { get => base.CreatorUserName; set => base.CreatorUserName = value; }
        [NotMapped]
        public override string? UpdatetorUserName { get => base.UpdatetorUserName; set => base.UpdatetorUserName = value; }
    }
}
