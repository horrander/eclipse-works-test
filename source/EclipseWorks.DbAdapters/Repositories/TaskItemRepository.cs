using EclipseWorks.Domain.Interfaces.Repositories;
using EclipseWorks.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace EclipseWorks.DbAdapter.Repositories;

public class TaskItemRepository : ITaskItemRepository
{
    private readonly AppDbContext _context;
    private readonly DbSet<TaskItem> _tasks;

    public TaskItemRepository(AppDbContext context)
    {
        _context = context ??
            throw new ArgumentNullException(nameof(context));

        _tasks = _context.Set<TaskItem>();
    }

    public async Task<TaskItem> CreateAsync(TaskItem task)
    {
        await _tasks.AddAsync(task);

        await _context.SaveChangesAsync();

        return task;
    }

    public async Task<TaskItem?> GetById(Guid id)
    {
        return await _tasks.FirstOrDefaultAsync(x => x.Id == id && x.RemovedAt == null);
    }

    public async Task<IEnumerable<TaskItem>> GetByProjectIdAsync(Guid projectId)
    {
        return await _tasks.Where(x => x.ProjectId == projectId && x.RemovedAt == null)
            .ToListAsync();
    }

    public async Task RemoveFromProjectAsync(TaskItem taskItem)
    {
        taskItem.SetRemovedDate();

        await UpdateAsync(taskItem);
    }

    public async Task<TaskItem> UpdateAsync(TaskItem task)
    {
        task.ModifiedAt = DateTime.Now;

        _tasks.Update(task);

        await _context.SaveChangesAsync();

        return task;
    }
}
