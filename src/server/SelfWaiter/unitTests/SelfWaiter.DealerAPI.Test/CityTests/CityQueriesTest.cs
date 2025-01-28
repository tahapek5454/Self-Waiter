using AutoFixture;
using Microsoft.Extensions.Logging;
using MockQueryable;
using Moq;
using SelfWaiter.DealerAPI.Core.Application.Dtos;
using SelfWaiter.DealerAPI.Core.Application.Features.Queries.CityQueries;
using SelfWaiter.DealerAPI.Core.Application.Repositories;
using SelfWaiter.DealerAPI.Core.Domain.Entities;
using SelfWaiter.DealerAPI.Test.Attributes;
using SelfWaiter.DealerAPI.Test.Commons;
using SelfWaiter.Shared.Core.Application.Utilities;
using SelfWaiter.Shared.Core.Application.Utilities.Consts;
using SelfWaiter.Shared.Core.Domain.Dtos;
using Xunit;

namespace SelfWaiter.DealerAPI.Test.CityTests
{
    public class CityQueriesTest: BaseTest
    {

        #region GetAllCityQueryHandler

        [AutoMoqData]
        [Theory]
        public async Task GetAllCityQueryHandler_WhenGetAllCityDatas_ReturnsEnumerableCityDto(GetAllCitiesQuery query)
        {
            var _cityRepositoryMock = new Mock<ICityRepository>();
            var _loggerMock = new Mock<ILogger<GetAllCitiesQuery>>();

            var returnedCityDatas = Fixture.CreateMany<City>().AsQueryable();
            _cityRepositoryMock.Setup(x => x.Query()).Returns(returnedCityDatas);


            var handler = new GetAllCitiesQuery.GetAllCityQueryHandler(_cityRepositoryMock.Object, _loggerMock.Object);
            var result = await handler.Handle(query, CancellationToken.None);

            Assert.NotEmpty(result);
            Assert.IsAssignableFrom<IEnumerable<CityDto>>(result);
        }

        #endregion


        #region GetCitiesQueryHandler


        [Fact]
        public async Task GetCitiesQueryHandler_WhenGetCitiesDatas_ReturnsPaginationResultCityDtor()
        {
            var _cityRepositoryMock = new Mock<ICityRepository>();

            DynamicRequest? dynamicRequest = null;
            GetCitiesQuery query = Fixture.Build<GetCitiesQuery>()
                                                .With(x => x.DynamicRequest, dynamicRequest)
                                                .With(x => x.Size, 10)
                                                .With(x => x.Page, 1)
                                                .Create();

            var returnedCityDatas = Fixture.CreateMany<City>().AsQueryable();
            _cityRepositoryMock.Setup(x => x.Query()).Returns(returnedCityDatas);

            var handler = new GetCitiesQuery.GetCitiesQueryHandler(_cityRepositoryMock.Object);
            var response = await handler.Handle(query, CancellationToken.None);

            Assert.NotNull(response);
            Assert.NotEmpty(response.Data);
        }



        [Fact]
        public async Task GetCitiesQueryHandler_WhenReturnsFilteredData_ReturnsPaginationResultCityDto()
        {
            var _cityRepositoryMock = new Mock<ICityRepository>();

            GetCitiesQuery query = Fixture.Build<GetCitiesQuery>()
                                                .With(x => x.DynamicRequest, new DynamicRequest()
                                                {
                                                    Sort = null,
                                                    Filter = new Filter()
                                                    {
                                                        Field = "name",
                                                        Logic = "and",
                                                        Operator = "eq",
                                                        Value = "Serdivan"
                                                    }
                                                })
                                                .With(x => x.Size, 10)
                                                .With(x => x.Page, 1)
                                                .Create();

            string[] cityNames = ["Adapazarı", "Serdivan"];
            int index = 0;
            var returnedCityDatas = Fixture.Build<City>()
                                              .With(x => x.Name, () => cityNames[index++])
                                              .CreateMany(2)
                                              .AsQueryable();
            _cityRepositoryMock.Setup(x => x.Query()).Returns(returnedCityDatas);


            var handler = new GetCitiesQuery.GetCitiesQueryHandler(_cityRepositoryMock.Object);
            var response = await handler.Handle(query, CancellationToken.None);

            Assert.NotNull(response);
            Assert.NotEmpty(response.Data);

        }

        [Fact]
        public async Task GetCitiesQueryHandler_WhenReturnsFilteredAndSortedData_ReturnsPaginationResultCityDto()
        {

            var _cityRepositoryMock = new Mock<ICityRepository>();

            GetCitiesQuery query = Fixture.Build<GetCitiesQuery>()
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
                                                        Value = "Serdivan"
                                                    }
                                                })
                                                .With(x => x.Size, 10)
                                                .With(x => x.Page, 1)
                                                .Create();

            string[] countryNames = ["Adapazarı", "Serdivan"];
            int index = 0;
            var returnedCountryDatas = Fixture.Build<City>()
                                              .With(x => x.Name, () => countryNames[index++])
                                              .CreateMany(2)
                                              .AsQueryable();
            _cityRepositoryMock.Setup(x => x.Query()).Returns(returnedCountryDatas);


            var handler = new GetCitiesQuery.GetCitiesQueryHandler(_cityRepositoryMock.Object);
            var response = await handler.Handle(query, CancellationToken.None);

            Assert.NotNull(response);
            Assert.NotEmpty(response.Data);

        }

        #endregion

        #region GetCityByIdQueryHandler

        [AutoMoqData]
        [Theory]
        public async Task GetCityByIdQueryHandler_WhenCityGet_ReturnsCityDto(GetCityByIdQuery query)
        {
            var _cityRepositoryMock = new Mock<ICityRepository>();

            var returnedCityData = Fixture.Create<City>();
            _cityRepositoryMock.Setup(x =>x.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<bool>())).ReturnsAsync(returnedCityData);

            var handler = new GetCityByIdQuery.GetCityByIdQueryHandler(_cityRepositoryMock.Object);
            var result = await handler.Handle(query, CancellationToken.None);

            Assert.NotNull(result);
            Assert.IsAssignableFrom<CityDto>(result);
        }

        [AutoMoqData]
        [Theory]
        public async Task GetCityByIdQueryHandler_WhenCityNotFound_ThrowsSelfWaiterException(GetCityByIdQuery query)
        {
            var _cityRepositoryMock = new Mock<ICityRepository>();

            City? returnedCityData = null;
            _cityRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<bool>())).ReturnsAsync(returnedCityData);

            var handler = new GetCityByIdQuery.GetCityByIdQueryHandler(_cityRepositoryMock.Object);
            var exception = await Assert.ThrowsAsync<SelfWaiterException>(async ()=> await handler.Handle(query, CancellationToken.None));

            Assert.Equal(ExceptionMessages.CityNotFound, exception.Message);
        }

        #endregion

        #region GetCityByIdWithCountryQueryHandler

        [AutoMoqData]
        [Theory]
        public async Task GetCityByIdWithCountryQueryHandler_WhenGetCity_ReturnsCityDto(GetCityByIdWithCountryQuery query)
        {
            var _cityRepositoryMock = new Mock<ICityRepository>();
            var returnedCityDatas = Fixture.Build<City>().With(x => x.Id, query.Id).CreateMany(1).AsQueryable();
            
            _cityRepositoryMock.Setup(x =>x.Query()).Returns(returnedCityDatas.BuildMock());

            var handler = new GetCityByIdWithCountryQuery.GetCityByIdWithCountryQueryHandler(_cityRepositoryMock.Object);
            var result = await handler.Handle(query, CancellationToken.None);

            Assert.NotNull(result);
            Assert.IsAssignableFrom<CityDto>(result);
        }


        [AutoMoqData]
        [Theory]
        public async Task GetCityByIdWithCountryQueryHandler_WhenNotFoundCity_ThrowsSelfWaiterException(GetCityByIdWithCountryQuery query)
        {
            var _cityRepositoryMock = new Mock<ICityRepository>();
            var returnedCityDatas = Fixture.CreateMany<City>(1).AsQueryable();

            _cityRepositoryMock.Setup(x => x.Query()).Returns(returnedCityDatas.BuildMock());

            var handler = new GetCityByIdWithCountryQuery.GetCityByIdWithCountryQueryHandler(_cityRepositoryMock.Object);
            var exception = await Assert.ThrowsAsync<SelfWaiterException>(async () => await handler.Handle(query, CancellationToken.None));

            Assert.Equal(ExceptionMessages.CityNotFound, exception.Message);
        }

        #endregion
    }
}
