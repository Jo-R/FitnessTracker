﻿using AutoMapper;
using FitnessTracker.Data.Models.Requests.Users;
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
    public class PatchUserProfileHandler : IRequestHandler<PatchUserProfileRequest, RequestResult<UserResponse>>
    {
        private readonly FitnessTrackerContext _ctx;
        private readonly ILogger<PatchUserProfileHandler> _logger;
        private readonly IMapper _mapper;

        public PatchUserProfileHandler(
            FitnessTrackerContext ctx, 
            ILogger<PatchUserProfileHandler> logger,
            IMapper mapper
         )
        {
            _ctx = ctx;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<RequestResult<UserResponse>> Handle(PatchUserProfileRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _ctx.Users.FirstOrDefaultAsync(x => x.Id == request.Id);
                if (user == null)
                {
                    _logger.LogError("user not found");
                    return RequestResult.Error<UserResponse>();
                }

                user.UserProfile = request.ProfileContent;
                await _ctx.SaveChangesAsync();

                var updatedUser = await _ctx.Users.FirstOrDefaultAsync(x => x.Id == request.Id);

                if (updatedUser == null)
                {
                    _logger.LogError("user not found after update");
                    return RequestResult.Error<UserResponse>();
                }

                var response = _mapper.Map<UserResponse>(updatedUser);

                return RequestResult.Success(response);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error updating user profile user", ex);
                return RequestResult.Error<UserResponse>();
            }
        }
    }
}
