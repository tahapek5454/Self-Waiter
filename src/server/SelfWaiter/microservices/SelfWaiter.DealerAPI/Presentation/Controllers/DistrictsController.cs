﻿using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SelfWaiter.DealerAPI.Core.Application.Features.Queries.DistrictQueries;
using SelfWaiter.Shared.Core.Domain.Dtos;

namespace SelfWaiter.DealerAPI.Presentation.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DistrictsController(IMediator _mediator) : ControllerBase
    {

        #region Commands
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
