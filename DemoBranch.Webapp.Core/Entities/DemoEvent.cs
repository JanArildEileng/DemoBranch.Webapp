using DemoBranch.Webapp.Domain.Common;
using System;

namespace DemoBranch.Webapp.Domain.Entities

{
    public class DemoEvent: EntityBase
    {
        public string EventType { get; set; }
        public DateTime DateTime { get; set; }
    }
}
