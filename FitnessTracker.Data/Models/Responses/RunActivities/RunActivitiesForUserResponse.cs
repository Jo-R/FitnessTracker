using System;
using System.Collections.Generic;
using System.Text;

namespace FitnessTracker.Data.Models.Responses.RunActivities
{
    public class RunActivitiesForUserResponse
    {
        public IEnumerable<RunActivityResponse> Items { get; set; }

        public int ItemCount { get; set; }

        public int PageNumber { get; set; }

        public int PageSize { get; set; }
    }
}
