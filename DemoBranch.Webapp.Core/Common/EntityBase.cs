using System;

namespace DemoBranch.Webapp.Domain.Common
{
    public class EntityBase
    {

        public Guid EventId { get; private set; } = Guid.NewGuid();
        public DateTime Timestamp { get; private set; } = DateTime.Now;

     
    }
}
