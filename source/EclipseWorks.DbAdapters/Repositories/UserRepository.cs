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

    private void Create(User user)
    {
        _users.Add(user);

        _context.SaveChanges();
    }

    public async Task<User?> GetByIdAsync(Guid id)
    {
        return await _users.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<IEnumerable<User>> GetUsersAsync()
    {
        if (_context.Database.EnsureCreated())
        {
            _context.Database.Migrate();

            Create(new User("eclipseworks@email.com"));
        }

        return await _users
            .Where(x => x.RemovedAt == null)
            .ToListAsync();
    }
}
