using AutoMapper;
using EclipseWorks.Application.Dtos.Request;
using EclipseWorks.Application.Dtos.Response;
using EclipseWorks.Application.Interfaces;
using EclipseWorks.Domain.Interfaces.Services;
using EclipseWorks.Domain.Models;

namespace EclipseWorks.Application.ApplicationServices;

public class TaskItemAppService : ITaskItemAppService
{
    private readonly ITaskItemService _taskItemService;
    private readonly IProjectService _projectService;

    private readonly IMapper _mapper;

    public TaskItemAppService(ITaskItemService taskItemService,
        IProjectService projectService,
        IMapper mapper)
    {
        _taskItemService = taskItemService ??
            throw new ArgumentNullException(nameof(taskItemService));

        _projectService = projectService ??
            throw new ArgumentNullException(nameof(projectService));

        _mapper = mapper ??
            throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<TaskItemResponse> CreateAsync(CreateTaskItemRequest taskRequest)
    {
        var project = await _projectService.GetByIdAsync(taskRequest.ProjectId);

        var task = _mapper.Map<TaskItem>(taskRequest);

        await _taskItemService.CreateAsync(project, task);

        return _mapper.Map<TaskItemResponse>(task);
    }

    public async Task<TaskItemResponse> GetByid(Guid id)
    {
        var task = await _taskItemService.GetById(id);

        return _mapper.Map<TaskItemResponse>(task);
    }

    public async Task<IEnumerable<TaskItemResponse>> GetByProjectIdAsync(Guid projectId)
    {
        var taskItems = await _taskItemService.GetByProjectIdAsync(projectId);

        return _mapper.Map<IEnumerable<TaskItemResponse>>(taskItems);
    }

    public Task RemoveFromProjectAsync(Guid projectId)
    {
        throw new NotImplementedException();
    }

    public async Task<TaskItemResponse> UpdateAsync(UpdateTaskItemRequest taskRequest)
    {
        var taskItem = _mapper.Map<TaskItem>(taskRequest);

        await _taskItemService.UpdateAsync(taskItem);

        return _mapper.Map<TaskItemResponse>(taskItem);
    }
}
