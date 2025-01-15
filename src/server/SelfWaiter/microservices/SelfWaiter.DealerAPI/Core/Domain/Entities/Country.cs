using System.ComponentModel.DataAnnotations.Schema;
using SelfWaiter.DealerAPI.Core.Domain.Entities.Abstract;

namespace SelfWaiter.DealerAPI.Core.Domain.Entities
{
    public class Country: NotifiableEntity
    {
        public string Name { get; set; }
        public virtual IEnumerable<City> Cities { get; set; }

        [NotMapped]
        public override string? CreatorUserName { get => base.CreatorUserName; set => base.CreatorUserName = value; }
        [NotMapped]
        public override string? UpdatetorUserName { get => base.UpdatetorUserName; set => base.UpdatetorUserName = value; }
    }
}
