using FitnessTracker.Data.Models.Requests.RunActivities;
using FitnessTracker.Data.Models.Responses;
using FitnessTracker.Data.Models;
using FitnessTracker.Data.Models.Responses.RunActivities;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using AutoMapper;

namespace FitnessTracker.Data.Handlers.RunActivities
{
    public class AddRunActivityHandler : IRequestHandler<AddRunActivityRequest, RequestResult<RunActivityResponse>>
    {
        private readonly FitnessTrackerContext _ctx;
        private readonly ILogger<AddRunActivityHandler> _logger;
        private readonly IMapper _mapper;

        public AddRunActivityHandler(
            FitnessTrackerContext ctx, 
            ILogger<AddRunActivityHandler> logger,
            IMapper mapper
         )
        {
            _ctx = ctx;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task<RequestResult<RunActivityResponse>> Handle(AddRunActivityRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var activity = _mapper.Map<RunActivity>(request);
                await _ctx.RunActivities.AddAsync(activity);
                await _ctx.SaveChangesAsync();

                var createdActivity = await _ctx.RunActivities.FindAsync(activity.Id);

                if (createdActivity == null)
                {
                    _logger.LogError("activity not found after creation");
                    return RequestResult.Error<RunActivityResponse>();
                }

                var response = _mapper.Map<RunActivityResponse>(createdActivity);

                return RequestResult.Success(response);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error creating run activity", ex);
                return RequestResult.Error<RunActivityResponse>();
            }
        }
    }
}
