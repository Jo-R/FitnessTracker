using FitnessTracker.Data.Models.Requests.Users;
using FitnessTracker.Data.Models.Responses.Users;
using FitnessTracker.Data.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using FitnessTracker.Data.Models.Responses;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace FitnessTracker.Data.Handlers.Users
{
    public class CreateUserHandler : IRequestHandler<CreateUserRequest, RequestResult<UserResponse>>
    {
        private readonly FitnessTrackerContext _ctx;
        private readonly ILogger<CreateUserHandler> _logger;
        private readonly IMapper _mapper;

        public CreateUserHandler(
            FitnessTrackerContext ctx, 
            ILogger<CreateUserHandler> logger,
            IMapper mapper
        )
        {
            _ctx = ctx;
            _logger = logger;
            _mapper = mapper;
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
                var user = _mapper.Map<User>(request);
                user.Active = true;
                await _ctx.Users.AddAsync(user);
                await _ctx.SaveChangesAsync();

                var createdUser = await _ctx.Users.FirstOrDefaultAsync(x => x.Email == request.Email);

                if (createdUser == null)
                {
                    _logger.LogError("user not found after creation");
                    return RequestResult.Error<UserResponse>(); 
                }

                var response = _mapper.Map<UserResponse>(createdUser);

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
