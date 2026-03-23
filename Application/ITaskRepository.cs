namespace Application;

public interface ITaskRepository
{
     Domain.Task Get(int id);
     void Add(Domain.Task task);
     void Remove(int id);
     void Update(Domain.Task task);
}