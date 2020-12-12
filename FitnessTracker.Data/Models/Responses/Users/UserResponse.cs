using System;
using System.Collections.Generic;
using System.Text;

namespace FitnessTracker.Data.Models.Responses.Users
{
    public class UserResponse
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTimeOffset DateOfBirth { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string UserProfile { get; set; }
    }
}
