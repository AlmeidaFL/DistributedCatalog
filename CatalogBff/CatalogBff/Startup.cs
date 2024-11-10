using CatalogBff.Integration;
using CatalogBff.Integration.Implementation;

namespace CatalogBff;

public class Startup
{
    private readonly IConfiguration configuration;

    public Startup(IConfiguration configuration)
    {
        this.configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddGrpcClient<CatalogService.CatalogService.CatalogServiceClient>(o =>
        {
            o.Address = new Uri(this.configuration.GetValue<string>("CatalogService:Url")!);
        });

        services.AddTransient<IProductService, ProductService>();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseCors();
        app.UseStaticFiles();
        
        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseEndpoints(config =>
        {
            config.MapFallbackToFile("index.html");
            config.MapControllers();
        });
    }
}