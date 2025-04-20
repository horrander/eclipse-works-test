using EclipseWorks.Application.Dtos.Request;
using EclipseWorks.Application.Dtos.Response;
using EclipseWorks.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EclipseWorks.WebApi.Controllers
{
    [Route("api/task")]
    [ApiController]
    public class TaskItemController : ControllerBase
    {
        private readonly ITaskItemAppService _taskItemAppService;

        public TaskItemController(ITaskItemAppService taskItemAppService)
        {
            _taskItemAppService = taskItemAppService ??
                throw new ArgumentNullException(nameof(taskItemAppService));
        }

        /// <summary>
        /// Get all tasks from a project
        /// </summary>
        /// <param name="projetcId">Project Id</param>
        /// <returns>All tasks from a especific project</returns>
        [HttpGet("project/{projetcId:Guid}")]
        [ProducesResponseType(typeof(TaskItemResponse), 200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetTasksByProject(Guid projetcId)
        {
            var tasks = await _taskItemAppService.GetByProjectIdAsync(projetcId);

            return Ok(tasks);
        }

        /// <summary>
        /// Create a Task to a specific Project
        /// </summary>
        /// <param name="taskItemRequest">New Task</param>
        /// <returns>New Task</returns>
        [HttpPost]
        [ProducesResponseType(typeof(TaskItemResponse), 201)]
        [ProducesResponseType(412)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Create(CreateTaskItemRequest taskItemRequest)
        {
            var task = await _taskItemAppService.CreateAsync(taskItemRequest);

            return Created("", task);
        }

        /// <summary>
        /// Update a Task
        /// </summary>
        /// <param name="taskItemRequest">Updated Task</param>
        /// <returns></returns>
        [HttpPatch]
        [ProducesResponseType(typeof(TaskItemResponse), 200)]
        [ProducesResponseType(412)]
        public async Task<IActionResult> Update(UpdateTaskItemRequest taskItemRequest)
        {
            var task = await _taskItemAppService.UpdateAsync(taskItemRequest);

            return Ok(task);
        }

        /// <summary>
        /// Get a task by Id
        /// </summary>
        /// <param name="id">Task Id</param>
        /// <returns>Task</returns>
        [HttpGet("{id:Guid}")]
        [ProducesResponseType(typeof(TaskItemResponse), 200)]
        [ProducesResponseType(412)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var task = await _taskItemAppService.GetByid(id);

            return Ok(task);
        }
    }
}
