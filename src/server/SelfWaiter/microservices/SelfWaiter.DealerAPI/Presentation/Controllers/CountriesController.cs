using MediatR;
using Microsoft.AspNetCore.Mvc;
using SelfWaiter.DealerAPI.Core.Application.Features.Commands.CountryCommands;

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
            var r = await _mediator.Send(request);

            return Ok(r);
        }

        #endregion

        #region Queries

        #endregion
    }
}
