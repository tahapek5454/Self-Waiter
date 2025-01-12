using MediatR;
using SelfWaiter.DealerAPI.Core.Application.Dtos;
using SelfWaiter.DealerAPI.Core.Application.Features.Rules;
using SelfWaiter.DealerAPI.Core.Application.Mapper;
using SelfWaiter.DealerAPI.Core.Application.Repositories;
using SelfWaiter.Shared.Core.Application.Extension;
using SelfWaiter.Shared.Core.Domain.Dtos;

namespace SelfWaiter.DealerAPI.Core.Application.Features.Queries.CountryQueries
{
    public class GetCountriesQuery: DynamicRequest<PaginationResult<CountryDto>>
    {
        public class GetCountriesQueryHandler(ICountryRepository _countryRepository) : IRequestHandler<GetCountriesQuery, PaginationResult<CountryDto>>
        {
            public async Task<PaginationResult<CountryDto>> Handle(GetCountriesQuery request, CancellationToken cancellationToken)
            {
                var query = _countryRepository
                                        .Query();

                if (request.DynamicRequest is not null)
                    query = query.ToDynamic(request.DynamicRequest);

                var queryDto = ObjectMapper.Mapper.ProjectTo<CountryDto>(query);
                var r = queryDto.GetPage(request.Page, request.Size);

                return await Task.FromResult(r);    
            }
        }
    }
}
