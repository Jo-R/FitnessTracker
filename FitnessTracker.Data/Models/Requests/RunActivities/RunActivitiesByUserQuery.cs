using FitnessTracker.Data.Models.Responses;
using FitnessTracker.Data.Models.Responses.RunActivities;
using MediatR;
using System;

namespace FitnessTracker.Data.Models.Requests.RunActivities
{
    public class RunActivitiesByUserQuery: IRequest<RequestResult<RunActivitiesForUserResponse>>
    {
        public Guid UserId { get; set; }

        public int PageNumber { get; set; }

        public int PageSize { get; set; }
    }
}
