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
    public class ActivitesController : ControllerBase
    {
        private readonly ILogger<ActivitesController> _logger;
       
        public ActivitesController(ILogger<ActivitesController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<Activity> Get()
        {
            return new List<Activity>()
            {
                new Activity
                {
                    Date = new DateTime(),
                    Type = ActivityType.Running,
                    Comments = "lovely"
                }
            };
        }
    }
}
