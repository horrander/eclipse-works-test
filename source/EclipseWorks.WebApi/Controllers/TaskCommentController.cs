using EclipseWorks.Application.Dtos.Request;
using EclipseWorks.Application.Dtos.Response;
using EclipseWorks.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EclipseWorks.WebApi.Controllers
{
    [Route("api/taskComment")]
    [ApiController]
    public class TaskCommentController : ControllerBase
    {
        private readonly ITaskCommentAppService _taskCommentAppService;

        public TaskCommentController(ITaskCommentAppService taskCommentAppService)
        {
            _taskCommentAppService = taskCommentAppService ??
                throw new ArgumentNullException(nameof(taskCommentAppService));
        }

        /// <summary>
        /// Create a task comment 
        /// </summary>
        /// <param name="createTaskCommentRequest">Task comment</param>
        /// <returns>New task comment</returns>
        [HttpPost]
        [ProducesResponseType(typeof(TaskCommentResponse), 201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Create(CreateTaskCommentRequest createTaskCommentRequest)
        {
            var taskCommentResponse = await _taskCommentAppService.CreateAsync(createTaskCommentRequest);

            return Created("", taskCommentResponse);
        }
    }
}
