using DemoBranch.Webapp.Appliction.Contracts;
using DemoBranch.Webapp.Domain.Entities;
using DemoBranch.Webapp.Domain.Enums;
using MediatR;
using Newtonsoft.Json;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DemoBranch.Webapp.Appliction.Person.Commands.Create
{

    public class CreatePersonCommand : IRequest<DemoEvent>
    {
        public CreatePerson CreatePerson { get; set; }
    }


    public class CreatePersonHandler: IRequestHandler<CreatePersonCommand, DemoEvent>
    {
        private readonly IDemoEventRepository demoEventRepository;

        public CreatePersonHandler(IDemoEventRepository demoEventRepository)
        {
            this.demoEventRepository = demoEventRepository;
        }

        public Task<DemoEvent> Handle(CreatePersonCommand request, CancellationToken cancellationToken)
        {
            DemoEvent demoEvent = new DemoEvent(EventTypes.CreatePersonEvent)
            {
                EventDetails = JsonConvert.SerializeObject(request.CreatePerson)
            };
            return Task.FromResult(demoEventRepository.AddEvent(demoEvent));
        }
    }
    
}
