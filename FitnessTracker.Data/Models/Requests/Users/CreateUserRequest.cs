using FitnessTracker.Data.Models.Responses.Users;
using MediatR;
using System;

namespace FitnessTracker.Data.Models.Requests.Users
{
    public class CreateUserRequest: IRequest<UserSummaryResponse>
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }
}
