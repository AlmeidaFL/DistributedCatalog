using CatalogService.Services;

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
                webBuilder.ConfigureKestrel((context, options) =>
                {
                    var port = int.Parse(context.Configuration["Kestrel:Port"]!);
                    options.ListenLocalhost(port);
                });
                
                webBuilder.UseStartup<Startup>();
            });
}