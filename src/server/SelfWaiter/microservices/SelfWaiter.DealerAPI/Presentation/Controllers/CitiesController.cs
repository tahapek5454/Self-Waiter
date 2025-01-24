using MediatR;
using Microsoft.AspNetCore.Mvc;
using SelfWaiter.DealerAPI.Core.Application.Features.Queries.CityQueries;
using SelfWaiter.Shared.Core.Domain.Dtos;

namespace SelfWaiter.DealerAPI.Presentation.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CitiesController(IMediator _mediator) : ControllerBase
    {
        #region Commands
        #endregion

        #region Queries
        [HttpGet]
        public async Task<IActionResult> GetAllCities([FromQuery] GetAllCitiesQuery query)
        {
            var r = await _mediator.Send(query);

            return Ok(r);
        }

        [HttpPost]
        public async Task<IActionResult> GetCities([FromQuery] GetCitiesQuery query, [FromBody] DynamicRequest? dynamicRequest)
        {
            query.DynamicRequest = dynamicRequest;

            var r = await _mediator.Send(query);    
            return Ok(r);
        }

        [HttpGet]
        public async Task<IActionResult> GetCityById([FromQuery] GetCityByIdQuery query)
        {
            var r = await _mediator.Send(query);
            return Ok(r);
        }

        [HttpGet]
        public async Task<IActionResult> GetCityByIdWithCountry([FromQuery] GetCityByIdWithCountryQuery query)
        {
            var r = await _mediator.Send(query);
            return Ok(r);
        }
        #endregion

    }
}
