using FitnessTracker.Data.Models.Responses;
using FitnessTracker.Data.Models.Responses.RunActivities;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FitnessTracker.Data.Models.Requests.RunActivities
{
    public class AddRunActivityRequest: IRequest<RequestResult<RunActivityResponse>>
    {
        /// <summary>
        /// The user ID the activity belongs to
        /// </summary>
        [Required]
        public Guid UserId { get; set; }

        /// <summary>
        /// The date of the activity
        /// </summary>
        [Required]
        public DateTimeOffset Date { get; set; }

        /// <summary>
        /// A title for the activity
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// The distance of the run, in miles
        /// </summary>
        [Required]
        public double DistanceMile { get; set; }

        /// <summary>
        /// The duration of the run (hh:mm:ss)
        /// </summary>
        public TimeSpan Duration { get; set; }

        /// <summary>
        /// The average heart rate from the run
        /// </summary>
        public int AverageHr { get; set; }

        /// <summary>
        /// The maximum heart rate from the run
        /// </summary>
        public int MaxHr { get; set; }

        /// <summary>
        /// The average pace per mile (hh:mm:ss)
        /// </summary>
        public TimeSpan AveragePaceMile { get; set; }

        /// <summary>
        /// Notes about the run
        /// </summary>
        public string Notes { get; set; }
    }
}
