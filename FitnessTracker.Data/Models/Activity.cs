using System;
using System.Collections.Generic;
using System.Text;

namespace FitnessTracker.Data.Models
{
    public enum ActivityType
    {
        Running,
        Yoga
    }

    public class Activity
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public DateTime Date { get; set; }
        public ActivityType Type { get; set; }
        public string Comments { get; set; }

        public User User {get; set;}
    }
}
