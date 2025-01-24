using MediatR;
using Microsoft.AspNetCore.Mvc;
using SelfWaiter.DealerAPI.Core.Application.Features.Commands.CityCommands;
using SelfWaiter.DealerAPI.Core.Application.Features.Queries.CityQueries;
using SelfWaiter.Shared.Core.Domain.Dtos;

namespace SelfWaiter.DealerAPI.Presentation.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CitiesController(IMediator _mediator) : ControllerBase
    {
        #region Commands
        [HttpPost]
        public async Task<IActionResult> CreateCity([FromBody] CreateCityCommand command)
        {
            var r = await _mediator.Send(command);

            return Ok(r);
        }

        [HttpPost]
        public async Task<IActionResult> CreateRangeCity([FromBody] CreateRangeCityCommand command)
        {
            var r = await _mediator.Send(command);

            return Ok(r);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCity([FromQuery] DeleteCityCommand command)
        {
            var r = await _mediator.Send(command);
            return Ok(r);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteRangeCity([FromBody] DeleteRangeCityCommand command)
        {
            var r = await _mediator.Send(command);
            return Ok(r);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCity([FromBody] UpdateCityCommand command)
        {
            var r = await _mediator.Send(command);

            return Ok();
        }

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
