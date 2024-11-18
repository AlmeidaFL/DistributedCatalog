using CatalogService.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CatalogService;

public class Startup
{
    private readonly IConfiguration configuration;

    public Startup(IConfiguration configuration)
    {
        this.configuration = configuration;
    }
    
    public void ConfigureServices(IServiceCollection services)
    {
        // Add services to the container.
        services.AddGrpc(options =>
        {
            options.MaxReceiveMessageSize = null;  // Limite de 10MB para a mensagem recebida
            options.MaxSendMessageSize = null;  // Limite de 10MB para a mensagem enviada
        });

        var databaseConnection = configuration.GetConnectionString("DefaultConnection");
        services.AddDbContext<CatalogDbContext>(options =>
            options.UseMySql(databaseConnection, 
                new MySqlServerVersion(ServerVersion.AutoDetect(databaseConnection)))
                .LogTo(Console.WriteLine)
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors());
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseRouting();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapGrpcService<Services.CatalogService>();
        });
    }
}