using DemoBranch.Webapp.Appliction.Contracts;
using DemoBranch.Webapp.Domain.Entities;
using DemoBranch.Webapp.Domain.Enums;
using MediatR;
using Newtonsoft.Json;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DemoBranch.Webapp.Appliction.Person.Commands.Change
{
    public class ChangePersonCommand : IRequest<DemoEvent>
    {
        public Guid AggregateId { get; set; }
        public ChangePerson changePerson { get; set; }
    }


    public class ChangePersonHandler: IRequestHandler<ChangePersonCommand, DemoEvent>
    {
        private readonly IDemoEventRepository demoEventRepository;

        public ChangePersonHandler(IDemoEventRepository demoEventRepository)
        {
            this.demoEventRepository = demoEventRepository;
        }


        public Task<DemoEvent> Handle(ChangePersonCommand request, CancellationToken cancellationToken)
        {

            DemoEvent demoEvent = new DemoEvent(EventTypes.ChangePersonEvent, request.AggregateId)
            {
                EventDetails = JsonConvert.SerializeObject(request.changePerson)
            };
            return  Task.FromResult(demoEventRepository.AddEvent(demoEvent));
        }

    }
}

