using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace DepositProfit
{
    public class Worker : BackgroundService
    {
        private HttpClient client;
        //private readonly ILogger<Worker> _logger;

        //public Worker(ILogger<Worker> logger)
        //{
        //    _logger = logger;
        //}

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            client = new HttpClient();
            return base.StartAsync(cancellationToken);
        }
        public override Task StopAsync(CancellationToken cancellationToken)
        {
            client.Dispose();
            return base.StopAsync(cancellationToken);
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var result = await client.PostAsync("http://localhost:5000/api/Profit", null);

                if (result.IsSuccessStatusCode)
                    Console.WriteLine("ok");
                    //_logger.LogInformation("web site is up . status code : {code}", result.StatusCode);
                else
                    Console.WriteLine("! ok");
                    //_logger.LogInformation("web site is down . status code : {code}", result.StatusCode);

                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
