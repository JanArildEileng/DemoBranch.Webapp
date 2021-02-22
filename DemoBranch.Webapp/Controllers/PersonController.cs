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
    public class PersonController : ControllerBase
    {
        private readonly DemoEventContext demoEventContext;
        private readonly ILogger<PersonController> _logger;

        public PersonController(DemoEventContext DemoEventContext,ILogger<PersonController> logger)
        {
            demoEventContext = DemoEventContext;
            _logger = logger;

            demoEventContext.Database.EnsureCreated();

        }

        [HttpGet("GetAll")]
        public IEnumerable<Person> GetAll()
        {
            var uniqPersonId = demoEventContext.DemoEvent.Select(e => e.AggregateId).Distinct();
            var personListe = new List<Person>();


            foreach(var id in uniqPersonId)
            {
                var events = demoEventContext.DemoEvent.Where(e => e.AggregateId == id).OrderBy(e => e.Timestamp);
                Person person = null;
                foreach (var demoevent in events)
                {
                   
                    switch(demoevent.EventType)
                    {
                        case EventTypes.CreateEvent:
                            {
                                person = new Person();
                                var details = JsonConvert.DeserializeObject<CreateEvent>(demoevent.EventDetails);
                                person.Name = details.Name;
                            }
                            break;

                        case EventTypes.ChangeEvent:
                            {
                                var details = JsonConvert.DeserializeObject<ChangeEvent>(demoevent.EventDetails);
                                person.Name = details.Name;

                            }
                            break;

                      default:
                            _logger.LogInformation($"Ukjent eventtype {demoevent.EventType}");
                            break;

                    }

                }
                personListe.Add(person);
        }


            return personListe;
        }


        [HttpPost("CreateEvent")]
        public ActionResult<DemoEvent> PostCreateEvent([FromBody] CreateEvent CreateEvent)
        {

            DemoEvent demoEvent = new DemoEvent(EventTypes.CreateEvent)
            {
                EventDetails = JsonConvert.SerializeObject(CreateEvent)
            };

            demoEventContext.DemoEvent.Add(demoEvent);
            demoEventContext.SaveChanges();
            return Created("", demoEvent);
        }


        [HttpPatch("ChangeEvent/{AggregateId:Guid}")]
        public ActionResult<DemoEvent> PatchChangeEvent(Guid AggregateId, [FromBody] ChangeEvent ChangeEvent)
        {

            DemoEvent demoEvent = new DemoEvent(EventTypes.ChangeEvent, AggregateId)
            {
                EventDetails = JsonConvert.SerializeObject(ChangeEvent)
            };

            demoEventContext.DemoEvent.Add(demoEvent);
            demoEventContext.SaveChanges();
            return Created("", demoEvent);
        }
    }
}
