using AutoFixture;
using Microsoft.Extensions.Logging;
using MockQueryable;
using Moq;
using SelfWaiter.DealerAPI.Core.Application.Dtos;
using SelfWaiter.DealerAPI.Core.Application.Features.Queries.CountryQueries;
using SelfWaiter.DealerAPI.Core.Application.Features.Queries.DistrictQueries;
using SelfWaiter.DealerAPI.Core.Application.Repositories;
using SelfWaiter.DealerAPI.Core.Domain.Entities;
using SelfWaiter.DealerAPI.Test.Attributes;
using SelfWaiter.DealerAPI.Test.Commons;
using SelfWaiter.Shared.Core.Application.Utilities;
using SelfWaiter.Shared.Core.Application.Utilities.Consts;
using SelfWaiter.Shared.Core.Domain.Dtos;
using Xunit;

namespace SelfWaiter.DealerAPI.Test.DistrictTests
{
    public class DistrictQueriesTest: BaseTest
    {
        #region GetAllDistrictsQueryHandler

        [AutoMoqData]
        [Theory]
        public async Task GetAllDistrictsQueryHandler_WhenGetAllDistricts_ReturnsEnumreableDistrictDto(GetAllDistrictsQuery query)
        {
            var _districtRepositoryMock = new Mock<IDistrictRepository>();
            var _loggerMock = new Mock<ILogger<IDistrictRepository>>();

            var returnedDistrictDatas = Fixture.CreateMany<District>().AsQueryable();

            _districtRepositoryMock.Setup(x => x.Query()).Returns(returnedDistrictDatas);

            var handler = new GetAllDistrictsQuery.GetAllDistrictsQueryHandler(_districtRepositoryMock.Object, _loggerMock.Object);
            var response = await handler.Handle(query, CancellationToken.None);

            Assert.NotEmpty(response);
            Assert.IsAssignableFrom<IEnumerable<DistrictDto>>(response);

        }

        #endregion


        #region GetDistrictByIdQueryHandler

        [AutoMoqData]
        [Theory]
        public async Task GetDistrictByIdQueryHandler_WhenDistrictGet_ReturnsDistrictDto(GetDistrictByIdQuery query)
        {
            var _districtRepositoryMock = new Mock<IDistrictRepository>();

            var returnedDistrictData = Fixture.Create<District>();
            _districtRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<bool>())).ReturnsAsync(returnedDistrictData);

            var handler = new GetDistrictByIdQuery.GetDistrictByIdQueryHandler(_districtRepositoryMock.Object);
            var response = await handler.Handle(query, CancellationToken.None);

            Assert.NotNull(response);
            Assert.IsAssignableFrom<DistrictDto>(response);
        }



        [AutoMoqData]
        [Theory]
        public async Task GetDistrictByIdQueryHandler_WhenDistrictNotFound_ThrowsSelfWaiterException(GetDistrictByIdQuery query)
        {
            var _districtRepositoryMock = new Mock<IDistrictRepository>();

            District? returnedDistrictData = null;
            _districtRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<bool>())).ReturnsAsync(returnedDistrictData);

            var handler = new GetDistrictByIdQuery.GetDistrictByIdQueryHandler(_districtRepositoryMock.Object);
            var exception = await Assert.ThrowsAsync<SelfWaiterException>(async () => await handler.Handle(query, CancellationToken.None));

            Assert.Equal(ExceptionMessages.DistrictNotFound, exception.Message);
        }

        #endregion


        #region GetDistrictByIdWithCityQueryHandler

        [AutoMoqData]
        [Theory]
        public async Task GetDistrictByIdWithCityQueryHandler_WhenDistrictGet_ReturnsDistrictDto(GetDistrictByIdWithCityQuery query)
        {
            var _districtRepositoryMock = new Mock<IDistrictRepository>();

            var returnedDistrictData = Fixture.Build<District>().With(x => x.Id, query.Id).CreateMany(1).AsQueryable() ;
            _districtRepositoryMock.Setup(x => x.Query()).Returns(returnedDistrictData.BuildMock());

            var handler = new GetDistrictByIdWithCityQuery.GetDistrictByIdWithCityQueryHandler(_districtRepositoryMock.Object);
            var response = await handler.Handle(query, CancellationToken.None);

            Assert.NotNull(response);
            Assert.IsAssignableFrom<DistrictDto>(response);
        }



        [AutoMoqData]
        [Theory]
        public async Task GetDistrictByIdWithCityQueryHandler_WhenDistrictNotFound_ThrowsSelfWaiterException(GetDistrictByIdWithCityQuery query)
        {
            var _districtRepositoryMock = new Mock<IDistrictRepository>();

            var returnedDistrictData = Fixture.Build<District>().CreateMany(1).AsQueryable();
            _districtRepositoryMock.Setup(x => x.Query()).Returns(returnedDistrictData.BuildMock());

            var handler = new GetDistrictByIdWithCityQuery.GetDistrictByIdWithCityQueryHandler(_districtRepositoryMock.Object);
            var exception = await Assert.ThrowsAsync<SelfWaiterException>(async () => await handler.Handle(query, CancellationToken.None));

            Assert.Equal(ExceptionMessages.DistrictNotFound, exception.Message);
        }

        #endregion


        #region GetDistrictsQueryHandler


        [Fact]
        public async Task GetDistrictsQueryHandler_WhenReturnsData_ReturnsPaginationResultDistrictDto()
        {

            var _districtRepositoryMock = new Mock<IDistrictRepository>();

            DynamicRequest? dynamicRequest = null;
            GetDistrictsQuery query = Fixture.Build<GetDistrictsQuery>()
                                                .With(x => x.DynamicRequest, dynamicRequest)
                                                .With(x => x.Size, 10)
                                                .With(x => x.Page, 1)
                                                .Create();


            var returnedDatas = Fixture.CreateMany<District>().AsQueryable();
            _districtRepositoryMock.Setup(x => x.Query()).Returns(returnedDatas);


            var handler = new GetDistrictsQuery.GetDistrictsQueryHandler(_districtRepositoryMock.Object);
            var response = await handler.Handle(query, CancellationToken.None);

            Assert.NotNull(response);
            Assert.NotEmpty(response.Data);

        }


        [Fact]
        public async Task GetDistrictsQueryHandler_WhenReturnsFilteredData_ReturnsPaginationResultDistrictDto()
        {

            var _districtRepositoryMock = new Mock<IDistrictRepository>();


            GetDistrictsQuery query = Fixture.Build<GetDistrictsQuery>()
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

            string[] districtNames = ["Adapazarı", "Serdivan"];
            int index = 0;
            var returnedDatas = Fixture.Build<District>()
                                              .With(x => x.Name, () => districtNames[index++])
                                              .CreateMany(2)
                                              .AsQueryable();
            _districtRepositoryMock.Setup(x => x.Query()).Returns(returnedDatas);


            var handler = new GetDistrictsQuery.GetDistrictsQueryHandler(_districtRepositoryMock.Object);
            var response = await handler.Handle(query, CancellationToken.None);

            Assert.NotNull(response);
            Assert.NotEmpty(response.Data);

        }

        [Fact]
        public async Task GetDistrictsQueryHandler_WhenReturnsFilteredAndSortedData_ReturnsPaginationResultDistrictDto()
        {

            var _districtRepositoryMock = new Mock<IDistrictRepository>();

            GetDistrictsQuery query = Fixture.Build<GetDistrictsQuery>()
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

            string[] districtNames = ["Adapazarı", "Serdivan"];
            int index = 0;
            var returnedDatas = Fixture.Build<District>()
                                              .With(x => x.Name, () => districtNames[index++])
                                              .CreateMany(2)
                                              .AsQueryable();
            _districtRepositoryMock.Setup(x => x.Query()).Returns(returnedDatas);


            var handler = new GetDistrictsQuery.GetDistrictsQueryHandler(_districtRepositoryMock.Object);
            var response = await handler.Handle(query, CancellationToken.None);

            Assert.NotNull(response);
            Assert.NotEmpty(response.Data);

        }

        #endregion
    }
}
