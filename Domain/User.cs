namespace Domain;

public class User
{
    public int ID { get; private set;}
    public string Name { get; private set;}

    public User(int id, string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("User name can't be empty");

        ID = id;
        Name = name;
    }
}