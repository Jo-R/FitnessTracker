using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace FitnessTracker.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ActivitesController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<ActivitesController> _logger;

        public ActivitesController(ILogger<ActivitesController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<Activties> Get()
        {
            // some stuff
        }
    }
}
