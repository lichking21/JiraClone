using Domain;

namespace Application;

public interface IUserRepository
{
    User? Get(long id);
    void Add(User user);
    void Remove(long id);
}
