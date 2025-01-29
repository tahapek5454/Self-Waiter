using AutoFixture;
using Microsoft.Extensions.Caching.Hybrid;
using Microsoft.Extensions.Logging;
using MockQueryable;
using Moq;
using SelfWaiter.DealerAPI.Core.Application.Dtos;
using SelfWaiter.DealerAPI.Core.Application.Features.Queries.DealerQueries;
using SelfWaiter.DealerAPI.Core.Application.Features.Queries.DistrictQueries;
using SelfWaiter.DealerAPI.Core.Application.Repositories;
using SelfWaiter.DealerAPI.Core.Domain.Entities;
using SelfWaiter.DealerAPI.Test.Attributes;
using SelfWaiter.DealerAPI.Test.Commons;
using SelfWaiter.Shared.Core.Application.Utilities;
using SelfWaiter.Shared.Core.Application.Utilities.Consts;
using SelfWaiter.Shared.Core.Domain.Dtos;
using Xunit;

namespace SelfWaiter.DealerAPI.Test.DealerTests
{
    public class DealerQueriesTest: BaseTest
    {
        #region GetAllDealersQueryHandler

        [AutoMoqData]
        [Theory]
        public async Task GetAllDealersQueryHandler_WhenGetAllDealerData_ReturnsEnumreableDealerDto(GetAllDealersQuery query)
        {
            var _dealerRepositoryMock = new Mock<IDealerRepository>();
            var _loggerMock = new Mock<ILogger<GetAllDealersQuery>>();

            var returnedDealerDatas = Fixture.CreateMany<Dealer>().AsQueryable();

            _dealerRepositoryMock.Setup(x => x.Query()).Returns(returnedDealerDatas);

            var handler = new GetAllDealersQuery.GetAllDealersQueryHandler(_dealerRepositoryMock.Object, _loggerMock.Object);   
            var result = await handler.Handle(query, CancellationToken.None);

            Assert.NotEmpty(result);
            Assert.IsAssignableFrom<IEnumerable<DealerDto>>(result);
        }

        #endregion

        #region GetDealerByIdQuery


        [AutoMoqData]
        [Theory]
        public async Task GetDealerByIdQueryHandler_WhenDealerGetFromCache_ReturnsDealerDto(GetDealerByIdQuery query)
        {
            var _dealerRepositoryMock = new Mock<IDealerRepository>();
            var _hybridCacheMock = new Mock<HybridCache>();
            var cachedDealer = Fixture.Create<DealerDto>();

            _hybridCacheMock
                        .Setup(hc => hc.GetOrCreateAsync(
                            It.IsAny<string>(), 
                            It.IsAny<Guid>(),
                            It.IsAny<Func<Guid, CancellationToken, ValueTask<DealerDto>>>(),
                            It.IsAny<HybridCacheEntryOptions?>(),
                            It.IsAny<IEnumerable<string>?>(), 
                            It.IsAny<CancellationToken>()
                            )
                        ).ReturnsAsync(cachedDealer);
                        

            var handler = new GetDealerByIdQuery.GetDealerByIdQueryHandler(_dealerRepositoryMock.Object, _hybridCacheMock.Object);
            var result = await handler.Handle(query, CancellationToken.None);

            Assert.NotNull(result);
            Assert.IsAssignableFrom<DealerDto>(result);
        }


        [AutoMoqData]
        [Theory]
        public async Task GetDealerByIdQueryHandler_WhenDealerGetFromDb_ReturnsDealerDto(GetDealerByIdQuery query)
        {
            var _dealerRepositoryMock = new Mock<IDealerRepository>();
            var _hybridCacheMock = new Mock<HybridCache>();


            var reachDb = (string key, Guid state, Func<Guid, CancellationToken, ValueTask<DealerDto>> factory,
                            HybridCacheEntryOptions? options, IEnumerable<string>? tags, CancellationToken token) =>
                            {
                                return factory(state, token);
                            };
            _hybridCacheMock
                        .Setup(hc => hc.GetOrCreateAsync(
                            It.IsAny<string>(),
                            It.IsAny<Guid>(),
                            It.IsAny<Func<Guid, CancellationToken, ValueTask<DealerDto>>>(),
                            It.IsAny<HybridCacheEntryOptions?>(),
                            It.IsAny<IEnumerable<string>?>(),
                            It.IsAny<CancellationToken>()
                            )
                        ).Returns(reachDb);


            var returnedDealerData = Fixture.Create<Dealer>();
            _dealerRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<bool>())).ReturnsAsync(returnedDealerData);


            var handler = new GetDealerByIdQuery.GetDealerByIdQueryHandler(_dealerRepositoryMock.Object, _hybridCacheMock.Object);
            var result = await handler.Handle(query, CancellationToken.None);

            Assert.NotNull(result);
            Assert.IsAssignableFrom<DealerDto>(result);
        }

        [AutoMoqData]
        [Theory]
        public async Task GetDealerByIdQueryHandler_WhenDealerGetFromDbButDealerNotFound_ThrowsSelfWaiterException(GetDealerByIdQuery query)
        {
            var _dealerRepositoryMock = new Mock<IDealerRepository>();
            var _hybridCacheMock = new Mock<HybridCache>();


            var reachDb = (string key, Guid state, Func<Guid, CancellationToken, ValueTask<DealerDto>> factory,
                            HybridCacheEntryOptions? options, IEnumerable<string>? tags, CancellationToken token) =>
            {
                return factory(state, token);
            };
            _hybridCacheMock
                        .Setup(hc => hc.GetOrCreateAsync(
                            It.IsAny<string>(),
                            It.IsAny<Guid>(),
                            It.IsAny<Func<Guid, CancellationToken, ValueTask<DealerDto>>>(),
                            It.IsAny<HybridCacheEntryOptions?>(),
                            It.IsAny<IEnumerable<string>?>(),
                            It.IsAny<CancellationToken>()
                            )
                        ).Returns(reachDb);


            Dealer? returnedDealerData = null;
            _dealerRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<bool>())).ReturnsAsync(returnedDealerData);


            var handler = new GetDealerByIdQuery.GetDealerByIdQueryHandler(_dealerRepositoryMock.Object, _hybridCacheMock.Object);
            var exception = await Assert.ThrowsAsync<SelfWaiterException>(async () => await handler.Handle(query, CancellationToken.None));

            Assert.Equal(ExceptionMessages.DealerNotFound, exception.Message);
        }

        #endregion


        #region GetDealersByCreatorUserIdQueryHandler

        [AutoMoqData]
        [Theory]
        public async Task GetDealersByCreatorUserIdQueryHandler_WhenDealersGetFromCache_ReturnsEnumerableDealerDto(GetDealersByCreatorUserIdQuery query)
        {
            var _dealerRepositoryMock = new Mock<IDealerRepository>();
            var _hybridCacheMock = new Mock<HybridCache>();

            var cachedDealers = Fixture.CreateMany<DealerDto>().ToList();

            _hybridCacheMock
                       .Setup(hc => hc.GetOrCreateAsync(
                           It.IsAny<string>(),
                           It.IsAny<Guid?>(),
                           It.IsAny<Func<Guid?, CancellationToken, ValueTask<List<DealerDto>>>>(),
                           It.IsAny<HybridCacheEntryOptions?>(),
                           It.IsAny<IEnumerable<string>?>(),
                           It.IsAny<CancellationToken>()
                           )
                       ).ReturnsAsync(cachedDealers);


            var handler = new GetDealersByCreatorUserIdQuery.GetDealersByCreatorUserIdQueryHandler(_dealerRepositoryMock.Object, _hybridCacheMock.Object);
            var result = await handler.Handle(query, CancellationToken.None);

            Assert.NotEmpty(result);
            Assert.IsAssignableFrom<IEnumerable<DealerDto>>(result);

        }


        [AutoMoqData]
        [Theory]
        public async Task GetDealersByCreatorUserIdQueryHandler_WhenDealersGetFromDb_ReturnsEnumerableDealerDto(GetDealersByCreatorUserIdQuery query)
        {
            var _dealerRepositoryMock = new Mock<IDealerRepository>();
            var _hybridCacheMock = new Mock<HybridCache>();




            var reachDb = (string key, Guid? state, Func<Guid?, CancellationToken, ValueTask<List<DealerDto>>> factory,
                            HybridCacheEntryOptions? options, IEnumerable<string>? tags, CancellationToken token) =>
            {
                return factory(state, token);
            };
            _hybridCacheMock
                       .Setup(hc => hc.GetOrCreateAsync(
                           It.IsAny<string>(),
                           It.IsAny<Guid?>(),
                           It.IsAny<Func<Guid?, CancellationToken, ValueTask<List<DealerDto>>>>(),
                           It.IsAny<HybridCacheEntryOptions?>(),
                           It.IsAny<IEnumerable<string>?>(),
                           It.IsAny<CancellationToken>()
                           )
                       ).Returns(reachDb);

            var returnedDealerDatas = Fixture.Build<Dealer>().With(x => x.CreatorUserId, query.CreatorUserId).CreateMany();
            _dealerRepositoryMock.Setup(x => x.Query()).Returns(returnedDealerDatas.AsQueryable().BuildMock());

            var handler = new GetDealersByCreatorUserIdQuery.GetDealersByCreatorUserIdQueryHandler(_dealerRepositoryMock.Object, _hybridCacheMock.Object);
            var result = await handler.Handle(query, CancellationToken.None);

            Assert.NotEmpty(result);
            Assert.IsAssignableFrom<IEnumerable<DealerDto>>(result);

        }


        #endregion

        #region GetDealersByUserIdQueryHandler

        [AutoMoqData]
        [Theory]
        public async Task GetDealersByUserIdQueryHandler_WhenDealerFromCache_ReturnsEnumerableDealerDto(GetDealersByUserIdQuery query)
        {
            var _dealerRepositoryMock = new Mock<IDealerRepository>();
            var _hybridCacheMock = new Mock<HybridCache>();

            var cachedDealers = Fixture.CreateMany<DealerDto>().ToList();

            _hybridCacheMock
                       .Setup(hc => hc.GetOrCreateAsync(
                           It.IsAny<string>(),
                           It.IsAny<Guid?>(),
                           It.IsAny<Func<Guid?, CancellationToken, ValueTask<List<DealerDto>>>>(),
                           It.IsAny<HybridCacheEntryOptions?>(),
                           It.IsAny<IEnumerable<string>?>(),
                           It.IsAny<CancellationToken>()
                           )
                       ).ReturnsAsync(cachedDealers);

            var handler = new GetDealersByUserIdQuery.GetDealersByUserIdQueryHandler(_dealerRepositoryMock.Object, _hybridCacheMock.Object);
            var result = await handler.Handle(query, CancellationToken.None);

            Assert.NotEmpty(result);
            Assert.IsAssignableFrom<IEnumerable<DealerDto>>(result);
        }


        [AutoMoqData]
        [Theory]
        public async Task GetDealersByUserIdQueryHandler_WhenDealerFromDb_ReturnsEnumerableDealerDto(GetDealersByUserIdQuery query)
        {
            var _dealerRepositoryMock = new Mock<IDealerRepository>();
            var _hybridCacheMock = new Mock<HybridCache>();

            var reachDb = (string key, Guid? state, Func<Guid?, CancellationToken, ValueTask<List<DealerDto>>> factory,
                            HybridCacheEntryOptions? options, IEnumerable<string>? tags, CancellationToken token) =>
            {
                return factory(state, token);
            };
            _hybridCacheMock
                       .Setup(hc => hc.GetOrCreateAsync(
                           It.IsAny<string>(),
                           It.IsAny<Guid?>(),
                           It.IsAny<Func<Guid?, CancellationToken, ValueTask<List<DealerDto>>>>(),
                           It.IsAny<HybridCacheEntryOptions?>(),
                           It.IsAny<IEnumerable<string>?>(),
                           It.IsAny<CancellationToken>()
                           )
                       ).Returns(reachDb);

            
            var userProfileAndDealersDatas = Fixture.Build<UserProfileAndDealer>()
                .With(x => x.UserProfileId, () => query.UserId)
                .CreateMany(1)
                .ToList();
            var returnedDealerDatas = Fixture.Build<Dealer>().With(x => x.UserProfileAndDealers, userProfileAndDealersDatas).CreateMany();
            _dealerRepositoryMock.Setup(x => x.Query()).Returns(returnedDealerDatas.AsQueryable().BuildMock());

            var handler = new GetDealersByUserIdQuery.GetDealersByUserIdQueryHandler(_dealerRepositoryMock.Object, _hybridCacheMock.Object);
            var result = await handler.Handle(query, CancellationToken.None);

            Assert.NotEmpty(result);
            Assert.IsAssignableFrom<IEnumerable<DealerDto>>(result);
        }

        #endregion


        #region GetDealersQueryHandler

        [Fact]
        public async Task GetDealersQueryHandler_WhenGetDealers_ReturnsEnumrableDealerDto()
        {
            var _dealerRepositoryMock = new Mock<IDealerRepository>();


            DynamicRequest? dynamicRequest = null;
            GetDealersQuery query = Fixture.Build<GetDealersQuery>()
                                                .With(x => x.DynamicRequest, dynamicRequest)
                                                .With(x => x.Size, 10)
                                                .With(x => x.Page, 1)
                                                .Create();


            var returnedDealerDatas = Fixture.CreateMany<Dealer>(3);

            _dealerRepositoryMock.Setup(x => x.Query()).Returns(returnedDealerDatas.AsQueryable());

            var handler = new GetDealersQuery.GetDealersQueryHandler(_dealerRepositoryMock.Object);
            var result = await handler.Handle(query, CancellationToken.None);

            Assert.NotNull(result);
            Assert.IsAssignableFrom<IEnumerable<DealerDto>>(result.Data);

        }


        [Fact]
        public async Task GetDistrictsQueryHandler_WhenReturnsFilteredData_ReturnsPaginationResultDistrictDto()
        {
            var _dealerRepositoryMock = new Mock<IDealerRepository>();

            GetDealersQuery query = Fixture.Build<GetDealersQuery>()
                                                .With(x => x.DynamicRequest, new DynamicRequest()
                                                {
                                                   Sort = null,
                                                    Filter = new Filter()
                                                    {
                                                        Field = "name",
                                                        Logic = "and",
                                                        Operator = "eq",
                                                        Value = "Test"
                                                    }
                                                })
                                                .With(x => x.Size, 10)
                                                .With(x => x.Page, 1)
                                                .Create();

            string[] dealerNames = ["Test", "Test2"];
            int index = 0;
            var returnedDatas = Fixture.Build<Dealer>()
                                              .With(x => x.Name, () => dealerNames[index++])
                                              .CreateMany(2)
                                              .AsQueryable();
            _dealerRepositoryMock.Setup(x => x.Query()).Returns(returnedDatas);


            var handler = new GetDealersQuery.GetDealersQueryHandler(_dealerRepositoryMock.Object);
            var response = await handler.Handle(query, CancellationToken.None);

            Assert.NotNull(response);
            Assert.NotEmpty(response.Data);

        }

        [Fact]
        public async Task GetDealersQueryHandler_WhenReturnsFilteredAndSortedData_ReturnsPaginationResultDealerDto()
        {

            var _dealerRepositoryMock = new Mock<IDealerRepository>();

            GetDealersQuery query = Fixture.Build<GetDealersQuery>()
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
                                                        Value = "Test"
                                                    }
                                                })
                                                .With(x => x.Size, 10)
                                                .With(x => x.Page, 1)
                                                .Create();

            string[] dealerNames = ["Test", "Test2"];
            int index = 0;
            var returnedDatas = Fixture.Build<Dealer>()
                                              .With(x => x.Name, () => dealerNames[index++])
                                              .CreateMany(2)
                                              .AsQueryable();
            _dealerRepositoryMock.Setup(x => x.Query()).Returns(returnedDatas);


            var handler = new GetDealersQuery.GetDealersQueryHandler(_dealerRepositoryMock.Object);
            var response = await handler.Handle(query, CancellationToken.None);

            Assert.NotNull(response);
            Assert.NotEmpty(response.Data);

        }

        #endregion
    }

}
