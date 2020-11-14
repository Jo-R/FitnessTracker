using FitnessTracker.Data.Models.Requests.Users;
using FitnessTracker.Data.Models.Responses.Users;
using FitnessTracker.Data.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace FitnessTracker.Data.Handlers.Users
{
    public class CreateUserHandler : IRequestHandler<CreateUserRequest, UserSummaryResponse>
    {
        private readonly FitnessTrackerContext _ctx;

        public CreateUserHandler(FitnessTrackerContext ctx)
        {
            _ctx = ctx;
        }
        public async Task<UserSummaryResponse> Handle(CreateUserRequest request, CancellationToken cancellationToken)
        {
            // TODO ?validation - CHEK EMAIL NOT ALREADY IN USE
            // TODO error return values?
            // TODO ?automapper

            using (_ctx)
            {
                var user = new User
                {
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    DateOfBirth = request.DateOfBirth,
                    Email = request.Email,
                    PhoneNumber = request.PhoneNumber
                };
                await _ctx.Users.AddAsync(user);
                await _ctx.SaveChangesAsync();

                var createdUser =  _ctx.Users.FirstOrDefault(x => x.Email == request.Email);

                var response = new UserSummaryResponse
                {
                    Id = createdUser.Id,
                    FirstName = createdUser.FirstName,
                    LastName = createdUser.LastName
                };

                return response;
            }

        }
    }
}
