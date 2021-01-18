﻿using FitnessTracker.Data.Models.Requests.Users;
using FitnessTracker.Data.Models.Responses;
using FitnessTracker.Data.Models.Responses.Users;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FitnessTracker.Data.Handlers.Users
{
    public class UserByIdQueryHandler : IRequestHandler<UserByIdQuery, RequestResult<UserResponse>>
    {
        private readonly FitnessTrackerContext _ctx;
        private readonly ILogger<UserByIdQueryHandler> _logger;

        public UserByIdQueryHandler(FitnessTrackerContext ctx, ILogger<UserByIdQueryHandler> logger)
        {
            _ctx = ctx;
            _logger = logger;
        }

        public async Task<RequestResult<UserResponse>> Handle(UserByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _ctx.Users.FirstOrDefaultAsync(x => x.Id == request.Id);
                if (user == null)
                {
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
                _logger.LogError("Error getting user", ex);
                return RequestResult.Error<UserResponse>();             }
        }
    }
}
