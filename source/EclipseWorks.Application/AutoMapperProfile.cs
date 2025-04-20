using AutoMapper;
using EclipseWorks.Application.Dtos.Request;
using EclipseWorks.Application.Dtos.Response;
using EclipseWorks.Domain.Models;

namespace EclipseWorks.Application;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateRequestMaps();
        CreateResponseMaps();
    }

    private void CreateRequestMaps()
    {
        CreateMap<CreateProjectRequest, Project>();
        CreateMap<CreateTaskItemRequest, TaskItem>();
        CreateMap<UpdateTaskItemRequest, TaskItem>();
    }

    private void CreateResponseMaps()
    {
        CreateMap<User, UserResponse>();
        CreateMap<Project, ProjectResponse>();
        CreateMap<TaskItem, TaskItemResponse>();
    }
}
