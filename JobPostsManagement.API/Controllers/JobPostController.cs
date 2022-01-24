using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using JobPostsManagement.API.Models;

namespace JobPostsManagement.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class JobPostController : ControllerBase
    {

        private readonly ILogger<JobPostController> _logger;

        public JobPostController(ILogger<JobPostController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<JobPost> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new JobPost
            {
            })
            .ToArray();
        }
    }
}
