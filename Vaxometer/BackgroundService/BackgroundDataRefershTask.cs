using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Vaxometer.Repository;
using Vaxometer.Manager;

namespace Vaxometer.BackgroundService
{
    public class BackgroundDataRefershTask : Microsoft.Extensions.Hosting.BackgroundService
    {
        public IServiceProvider Services { get; }
        private Timer _timer;
        private readonly ILogger<BackgroundDataRefershTask> _logger;
        

        public BackgroundDataRefershTask(IServiceProvider services, ILogger<BackgroundDataRefershTask> logger)
        {
             Services = services;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation(
                "Consume Scoped Service Hosted Service running.");

            //_timer = new Timer(DoWork, null, TimeSpan.Zero,
            //        TimeSpan.FromMinutes(5));
            await DoWork(stoppingToken);
        }

        private async Task DoWork(CancellationToken stoppingToken)
        {
            _logger.LogInformation(
                "Consume Scoped Service Hosted Service is working.");

            try
            {
                using (var scope = Services.CreateScope())
                {
                    var scopedProcessingService =
                        scope.ServiceProvider
                            .GetRequiredService<IVexoManager>();
                    var cowinResponse = await scopedProcessingService.RefershData();
                }
                await Task.Delay(300000, stoppingToken); //5mins
            }
            catch (Exception) { }
        }

        public override async Task StopAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation(
                "Consume Scoped Service Hosted Service is stopping.");

            await base.StopAsync(stoppingToken);
        }
    }
}
