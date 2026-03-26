using Domain;

namespace Application;

public interface IUserRepository
{
    User? Get(int id);
    void Add(User user);
    void Remove(int id);
}
