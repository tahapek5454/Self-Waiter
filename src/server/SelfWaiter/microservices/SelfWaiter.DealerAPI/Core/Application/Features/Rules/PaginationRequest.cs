using MediatR;
using SelfWaiter.Shared.Core.Domain.Dtos;

namespace SelfWaiter.DealerAPI.Core.Application.Features.Rules
{
    public class PaginationRequest<T>(int page, int size) : PaginationRequest(page, size), IRequest<T>
    {

        public PaginationRequest():this(1,10)
        {
            
        }
    }
}
