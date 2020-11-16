﻿using FitnessTracker.Data.Models.Responses.Users;
using MediatR;
using System;
using System.ComponentModel.DataAnnotations;

namespace FitnessTracker.Data.Models.Requests.Users
{
    public class CreateUserRequest: IRequest<UserSummaryResponse>
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public DateTime DateOfBirth { get; set; }
        [EmailAddress]
        [Required]
        public string Email { get; set; }
        [Required]
        [Phone] // TODO need better validation for this
        public string PhoneNumber { get; set; }
    }
}