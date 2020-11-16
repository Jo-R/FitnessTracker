using FitnessTracker.Data.Models.Requests.Users;
using FitnessTracker.Data.Models.Responses.Users;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        // GET by id
        // GET by email
        // DELETE by id
        // PATCH profile

        // TODO swagger

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserSummaryResponse))]
        public async Task<IActionResult> AddUser(CreateUserRequest request)
        {
            if (request == null)
            {
                return BadRequest("The request cannot be null");
            }
            
            var response = await _mediator.Send(request);

            if (response == null)
            {
                return BadRequest("The user could not be created"); 
            }

            return Ok(response);

        }
    }
}
