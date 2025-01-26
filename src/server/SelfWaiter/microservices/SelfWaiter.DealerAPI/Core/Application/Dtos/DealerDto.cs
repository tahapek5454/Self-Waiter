using SelfWaiter.DealerAPI.Core.Domain.Entities;
using SelfWaiter.Shared.Core.Domain.Dtos;

namespace SelfWaiter.DealerAPI.Core.Application.Dtos
{
    public class DealerDto: BaseDto
    {
        public DealerDto() { }
        public DealerDto(Dealer dealer)
        {
            Id = dealer.Id;
            CreatorUserName = dealer.CreatorUserName;
            CreatedDate = dealer.CreatedDate;
            Name = dealer.Name;
            Adress = dealer.Adress;
            PhoneNumber = dealer.PhoneNumber;
            ImageUrl = dealer.ImageUrl;
            CreatorUserId = dealer.CreatorUserId;
            DistrictId = dealer.DistrictId;
        }


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
