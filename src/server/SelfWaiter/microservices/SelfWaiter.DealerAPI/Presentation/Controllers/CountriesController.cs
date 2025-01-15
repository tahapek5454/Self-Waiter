using MediatR;
using Microsoft.AspNetCore.Mvc;
using SelfWaiter.DealerAPI.Core.Application.Features.Commands.CountryCommands;
using SelfWaiter.DealerAPI.Core.Application.Features.Queries.CountryQueries;
using SelfWaiter.DealerAPI.Core.Application.Repositories;
using SelfWaiter.Shared.Core.Domain.Dtos;

namespace SelfWaiter.DealerAPI.Presentation.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CountriesController(IMediator _mediator, ICountryRepository _countryRepository) : ControllerBase
    {

        #region Commands
        [HttpPost]
        public async Task<IActionResult> CreateCountry([FromBody] CreateCountryCommand request)
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

        #endregion

        #region Queries
        [HttpPost]
        public async Task<IActionResult> GetCountries([FromQuery] GetCountriesQuery request, [FromBody] DynamicRequest? dynamicRequest)
        {
            request.DynamicRequest = dynamicRequest;

            var r = await _mediator.Send(request);

            return Ok(r);
        }

        #endregion
    }
}
