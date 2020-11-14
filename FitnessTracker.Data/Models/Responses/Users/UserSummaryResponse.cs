using System;

namespace FitnessTracker.Data.Models.Responses.Users
{
    public class UserSummaryResponse
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}
