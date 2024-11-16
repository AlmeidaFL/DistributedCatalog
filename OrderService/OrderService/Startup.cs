using MassTransit;
using MassTransit.MultiBus;
using Microsoft.EntityFrameworkCore;
using OrderService.Application;
using OrderService.Application.Saga;
using OrderService.Persistence;

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
        services.AddDbContext<OrderDbContext>(options =>
            options.UseMySql(
                configuration["ConnectionStrings:DefaultConnection"], 
                ServerVersion.AutoDetect(configuration["ConnectionStrings:DefaultConnection"])));

        services.AddMassTransit(configurator =>
        {
            configurator.AddSagaStateMachine<OrderSaga, OrderState>()
                .EntityFrameworkRepository(r =>
                {
                    r.ConcurrencyMode = ConcurrencyMode.Optimistic;
                    r.AddDbContext<DbContext,SagaDbContext>((_, builder) =>
                    {
                        builder.UseMySql(
                            configuration["ConnectionStrings:DefaultConnection"], 
                            ServerVersion.AutoDetect(configuration["ConnectionStrings:DefaultConnection"]));
                    });
                });
            configurator.UsingRabbitMq(
                (context, cfg) =>
                {
                    cfg.Host(configuration["ConnectionStrings:RabbitMQ"]);
                    cfg.ConfigureEndpoints(context);
                });
            
        });
        
        services.AddGrpcClient<CatalogService.CatalogService.CatalogServiceClient>(o =>
        {
            o.Address = new Uri("https://localhost:5010");
        });
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
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

        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
        
    }
}