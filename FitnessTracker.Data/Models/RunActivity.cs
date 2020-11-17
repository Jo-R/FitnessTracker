using System;
using System.Collections.Generic;
using System.Text;

namespace FitnessTracker.Data.Models
{
    public class RunActivity
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public DateTime Date { get; set; }
        public string Title { get; set; }
        public double Distance { get; set; }
        public int Calories { get; set; }
        /// <summary>
        /// In seconds? or ticks? use TimeSpan (is Time on garmin)
        /// </summary>
        public int Duration { get; set; }

        public int AverageHr { get; set; }

        public int MaxHr { get; set; }
        /// <summary>
        /// Probably seconds again???
        /// </summary>
        public int AveragePace { get; set; }

        public string Notes { get; set; }


        public User User {get; set;}
    }
}
