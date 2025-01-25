using MediatR;
using Microsoft.AspNetCore.Mvc;
using SelfWaiter.DealerAPI.Core.Application.Features.Commands.DistrictCommands;
using SelfWaiter.DealerAPI.Core.Application.Features.Queries.DistrictQueries;
using SelfWaiter.Shared.Core.Domain.Dtos;

namespace SelfWaiter.DealerAPI.Presentation.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DistrictsController(IMediator _mediator) : ControllerBase
    {

        #region Commands
        [HttpPost]
        public async Task<IActionResult> CreateDistrict([FromBody] CreateDistrictCommand command)
        {
            var r = await _mediator.Send(command);

            return Created();
        }

        [HttpPost]
        public async Task<IActionResult> CreateRangeDistrict([FromBody] CreateRangeDistrictCommand command)
        {
            var r = await _mediator.Send(command);

            return Created();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteDistrict([FromQuery] DeleteDistrictCommand command)
        {
            var r = await _mediator.Send(command);

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteRangeDistrict([FromBody] DeleteRangeDistrictCommand command)
        {
            var r = await _mediator.Send(command);

            return NoContent();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateDistrict([FromBody] UpdateDistrictCommand command)
        {
            var r = await _mediator.Send(command);

            return NoContent();
        }
        #endregion

        #region Queries
        [HttpGet]
        public async Task<IActionResult> GetAllDistricts([FromQuery] GetAllDistrictsQuery query)
        {
            var r = await _mediator.Send(query);
            return Ok(r);
        }

        [HttpGet]
        public async Task<IActionResult> GetDistrictById([FromQuery] GetDistrictByIdQuery query)
        {
            var r = await _mediator.Send(query);

            return Ok(r);
        }

        [HttpGet]
        public async Task<IActionResult> GetDistrictByIdWithCity([FromQuery] GetDistrictByIdWithCityQuery query)
        {
            var r = await _mediator.Send(query);

            return Ok(r);
        }

        [HttpPost]
        public async Task<IActionResult> GetDistricts([FromQuery] GetDistrictsQuery query, DynamicRequest? request)
        {
            query.DynamicRequest = request;

            var r = await _mediator.Send(query);    
            return Ok(r);
        }
        #endregion
    }
}
