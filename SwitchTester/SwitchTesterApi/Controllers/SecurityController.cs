using Microsoft.AspNetCore.Mvc;
using SwitchTesterApi.DTOs;
using SwitchTesterApi.Services;

namespace SwitchTesterApi.Controllers
{
    /// <summary>
    /// Controller for managing security-related operations.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class SecurityController(ISecurityService service) : ControllerBase
    {
        /// <summary>
        /// Login a user by validating the provided credentials and generates an authentication token on successful login.
        /// </summary>
        /// <remarks>
        ///     Example:
        ///         POST /api/security/login
        ///         Payload:
        ///         {
        ///             "userName": "Jorge",
        ///             "password": "123*"
        ///         }
        /// </remarks>
        /// <param name="userDTO">The data containing the username and password for login.</param>
        /// <returns>
        /// A response containing the authentication token on successful login.
        /// </returns>
        /// <response code="200">Returns the authentication token on successful login.</response>
        /// <response code="400">If an error occurs during the login process.</response>
        [HttpPost("login")]
        [ProducesResponseType(typeof(UserLoginResponseDTO), 200)]
        [ProducesResponseType(typeof(BadRequestResponseDTO), 400)]
        public async Task<ActionResult<UserLoginResponseDTO>> LoginUser(UserLoginDTO userDTO)
        {
            try
            {
                var token = await service.LoginUserAsync(userDTO);
                return Ok(new UserLoginResponseDTO { Token = token });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new BadRequestResponseDTO { Message = ex.Message });
            }
            catch (Exception)
            {
                return BadRequest(new BadRequestResponseDTO());
            }
        }
    }
}
