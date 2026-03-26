namespace Domain;

public class User
{
    public long ID { get; private set;}
    public string? Name { get; private set;}

    private User() {}

    public User(long id, string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("User name can't be empty");

        ID = id;
        Name = name;
    }
}