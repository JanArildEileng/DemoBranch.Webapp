using DemoBranch.Webapp.Appliction.Model;
using DemoBranch.Webapp.Domain.Entities;
using DemoBranch.Webapp.Domain.Enums;
using DemoBranch.Webapp.Persistence.DataAksess;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoBranch.Webapp.Appliction.Person.Change
{
    public class ChangePersonHandler
    {
        private readonly DemoEventContext demoEventContext;

        public ChangePersonHandler(DemoEventContext DemoEventContext)
        {
            demoEventContext = DemoEventContext;
        }

        public DemoEvent ChangePerson(ChangePerson changePerson, Guid AggregateId)
        {

            DemoEvent demoEvent = new DemoEvent(EventTypes.ChangePersonEvent, AggregateId)
            {
                EventDetails = JsonConvert.SerializeObject(changePerson)
            };

            demoEventContext.DemoEvent.Add(demoEvent);
            demoEventContext.SaveChanges();
            return demoEvent;
        }

    }
}

