using FitnessTracker.Data.Models.Requests.Users;
using FitnessTracker.Data.Models.Responses.Users;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FitnessTracker.Data.Handlers.Users
{
    public class PatchUserProfileHandler : IRequestHandler<PatchUserProfileRequest, UserResponse>
    {
        private readonly FitnessTrackerContext _ctx;
        private readonly ILogger<PatchUserProfileHandler> _logger;

        public PatchUserProfileHandler(FitnessTrackerContext ctx, ILogger<PatchUserProfileHandler> logger)
        {
            _ctx = ctx;
            _logger = logger;
        }

        public Task<UserResponse> Handle(PatchUserProfileRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var user = _ctx.Users.FirstOrDefault(x => x.Id == request.Id);
                if (user == null)
                {
                    _logger.LogError("user not found");
                    return Task.FromResult<UserResponse>(null);
                }

                user.UserProfile = request.ProfileContent;
                _ctx.SaveChanges();

                var updatedUser = _ctx.Users.FirstOrDefault(x => x.Id == request.Id);

                if (updatedUser == null)
                {
                    _logger.LogError("user not found after update");
                    return Task.FromResult<UserResponse>(null);
                }

                var response = new UserResponse
                {
                    Id = updatedUser.Id,
                    FirstName = updatedUser.FirstName,
                    LastName = updatedUser.LastName,
                    Email = updatedUser.Email,
                    DateOfBirth = updatedUser.DateOfBirth,
                    PhoneNumber = updatedUser.PhoneNumber,
                    UserProfile = updatedUser.UserProfile
                };

                return Task.FromResult(response);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error updating user profile user", ex);
                return Task.FromResult<UserResponse>(null);
            }
        }
    }
}
