namespace Domain;

public class User
{
    public long ID { get; private set;}
    public string? Name { get; private set;}

    private User() {}

    public User(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name can't be empty");
            
        Name = name;
    }
}