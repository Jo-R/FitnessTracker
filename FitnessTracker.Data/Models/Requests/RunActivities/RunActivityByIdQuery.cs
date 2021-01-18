using FitnessTracker.Data.Models.Responses;
using FitnessTracker.Data.Models.Responses.RunActivities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace FitnessTracker.Data.Models.Requests.RunActivities
{
    public class RunActivityByIdQuery: IRequest<RequestResult<RunActivityResponse>>
    {
        public int Id { get; set; }
    }
}
