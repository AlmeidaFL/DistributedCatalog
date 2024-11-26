using Microsoft.AspNetCore.Server.Kestrel.Core;

namespace CatalogService;

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
                webBuilder.UseStartup<Startup>();

                var config = new ConfigurationBuilder()
                    .AddJsonFile("appsettings.json")
                    .Build();

                var url = config["Services:CatalogService:Url"];

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