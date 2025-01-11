namespace SelfWaiter.Shared.Core.Domain.Dtos
{
    public class PaginationRequest(int page, int size)
    {
        public int Page { get; set; } = page;
        public int Size { get; set; } = size;
    }
}
