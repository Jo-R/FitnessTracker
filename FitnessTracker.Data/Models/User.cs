using System;

namespace FitnessTracker.Data.Models
{
    public class User
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
        public DateTimeOffset DateOfBirth { get; set; }

        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        public string UserProfile { get; set; }

        public string Password { get; set; }

        public bool Active { get; set; }

    }
}
