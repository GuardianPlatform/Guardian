using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace Guardian
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder
                        .UseUrls("http://localhost")
                        .UseStartup<Startup>()
                        .UseKestrel(x =>
                        {
                            x.ListenAnyIP(80);
                            x.ListenAnyIP(44356);
                        })
                        .UseIIS();
                });
    }
}
