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

        services.AddDbContext<CatalogDbContext>(options =>
            options.UseMySql(configuration["ConnectionStrings:DefaultConnection"], 
                new MySqlServerVersion(new Version(8, 0, 21))));
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        
    }
}