using DemoBranch.Webapp.Core.Entities;
using DemoBranch.Webapp.Infra.DataAksess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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

        [HttpGet]
        public IEnumerable<DemoEvent> Get()
        {

            return demoEventContext.DemoEvent.ToList();
        }


        [HttpPost]
        public ActionResult<DemoEvent> Post([FromBody] DemoEvent demoEvent)
        {
            demoEvent.Id = Guid.NewGuid();
            demoEvent.EventType = "Post";
            demoEvent.DateTime =DateTime.Now;
            demoEventContext.DemoEvent.Add(demoEvent);
            demoEventContext.SaveChanges();

            return Created("",demoEvent);
        }
    }
}
