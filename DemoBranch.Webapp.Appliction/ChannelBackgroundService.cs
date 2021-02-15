using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;
using DemoBranch.Webapp.Domain.Entities;
using System.Threading.Channels;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;

namespace DemoBranch.Webapp.Appliction
{
    public class ChannelBackgroundService : BackgroundService
    {
        private readonly IServiceProvider services;
        private readonly ILogger<ChannelBackgroundService> logger;

        public ChannelBackgroundService(IServiceProvider services,ILogger<ChannelBackgroundService> logger)
        {
            this.services = services;
            this.logger = logger;
        }
        protected override async  Task ExecuteAsync(CancellationToken stoppingToken)
        {
            logger.LogInformation("ChannelBackgroundService running.");
            await DoWork(stoppingToken);
        }

        private async Task DoWork(CancellationToken stoppingToken)
        {
            logger.LogInformation("ChannelBackgroundService is working.");

            using (var scope = services.CreateScope())
            {
                //Channel<ChannelData> channel=scope.ServiceProvider
                //   .GetRequiredService<Channel<ChannelData>>();
                MyChannel1 MyChannel1 = scope.ServiceProvider
                  .GetRequiredService<MyChannel1>();
                Channel<ChannelData> channel = MyChannel1.MyChannel;

                for (int i = 0; i < 10; i++)
                {
                    await Task.Delay(2000);

                    var channelData = new ChannelData()
                    {
                        Id = Guid.NewGuid(),
                        Name = $"Background {i}"
                    };

                    if (channel.Writer.TryWrite(channelData))
                        logger.LogInformation($"channelData  {channelData.Name}");
                    else
                        logger.LogInformation("no channelData");
                }
            }
        }

          
    }
}
