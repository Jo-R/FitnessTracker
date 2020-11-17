using System;
using System.Collections.Generic;
using FitnessTracker.Api.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace FitnessTracker.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    // TODO get all activities (paged - maybe a summary?)
    // TODO get activity by id
    // TODO add activity
    // TODO  delete activity
    // TODO put (update) activity
    public class RunActivityController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RunActivityController(IMediator mediator)
        {
            _mediator = mediator;
        }

     
    }
}
