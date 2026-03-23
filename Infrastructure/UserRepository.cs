using Application;
using Domain;
namespace Infrastructure;

public class UserRepository : IUserRepository
{
    private Dictionary<int, User> _users = new Dictionary<int, User>();

    public void Add(User user)
    {
        _users.Add(user.ID, user);
    }

    public User Get(int id)
    {
        return _users[id];
    }

    public void Remove(int id)
    {
        _users.Remove(id);
    }
}
