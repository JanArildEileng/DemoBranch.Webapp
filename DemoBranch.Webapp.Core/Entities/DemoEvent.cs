using DemoBranch.Webapp.Domain.Common;
using DemoBranch.Webapp.Domain.Enums;
using System;

namespace DemoBranch.Webapp.Domain.Entities

{
    public class DemoEvent: EntityBase
    {

        private DemoEvent()
        {
        }

        public DemoEvent(EventTypes eventType, Guid? AggregateId=null)
        {
            this.EventType = eventType;
            if (AggregateId.HasValue)
                this.AggregateId = AggregateId.Value;
            else
                this.AggregateId = Guid.NewGuid();

        }

        public EventTypes EventType { get; private set; }
        public Guid AggregateId { get; private set; }

        public string EventDetails { get; set; }
    }
}
