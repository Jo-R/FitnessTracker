using FitnessTracker.Data.Models.Responses;
using FitnessTracker.Data.Models.Responses.Users;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace FitnessTracker.Data.Models.Requests.Users
{
    public class UserByEmailQuery: IRequest<RequestResult<UserResponse>>
    {
        public string Email { get; set; }
    }
}
