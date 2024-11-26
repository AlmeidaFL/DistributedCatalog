using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using RegisterService.Application.Services;
using RegisterService.Persistence;

namespace RegisterService;

public class Startup(IConfiguration configuration)
{
    private readonly IConfiguration configuration = configuration;

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddGrpc();
        
        var databaseConnection = configuration.GetConnectionString("DefaultConnection");
        
        services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));
        services.AddDbContext<UserDbContext>(options =>
            options.UseMySql(databaseConnection, 
                new MySqlServerVersion(ServerVersion.AutoDetect(databaseConnection)))
                .LogTo(Console.WriteLine)
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors());
        
        // services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        //     .AddJwtBearer();
        
        services.AddTransient<IUserRepository, UserRepository>();
        services.AddTransient<IUserService, UserService>();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseRouting();
        // app.UseAuthentication();
        // app.UseAuthorization();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapGrpcService<Web.RegisterController>();
        });
    }
}