namespace SelfWaiter.DealerAPI.Core.Application.Dtos
{
    public class BaseDto
    {
        public virtual Guid Id { get; set; }
        public virtual DateTime CreatedDate { get; set; }
        public virtual string? CreatorUserName { get; set; }
        public virtual DateTime? UpdatedDate { get; set; }
        public virtual string? UpdatetorUserName { get; set; }
    }
}
