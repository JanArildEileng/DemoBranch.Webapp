using DemoBranch.Webapp.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoBranch.Webapp.Infra.DataAksess
{
    public class DemoEventContext:DbContext
    {
        public DemoEventContext(DbContextOptions<DemoEventContext> options) : base(options) { }

        public DbSet<DemoEvent> DemoEvent { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DemoEvent>(
              e => e.HasKey(demoEvent => demoEvent.Id));

            modelBuilder.Entity<DemoEvent>().HasData(
            new List<DemoEvent>()
             {
                new DemoEvent() {Id=Guid.NewGuid(), EventType="testEvent",DateTime=DateTime.Now},
                new DemoEvent() {Id=Guid.NewGuid(), EventType="testEvent2",DateTime=DateTime.Now}
             }
             );
           
         
        }


    }
}
