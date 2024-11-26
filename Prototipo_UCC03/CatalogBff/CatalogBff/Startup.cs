using System.Text;
using CatalogBff.Integration;
using CatalogBff.Integration.Implementation;
using MassTransit;

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

        services.AddMassTransit(x =>
        {
            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host("rabbitmq://localhost", h =>
                {
                    h.Username("guest");
                    h.Password("guest");
                });
            });
        });

        services.AddGrpcClient<GrpcContracts.CatalogService.CatalogServiceClient>(o =>
        {

            o.Address = new Uri(this.configuration["Services:CatalogService:Url"]!);
        });

        services.AddGrpcClient<GrpcContracts.Register.RegisterService.RegisterServiceClient>(o =>
        {
            o.Address = new Uri(this.configuration["Services:RegisterService:Url"]!);
        });

        services.AddGrpcClient<GrpcContracts.Cart.CartServiceGrpc.CartServiceGrpcClient>(o =>
        {
            o.Address = new Uri(this.configuration["Services:OrderService:Url"]!);
        });

        services.AddGrpcClient<GrpcContracts.Order.OrderService.OrderServiceClient>(o =>
        {
            o.Address = new Uri(this.configuration["Services:OrderService:Url"]!);
        });

        services.AddTransient<IProductService, ProductService>();
        services.AddTransient<IRegisterService, RegisterService>();
        services.AddTransient<ICartService, CartService>();
        services.AddTransient<IOrderService, CatalogBff.Integration.Implementation.OrderService>();

        services.AddCors(options =>
        {
            options.AddPolicy("AllowAll", policy =>
            {
                policy.AllowAnyOrigin()  // Permite qualquer origem
                    .AllowAnyHeader()  // Permite qualquer cabeçalho
                    .AllowAnyMethod(); // Permite qualquer método (GET, POST, PUT, DELETE, etc.)
            });
        });
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseCors("AllowAll");
        app.UseDefaultFiles();
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
