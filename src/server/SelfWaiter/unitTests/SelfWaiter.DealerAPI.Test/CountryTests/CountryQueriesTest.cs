using AutoFixture;
using Microsoft.Extensions.Logging;
using Moq;
using SelfWaiter.DealerAPI.Core.Application.Dtos;
using SelfWaiter.DealerAPI.Core.Application.Features.Queries.CountryQueries;
using SelfWaiter.DealerAPI.Core.Application.Repositories;
using SelfWaiter.DealerAPI.Core.Domain.Entities;
using SelfWaiter.DealerAPI.Test.Attributes;
using SelfWaiter.DealerAPI.Test.Commons;
using SelfWaiter.Shared.Core.Application.Utilities;
using SelfWaiter.Shared.Core.Application.Utilities.Consts;
using SelfWaiter.Shared.Core.Domain.Dtos;
using Xunit;

namespace SelfWaiter.DealerAPI.Test.CountryTests
{
    public class CountryQueriesTest: BaseTest
    {
        #region GetAllCountriesQueryHandler

        [AutoMoqData]
        [Theory]
        public async Task GetAllCountriesQueryHandler_WhenReturnsAllData_ReturnsEnumerableCountryDto(GetAllCountriesQuery query)
        {
            var _countryRepositoryMock = new Mock<ICountryRepository>();
            var _loggerMock = new Mock<ILogger<GetAllCountriesQuery>>();

            var returnedCountryDatas = Fixture.CreateMany<Country>().AsQueryable();
            _countryRepositoryMock.Setup(x => x.Query()).Returns(returnedCountryDatas);

            var handler = new GetAllCountriesQuery.GetAllCountriesQueryHandler(_countryRepositoryMock.Object, _loggerMock.Object);
            var response = await handler.Handle(query, CancellationToken.None);

            Assert.NotEmpty(response);
            Assert.IsType<IEnumerable<CountryDto>>(response, false);
        }

        #endregion

        #region GetCountriesQueryHandler

        [Fact]
        public async Task GetCountriesQueryHandler_WhenReturnsData_ReturnsPaginationResultCountryDto()
        {

            var _countryRepositoryMock = new Mock<ICountryRepository>();

            DynamicRequest? dynamicRequest = null;
            GetCountriesQuery query = Fixture.Build<GetCountriesQuery>()
                                                .With(x => x.DynamicRequest, dynamicRequest)
                                                .With(x => x.Size, 10)
                                                .With(x => x.Page, 1)
                                                .Create();


            var returnedCountryDatas = Fixture.CreateMany<Country>().AsQueryable();
            _countryRepositoryMock.Setup(x => x.Query()).Returns(returnedCountryDatas);


            var handler = new GetCountriesQuery.GetCountriesQueryHandler(_countryRepositoryMock.Object);
            var response = await handler.Handle(query, CancellationToken.None);

            Assert.NotNull(response);
            Assert.NotEmpty(response.Data);

        }


        [Fact]
        public async Task GetCountriesQueryHandler_WhenReturnsFilteredData_ReturnsPaginationResultCountryDto()
        {

            var _countryRepositoryMock = new Mock<ICountryRepository>();

            GetCountriesQuery query = Fixture.Build<GetCountriesQuery>()
                                                .With(x => x.DynamicRequest, new DynamicRequest() 
                                                {
                                                    Sort = null,
                                                    Filter = new Filter()
                                                    {
                                                        Field = "name",
                                                        Logic = "and",
                                                        Operator = "eq",
                                                        Value = "Türkiye"
                                                    }
                                                })
                                                .With(x => x.Size, 10)
                                                .With(x => x.Page, 1)
                                                .Create();

            string [] countryNames = ["Türkiye", "Almanya"];
            int index = 0;
            var returnedCountryDatas = Fixture.Build<Country>()
                                              .With(x => x.Name, () => countryNames[index++])
                                              .CreateMany(2)
                                              .AsQueryable();
            _countryRepositoryMock.Setup(x => x.Query()).Returns(returnedCountryDatas);


            var handler = new GetCountriesQuery.GetCountriesQueryHandler(_countryRepositoryMock.Object);
            var response = await handler.Handle(query, CancellationToken.None);

            Assert.NotNull(response);
            Assert.NotEmpty(response.Data);

        }

        [Fact]
        public async Task GetCountriesQueryHandler_WhenReturnsFilteredAndSortedData_ReturnsPaginationResultCountryDto()
        {

            var _countryRepositoryMock = new Mock<ICountryRepository>();

            GetCountriesQuery query = Fixture.Build<GetCountriesQuery>()
                                                .With(x => x.DynamicRequest, new DynamicRequest()
                                                {
                                                    Sort = new List<Sort>() { 
                                                        new Sort()
                                                        {
                                                            Field = "CreatedDate",
                                                            Dir = "asc"
                                                        }
                                                    },
                                                    Filter = new Filter()
                                                    {
                                                        Field = "name",
                                                        Logic = "and",
                                                        Operator = "eq",
                                                        Value = "Türkiye"
                                                    }
                                                })
                                                .With(x => x.Size, 10)
                                                .With(x => x.Page, 1)
                                                .Create();

            string[] countryNames = ["Türkiye", "Almanya"];
            int index = 0;
            var returnedCountryDatas = Fixture.Build<Country>()
                                              .With(x => x.Name, () => countryNames[index++])
                                              .CreateMany(2)
                                              .AsQueryable();
            _countryRepositoryMock.Setup(x => x.Query()).Returns(returnedCountryDatas);


            var handler = new GetCountriesQuery.GetCountriesQueryHandler(_countryRepositoryMock.Object);
            var response = await handler.Handle(query, CancellationToken.None);

            Assert.NotNull(response);
            Assert.NotEmpty(response.Data);

        }

        #endregion

        #region GetCountryByIdQueryHandler

        [AutoMoqData]
        [Theory]
        public async Task GetCountryByIdQueryHandler_WhenCountryGet_ReturnsCountryDto(GetCountryByIdQuery query)
        {
            var _countryRepositoryMock = new Mock<ICountryRepository>();

            var returnedCountryData = Fixture.Create<Country>();
            _countryRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<bool>())).ReturnsAsync(returnedCountryData);

            var handler = new GetCountryByIdQuery.GetCountryByIdQueryHandler(_countryRepositoryMock.Object);
            var response = await handler.Handle(query, CancellationToken.None);

            Assert.NotNull(response);
            Assert.IsType<CountryDto>(response, false);
        }

        [AutoMoqData]
        [Theory]
        public async Task GetCountryByIdQueryHandler_WhenCountryNotFound_ThrowsSelfWaiterException(GetCountryByIdQuery query)
        {
            var _countryRepositoryMock = new Mock<ICountryRepository>();

            Country? returnedCountryData = null;
            _countryRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<bool>())).ReturnsAsync(returnedCountryData);

            var handler = new GetCountryByIdQuery.GetCountryByIdQueryHandler(_countryRepositoryMock.Object);



            var exception = await Assert.ThrowsAnyAsync<SelfWaiterException>(async () =>
            {
                 await handler.Handle(query, CancellationToken.None);
            });

            Assert.Equal(ExceptionMessages.CountryNotFound, exception.Message);
        }

        #endregion
    }
}
