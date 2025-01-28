using Microsoft.AspNetCore.Mvc;
using SelfWaiter.DealerAPI.Core.Application.Features.Commands.DistrictCommands;
using SelfWaiter.DealerAPI.Core.Application.Features.Queries.DistrictQueries;
using SelfWaiter.DealerAPI.Presentation.Controllers;
using SelfWaiter.DealerAPI.Test.Attributes;
using SelfWaiter.DealerAPI.Test.Commons;
using SelfWaiter.Shared.Core.Domain.Dtos;
using Xunit;

namespace SelfWaiter.DealerAPI.Test.DistrictTests
{
    public class DistrictControllerTest: BaseTest
    {
        #region Commands

        [AutoMoqData]
        [Theory]
        public async Task CreateDistrict_WhenCreateDistrict_ReturnCreated(CreateDistrictCommand  command)
        {
            var result = await new DistrictsController(MediatorBasic).CreateDistrict(command);

            Assert.IsAssignableFrom<IActionResult>(result);
        }

        [AutoMoqData]
        [Theory]
        public async Task CreateRangeDistrict_WhenCreateRangeDistrict_ReturnCreated(CreateRangeDistrictCommand command)
        {
            var result = await new DistrictsController(MediatorBasic).CreateRangeDistrict(command);

            Assert.IsAssignableFrom<IActionResult>(result);
        }


        [AutoMoqData]
        [Theory]
        public async Task DeleteDistrict_WhenDeleteDistrict_ReturnNoContent(DeleteDistrictCommand command)
        {
            var result = await new DistrictsController(MediatorBasic).DeleteDistrict(command);

            Assert.IsAssignableFrom<IActionResult>(result);
        }


        [AutoMoqData]
        [Theory]
        public async Task DeleteRangeDistrict_WhenDeleteRangeDistrict_ReturnNoContent(DeleteRangeDistrictCommand command)
        {
            var result = await new DistrictsController(MediatorBasic).DeleteRangeDistrict(command);

            Assert.IsAssignableFrom<IActionResult>(result);
        }


        [AutoMoqData]
        [Theory]
        public async Task UpdateDistrict_WhenUpdateDistrict_ReturnNoContent(UpdateDistrictCommand command)
        {
            var result = await new DistrictsController(MediatorBasic).UpdateDistrict(command);

            Assert.IsAssignableFrom<IActionResult>(result);
        }

        #endregion


        #region Queries


        [AutoMoqData]
        [Theory]
        public async Task GetAllDistricts_WhenGetAllDistricts_ReturnOk(GetAllDistrictsQuery query)
        {
            var result = await new DistrictsController(MediatorBasic).GetAllDistricts(query);

            Assert.IsAssignableFrom<IActionResult>(result);
        }

        [AutoMoqData]
        [Theory]
        public async Task GetDistrictById_WhenGetDistrict_ReturnOk(GetDistrictByIdQuery query)
        {
            var result = await new DistrictsController(MediatorBasic).GetDistrictById(query);

            Assert.IsAssignableFrom<IActionResult>(result);
        }

        [AutoMoqData]
        [Theory]
        public async Task GetDistrictByIdWithCity_WhenGetDistrict_ReturnOk(GetDistrictByIdWithCityQuery query)
        {
            var result = await new DistrictsController(MediatorBasic).GetDistrictByIdWithCity(query);

            Assert.IsAssignableFrom<IActionResult>(result);
        }

        [AutoMoqData]
        [Theory]
        public async Task GetDistricts_WhenGetistricts_ReturnOk(GetDistrictsQuery query, DynamicRequest? request)
        {
            var result = await new DistrictsController(MediatorBasic).GetDistricts(query, request);

            Assert.IsAssignableFrom<IActionResult>(result);
        }

        #endregion
    }
}
