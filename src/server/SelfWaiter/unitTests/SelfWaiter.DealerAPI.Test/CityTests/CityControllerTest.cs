using Microsoft.AspNetCore.Mvc;
using SelfWaiter.DealerAPI.Core.Application.Features.Commands.CityCommands;
using SelfWaiter.DealerAPI.Core.Application.Features.Queries.CityQueries;
using SelfWaiter.DealerAPI.Presentation.Controllers;
using SelfWaiter.DealerAPI.Test.Attributes;
using SelfWaiter.DealerAPI.Test.Commons;
using SelfWaiter.Shared.Core.Domain.Dtos;
using Xunit;

namespace SelfWaiter.DealerAPI.Test.CityTests
{
    public class CityControllerTest: BaseTest
    {
        #region Commands

        [AutoMoqData]
        [Theory]
        public async Task CreateCity_WhenCreatedCity_ReturnsCreated(CreateCityCommand command)
        {
            var result = await new CitiesController(MediatorBasic).CreateCity(command);

            Assert.NotNull(result);
            Assert.IsAssignableFrom<IActionResult>(result);
        }

        [AutoMoqData]
        [Theory]
        public async Task CreateRangeCity_WhenCreatedRabgeCity_ReturnsCreated(CreateRangeCityCommand command)
        {
            var result = await new CitiesController(MediatorBasic).CreateRangeCity(command);

            Assert.NotNull(result);
            Assert.IsAssignableFrom<IActionResult>(result);
        }


        [AutoMoqData]
        [Theory]
        public async Task DeleteCity_WhenDeleteCity_ReturnsNoContent(DeleteCityCommand command)
        {
            var result = await new CitiesController(MediatorBasic).DeleteCity(command);

            Assert.NotNull(result);
            Assert.IsAssignableFrom<IActionResult>(result);
        }


        [AutoMoqData]
        [Theory]
        public async Task DeleteRangeCity_WhenDeleteRangeCity_ReturnsNoContent(DeleteRangeCityCommand command)
        {
            var result = await new CitiesController(MediatorBasic).DeleteRangeCity(command);

            Assert.NotNull(result);
            Assert.IsAssignableFrom<IActionResult>(result);
        }

        [AutoMoqData]
        [Theory]
        public async Task UpdateCity_WhenUpdateCity_ReturnsNoContent(UpdateCityCommand command)
        {
            var result = await new CitiesController(MediatorBasic).UpdateCity(command);

            Assert.NotNull(result);
            Assert.IsAssignableFrom<IActionResult>(result);
        }

        #endregion


        #region Queries

        [AutoMoqData]
        [Theory]
        public async Task GetAllCities_WhenGetAllCity_ReturnsOk(GetAllCitiesQuery query)
        {
            var result = await new CitiesController(MediatorBasic).GetAllCities(query);

            Assert.NotNull(result);
            Assert.IsAssignableFrom<IActionResult>(result);
        }


        [AutoMoqData]
        [Theory]
        public async Task GetCities_WhenGetCities_ReturnsOk(GetCitiesQuery query, DynamicRequest? dynamicRequest)
        {
            var result = await new CitiesController(MediatorBasic).GetCities(query, dynamicRequest);

            Assert.NotNull(result);
            Assert.IsAssignableFrom<IActionResult>(result);
        }

        [AutoMoqData]
        [Theory]
        public async Task GetCityByIds_WhenGetCity_ReturnsOk(GetCityByIdQuery query)
        {
            var result = await new CitiesController(MediatorBasic).GetCityById(query);

            Assert.NotNull(result);
            Assert.IsAssignableFrom<IActionResult>(result);
        }

        [AutoMoqData]
        [Theory]
        public async Task GetCityByIdWithCountry_WhenGetAllCity_ReturnsOk(GetCityByIdWithCountryQuery query)
        {
            var result = await new CitiesController(MediatorBasic).GetCityByIdWithCountry(query);

            Assert.NotNull(result);
            Assert.IsAssignableFrom<IActionResult>(result);
        }
        #endregion
    }
}
