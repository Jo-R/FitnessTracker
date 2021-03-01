using AutoMapper;
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
    public class UserByEmailQueryHandler : IRequestHandler<UserByEmailQuery, RequestResult<UserResponse>>
    {
        private readonly FitnessTrackerContext _ctx;
        private readonly ILogger<UserByEmailQueryHandler> _logger;
        private readonly IMapper _mapper;

        public UserByEmailQueryHandler(
            FitnessTrackerContext ctx, 
            ILogger<UserByEmailQueryHandler> logger,
            IMapper mapper
        )
        {
            _ctx = ctx;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<RequestResult<UserResponse>> Handle(UserByEmailQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _ctx.Users.FirstOrDefaultAsync(x => x.Email == request.Email);
                if (user == null)
                {
                    return RequestResult.Error<UserResponse>();
                }
                var response = _mapper.Map<UserResponse>(user);
                return RequestResult.Success(response);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error getting user", ex);
                return RequestResult.Error<UserResponse>();
            }
        }
    }
}
