using EclipseWorks.Domain.Interfaces.Repositories;
using EclipseWorks.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace EclipseWorks.DbAdapter.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;
    private readonly DbSet<User> _users;

    public UserRepository(AppDbContext context)
    {
        _context = context ??
             throw new ArgumentNullException(nameof(context));

        _users = _context.Set<User>();
    }

    public IEnumerable<User> GetUsers()
    {
        return _users
            .Where(x => x.RemovedAt == null);
    }
}
