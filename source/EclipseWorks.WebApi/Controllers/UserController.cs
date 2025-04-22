using EclipseWorks.Application.Dtos.Response;
using EclipseWorks.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EclipseWorks.WebApi.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserAppService _userAppService;

        public UserController(IUserAppService userAppService)
        {
            _userAppService = userAppService ??
                throw new ArgumentNullException(nameof(userAppService));
        }

        /// <summary>
        /// Get all users
        /// </summary>
        /// <returns>List of Users</returns> 
        [HttpGet]
        [ProducesResponseType(typeof(UserResponse), 200)]
        [ProducesResponseType(typeof(UserResponse), 400)]
        public async Task<ActionResult> GetAllUsers()
        {
            var users = await _userAppService.GetUsersAsync();

            return Ok(users);
        }
    }
}
