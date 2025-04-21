using EclipseWorks.Domain.Interfaces.Repositories;
using EclipseWorks.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace EclipseWorks.DbAdapter.Repositories;

public class ProjectRepository : IProjectRepository
{
    private readonly AppDbContext _context;
    private readonly DbSet<Project> _projects;

    public ProjectRepository(AppDbContext context)
    {
        _context = context ??
            throw new ArgumentNullException(nameof(context));

        _projects = _context.Set<Project>();
    }

    public async Task<Project> CreateAsync(Project project)
    {
        _context.Attach(project.User);

        await _projects.AddAsync(project);

        await _context.SaveChangesAsync();

        return project;
    }

    public async Task<IEnumerable<Project>> GetAllAsync()
    {
        return await _projects.Where(x => x.RemovedAt == null)
            .Include(x => x.User)
            .Include(x => x.Tasks)
            .ToListAsync();
    }

    public async Task<Project?> GetByIdAsync(Guid id)
    {
        return await _projects
            .Include(x => x.User)
            .Include(x => x.Tasks)
            .FirstOrDefaultAsync(x => x.Id == id && x.RemovedAt == null);
    }

    public async Task<IEnumerable<Project>?> GetByUserIdAsync(Guid userId)
    {
        return await _projects
            .Include(x => x.User)
            .Include(x => x.Tasks)
            .Where(x => x.UserId == userId && x.RemovedAt == null)
            .ToListAsync();
    }

    public Task Remove(Guid id)
    {
        throw new NotImplementedException();
    }
}
