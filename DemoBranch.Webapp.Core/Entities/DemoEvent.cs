using DemoBranch.Webapp.Core.Common;
using System;

namespace DemoBranch.Webapp.Core.Entities

{
    public class DemoEvent: EntityBase
    {
        public string EventType { get; set; }
        public DateTime DateTime { get; set; }
    }
}
