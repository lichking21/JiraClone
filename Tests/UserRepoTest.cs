using Domain;
using Infrastructure.DbManager;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;

namespace Tests;

public class UserRepoTest
{

    [Fact]
    public void Execute_UserCRUD_Successfully()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) 
            .Options;

        using var context = new ApplicationDbContext(options);

        var log = NullLogger<UserRepository>.Instance;
        var repo = new UserRepository(context, log);

        var mockUser = new User(52, "Petushara");

        repo.Add(mockUser);
        var getUser = repo.Get(mockUser.ID);
        
        Assert.NotNull(getUser);
        Assert.Equal(mockUser.ID, getUser.ID);

        repo.Remove(mockUser.ID);
        var removedUser = repo.Get(mockUser.ID);
        Assert.Null(removedUser);
    }
}