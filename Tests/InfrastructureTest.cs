using Application;
using Domain;
using Infrastructure;

namespace Tests;

public class InfrastructureTest
{
    private readonly UserRepository _userRepo;
    private readonly TaskRepository _taskRepo;
    private readonly AssignTaskToUserUseCase _assignTask;

    public InfrastructureTest()
    {
        _userRepo = new UserRepository();
        _taskRepo = new TaskRepository();
        _assignTask = new AssignTaskToUserUseCase(_userRepo, _taskRepo);
    }


    [Fact]
    public void Execute_AddMethods_Successfully()
    {
        var mockUser = new User(1, "Eblan");
        var mockTask = new UserTask(2, "Pinat hui");

        _userRepo.Add(mockUser);
        _taskRepo.Add(mockTask);

        _assignTask.Execute(mockUser.ID, mockTask.ID);

        var asssignedTask = _taskRepo.Get(mockTask.ID);
        Assert.Equal(mockUser.ID, asssignedTask.UserId);
    }

    [Fact]
    public void Execute_GetMethods_Successfully()
    {
        var mockUser = new User(1337, "Eblan");
        var mockTask = new UserTask(52, "Pinat hui");
        var mockTask2 = new UserTask(228, "Sosat hui");

        _userRepo.Add(mockUser);
        _taskRepo.Add(mockTask);
        _taskRepo.Add(mockTask2);

        _assignTask.Execute(mockUser.ID, mockTask.ID);
        _assignTask.Execute(mockUser.ID, mockTask2.ID);

        var user = _userRepo.Get(mockUser.ID);
        Assert.NotNull(user);
        Assert.Equal("Eblan", user.Name);

        var task = _taskRepo.Get(mockTask.ID);
        Assert.NotNull(task);
        Assert.Equal("Pinat hui", task.Title);
        Assert.Equal(mockUser.ID, task.UserId);

        var userTasks = _taskRepo.GetUserTasks(mockUser.ID);
        Assert.NotNull(userTasks);
        Assert.Equal(2, userTasks.Count());
        Assert.Contains(userTasks, t => t.ID == mockTask.ID);
        Assert.Contains(userTasks, t => t.ID == mockTask2.ID);
    }

    [Fact]
    public void Execute_RemoveUser_Successfully()
    {
        var mockUser = new User(42, "Eblan");
        _userRepo.Add(mockUser);

        _userRepo.Remove(mockUser.ID);

        Assert.Throws<KeyNotFoundException>(() => _userRepo.Get(mockUser.ID));
    }

    [Fact]
    public void Execute_RemoveTask_Successfully()
    {
        var mockTask = new UserTask(1703, "Pinat hui");
        _taskRepo.Add(mockTask);

        _taskRepo.Remove(mockTask.ID);
        
        Assert.Throws<KeyNotFoundException>(() => _userRepo.Get(mockTask.ID));
    }
}
