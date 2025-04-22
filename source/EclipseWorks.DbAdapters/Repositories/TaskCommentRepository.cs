using EclipseWorks.Domain.Interfaces.Repositories;
using EclipseWorks.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace EclipseWorks.DbAdapter.Repositories;

public class TaskCommentRepository : ITaskCommentRepository
{
    private readonly AppDbContext _context;
    private readonly DbSet<TaskComment> _comment;

    public TaskCommentRepository(AppDbContext context)
    {
        _context = context ??
            throw new ArgumentNullException(nameof(context));

        _comment = _context.Set<TaskComment>();
    }

    public async Task CreateAsync(TaskComment taskComment)
    {
        await _comment.AddAsync(taskComment);

        await _context.SaveChangesAsync();
    }
}
