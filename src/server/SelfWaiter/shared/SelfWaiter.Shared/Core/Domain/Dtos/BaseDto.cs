namespace SelfWaiter.Shared.Core.Domain.Dtos
{
    public class BaseDto : IDto
    {
        public virtual Guid Id { get; set; }
        public virtual DateTime CreatedDate { get; set; }
        public virtual string? CreatorUserName { get; set; }
        public virtual DateTime? UpdatedDate { get; set; }
        public virtual string? UpdatetorUserName { get; set; }
    }
}
