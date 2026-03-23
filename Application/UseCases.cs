namespace Application;

public class UseCases
{
    private readonly ITaskRepository _repository;

    public UseCases(ITaskRepository repository)
    {
        _repository = repository;
    }

    public void CompleteTask(int id)
    {
        var task = _repository.Get(id);

        task.SetStatus(Domain.TaskStatus.Done);

        _repository.Update(task);
    }   
}