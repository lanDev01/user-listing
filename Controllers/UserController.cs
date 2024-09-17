using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using user_listing.Dto;
using user_listing.Models;
using user_listing.Services;

namespace user_listing.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserInterface _userInterface;
        public UserController(IUserInterface userInterface)
        {
            _userInterface = userInterface;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _userInterface.GetUsers();

            if (users.Status == false)
            {
                return NotFound(users);
            }

            return Ok(users);
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUserById(int userId)
        {
            var user = await _userInterface.GetUserById(userId);

            if (user.Status == false)
            {
                return NotFound(user);
            }

            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateUserDto user)
        {
            var users = await _userInterface.CreateUser(user);

            if (users.Status == false)
            {
                return BadRequest(users);
            }

            return Ok(users);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUser(UpdateUserDto user)
        {
            var users = await _userInterface.UpdateUser(user);

            if (users.Status == false)
            {
                return BadRequest(users);
            }

            return Ok(users);
        }

        [HttpDelete]
        public async Task<IActionResult> RemoveUser(int userId)
        {
            var users = await _userInterface.RemoveUser(userId);

            if (users.Status == false)
            {
                return BadRequest(users);
            }

            return Ok(users);
        }
    }
}
