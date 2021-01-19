using FitnessTracker.Data.Models.Requests.Users;
using FitnessTracker.Data.Models.Responses.Users;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace FitnessTracker.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Get a user by their ID
        /// </summary>
        /// <param name="id">The user id</param>
        /// <returns>Details of the user</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserResponse))]
        public async Task<IActionResult> GetUserById(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest("id must have a value");
            }

            var response = await _mediator.Send(new UserByIdQuery { Id = id });

            if (response.IsSuccess)
            {
                return Ok(response.Obj);
            }

            return NotFound();
        }

        /// <summary>
        /// Get a user by their email address
        /// </summary>
        /// <param name="email">The user's email address</param>
        /// <returns>The details of the user</returns>
        [HttpGet("email/{email}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserResponse))]
        public async Task<IActionResult> GetUserByEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return BadRequest("id must have a value");
            }

            var response = await _mediator.Send(new UserByEmailQuery { Email = email });

            if (response.IsSuccess)
            {
                return Ok(response.Obj);
            }

            return NotFound();
        }
        
        /// <summary>
        /// Add a user
        /// </summary>
        /// <param name="request">The user's details</param>
        /// <returns>The user's details (including their ID)</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserResponse))]
        public async Task<IActionResult> AddUser(CreateUserRequest request)
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

            return BadRequest("The user could not be created");
           
        }

        /// <summary>
        /// Update a user's profile section
        /// </summary>
        /// <param name="request">The user id and the profile content</param>
        /// <returns>The user's details</returns>
        [HttpPatch("user-profile")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserResponse))]
        public async Task<IActionResult> PatchUserProfile(PatchUserProfileRequest request)
        {
            if (request == null || string.IsNullOrWhiteSpace(request.ProfileContent))
            {
                return BadRequest("Invalid request");
            }

            var response = await _mediator.Send(request);

            if (response.IsSuccess)
            {
                return Ok(response.Obj);
            }

            return BadRequest("The user's profile could not be updated");
           
        }
    }
}
