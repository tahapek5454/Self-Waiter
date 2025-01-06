using SelfWaiter.Shared.Core.Domain.Dtos;

namespace SelfWaiter.DealerAPI.Core.Application.Dtos
{
    public class CityDto: BaseDto
    {
        public string Name { get; set; }
        public Guid CountryId { get; set; }
        public CountryDto Country { get; set; }
    }
}
