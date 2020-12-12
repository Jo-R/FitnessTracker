using FitnessTracker.Data.Models.Requests.Users;
using FitnessTracker.Data.Models.Responses.Users;
using FitnessTracker.Data.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.Extensions.Logging;
using FitnessTracker.Data.Models.Responses;

namespace FitnessTracker.Data.Handlers.Users
{
    public class CreateUserHandler : IRequestHandler<CreateUserRequest, RequestResult<UserResponse>>
    {
        private readonly FitnessTrackerContext _ctx;
        private readonly ILogger<CreateUserHandler> _logger;

        public CreateUserHandler(FitnessTrackerContext ctx, ILogger<CreateUserHandler> logger)
        {
            _ctx = ctx;
            _logger = logger;
        }
        public Task<RequestResult<UserResponse>> Handle(CreateUserRequest request, CancellationToken cancellationToken)
        {            
            try
            {
                // can only have one user with an email address, enforced by db but fail fast here...
                // TODO is this the right place to do this?
                var existingUser = _ctx.Users.FirstOrDefault(x => x.Email == request.Email);
                if (existingUser != null)
                {
                   return Task.FromResult(RequestResult.Error<UserResponse>());
                }
                var user = new User
                {
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    DateOfBirth = request.DateOfBirth,
                    Email = request.Email,
                    PhoneNumber = request.PhoneNumber,
                    Active = true
                };
                _ctx.Users.Add(user);
                _ctx.SaveChanges();

                var createdUser = _ctx.Users.FirstOrDefault(x => x.Email == request.Email);

                if (createdUser == null)
                {
                    _logger.LogError("user not found after creation");
                    return Task.FromResult(RequestResult.Error<UserResponse>()); 
                }

                var response = new UserResponse
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    DateOfBirth = user.DateOfBirth,
                    PhoneNumber = user.PhoneNumber,
                    UserProfile = user.UserProfile
                };

                return Task.FromResult(RequestResult.Success(response));
            }
            catch (Exception ex)
            {
                _logger.LogError("Error creating user", ex);
                return Task.FromResult(RequestResult.Error<UserResponse>());
            }

        }
    }
}
