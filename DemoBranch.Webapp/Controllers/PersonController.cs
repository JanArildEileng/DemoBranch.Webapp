using AutoMapper;
using DemoBranch.Webapp.Appliction.Model;
using DemoBranch.Webapp.Appliction.Person.Commands.Change;
using DemoBranch.Webapp.Appliction.Person.Commands.Create;
using DemoBranch.Webapp.Appliction.Person.Queries;
using DemoBranch.Webapp.Domain.Entities;
using DemoBranch.Webapp.Persistence.DataAksess;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DemoBranch.Webapp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PersonController : ControllerBase
    {
        private readonly DemoEventContext demoEventContext;
        private readonly ILogger<PersonController> _logger;
        private readonly IMapper mapper;
        private readonly IMediator mediator;

        public PersonController(DemoEventContext DemoEventContext,ILogger<PersonController> logger, IMapper mapper, IMediator mediator)
        {
            demoEventContext = DemoEventContext;
            _logger = logger;
            this.mapper = mapper;
            this.mediator = mediator;
            demoEventContext.Database.EnsureCreated();

        }

        [HttpGet("GetAllPerson")]
        public IEnumerable<PersonInfo> GetAllPerson([FromServices] GetAllPersonHandler getAllPersonHandler)
        {
            var personListe = getAllPersonHandler.GetAll();

            return personListe;
        }


        [HttpPost("CreatePerson")]
        public ActionResult<DemoEventDto> CreatePerson([FromBody] CreatePerson createPerson, [FromServices] CreatePersonHandler createPersonHandler)
        {

            var task = mediator.Send(new CreatePersonCommand() { CreatePerson = createPerson });
             return Created("", mapper.Map<DemoEventDto>(task.Result));
        }


        [HttpPatch("ChangePerson/{AggregateId:Guid}")]
        public ActionResult<DemoEventDto> ChangePerson(Guid AggregateId, [FromBody] ChangePerson changePerson, [FromServices] ChangePersonHandler changePersonHandler)
        {
            var task=  mediator.Send(new ChangePersonCommand() { AggregateId = AggregateId, changePerson = changePerson });
            return Accepted("", mapper.Map<DemoEventDto>(task.Result));
        }

        [HttpDelete("DeletePerson/{AggregateId:Guid}")]
        public ActionResult<DemoEventDto> DeletePerson(Guid AggregateId, [FromBody] ChangePerson changePerson, [FromServices] ChangePersonHandler changePersonHandler)
        {
            var task = mediator.Send(new ChangePersonCommand() { AggregateId = AggregateId, changePerson = changePerson });
            return Accepted("", mapper.Map<DemoEventDto>(task.Result));
        }
    }
}
