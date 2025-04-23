using System;
using EclipseWorks.Application.Dtos.Request;
using EclipseWorks.Application.Dtos.Response;

namespace EclipseWorks.Application.Interfaces;

public interface ITaskItemAppService
{
    Task<TaskItemResponse> CreateAsync(CreateTaskItemRequest task);
    Task<TaskItemResponse> UpdateAsync(UpdateTaskItemRequest task);
    Task<IEnumerable<TaskItemResponse>> GetByProjectIdAsync(Guid projectId);
    Task<TaskItemResponse> GetByid(Guid id);
    Task RemoveFromProjectAsync(Guid projectId);
}
