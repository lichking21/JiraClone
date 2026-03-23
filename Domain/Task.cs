namespace Domain;

enum TaskStatus
{
    Done,
    ToDo
}

public class Task
{
    public int ID {get; private set;}
    public string Title {get; private set;}
    public string Status {get; private set;}

    public Task (int id, string title)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Task title can't be null or empty");

        ID = id;
        Title = title;
        Status = TaskStatus.ToDo;
    }

    public void Rename(string newTitle)
    {
        if (string.IsNullOrWhiteSpace(Title))
            throw new ArgumentException("Title is empty! Maybe Task wasn't created yet");

        Title = newTitle;
    }

    public void SetStatus(string status)
    {
        if (string.IsNullOrWhiteSpace(status))
            throw new ArgumentException("Status can't be null or empty");

        if (status == TaskStatus.Done) 
            Status = TaskStatus.Done;

        if (status == TaskStatus.ToDo)
            Status = TaskStatus.ToDo;
    }
}