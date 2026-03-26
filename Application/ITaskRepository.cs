using Domain;

namespace Application;

public interface ITaskRepository
{
     UserTask? Get(long id);
     void Add(UserTask task);
     void Remove(long id);
     void Update(UserTask task);
     List<UserTask> GetUserTasks(long userId);
}