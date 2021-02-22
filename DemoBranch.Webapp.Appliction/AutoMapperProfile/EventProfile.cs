using AutoMapper;
using DemoBranch.Webapp.Appliction.Model;
using DemoBranch.Webapp.Domain.Entities;

namespace DemoBranch.Webapp.Appliction.AutoMapperProfile
{
    public class EventProfile : Profile
    {
        public EventProfile()
        {
            CreateMap<DemoEvent, DemoEventDto>();


        }
    }
}

