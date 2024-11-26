namespace CatalogBff;

public class Program
{
    private static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
                webBuilder.UseWebRoot("wwwroot/browser");

                var config = new ConfigurationBuilder()
                    .AddJsonFile("appsettings.json")
                    .Build();

                var url = config["Services:CatalogBff:Url"];

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

    public static void Main(string[] args)
    {
        CreateHostBuilder(args).Build().Run();
    }
}
