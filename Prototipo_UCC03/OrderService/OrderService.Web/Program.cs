using MassTransit;
using Microsoft.EntityFrameworkCore;
using OrderService.Application;
using OrderService.Persistence;

namespace OrderService;

public class Program
{
    public static void Main(string[] args)
    {
        CreateHostBuilder(args).Build().Run();
    }
    
    private static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();

                var config = new ConfigurationBuilder()
                    .AddJsonFile("appsettings.json")
                    .Build();

                var url = config["Services:OrderService:Url"];

                if (!string.IsNullOrEmpty(url))
                {
                    var addressParts = url.Split(':');
                    var host = addressParts[0];
                    var port = int.Parse(addressParts[1]);

                    webBuilder.UseKestrel(options =>
                    {
                        options.Listen(System.Net.IPAddress.Parse(host), port);
                    });
                }
            });
}