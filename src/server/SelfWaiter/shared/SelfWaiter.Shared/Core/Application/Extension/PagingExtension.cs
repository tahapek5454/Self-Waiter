using SelfWaiter.Shared.Core.Domain.Dtos;

namespace SelfWaiter.Shared.Core.Application.Extension
{
    public static class PagingExtension
    {
        public static PaginationResult<T> GetPage<T>(this IQueryable<T> query, int page=1, int size=10)
        {
            var totalCount = query.Count();
            PageInfo pageInfo = new PageInfo(page, size, totalCount);
            var datas =  query
                            .Skip(pageInfo.Skip)
                            .Take(pageInfo.Size)
                            .ToList();
          
            PaginationResult<T> result = new PaginationResult<T>(datas, pageInfo);

            return result;
        }
    }
}
