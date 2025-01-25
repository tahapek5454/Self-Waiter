using MediatR;
using Microsoft.AspNetCore.Mvc;
using SelfWaiter.DealerAPI.Core.Application.Features.Commands.DealerCommands;

namespace SelfWaiter.DealerAPI.Presentation.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DealersController(IMediator _mediator, IWebHostEnvironment _env) : ControllerBase
    {

        #region Commands
        [HttpPost]
        public async Task<IActionResult> CreateDealer([FromBody] CreateDealerCommand command)
        {
            if(command.CreatorUserId is null)
            {
                var userId = HttpContext.User.GetUserIdOrDefault(_env.IsDevelopment());
                command.CreatorUserId = userId;
            }

            var r = await _mediator.Send(command);

            return Created();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteDealer([FromQuery] DeleteDealerCommand command)
        {
            var r = await _mediator.Send(command);

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteRangeDealerCommand([FromBody] DeleteRangeDealerCommand command)
        {
            var r = await _mediator.Send(command);

            return NoContent();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateDealer([FromBody] UpdateDealerCommand command)
        {
            var r = await _mediator.Send(command);

            return NoContent();
        }
        #endregion

        #region Queries
        #endregion
    }
}
