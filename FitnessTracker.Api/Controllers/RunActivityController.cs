using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FitnessTracker.Data.Models.Requests.RunActivity;
using FitnessTracker.Data.Models.Responses.RunActivity;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace FitnessTracker.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    // TODO get all activities (paged - maybe a summary?)
    // TODO get activity by id

    public class RunActivityController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RunActivityController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RunActivityResponse))]
        public async Task<IActionResult> GetUserById(int id)
        {
            var response = await _mediator.Send(new RunActivityByIdQuery { Id = id });

            if (response.IsSuccess)
            {
                return Ok(response.Obj);
            }

            return NotFound();
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RunActivityResponse))]
        public async Task<IActionResult> AddActivity(AddRunActivityRequest request)
        {
            if (request == null)
            {
                return BadRequest("The request cannot be null");
            }

            var response = await _mediator.Send(request);

            if (response.IsSuccess)
            {
                return Ok(response.Obj);
            }

            return BadRequest("The running activity could not be created");

        }
    }
}
