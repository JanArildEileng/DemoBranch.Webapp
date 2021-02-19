using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace DemoBranch.Services.RestService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestRestController : ControllerBase
    {

        private readonly ILogger<TestRestController> _logger;

        public TestRestController(ILogger<TestRestController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Get()
        {

            var o = new
            {
                Nameof = nameof(TestRestController),
                FullName = this.GetType().FullName,
                AssemlyFullName = Assembly.GetExecutingAssembly().GetName().FullName
            };


            return Ok(o);
        }
    }
}
