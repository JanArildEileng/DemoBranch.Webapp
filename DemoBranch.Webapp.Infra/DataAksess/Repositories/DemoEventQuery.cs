using DemoBranch.Webapp.Appliction.Contracts;
using DemoBranch.Webapp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoBranch.Webapp.Persistence.DataAksess.Repositories
{
    public class DemoEventQuery : IDemoEventQuery
    {
        private readonly DemoEventContext demoEventContext;

        public DemoEventQuery(DemoEventContext DemoEventContext)
        {
            demoEventContext = DemoEventContext;
        }

        public List<DemoEvent> GetEvents(Guid aggegateId)
        {
            return demoEventContext.DemoEvent.Where(e => e.AggregateId == aggegateId).OrderBy(e => e.Timestamp).ToList();
        }

        public IQueryable<Guid> GetDistinctAggegateId()
        {
            return  demoEventContext.DemoEvent.Select(e => e.AggregateId).Distinct();
        }

       
    }
}
