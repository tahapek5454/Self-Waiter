using MediatR;
using Microsoft.AspNetCore.Mvc;
using SelfWaiter.DealerAPI.Core.Application.Features.Commands.CountryCommands;
using SelfWaiter.DealerAPI.Core.Application.Features.Queries.CountryQueries;
using SelfWaiter.Shared.Core.Domain.Dtos;

namespace SelfWaiter.DealerAPI.Presentation.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CountriesController(IMediator _mediator) : ControllerBase
    {

        #region Commands
        [HttpPost]
        public async Task<IActionResult> CreateCountry([FromBody] CreateCountryCommand request)
        {
            await _mediator.Send(request);

            return Created();
        }

        [HttpPost]
        public async Task<IActionResult> CreateRangeCountry([FromBody] CreateRangeCountryCommand request)
        {
            await _mediator.Send(request);

            return Created();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCountry([FromBody] UpdateCountryCommand request)
        {
            await _mediator.Send(request);

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCountry([FromQuery] DeleteCountryCommand request)
        {
            await _mediator.Send(request);

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteRangeCountry([FromBody] DeleteRangeCountryCommand request)
        {
            await _mediator.Send(request);

            return NoContent();
        }

        #endregion

        #region Queries
        [HttpPost]
        public async Task<IActionResult> GetCountries([FromQuery] GetCountriesQuery request, [FromBody] DynamicRequest? dynamicRequest)
        {
            request.DynamicRequest = dynamicRequest;

            var r = await _mediator.Send(request);

            return Ok(r);
        }

        [HttpGet]
        public async Task<IActionResult> GetCountryById([FromQuery] GetCountryByIdQuery request)
        {

            var r = await _mediator.Send(request);

            return Ok(r);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCountries([FromQuery] GetAllCountriesQuery request)
        {

            var r = await _mediator.Send(request);

            return Ok(r);
        }

        #endregion
    }
}
