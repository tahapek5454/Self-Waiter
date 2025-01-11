namespace SelfWaiter.Shared.Core.Domain.Dtos
{
    public class PaginationResult<T>(IList<T> data, PageInfo pageInfo)
    {
        public IList<T> Data { get; set; } = data;
        public PageInfo PageInfo { get; set; } = pageInfo;

        public PaginationResult():this(new List<T>(), new())
        {
            
        }
    }
}
