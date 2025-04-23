using EclipseWorks.Application.Dtos.Request;
using EclipseWorks.Application.Dtos.Response;

namespace EclipseWorks.Application.Interfaces;

public interface ITaskCommentAppService
{
    Task<TaskCommentResponse> CreateAsync(CreateTaskCommentRequest taskCommentRequest);
}
