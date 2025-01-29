using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SelfWaiter.DealerAPI.Core.Application.Features.Commands.DealerCommands;
using SelfWaiter.DealerAPI.Core.Application.Features.Queries.DealerQueries;
using SelfWaiter.DealerAPI.Presentation.Controllers;
using SelfWaiter.DealerAPI.Test.Attributes;
using SelfWaiter.DealerAPI.Test.Commons;
using SelfWaiter.Shared.Core.Domain.Dtos;
using Xunit;

namespace SelfWaiter.DealerAPI.Test.DealerTests
{
    public class DealerControllerTest: BaseTest
    {
        #region Commands

        [AutoMoqData]
        [Theory]
        public async Task CreateDealer_WhenCreateDealer_ReturnsCreated(CreateDealerCommand command)
        {
            // we pass extenion metods for ex IsDevelopment(); and GetUserIdOrDefault(); we can not mock extension metods

            var envMock = new Mock<IWebHostEnvironment>();
            var result = await new DealersController(MediatorBasic, envMock.Object).CreateDealer(command);

            Assert.NotNull(result);
            Assert.IsAssignableFrom<IActionResult>(result);
        }


        [AutoMoqData]
        [Theory]
        public async Task DeleteDealer_WhenDeleteDealer_ReturnsNoContent(DeleteDealerCommand command)
        {

            var envMock = new Mock<IWebHostEnvironment>();
            var result = await new DealersController(MediatorBasic, envMock.Object).DeleteDealer(command);

            Assert.NotNull(result);
            Assert.IsAssignableFrom<IActionResult>(result);
        }

        [AutoMoqData]
        [Theory]
        public async Task DeleteRangeDealer_WhenDeleteRangeDealer_ReturnsNoContent(DeleteRangeDealerCommand command)
        {

            var envMock = new Mock<IWebHostEnvironment>();
            var result = await new DealersController(MediatorBasic, envMock.Object).DeleteRangeDealer(command);

            Assert.NotNull(result);
            Assert.IsAssignableFrom<IActionResult>(result);
        }


        [AutoMoqData]
        [Theory]
        public async Task UpdateDealer_WhenUpdateDealer_ReturnsNoContent(UpdateDealerCommand command)
        {

            var envMock = new Mock<IWebHostEnvironment>();
            var result = await new DealersController(MediatorBasic, envMock.Object).UpdateDealer(command);

            Assert.NotNull(result);
            Assert.IsAssignableFrom<IActionResult>(result);
        }


        [AutoMoqData]
        [Theory]
        public async Task AddUsersToDealer_WhenAddUsersToDealer_ReturnsNoContent(AddUsersToDealerCommand command)
        {

            var envMock = new Mock<IWebHostEnvironment>();
            var result = await new DealersController(MediatorBasic, envMock.Object).AddUsersToDealer(command);

            Assert.NotNull(result);
            Assert.IsAssignableFrom<IActionResult>(result);
        }

        [AutoMoqData]
        [Theory]
        public async Task RemoveUsersFromDealer_WhenRemoveUsersFromDealer_ReturnsNoContent(RemoveUsersFromDealerCommand command)
        {

            var envMock = new Mock<IWebHostEnvironment>();
            var result = await new DealersController(MediatorBasic, envMock.Object).RemoveUsersFromDealer(command);

            Assert.NotNull(result);
            Assert.IsAssignableFrom<IActionResult>(result);
        }




        #endregion


        #region Queries

        [AutoMoqData]
        [Theory]
        public async Task GetAllDealers_WhenGetAllDealers_ReturnsOkt(GetAllDealersQuery query)
        {

            var envMock = new Mock<IWebHostEnvironment>();
            var result = await new DealersController(MediatorBasic, envMock.Object).GetAllDealers(query);

            Assert.NotNull(result);
            Assert.IsAssignableFrom<IActionResult>(result);
        }

        [AutoMoqData]
        [Theory]
        public async Task GetDealerById_WhenGetDealerById_ReturnsOkt(GetDealerByIdQuery query)
        {

            var envMock = new Mock<IWebHostEnvironment>();
            var result = await new DealersController(MediatorBasic, envMock.Object).GetDealerById(query);

            Assert.NotNull(result);
            Assert.IsAssignableFrom<IActionResult>(result);
        }

        [AutoMoqData]
        [Theory]
        public async Task GetDealers_WhenGetDealers_ReturnsOkt(GetDealersQuery query, DynamicRequest? request)
        {

            var envMock = new Mock<IWebHostEnvironment>();
            var result = await new DealersController(MediatorBasic, envMock.Object).GetDealers(query, request);

            Assert.NotNull(result);
            Assert.IsAssignableFrom<IActionResult>(result);
        }


        #endregion
    }
}
