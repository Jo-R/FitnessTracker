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
    public class UserByIdQueryHandler : IRequestHandler<UserByIdQuery, RequestResult<UserResponse>>
    {
        private readonly FitnessTrackerContext _ctx;
        private readonly ILogger<UserByIdQueryHandler> _logger;
        private readonly IMapper _mapper;

        public UserByIdQueryHandler(
            FitnessTrackerContext ctx, 
            ILogger<UserByIdQueryHandler> logger,
            IMapper mapper
         )
        {
            _ctx = ctx;
            _logger = logger;
            _mapper = mapper;
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
                var response = _mapper.Map<UserResponse>(user);
                return RequestResult.Success(response);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error getting user", ex);
                return RequestResult.Error<UserResponse>();             }
        }
    }
}
