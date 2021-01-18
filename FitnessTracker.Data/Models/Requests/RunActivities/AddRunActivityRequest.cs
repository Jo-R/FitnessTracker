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
        [Required]
        public Guid UserId { get; set; }
        [Required]
        public DateTimeOffset Date { get; set; }
        public string Title { get; set; }
        [Required]
        public double DistanceMile { get; set; }
        public TimeSpan Duration { get; set; }
        public int AverageHr { get; set; }
        public int MaxHr { get; set; }
        public TimeSpan AveragePaceMile { get; set; }
        public string Notes { get; set; }
    }
}
