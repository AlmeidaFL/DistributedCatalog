namespace CatalogBff;

public class Program
{
    private static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
                webBuilder.UseWebRoot("wwwroot/browser");
            });

    public static void Main(string[] args)
    {
        CreateHostBuilder(args).Build().Run();
    }
}
