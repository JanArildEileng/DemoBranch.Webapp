using DemoBranch.Webapp.Appliction.Contracts;
using DemoBranch.Webapp.Appliction.Model;
using DemoBranch.Webapp.Domain.Entities;
using DemoBranch.Webapp.Domain.Enums;
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
        private readonly IDemoEventRepository demoEventRepository;

        public ChangePersonHandler(IDemoEventRepository demoEventRepository)
        {
            this.demoEventRepository = demoEventRepository;
        }

        public DemoEvent ChangePerson(ChangePerson changePerson, Guid AggregateId)
        {

            DemoEvent demoEvent = new DemoEvent(EventTypes.ChangePersonEvent, AggregateId)
            {
                EventDetails = JsonConvert.SerializeObject(changePerson)
            };
            return demoEventRepository.AddEvent(demoEvent);
        }

    }
}

