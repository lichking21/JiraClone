using Application;
using Domain;

namespace Infrastructure;

public class TaskRepository : ITaskRepository
{
    private Dictionary<int, UserTask> _tasks = new Dictionary<int, UserTask>();

    public void Add(UserTask task)
    {
        _tasks.Add(task.ID, task);
    }

    public UserTask Get(int id)
    {
        return _tasks[id];
    }

    public void Remove(int id)
    {
        _tasks.Remove(id);
    }

    public void Update(UserTask task)
    {
        if (!_tasks.ContainsKey(task.ID))
            throw new Exception($"(ERR) >> task with ID {task.ID} not found");

        _tasks[task.ID] = task;
    }

    public List<UserTask> GetUserTasks(int userId)
    {
        return _tasks.Values.Where(t => t.UserId == userId).ToList();
    }
}