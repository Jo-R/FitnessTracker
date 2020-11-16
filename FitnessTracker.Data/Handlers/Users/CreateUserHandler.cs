using FitnessTracker.Data.Models.Requests.Users;
using FitnessTracker.Data.Models.Responses.Users;
using FitnessTracker.Data.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.Extensions.Logging;

namespace FitnessTracker.Data.Handlers.Users
{
    public class CreateUserHandler : IRequestHandler<CreateUserRequest, UserSummaryResponse>
    {
        private readonly FitnessTrackerContext _ctx;
        private readonly ILogger<CreateUserHandler> _logger;

        public CreateUserHandler(FitnessTrackerContext ctx, ILogger<CreateUserHandler> logger)
        {
            _ctx = ctx;
            _logger = logger;
        }
        public Task<UserSummaryResponse> Handle(CreateUserRequest request, CancellationToken cancellationToken)
        {            
            try
            {
                // can only have one user with an email address, enforced by db but fail fast here...
                var existingUser = _ctx.Users.FirstOrDefault(x => x.Email == request.Email);
                if (existingUser != null)
                {
                    // TODO probably improve what return for failures
                    return Task.FromResult<UserSummaryResponse>(null);
                }
                var user = new User
                {
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    DateOfBirth = request.DateOfBirth,
                    Email = request.Email,
                    PhoneNumber = request.PhoneNumber
                };
                _ctx.Users.Add(user);
                _ctx.SaveChanges();

                var createdUser = _ctx.Users.FirstOrDefault(x => x.Email == request.Email);

                if (createdUser == null)
                {
                    _logger.LogError("user not found after creation");
                    return Task.FromResult<UserSummaryResponse>(null); ;
                }

                var response = new UserSummaryResponse
                {
                    Id = createdUser.Id,
                    FirstName = createdUser.FirstName,
                    LastName = createdUser.LastName
                };

                return Task.FromResult(response);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error creating user", ex);
                return Task.FromResult<UserSummaryResponse>(null); ;
            }

        }
    }
}
