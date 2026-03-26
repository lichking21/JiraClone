using Application;
using Domain;
using Infrastructure.DbManager;
using Microsoft.Extensions.Logging;
namespace Infrastructure;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<UserRepository> _log;

    public UserRepository(ApplicationDbContext context, ILogger<UserRepository> log)
    {
        _context = context;
        _log = log;
    }

    public bool IsUserExists(long id)
    {
        if (id == 0)
        {
            _log.LogError("(ERR) >> user ID can't be 0");
            return false;
        }

        return _context.Users.Any(u => u.ID == id);
    }

    public void Add(User user)
    {
        if (!IsUserExists(user.ID))
        {
            _context.Users.Add(user);
            _context.SaveChanges();

            _log.LogDebug($"(LOG) >> user [{user.ID}] was added to DataBase");
        }
        else
            _log.LogError($"(WARN) >> user [{user.ID}] already exists in DataBase");
    }

    public User? Get(long id)
    {
        return _context.Users
            .Find(id);
    }

    public void Remove(long id)
    {
        var user = Get(id);

        if (user != null)
        {
            _context.Users.Remove(user);
            _context.SaveChanges();

            _log.LogDebug($"(LOG) >> user [{id}] was removed from DataBase");
        }
    }
}
