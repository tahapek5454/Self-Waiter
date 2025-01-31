using MediatR;
using Microsoft.AspNetCore.Mvc;
using SelfWaiter.FileAPI.Core.Application.Features.Commands.DealerImageFileCommands;

namespace SelfWaiter.FileAPI.Presentation.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DealerImageFilesController(IMediator _mediator) : ControllerBase
    {
        #region Commands

        [HttpPost]
        public async Task<IActionResult> CreateRangeDealerImageFile([FromQuery] CreateRangeDealerImageFileCommand command)
        {
            command.FormFileCollection = Request.Form.Files;
            var result = await _mediator.Send(command);

            return NoContent();
        }


        [HttpDelete]
        public async Task<IActionResult> DeleteRangeDealerImageFile([FromBody] DeleteRangeDealerImageFileCommand command)
        {
            var result = await _mediator.Send(command);

            return NoContent();
        }

        #endregion
    }
}
