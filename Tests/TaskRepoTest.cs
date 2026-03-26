using Domain;
using Infrastructure.DbManager;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Logging;
using Npgsql.Replication;

namespace Tests;

public class TaskRepoTest
{
    [Fact]
    public void Add_NewTask_AddsToDb()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) 
            .Options;

        var task = new UserTask(228, "Podrochit'");
        var log = NullLogger<TaskRepository>.Instance;

        // Act
        using (var context = new ApplicationDbContext(options))
        {
            var tRepo = new TaskRepository(context, log);
            tRepo.Add(task);
        }

        // Assert
        using (var context = new ApplicationDbContext(options))
        {
            var tRepo = new TaskRepository(context, log);
            var getTask = tRepo.Get(task.ID);

            Assert.NotNull(getTask);
        }
    }

    [Fact]
    public void Update_ValidTask_ChangesStatusInDb()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) 
            .Options;

        var task = new UserTask(228, "Podrochit'");
        var log = NullLogger<TaskRepository>.Instance;

        using (var context = new ApplicationDbContext(options))
        {
            var tRepo = new TaskRepository(context, log);
            tRepo.Add(task); 
        }        

        // Act
        using (var context = new ApplicationDbContext(options))
        {
            var tRepo = new TaskRepository(context, log);
            var getTask = tRepo.Get(task.ID);

            if (getTask != null)
            {
                getTask.SetStatus(Domain.TaskStatus.Done);
                tRepo.Update(getTask);
            }
        }

        // Assert
        using (var context = new ApplicationDbContext(options))
        {
            var tRepo = new TaskRepository(context, log);
            var updatedTask = tRepo.Get(task.ID);

            Assert.NotNull(updatedTask);
            Assert.Equal(Domain.TaskStatus.Done, updatedTask.Status);
        }
    }

    [Fact]
    public void GetUserTasks_ValidList_ReturnsTasksFromDb()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) 
            .Options;

        var task = new UserTask(228, "Podrochit'");
        var tLog = NullLogger<TaskRepository>.Instance;

        var user = new User(69, "Debil");
        var uLog = NullLogger<UserRepository>.Instance;

        using (var context = new ApplicationDbContext(options))
        {
            var tRepo = new TaskRepository(context, tLog);
            tRepo.Add(task); 

            var uRepo = new UserRepository(context, uLog);
            uRepo.Add(user);

        }   

        // Act
        using (var context = new ApplicationDbContext(options))
        {            
            var tRepo = new TaskRepository(context, tLog);
            task.AssignTo(user.ID);
            tRepo.Update(task);
        }

        // Assert
        using (var context = new ApplicationDbContext(options))
        {
            var tRepo = new TaskRepository(context, tLog);

            var userTasks = tRepo.GetUserTasks(task.UserId);

            Assert.Single(userTasks);
        }
    }

    [Fact]
    public void Remove_ExistingTask_DeletesFromDb()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) 
            .Options;

        var task = new UserTask(1337, "Vykinut' musor");
        var log = NullLogger<TaskRepository>.Instance;

        using (var context = new ApplicationDbContext(options))
        {
            var tRepo = new TaskRepository(context, log);
            tRepo.Add(task); 
        }        

        // Act
        using (var context = new ApplicationDbContext(options))
        {
            var tRepo = new TaskRepository(context, log);
            tRepo.Remove(task.ID);
        }

        // Assert
        using (var context = new ApplicationDbContext(options))
        {
            var tRepo = new TaskRepository(context, log);
            var deletedTask = tRepo.Get(task.ID);

            Assert.Null(deletedTask);
        }
    }
}