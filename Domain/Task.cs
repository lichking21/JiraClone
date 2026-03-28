namespace Domain;

public class UserTask
{
    public long ID {get; private set;}
    public string? Title {get; private set;}
    public TaskStatus Status {get; private set;} = TaskStatus.ToDo;
    public long? UserId {get; private set;}

    private UserTask() {}

    public UserTask(string title)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Title can't be empty");

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

    public void AssignTo(long userId)
    {
        UserId = userId;
    }
}