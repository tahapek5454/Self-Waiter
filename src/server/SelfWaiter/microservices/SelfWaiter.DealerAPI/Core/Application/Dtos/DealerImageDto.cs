using SelfWaiter.Shared.Core.Domain.Dtos;

namespace SelfWaiter.DealerAPI.Core.Application.Dtos
{
    public class DealerImageDto: BaseDto
    {
        public string FileName { get; set; }
        public string Path { get; set; }
        public string Storage { get; set; }
        public Guid RelationId { get; set; }
        public DealerDto Dealer { get; set; }
    }
}
