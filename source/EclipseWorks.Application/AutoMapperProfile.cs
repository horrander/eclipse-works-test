using AutoMapper;
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

    }

    private void CreateResponseMaps()
    {
        CreateMap<User, UserResponse>();
    }
}
