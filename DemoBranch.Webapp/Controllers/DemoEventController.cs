using DemoBranch.Webapp.Appliction.Model;
using DemoBranch.Webapp.Domain.Entities;
using DemoBranch.Webapp.Domain.Enums;
using DemoBranch.Webapp.Persistence.DataAksess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoBranch.Webapp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DemoEventController : ControllerBase
    {
        private readonly DemoEventContext demoEventContext;
        private readonly ILogger<DemoEventController> _logger;

        public DemoEventController(DemoEventContext DemoEventContext,ILogger<DemoEventController> logger)
        {
            demoEventContext = DemoEventContext;
            _logger = logger;

            demoEventContext.Database.EnsureCreated();

        }

        [HttpGet("GetAll")]
        public IEnumerable<DemoEvent> GetAll()
        {

            return demoEventContext.DemoEvent.ToList();
        }


      



    }
}
