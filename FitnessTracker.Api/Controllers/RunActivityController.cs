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

        /// <summary>
        /// Get an individual run activity using its ID
        /// </summary>
        /// <param name="id">The id of the activity</param>
        /// <returns>A run activity response with all the run informatio</returns>
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

        /// <summary>
        /// Get all the run activities for a user (identified by their id). Can be paged.
        /// </summary>
        /// <param name="id">The user id to return the data for</param>
        /// <param name="pageNumber">The page number (defaults 1)</param>
        /// <param name="pageSize">The page size (defaults 50)</param>
        /// <returns>A list of the user's run activities along with the page number/size and total items</returns>
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

        /// <summary>
        /// Add a runa ctivity for a user
        /// </summary>
        /// <param name="request">The activity details</param>
        /// <returns>The activity details (including its ID)</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RunActivityResponse))]
        public async Task<IActionResult> AddActivity(AddRunActivityRequest request)
        {
            if (request == null || request.UserId == Guid.Empty)
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
