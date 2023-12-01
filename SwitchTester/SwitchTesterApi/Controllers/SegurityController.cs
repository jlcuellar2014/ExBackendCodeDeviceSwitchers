using Microsoft.AspNetCore.Mvc;
using SwitchTesterApi.DTOs;
using SwitchTesterApi.Services;

namespace SwitchTesterApi.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class SegurityController(ISegurityService service) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CreateUserAsync(ApplicationUserCreateDTO userDTO)
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
        public async Task<IActionResult> UpdateUserAsync(ApplicationUserUpdateDTO userDTO)
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

        [HttpPost("login")]
        public async Task<ActionResult<ApplicationUserLoginResponseDTO>> LoginUser(ApplicationUserLoginDTO userDTO)
        {
            try
            {
                var token = await service.LoginUserAsync(userDTO);
                return Ok(new ApplicationUserLoginResponseDTO { Token = token });
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
