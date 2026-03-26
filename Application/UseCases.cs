namespace Application;

public class CompleteTaskUseCase
{
    private readonly ITaskRepository _taskRepo;

    public CompleteTaskUseCase(ITaskRepository taskRepo)
    {
        _taskRepo = taskRepo;
    }

    public void Execute(long id)
    {
        var task = _taskRepo.Get(id);
        if (task == null)
            throw new KeyNotFoundException($"(ERR) >> task with ID [{id}] not found"); 

        task.SetStatus(Domain.TaskStatus.Done);

        _taskRepo.Update(task);
    } 
}

public class AssignTaskToUserUseCase
{
    private readonly IUserRepository _userRepo;
    private readonly ITaskRepository _taskRepo;

    public AssignTaskToUserUseCase(IUserRepository userRepo, ITaskRepository taskRepo)
    {
        _userRepo = userRepo;
        _taskRepo = taskRepo;
    }

    
    public void Execute(long userId, long taskId)
    {
        if (_userRepo.Get(userId) == null)
            throw new KeyNotFoundException($"(ERR) >> user with ID [{userId}] not found");

        var task = _taskRepo.Get(taskId);
        if (task == null)
            throw new KeyNotFoundException($"(ERR) >> task with ID [{taskId}] not found");

        task.AssignTo(userId);
        _taskRepo.Update(task);
    }
}