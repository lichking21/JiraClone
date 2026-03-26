namespace Domain;

public class UserTask
{
    public int ID {get; private set;}
    public string? Title {get; private set;}
    public TaskStatus Status {get; private set;} = TaskStatus.ToDo;
    public int UserId {get; private set;}

    private UserTask() {}

    public UserTask (int id, string title)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Task title can't be null or empty");

        ID = id;
        Title = title;
    }

    public void Rename(string newTitle)
    {
        if (string.IsNullOrWhiteSpace(Title))
            throw new ArgumentException("Title is empty! Maybe Task wasn't created yet");

        Title = newTitle;
    }

    public void SetStatus(TaskStatus status)
    {
        if (status == TaskStatus.Done) 
            Status = status;

        if (status == TaskStatus.ToDo)
            Status = status;
    }

    public void AssignTo(int userId)
    {
        UserId = userId;
    }
}