using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SwitchTesterApi.DTOs;
using SwitchTesterApi.Services;

namespace SwitchTesterApi.Controllers
{
    /// <summary>
    /// Controller for managing user-related operations.
    /// </summary>
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController(IUsersService service) : ControllerBase
    {
        /// <summary>
        /// Creates a new user using the provided user data.
        /// </summary>
        /// <remarks>
        ///     Example:
        ///         POST /api/users
        ///         Payload:
        ///         {
        ///             "userName": "Jade2018",
        ///             "password": "aB*25daaas"
        ///         }
        /// </remarks>
        /// <param name="userDTO">The data for creating a new user.</param>
        /// <returns>
        /// A response indicating the success of the user creation.
        /// </returns>
        /// <response code="200">Returns a success response on successful user creation.</response>
        /// <response code="400">If the provided user data is invalid or if an error occurs during the creation process.</response>
        [ProducesResponseType(typeof(OkResponseDTO), 200)]
        [ProducesResponseType(typeof(BadRequestResponseDTO), 400)]
        [HttpPost]
        public async Task<ActionResult<OkResponseDTO>> CreateUserAsync(UserCreateDTO userDTO)
        {
            try
            {
                await service.CreateUserAsync(userDTO);

                return Ok(new OkResponseDTO());
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

        /// <summary>
        /// Updates user information using the provided user update data.
        /// </summary>
        /// <remarks>
        ///     Example:
        ///         PATCH /api/users
        ///         Payload:
        ///         {
        ///             "userName": "Jade2018",
        ///             "password": "aB*25daaas",
        ///             "newPassword": "aB*25da785",
        ///         }
        /// </remarks>
        /// <param name="userDTO">The data for updating user information.</param>
        /// <returns>
        /// A response indicating the success of the user information update.
        /// </returns>
        /// <response code="200">Returns a success response on successful user information update.</response>
        /// <response code="400">If the provided user update data is invalid or if an error occurs during the update process.</response>
        [HttpPatch]
        [AllowAnonymous]
        [ProducesResponseType(typeof(OkResponseDTO), 200)]
        [ProducesResponseType(typeof(BadRequestResponseDTO), 400)]
        public async Task<ActionResult<OkResponseDTO>> UpdateUserAsync(UserUpdateDTO userDTO)
        {
            try
            {
                await service.UpdateUserAsync(userDTO);
                return Ok(new OkResponseDTO());
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
