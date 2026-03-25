using CLI;
using Domain;
using Infrastructure.DbManager;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Tests;

public class BootstrapperTest
{
    [Fact]
    public void Execute_Bootstrapper_Successfully()
    {
        var settings = new Dictionary<string, string?> {
            {"ConnectionString:DefaultConnection", "Host:localhost;Database:jiraclone"}  
        };

        IConfiguration configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(settings)
            .Build();
        
        var services = new ServiceCollection();

        Bootstraper.ConfigureServices(services, configuration);
        var serviceProvider = services.BuildServiceProvider();

        using var scope = serviceProvider.CreateScope();
        var provider = scope.ServiceProvider;

        var dbContext = provider.GetRequiredService<ApplicationDbContext>();
    
        Assert.NotNull(dbContext);
    }
}