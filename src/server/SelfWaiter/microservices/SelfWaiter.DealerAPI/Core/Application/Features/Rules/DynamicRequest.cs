namespace SelfWaiter.DealerAPI.Core.Application.Features.Rules
{
    public class DynamicRequest<T>(int page, int size): PaginationRequest<T>(page, size)
    {
        public DynamicRequest():this(1, 10)
        {
            
        }
    }
}
