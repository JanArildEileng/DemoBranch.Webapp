using AutoMapper;
using DemoBranch.Webapp.Appliction.Model;
using DemoBranch.Webapp.Appliction.Person.Commands.Change;
using DemoBranch.Webapp.Appliction.Person.Commands.Create;
using DemoBranch.Webapp.Appliction.Person.Queries;
using DemoBranch.Webapp.Domain.Entities;
using DemoBranch.Webapp.Persistence.DataAksess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace DemoBranch.Webapp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PersonController : ControllerBase
    {
        private readonly DemoEventContext demoEventContext;
        private readonly ILogger<PersonController> _logger;
        private readonly IMapper mapper;

        public PersonController(DemoEventContext DemoEventContext,ILogger<PersonController> logger, IMapper mapper)
        {
            demoEventContext = DemoEventContext;
            _logger = logger;
            this.mapper = mapper;
            demoEventContext.Database.EnsureCreated();

        }

        [HttpGet("GetAllPerson")]
        public IEnumerable<PersonInfo> GetAllPerson([FromServices] GetAllPersonHandler getAllPersonHandler)
        {
            var personListe = getAllPersonHandler.GetAll();

            return personListe;
        }


        [HttpPost("CreatePerson")]
        public ActionResult<DemoEventDto> CreatePerson([FromBody] CreatePerson createEvent ,[FromServices] CreatePersonHandler createPersonHandler)
        {




            DemoEventDto demoEventDto = mapper.Map<DemoEventDto>(createPersonHandler.CreatePerson(createEvent));
            return Created("", demoEventDto);
        }


        [HttpPatch("ChangePerson/{AggregateId:Guid}")]
        public ActionResult<DemoEventDto> ChangePerson(Guid AggregateId, [FromBody] ChangePerson ChangeEvent, [FromServices] ChangePersonHandler changePersonHandler)
        {
            DemoEventDto demoEventDto = mapper.Map<DemoEventDto>(changePersonHandler.ChangePerson(ChangeEvent, AggregateId));
            return Accepted("", demoEventDto);
        }
    }
}
