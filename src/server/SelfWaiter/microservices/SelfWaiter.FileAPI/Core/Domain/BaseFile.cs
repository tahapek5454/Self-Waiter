using SelfWaiter.FileAPI.Core.Domain.Abstract;
using System.ComponentModel.DataAnnotations.Schema;

namespace SelfWaiter.FileAPI.Core.Domain
{
    public class BaseFile: NotifiableEntity
    {
        public string FileName { get; set; }
        public string Path { get; set; }
        public string Storage { get; set; }
        public Guid RelationId { get; set; }

        [NotMapped]
        public override DateTime? UpdatedDate { get => base.UpdatedDate; set => base.UpdatedDate = value; }
        [NotMapped]
        public override string? UpdatetorUserName { get => base.UpdatetorUserName; set => base.UpdatetorUserName = value; }
    }
}
