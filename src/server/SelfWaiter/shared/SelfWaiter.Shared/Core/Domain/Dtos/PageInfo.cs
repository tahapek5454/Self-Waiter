namespace SelfWaiter.Shared.Core.Domain.Dtos
{
    public struct PageInfo(int page, int size, int totalRowCount)
    {
        public const int DefaultPage = 1;
        public const int DefaultSize = 10;

        public int Page { get; set; } = page;
        public int Size { get; set; } = size;
        public int TotalRowCount { get; set; } = totalRowCount;
        public int TotalPageCount => (int)Math.Ceiling((double)TotalRowCount / (double)Size);
        public int Skip => (Page - 1) * Size;
        public bool HasNextPage => Page < TotalPageCount;

        public PageInfo():this(1, 10, 0)
        {
            
        }
    }
}
