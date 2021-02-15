using DemoBranch.Webapp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace DemoBranch.Webapp.Appliction
{
    public class MyChannel1 : Channel<ChannelData>
    {
        public Channel<ChannelData> MyChannel { get; }

        public MyChannel1()
        {
            MyChannel = Channel.CreateUnbounded<ChannelData>();
        }
    }
}
