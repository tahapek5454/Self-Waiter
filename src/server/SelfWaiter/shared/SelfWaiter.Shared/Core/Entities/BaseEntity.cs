
namespace SelfWaiter.Shared.Core.Entities;

public abstract class BaseEntity : IEntity
{
    public virtual Guid Id { get; set ; }
    public virtual DateTime CreatedDate { get; set ; }
    public virtual string? CreatorUserName { get; set ; }
    public virtual DateTime? UpdatedDate { get; set ; }
    public virtual string? UpdatetorUserName { get; set ; }
}

