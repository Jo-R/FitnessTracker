using FitnessTracker.Data.Models.Requests.RunActivities;
using FitnessTracker.Data.Models.Responses;
using FitnessTracker.Data.Models.Responses.RunActivities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FitnessTracker.Data.Handlers.RunActivities
{    
    public class RunActivitiesByUserQueryHandler: IRequestHandler<RunActivitiesByUserQuery, RequestResult<RunActivitiesForUserResponse>>
    {
        private readonly FitnessTrackerContext _ctx;
        private readonly ILogger<RunActivitiesByUserQueryHandler> _logger;

        public RunActivitiesByUserQueryHandler(FitnessTrackerContext ctx, ILogger<RunActivitiesByUserQueryHandler> logger)
        {
            _ctx = ctx;
            _logger = logger;
        }

        public async Task<RequestResult<RunActivitiesForUserResponse>> Handle(RunActivitiesByUserQuery request, CancellationToken cancellationToken)
        {
            try
            {
                IEnumerable<RunActivityResponse> activities = await _ctx.RunActivities.Where(x => x.UserId == request.UserId)
                    .Skip((request.PageNumber - 1) * request.PageSize).Take(request.PageSize)
                    .Select(x => new RunActivityResponse
                    {
                        Id = x.Id,
                        UserId = x.UserId,
                        Date = x.Date,
                        Title = x.Title,
                        DistanceMile = x.DistanceMile,
                        Duration = x.Duration,
                        AverageHr = x.AverageHr,
                        MaxHr = x.MaxHr,
                        AveragePaceMile = x.AveragePaceMile,
                        Notes = x.Notes,

                    })
                    .ToListAsync();

                var result = new RunActivitiesForUserResponse
                {
                    Items = activities,
                    PageNumber = request.PageNumber,
                    PageSize = request.PageSize,
                    ItemCount = activities.Count()
                };

                return RequestResult.Success(result);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error getting run activity", ex);
                return RequestResult.Error<RunActivitiesForUserResponse>(); ;
            }
        }
    }
}

