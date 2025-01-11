using MediatR;
using Microsoft.AspNetCore.Mvc;
using SelfWaiter.DealerAPI.Core.Application.Features.Commands.CountryCommands;
using SelfWaiter.DealerAPI.Core.Application.Features.Queries.CountryQueries;
using SelfWaiter.DealerAPI.Core.Application.Repositories;

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
            var r = await _mediator.Send(request);

            return Ok(r);
        }

        #endregion

        #region Queries
        [HttpPost]
        public async Task<IActionResult> GetCountries([FromQuery] GetCountriesQuery request)
        {
            var r = await _mediator.Send(request);

            return Ok(r);
        }

        #endregion
    }
}
