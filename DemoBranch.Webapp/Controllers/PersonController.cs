using DemoBranch.Webapp.Appliction.Model;
using DemoBranch.Webapp.Appliction.Person.Change;
using DemoBranch.Webapp.Appliction.Person.Create;
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


            foreach(var aggregateId in uniqPersonId)
            {
                var events = demoEventContext.DemoEvent.Where(e => e.AggregateId == aggregateId).OrderBy(e => e.Timestamp);
                Person person = null;
                foreach (var demoevent in events)
                {
                   
                    switch(demoevent.EventType)
                    {
                        case EventTypes.CreatePersonEvent:
                            {
                                person = new Person() { Id = aggregateId };
                                var details = JsonConvert.DeserializeObject<CreatePerson>(demoevent.EventDetails);
                                person.Name = details.Name;
                            }
                            break;

                        case EventTypes.ChangePersonEvent:
                            {
                                var details = JsonConvert.DeserializeObject<ChangePerson>(demoevent.EventDetails);
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


        [HttpPost("CreatePerson")]
        public ActionResult<DemoEvent> CreatePerson([FromBody] CreatePerson createEvent ,[FromServices] CreatePersonHandler createPersonHandler)
        {

            DemoEvent demoEvent = createPersonHandler.CreatePerson(createEvent);
            return Created("", demoEvent);
        }


        [HttpPatch("ChangePerson/{AggregateId:Guid}")]
        public ActionResult<DemoEvent> ChangePerson(Guid AggregateId, [FromBody] ChangePerson ChangeEvent, [FromServices] ChangePersonHandler changePersonHandler)
        {

            DemoEvent demoEvent = changePersonHandler.ChangePerson(ChangeEvent, AggregateId);
            return Created("", demoEvent);
        }
    }
}
