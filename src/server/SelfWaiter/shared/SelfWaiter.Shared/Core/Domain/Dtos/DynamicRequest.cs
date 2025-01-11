namespace SelfWaiter.Shared.Core.Domain.Dtos
{
    public class DynamicRequest
    {
        public IEnumerable<Sort>? Sort { get; set; }
        public Filter? Filter { get; set; }

        public DynamicRequest()
        {

        }

        public DynamicRequest(IEnumerable<Sort>? sort, Filter? filter)
        {
            Filter = filter;
            Sort = sort;
        }
    }
}
