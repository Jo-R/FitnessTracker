using System;

namespace FitnessTracker.Data.Models.Responses.RunActivity
{
    public class RunActivityResponse
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public DateTimeOffset Date { get; set; }
        public string Title { get; set; }
        public double DistanceMile { get; set; }
        public TimeSpan Duration { get; set; }
        public int AverageHr { get; set; }
        public int MaxHr { get; set; }
        public TimeSpan AveragePaceMile { get; set; }
        public string Notes { get; set; }

    }
}
