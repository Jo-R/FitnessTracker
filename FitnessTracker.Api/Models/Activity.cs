using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FitnessTracker.Api.Models
{
    public enum ActivityType
    {
        Running,
        Yoga
    }

    public class Activity
    {
        public DateTime Date { get; set; }
        public ActivityType Type { get; set; }
        public string Comments { get; set; }
    }
}
