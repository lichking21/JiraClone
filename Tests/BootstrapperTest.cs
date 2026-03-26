using CLI;
using Domain;
using Infrastructure.DbManager;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Tests;

public class BootstrapperTest
{
    [Fact]
    public void ConfigureServices_ResolvesDbContext()
    {
        var settings = new Dictionary<string, string?> {
            {"ConnectionStrings:DefaultConnection", "Host=localhost;Database=jiraclone"}  
        };

        IConfiguration configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(settings)
            .Build();
        
        var services = new ServiceCollection();

        Bootstraper.ConfigureServices(services, configuration);
        var serviceProvider = services.BuildServiceProvider();

        using var scope = serviceProvider.CreateScope();

        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    
        Assert.NotNull(dbContext);
    }
}