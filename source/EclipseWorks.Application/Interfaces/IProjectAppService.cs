using EclipseWorks.Application.Dtos.Request;
using EclipseWorks.Application.Dtos.Response;

namespace EclipseWorks.Application.Interfaces;

public interface IProjectAppService
{
    Task<IEnumerable<ProjectResponse>> GetAllAsync();

    Task<ProjectResponse> CreateAsync(CreateProjectRequest project);

    Task<IEnumerable<ProjectResponse>> GetByUserIdAsync(Guid userId);

    Task RemoveAsync(Guid projectId);
}
