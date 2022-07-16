using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Guardian.Infrastructure.Extension;
using Guardian.Infrastructure.Email;

namespace Guardian.Worker.Email
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddTransient<IEmailService, EmailService>();
                    services.AddMicroservices(hostContext.Configuration);
                    services.AddMailSetting(hostContext.Configuration);
                    services.AddEventHub(hostContext.Configuration);
                    services.AddHostedService<Worker>();
                });
    }
}
