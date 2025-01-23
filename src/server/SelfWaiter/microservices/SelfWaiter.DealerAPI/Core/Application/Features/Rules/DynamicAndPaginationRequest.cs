using SelfWaiter.Shared.Core.Domain.Dtos;

namespace SelfWaiter.DealerAPI.Core.Application.Features.Rules
{
    public class DynamicAndPaginationRequest<T>(int page, int size): PaginationRequest<T>(page, size)
    {
        public DynamicRequest? DynamicRequest { get; set; }
        public DynamicAndPaginationRequest():this(1, 10)
        {
            
        }
    }
}
