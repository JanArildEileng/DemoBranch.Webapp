using DemoBranch.Webapp.Appliction.Contracts;
using DemoBranch.Webapp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoBranch.Webapp.Persistence.DataAksess.Repositories
{
    public class DemoEventRepository : IDemoEventRepository
    {
        private readonly DemoEventContext demoEventContext;

        public DemoEventRepository(DemoEventContext DemoEventContext)
        {
            demoEventContext = DemoEventContext;
        }
        public DemoEvent AddEvent(DemoEvent demoEvent)
        {
            demoEventContext.DemoEvent.Add(demoEvent);
            demoEventContext.SaveChanges();
            return demoEvent;
        }
    }
}
