using MediatR;
using Microsoft.AspNetCore.Mvc;
using SelfWaiter.DealerAPI.Core.Application.Features.Commands.DealerCommands;
using SelfWaiter.DealerAPI.Core.Application.Features.Queries.DealerQueries;
using SelfWaiter.Shared.Core.Domain.Dtos;

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
        public async Task<IActionResult> DeleteRangeDealer([FromBody] DeleteRangeDealerCommand command)
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

        [HttpPost]
        public async Task<IActionResult> AddUsersToDealer([FromBody] AddUsersToDealerCommand command)
        {
            var r = await _mediator.Send(command);

            return Created();
        }

        [HttpDelete]
        public async Task<IActionResult> RemoveUsersFromDealer([FromBody] RemoveUsersFromDealerCommand command)
        {
            var r = await _mediator.Send(command);

            return NoContent();
        }

        #endregion

        #region Queries
        [HttpGet]
        public async Task<IActionResult> GetAllDealers([FromQuery] GetAllDealersQuery query)
        {
            var r = await _mediator.Send(query);

            return Ok(r);
        }

        [HttpGet]
        public async Task<IActionResult> GetDealerById([FromQuery] GetDealerByIdQuery query)
        {
            var r = await _mediator.Send(query);

            return Ok(r);
        }

        [HttpGet]
        public async Task<IActionResult> GetDealersByCreatorUserId([FromQuery] GetDealersByCreatorUserIdQuery query) //NOSONAR
        {      
            var userId = HttpContext.User.GetUserIdOrDefault(_env.IsDevelopment());
            query.CreatorUserId = userId;

            var r = await _mediator.Send(query); 
            
            return Ok(r);
        }

        [HttpGet]
        public async Task<IActionResult> GetDealersByUserId([FromQuery] GetDealersByUserIdQuery query) //NOSONAR
        {
            var userId = HttpContext.User.GetUserIdOrDefault(_env.IsDevelopment());
            query.UserId = userId;

            var r = await _mediator.Send(query);

            return Ok(r);
        }

        [HttpPost]
        public async Task<IActionResult> GetDealers([FromQuery] GetDealersQuery query, [FromBody] DynamicRequest? request)
        {
            query.DynamicRequest = request;

            var r = await _mediator.Send(query);

            return Ok(r);
        }
        #endregion
    }
}
