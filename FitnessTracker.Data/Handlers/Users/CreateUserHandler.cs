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
using Microsoft.EntityFrameworkCore;

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
        public async Task<RequestResult<UserResponse>> Handle(CreateUserRequest request, CancellationToken cancellationToken)
        {            
            try
            {
                // can only have one user with an email address, enforced by db but fail fast here...
                var existingUser = await _ctx.Users.FirstOrDefaultAsync(x => x.Email == request.Email);
                if (existingUser != null)
                {
                   return RequestResult.Error<UserResponse>();
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
                await _ctx.Users.AddAsync(user);
                await _ctx.SaveChangesAsync();

                var createdUser = await _ctx.Users.FirstOrDefaultAsync(x => x.Email == request.Email);

                if (createdUser == null)
                {
                    _logger.LogError("user not found after creation");
                    return RequestResult.Error<UserResponse>(); 
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

                return RequestResult.Success(response);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error creating user", ex);
                return RequestResult.Error<UserResponse>();
            }

        }
    }
}
