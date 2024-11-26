using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using OrderService.Application.Saga;
using OrderService.Application.Services;
using OrderService.Application.Services.Implementation;
using OrderService.Integration;
using OrderService.Persistence;
using OrderService.Persistence.Repositories;
using OrderService.Persistence.Repositories.Implementation;
using ServerVersion = Microsoft.EntityFrameworkCore.ServerVersion;

namespace OrderService;

public class Startup
{
    private readonly IConfiguration configuration;

    public Startup(IConfiguration configuration)
    {
        this.configuration = configuration;
    }
    
    public void ConfigureServices(IServiceCollection services)
    {
        AddServicesDependecies(services);
        AddMongoDatabase(services);
        
        services.AddDbContext<OrderDbContext>(options =>
            options.UseMySql(
                configuration.GetConnectionString("DefaultConnection"),
                ServerVersion.AutoDetect(configuration.GetConnectionString("DefaultConnection"))
            ));

        
        services.AddMassTransit(configurator =>
        {
            configurator.AddConsumer<PaymentService>();
            configurator.AddConsumer<ShipmentService>();

            configurator.AddSagaStateMachine<OrderSaga, OrderState>()
                .InMemoryRepository();
                // .EntityFrameworkRepository(r =>
                // {
                //     r.ConcurrencyMode = ConcurrencyMode.Optimistic;
                //     r.AddDbContext<DbContext,SagaDbContext>((_, builder) =>
                //     {
                //         builder.UseMySql(
                //             configuration["ConnectionStrings:DefaultConnection"], 
                //             ServerVersion.AutoDetect(configuration["ConnectionStrings:DefaultConnection"]));
                //     });
                // });
            configurator.UsingRabbitMq(
                (context, cfg) =>
                {
                    cfg.Host("localhost", "/", c =>
                    {
                        c.Username("guest");
                        c.Password("guest");
                    });
                    cfg.ReceiveEndpoint("order-state", e =>
                    {
                        e.ConfigureSaga<OrderState>(context);
                    });
                    cfg.UseInMemoryOutbox();
                    cfg.ConfigureEndpoints(context);
                });
        });

        services.AddGrpc();
        services.AddGrpcClient<GrpcContracts.CatalogService.CatalogServiceClient>(o =>
        {
            o.Address = new Uri(configuration["Services:CatalogService:Url"]!);
        });
        services.AddGrpcClient<GrpcContracts.Register.RegisterService.RegisterServiceClient>(o =>
        {
            o.Address = new Uri(configuration["Services:RegisterService:Url"]!);
        });
    }

    private void AddServicesDependecies(IServiceCollection services)
    {
        services.AddTransient<IOrderService, Application.Services.Implementation.OrderService>();
        services.AddTransient<ICartService, Application.Services.Implementation.CartService>();
        services.AddTransient<ICartRepository, CartRepository>();
        services.AddTransient<RegisterServiceClient>();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        // Configure the HTTP request pipeline.
        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapGrpcService<Controllers.CartService>();
            endpoints.MapGrpcService<Controllers.OrderService>();
        });
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
}