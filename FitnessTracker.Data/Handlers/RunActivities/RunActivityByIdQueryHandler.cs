using FitnessTracker.Data.Models.Requests.RunActivities;
using FitnessTracker.Data.Models.Responses;
using FitnessTracker.Data.Models.Responses.RunActivities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FitnessTracker.Data.Handlers.RunActivities
{
    public class RunActivityByIdQueryHandler : IRequestHandler<RunActivityByIdQuery, RequestResult<RunActivityResponse>>
    {
        private readonly FitnessTrackerContext _ctx;
        private readonly ILogger<RunActivityByIdQueryHandler> _logger;

        public RunActivityByIdQueryHandler(FitnessTrackerContext ctx, ILogger<RunActivityByIdQueryHandler> logger)
        {
            _ctx = ctx;
            _logger = logger;
        }

        public async Task<RequestResult<RunActivityResponse>> Handle(RunActivityByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var activity = await _ctx.RunActivities.FirstOrDefaultAsync(x => x.Id == request.Id);
                if (activity == null)
                {
                    return RequestResult.Error<RunActivityResponse>();
                }
                var response = new RunActivityResponse
                {
                    Id = activity.Id,
                    UserId = activity.UserId,
                    Date = activity.Date,
                    Title = activity.Title,
                    DistanceMile = activity.DistanceMile,
                    Duration = activity.Duration,
                    AverageHr = activity.AverageHr,
                    MaxHr = activity.MaxHr,
                    AveragePaceMile = activity.AveragePaceMile,
                    Notes = activity.Notes,

                };
                return RequestResult.Success(response);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error getting run activity", ex);
                return RequestResult.Error<RunActivityResponse>(); ;
            }
        }
    }
}
