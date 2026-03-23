using Application;

namespace Infrastructure;

public class TaskRepository : ITaskRepository
{
    private Dictionary<int, Domain.Task> _tasks = new Dictionary<int, Domain.Task>();

    public void Add(Domain.Task task)
    {
        _tasks.Add(task.ID, task);
    }

    public Domain.Task Get(int id)
    {
        return _tasks[id];
    }

    public void Remove(int id)
    {
        _tasks.Remove(id);
    }

    public void Update(Domain.Task task)
    {
        if (!_tasks.ContainsKey(task.ID))
            throw new Exception($"Task {task.Title} not found");

        _tasks[task.ID] = task;
    }
}