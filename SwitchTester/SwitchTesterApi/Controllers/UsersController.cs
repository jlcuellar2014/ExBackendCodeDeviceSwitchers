using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SwitchTesterApi.DTOs;
using SwitchTesterApi.Services;

namespace SwitchTesterApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController(IUsersService service) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CreateUserAsync(UserCreateDTO userDTO)
        {
            try
            {
                await service.CreateUserAsync(userDTO);
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPatch]
        public async Task<IActionResult> UpdateUserAsync(UserUpdateDTO userDTO)
        {
            try
            {
                await service.UpdateUserAsync(userDTO);
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
