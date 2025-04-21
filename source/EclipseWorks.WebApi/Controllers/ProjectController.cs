using EclipseWorks.Application.Dtos.Request;
using EclipseWorks.Application.Dtos.Response;
using EclipseWorks.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EclipseWorks.WebApi.Controllers
{
    [Route("api/project")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectAppService _projectAppService;

        public ProjectController(IProjectAppService projectAppService)
        {
            _projectAppService = projectAppService ??
                throw new ArgumentNullException(nameof(projectAppService));
        }

        /// <summary>
        /// Create a new project
        /// </summary>
        /// <param name="createProjectRequest">Object representing a Project</param>
        /// <returns>New Project</returns>
        [HttpPost]
        [ProducesResponseType(typeof(ProjectResponse), 201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreateAsync(CreateProjectRequest createProjectRequest)
        {
            var projectResponse = await _projectAppService.CreateAsync(createProjectRequest);

            return Created("", projectResponse);
        }

        /// <summary>
        /// Get all projects 
        /// </summary>
        /// <returns>Lisf of projects</returns>
        [HttpGet]
        [ProducesResponseType(typeof(ProjectResponse), 200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetAllProjects()
        {
            var projects = await _projectAppService.GetAllAsync();

            return Ok(projects);
        }

        /// <summary>
        /// Get all projects by a user id
        /// </summary>
        /// <param name="userId">User Id</param>
        /// <returns>List of projects</returns>
        [HttpGet("{userId:Guid}")]
        [ProducesResponseType(typeof(ProjectResponse), 200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetByUserId(Guid userId)
        {
            var projects = await _projectAppService.GetByUserIdAsync(userId);

            return Ok(projects);
        }

        /// <summary>
        /// Remove a project
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        [HttpDelete("{projectId:Guid}")]
        [ProducesResponseType(typeof(ProjectResponse), 200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Remove(Guid projectId)
        {
            await _projectAppService.RemoveAsync(projectId);

            return Ok();
        }
    }
}
