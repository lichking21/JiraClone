using Application;
using Domain;
using Infrastructure.DbManager;
using Microsoft.Extensions.Logging;

namespace Infrastructure;

public class TaskRepository : ITaskRepository
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<TaskRepository> _log;

    public TaskRepository(ApplicationDbContext context, ILogger<TaskRepository> log)
    {
        _context = context;
        _log = log;
    }

    public void Add(UserTask task)
    {
        _context.Tasks.Add(task);
        _context.SaveChanges();

        _log.LogDebug($"(LOG) >> task [{task.ID}] added to DataBase");
    }

    public UserTask? Get(long id)
    {
        return _context.Tasks.Find(id);
    }

    public void Remove(long id)
    {
        var task = Get(id);

        if (task != null)
        {
            _context.Tasks.Remove(task);
            _context.SaveChanges();
            _log.LogDebug($"(LOG) >> task [{task.ID}] was removed from DataBase");
        } 
    }

    public void Update(UserTask newTask)
    {
        _context.Tasks.Update(newTask);
        _context.SaveChanges();
    }

    public List<UserTask> GetUserTasks(long userId)
    {
        return _context.Tasks.Where(t => t.UserId == userId).ToList();
    }
}