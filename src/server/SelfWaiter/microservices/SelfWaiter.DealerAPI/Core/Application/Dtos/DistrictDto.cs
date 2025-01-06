namespace SelfWaiter.DealerAPI.Core.Application.Dtos
{
    public class DistrictDto
    {
        public string Name { get; set; }
        public Guid CityId { get; set; }
        public CityDto City { get; set; }
    }
}
