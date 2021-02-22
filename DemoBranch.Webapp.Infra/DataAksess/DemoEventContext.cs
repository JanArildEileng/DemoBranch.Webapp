using DemoBranch.Webapp.Domain.Entities;
using DemoBranch.Webapp.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoBranch.Webapp.Persistence.DataAksess
{
    public class DemoEventContext:DbContext
    {
        public DemoEventContext(DbContextOptions<DemoEventContext> options) : base(options) { }

        public DbSet<DemoEvent> DemoEvent { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DemoEvent>(
              e => e.HasKey(demoEvent => demoEvent.EventId));



            //modelBuilder.Entity<DemoEvent>().HasData(
            //new List<DemoEvent>()
            // {
            //    new DemoEvent(EventTypes.CreateEvent) {
            //        EventDetails=JsonConvert.SerializeObject(new CreateEvent() { })   },
               
            // }
            // );


        }


    }
}
