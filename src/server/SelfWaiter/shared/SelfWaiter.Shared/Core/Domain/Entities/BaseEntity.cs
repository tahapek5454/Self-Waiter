namespace SelfWaiter.Shared.Core.Domain.Entities;

public abstract class BaseEntity : IEntity
{
    public virtual Guid Id { get; set; }
    public virtual DateTime CreatedDate { get; set; }
    public virtual string? CreatorUserName { get; set; }
    public virtual DateTime? UpdatedDate { get; set; }
    public virtual string? UpdatetorUserName { get; set; }
    public bool IsValid { get; set; } = true;
}

