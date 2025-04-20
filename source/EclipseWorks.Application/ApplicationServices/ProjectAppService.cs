using AutoMapper;
using EclipseWorks.Application.Dtos.Request;
using EclipseWorks.Application.Dtos.Response;
using EclipseWorks.Application.Interfaces;
using EclipseWorks.Domain.Interfaces.Services;
using EclipseWorks.Domain.Models;

namespace EclipseWorks.Application.ApplicationServices;

public class ProjectAppService : IProjectAppService
{
    private readonly IProjectService _projectService;
    private readonly IUserService _userService;
    private readonly IMapper _mapper;

    public ProjectAppService(IProjectService projectAppService,
        IUserService userService,
        IMapper mapper)
    {
        _projectService = projectAppService
            ?? throw new ArgumentNullException(nameof(projectAppService));

        _userService = userService ??
            throw new ArgumentNullException(nameof(userService));

        _mapper = mapper ??
            throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<ProjectResponse> CreateAsync(CreateProjectRequest projectRequest)
    {
        var project = _mapper.Map<Project>(projectRequest);

        try
        {
            var user = await _userService.GetByIdAsync(project.UserId);

            await _projectService.CreateAsync(project, user);
        }
        catch (Exception)
        {

            throw;
        }

        return _mapper.Map<ProjectResponse>(project);
    }

    public async Task<IEnumerable<ProjectResponse>> GetAllAsync()
    {
        var projects = await _projectService.GetAllAsync();

        return _mapper.Map<IEnumerable<ProjectResponse>>(projects);
    }
}
