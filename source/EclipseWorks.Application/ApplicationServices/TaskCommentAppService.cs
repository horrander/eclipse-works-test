using System;
using AutoMapper;
using EclipseWorks.Application.Dtos.Request;
using EclipseWorks.Application.Dtos.Response;
using EclipseWorks.Application.Interfaces;
using EclipseWorks.Domain.Interfaces.Services;
using EclipseWorks.Domain.Models;

namespace EclipseWorks.Application.ApplicationServices;

public class TaskCommentAppService : ITaskCommentAppService
{
    private readonly ITaskCommentService _taskCommentService;
    private readonly IMapper _mapper;

    public TaskCommentAppService(ITaskCommentService taskCommentService,
        IMapper mapper)
    {
        _taskCommentService = taskCommentService ??
            throw new ArgumentNullException(nameof(taskCommentService));

        _mapper = mapper ??
            throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<TaskCommentResponse> CreateAsync(CreateTaskCommentRequest taskCommentRequest)
    {
        var taskComment = _mapper.Map<TaskComment>(taskCommentRequest);

        await _taskCommentService.CreateAsync(taskComment);

        return _mapper.Map<TaskCommentResponse>(taskComment);
    }
}
