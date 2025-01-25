using SelfWaiter.Shared.Core.Domain.Dtos;

namespace SelfWaiter.DealerAPI.Core.Application.Dtos
{
    public class DealerDto: BaseDto
    {
        public string Name { get; set; }
        public string? Adress { get; set; }
        public string? PhoneNumber { get; set; }
        public string? ImageUrl { get; set; }
        public Guid? CreatorUserId { get; set; }
        public virtual UserProfileDto? CreatorUser { get; set; }
        public Guid DistrictId { get; set; }
        public virtual DistrictDto District { get; set; }
    }
}
