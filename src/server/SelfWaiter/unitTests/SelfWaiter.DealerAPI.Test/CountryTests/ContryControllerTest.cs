using Microsoft.AspNetCore.Mvc;
using SelfWaiter.DealerAPI.Core.Application.Features.Commands.CountryCommands;
using SelfWaiter.DealerAPI.Core.Application.Features.Queries.CountryQueries;
using SelfWaiter.DealerAPI.Presentation.Controllers;
using SelfWaiter.DealerAPI.Test.Attributes;
using SelfWaiter.DealerAPI.Test.Commons;
using SelfWaiter.Shared.Core.Domain.Dtos;
using Xunit;

namespace SelfWaiter.DealerAPI.Test.CountryTests
{
    public class ContryControllerTest: BaseTest
    {
        #region Commands

        [AutoMoqData]
        [Theory]
        public async Task CreateCountry_WhenCountryCreated_ReturnsCreated(CreateCountryCommand command)
        {
            var controller = new CountriesController(MediatorBasic);
            var resutl = await controller.CreateCountry(command);

            Assert.NotNull(resutl);
            Assert.IsAssignableFrom<IActionResult>(resutl);
        }


        [AutoMoqData]
        [Theory]
        public async Task CreateRangeCountry_WhenCountryRangeCreated_ReturnsCreated(CreateRangeCountryCommand command)
        {
            var controller = new CountriesController(MediatorBasic);
            var resutl = await controller.CreateRangeCountry(command);

            Assert.NotNull(resutl);
            Assert.IsAssignableFrom<IActionResult>(resutl);
        }

        [AutoMoqData]
        [Theory]
        public async Task UpdateCountry_WhenCountryUpdated_ReturnsNoContent(UpdateCountryCommand command)
        {
            var controller = new CountriesController(MediatorBasic);
            var resutl = await controller.UpdateCountry(command);

            Assert.NotNull(resutl);
            Assert.IsAssignableFrom<IActionResult>(resutl);
        }

        [AutoMoqData]
        [Theory]
        public async Task DeleteCountry_WhenCountryDeleted_ReturnsNoContent(DeleteCountryCommand command)
        {
            var controller = new CountriesController(MediatorBasic);
            var resutl = await controller.DeleteCountry(command);

            Assert.NotNull(resutl);
            Assert.IsAssignableFrom<IActionResult>(resutl);
        }

        [AutoMoqData]
        [Theory]
        public async Task DeleteRangeCountry_WhenCountryRangeDelted_ReturnsNoContent(DeleteRangeCountryCommand command)
        {
            var controller = new CountriesController(MediatorBasic);
            var resutl = await controller.DeleteRangeCountry(command);

            Assert.NotNull(resutl);
            Assert.IsAssignableFrom<IActionResult>(resutl);
        }


        #endregion

        #region Queries

        [AutoMoqData]
        [Theory]    
        public async Task GetCountries_WhenCountriesGet_ReturnsOk(GetCountriesQuery request, DynamicRequest? dynamicRequest)
        {
            var controller = new CountriesController(MediatorBasic);
            var response = await controller.GetCountries(request, dynamicRequest);

            Assert.NotNull(response);
            Assert.IsAssignableFrom<IActionResult>(response);
        }

        [AutoMoqData]
        [Theory]
        public async Task GetCountryById_WhenCountryGet_ReturnsOk(GetCountryByIdQuery request)
        {
            var controller = new CountriesController(MediatorBasic);
            var response = await controller.GetCountryById(request);

            Assert.NotNull(response);
            Assert.IsAssignableFrom<IActionResult>(response);
        }

        [AutoMoqData]
        [Theory]
        public async Task GetAllCountries_WhenCountriesGet_ReturnsOk(GetAllCountriesQuery request)
        {
            var controller = new CountriesController(MediatorBasic);
            var response = await controller.GetAllCountries(request);

            Assert.NotNull(response);
            Assert.IsAssignableFrom<IActionResult>(response);
        }

        #endregion
    }
}
