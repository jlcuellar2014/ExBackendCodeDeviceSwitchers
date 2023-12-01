using Microsoft.AspNetCore.Mvc;
using SwitchTesterApi.DTOs;
using SwitchTesterApi.Services;

namespace SwitchTesterApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SecurityController(ISecurityService service) : ControllerBase
    {
        [HttpPost("login")]
        public async Task<ActionResult<UserLoginResponseDTO>> LoginUser(UserLoginDTO userDTO)
        {
            try
            {
                var token = await service.LoginUserAsync(userDTO);
                return Ok(new UserLoginResponseDTO { Token = token });
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
