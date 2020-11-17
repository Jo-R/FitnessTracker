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
    public class UserByEmailQueryHandler : IRequestHandler<UserByEmailQuery, UserResponse>
    {
        private readonly FitnessTrackerContext _ctx;
        private readonly ILogger<UserByEmailQueryHandler> _logger;

        public UserByEmailQueryHandler(FitnessTrackerContext ctx, ILogger<UserByEmailQueryHandler> logger)
        {
            _ctx = ctx;
            _logger = logger;
        }

        public Task<UserResponse> Handle(UserByEmailQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var user = _ctx.Users.FirstOrDefault(x => x.Email == request.Email);
                if (user == null)
                {
                    return Task.FromResult<UserResponse>(null);
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
                return Task.FromResult(response);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error getting user", ex);
                return Task.FromResult<UserResponse>(null); ;
            }
        }
    }
}
