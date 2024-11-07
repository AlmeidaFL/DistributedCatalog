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
        services.AddGrpc();

        var databaseConnection = configuration.GetConnectionString("Catalog");
        services.AddDbContext<CatalogDbContext>(options =>
            options.UseMySql(databaseConnection, 
                new MySqlServerVersion(ServerVersion.AutoDetect(databaseConnection))));
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        
    }
}