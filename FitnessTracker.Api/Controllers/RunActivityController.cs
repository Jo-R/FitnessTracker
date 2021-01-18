using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FitnessTracker.Data.Models.Requests.RunActivities;
using FitnessTracker.Data.Models.Responses.RunActivities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace FitnessTracker.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    
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
        public async Task<IActionResult> GetActivityById(int id)
        {
            var response = await _mediator.Send(new RunActivityByIdQuery { Id = id });

            if (response.IsSuccess)
            {
                return Ok(response.Obj);
            }

            return NotFound();
        }

        [HttpGet("user/{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RunActivitiesForUserResponse))]
        public async Task<IActionResult> GetActivityForUser(Guid id, [FromQuery] int pageNumber = 1, int pageSize = 50)
        {
            var response = await _mediator.Send(new RunActivitiesByUserQuery { 
                UserId = id, 
                PageNumber = pageNumber,
                PageSize = pageSize
            });

            if (response.IsSuccess)
            {
                return Ok(response.Obj);
            }

            return BadRequest();
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
