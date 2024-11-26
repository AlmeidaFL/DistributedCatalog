using CatalogService.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using ServerVersion = Microsoft.EntityFrameworkCore.ServerVersion;

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

        services.AddTransient<IProductRepository, ProductRepository>();
        
        AddMongoDatabase(services);

        // var databaseConnection = configuration.GetConnectionString("DefaultConnection");
        // services.AddDbContext<CatalogDbContext>(options =>
        //     options.UseMySql(databaseConnection, 
        //         new MySqlServerVersion(ServerVersion.AutoDetect(databaseConnection)))
        //         .LogTo(Console.WriteLine)
        //         .EnableSensitiveDataLogging()
        //         .EnableDetailedErrors());
    }

    private void AddMongoDatabase(IServiceCollection services)
    {
        services.Configure<MongoDbSettings>(configuration.GetSection("MongoDbSettings"));
        services.AddSingleton<IMongoClient>(sp =>
        {
            var settings = sp.GetRequiredService<IOptions<MongoDbSettings>>();
            return new MongoClient(settings.Value.ConnectionString);
        });
        services.AddScoped(sp =>
        {
            var settings = sp.GetRequiredService<IOptions<MongoDbSettings>>().Value;
            var client = sp.GetRequiredService<IMongoClient>();
            return client.GetDatabase(settings.DatabaseName);
        });
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