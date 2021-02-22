using DemoBranch.Webapp.Appliction.Contracts;
using DemoBranch.Webapp.Domain.Entities;
using DemoBranch.Webapp.Domain.Enums;
using Newtonsoft.Json;

namespace DemoBranch.Webapp.Appliction.Person.Commands.Create
{
    public class CreatePersonHandler
    {
        private readonly IDemoEventRepository demoEventRepository;

        public CreatePersonHandler(IDemoEventRepository demoEventRepository)
        {
            this.demoEventRepository = demoEventRepository;
        }

            public DemoEvent CreatePerson(CreatePerson CreateEvent)
            {

                DemoEvent demoEvent = new DemoEvent(EventTypes.CreatePersonEvent)
                {
                    EventDetails = JsonConvert.SerializeObject(CreateEvent)
                };

            
                return demoEventRepository.AddEvent(demoEvent);
            }

    }
}
