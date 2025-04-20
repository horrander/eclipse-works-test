using EclipseWorks.Application.Dtos.Request;
using EclipseWorks.Application.Dtos.Response;
using EclipseWorks.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EclipseWorks.WebApi.Controllers
{
    [Route("api/[controller]")]
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
            var projectsResponse = await _projectAppService.GetAllAsync();

            return Ok(projectsResponse);
        }
    }
}
