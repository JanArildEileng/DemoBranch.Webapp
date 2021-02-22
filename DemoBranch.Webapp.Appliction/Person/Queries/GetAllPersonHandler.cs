using DemoBranch.Webapp.Appliction.Contracts;
using DemoBranch.Webapp.Appliction.Person.Commands.Change;
using DemoBranch.Webapp.Appliction.Person.Commands.Create;
using DemoBranch.Webapp.Domain.Enums;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace DemoBranch.Webapp.Appliction.Person.Queries
{
    public class GetAllPersonHandler
    {
        private readonly IDemoEventQuery demoEventQuery;
        private readonly ILogger<GetAllPersonHandler> _logger;

        public GetAllPersonHandler(IDemoEventQuery demoEventQuery, ILogger<GetAllPersonHandler> logger)
        {
            this.demoEventQuery = demoEventQuery;
            this._logger = logger;
        }

        public List<PersonInfo> GetAll()
        {
            var uniqPersonId = demoEventQuery.GetDistinctAggegateId();
            var personListe = new List<PersonInfo>();


            foreach (var aggregateId in uniqPersonId)
            {
                var events = demoEventQuery.GetEvents(aggregateId);
                PersonInfo person = null;
                foreach (var demoevent in events)
                {

                    switch (demoevent.EventType)
                    {
                        case EventTypes.CreatePersonEvent:
                            {
                                person = new PersonInfo() { Id = aggregateId };
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
    }
    
}
