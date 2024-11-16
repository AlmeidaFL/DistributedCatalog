using System.Text;
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
        app.Use(async (context, next) =>
        {
            // Log da requisição
            Console.WriteLine($"Request: {context.Request.Method} {context.Request.Path}");

            // Log do corpo da requisição (se necessário)
            if (context.Request.ContentLength > 0)
            {
                context.Request.EnableBuffering(); // Necessário para ler o corpo da requisição mais de uma vez
                using (var reader = new StreamReader(context.Request.Body, Encoding.UTF8, leaveOpen: true))
                {
                    var body = await reader.ReadToEndAsync();
                    Console.WriteLine($"Request Body: {body}");
                    context.Request.Body.Position = 0; // Reinicia a posição para que outros middlewares possam ler o corpo
                }
            }

            await next.Invoke();

            // Log da resposta
            Console.WriteLine($"Response: {context.Response.StatusCode}");
        });

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