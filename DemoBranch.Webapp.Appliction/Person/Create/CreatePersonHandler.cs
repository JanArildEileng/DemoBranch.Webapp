using DemoBranch.Webapp.Appliction.Model;
using DemoBranch.Webapp.Domain.Entities;
using DemoBranch.Webapp.Domain.Enums;
using DemoBranch.Webapp.Persistence.DataAksess;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace DemoBranch.Webapp.Appliction.Person.Create
{
    public class CreatePersonHandler
    {
        private readonly DemoEventContext demoEventContext;

        public CreatePersonHandler(DemoEventContext DemoEventContext)
        {
            demoEventContext = DemoEventContext;
        }

            public DemoEvent CreatePerson(CreatePerson createPerson)
            {

                DemoEvent demoEvent = new DemoEvent(EventTypes.CreatePersonEvent)
                {
                    EventDetails = JsonConvert.SerializeObject(createPerson)
                };

                demoEventContext.DemoEvent.Add(demoEvent);
                demoEventContext.SaveChanges();
                return demoEvent;
            }

    }
}
