namespace SelfWaiter.DealerAPI.Core.Application.Dtos
{
    public class CityDto
    {
        public string Name { get; set; }
        public Guid CountryId { get; set; }
        public CountryDto Country { get; set; }
    }
}
