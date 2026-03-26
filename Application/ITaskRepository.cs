using Domain;

namespace Application;

public interface ITaskRepository
{
     UserTask? Get(int id);
     void Add(UserTask task);
     void Remove(int id);
     void Update(UserTask task);
     List<UserTask> GetUserTasks(int userId);
}