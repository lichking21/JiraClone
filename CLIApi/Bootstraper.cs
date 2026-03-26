using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Infrastructure.DbManager;
using Microsoft.EntityFrameworkCore;
using Domain;
using Application;
using Infrastructure;

namespace CLI;

public static class Bootstraper
{
    public static IHost BuildApp()
    {
        var builder = Host.CreateApplicationBuilder();

        builder.Configuration
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddEnvironmentVariables();

        ConfigureServices(builder.Services, builder.Configuration);

        return builder.Build();
    }

    public static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(
            options => options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"))
        );

        services.AddScoped<ITaskRepository, TaskRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
    }
}