using FitnessTracker.Data.Models.Responses;
using FitnessTracker.Data.Models.Responses.RunActivity;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace FitnessTracker.Data.Models.Requests.RunActivity
{
    public class RunActivityByIdQuery: IRequest<RequestResult<RunActivityResponse>>
    {
        public int Id { get; set; }
    }
}
