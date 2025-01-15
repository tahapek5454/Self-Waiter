namespace SelfWaiter.Shared.Core.Domain.Entities;

public interface IEntity
{
    Guid Id { get; set; }
    public bool IsValid { get; set; }
}

