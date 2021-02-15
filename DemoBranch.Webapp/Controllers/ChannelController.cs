using DemoBranch.Webapp.Appliction;
using DemoBranch.Webapp.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Channels;

namespace DemoBranch.Webapp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ChannelController : ControllerBase
    {
        private readonly ILogger<ChannelController> _logger;
        private readonly Channel<ChannelData> channel;

        public ChannelController(ILogger<ChannelController> logger, MyChannel1 MyChannel1)
        {
      
            _logger = logger;
            this.channel = MyChannel1.MyChannel;
        }

        [HttpGet]
        public ActionResult Get()
        {
            var liste = new List<ChannelData>();

            //get all from queue
          while(  channel.Reader.TryRead(out ChannelData channelData))
          {
              liste.Add(channelData);
          }

            return Ok(liste);
        }


        [HttpPost]
        public ActionResult Post([FromBody] ChannelData channelData)
        {
            channelData.Id = Guid.NewGuid();

            //post to queue
            if (channel.Writer.TryWrite(channelData))
            {
                return Created("", channelData);
            }
            else
                return BadRequest();
        }
    }
}
