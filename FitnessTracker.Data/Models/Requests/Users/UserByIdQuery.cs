using FitnessTracker.Data.Models.Responses;
using FitnessTracker.Data.Models.Responses.Users;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FitnessTracker.Data.Models.Requests.Users
{
    public class UserByIdQuery: IRequest<RequestResult<UserResponse>>
    {
        [Required]
        public Guid Id { get; set; }
    }
}
