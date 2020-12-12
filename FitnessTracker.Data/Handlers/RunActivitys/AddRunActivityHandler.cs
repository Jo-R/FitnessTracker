using FitnessTracker.Data.Models.Requests.RunActivity;
using FitnessTracker.Data.Models.Responses;
using FitnessTracker.Data.Models;
using FitnessTracker.Data.Models.Responses.RunActivity;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace FitnessTracker.Data.Handlers.RunActivitys
{
    public class AddRunActivityHandler : IRequestHandler<AddRunActivityRequest, RequestResult<RunActivityResponse>>
    {
        private readonly FitnessTrackerContext _ctx;
        private readonly ILogger<AddRunActivityHandler> _logger;

        public AddRunActivityHandler(FitnessTrackerContext ctx, ILogger<AddRunActivityHandler> logger)
        {
            _ctx = ctx;
            _logger = logger;
        }
        public Task<RequestResult<RunActivityResponse>> Handle(AddRunActivityRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var activity = new RunActivity
                {
                   UserId = request.UserId,
                   Date = request.Date,
                   Title = request.Title,
                   DistanceMile = request.DistanceMile,
                   Duration = request.Duration,
                   AverageHr = request.AverageHr,
                   MaxHr = request.MaxHr,
                   AveragePaceMile = request.AveragePaceMile,
                   Notes = request.Notes
                };
                _ctx.RunActivities.Add(activity);
                _ctx.SaveChanges();

                var createdActivity = _ctx.RunActivities.Find(activity.Id);

                if (createdActivity == null)
                {
                    _logger.LogError("activity not found after creation");
                    return Task.FromResult(RequestResult.Error<RunActivityResponse>());
                }

                var response = new RunActivityResponse
                {
                    Id = createdActivity.Id,
                    UserId = createdActivity.UserId,
                    Date = createdActivity.Date,
                    Title = createdActivity.Title,
                    DistanceMile = createdActivity.DistanceMile,
                    Duration = createdActivity.Duration,
                    AverageHr = createdActivity.AverageHr,
                    MaxHr = createdActivity.MaxHr,
                    AveragePaceMile = createdActivity.AveragePaceMile,
                    Notes = createdActivity.Notes
                };

                return Task.FromResult(RequestResult.Success(response));
            }
            catch (Exception ex)
            {
                _logger.LogError("Error creating run activity", ex);
                return Task.FromResult(RequestResult.Error<RunActivityResponse>());
            }
        }
    }
}
