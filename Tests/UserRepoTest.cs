using Domain;
using Infrastructure.DbManager;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;

namespace Tests;

public class UserRepoTest
{

    [Fact]
    public void Add_NewUser_AddsToDb()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) 
            .Options;

        var user = new User(52, "Eblusha");
        var log = NullLogger<UserRepository>.Instance;

        // Act
        using (var context = new ApplicationDbContext(options))
        {
            var repo = new UserRepository(context, log);
            repo.Add(user);
        }

        // Assert
        using (var context = new ApplicationDbContext(options))
        {
            var repo = new UserRepository(context, log);
            var getUser = repo.Get(user.ID);

            Assert.NotNull(getUser);
        }
    }

    [Fact]
    public void Remove_ExistingUser_DeletesFromDb()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) 
            .Options;

        var user = new User(1703, "Opustit' petucha");
        var log = NullLogger<UserRepository>.Instance;

        using (var context = new ApplicationDbContext(options))
        {
            var repo = new UserRepository(context, log);
            repo.Add(user); 
        }        

        // Act
        using (var context = new ApplicationDbContext(options))
        {
            var repo = new UserRepository(context, log);
            repo.Remove(user.ID);
        }

        // Assert
        using (var context = new ApplicationDbContext(options))
        {
            var repo = new UserRepository(context, log);
            var deletedUser = repo.Get(user.ID);

            Assert.Null(deletedUser); 
        }
    }
}