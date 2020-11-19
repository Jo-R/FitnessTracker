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
    // TODO password in create user and also change password
    // TODO unit tests for this and handlers
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

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
