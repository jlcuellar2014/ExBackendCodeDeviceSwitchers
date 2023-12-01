using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SwitchTesterApi.DTOs;
using SwitchTesterApi.Models;
using SwitchTesterApi.Models.Contexts;
using SwitchTesterApi.Settings;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SwitchTesterApi.Services
{
    public class SecurityService(ISecurityContext context, IOptions<JwtConfiguration> jwtConfigs) : ISecurityService
    {
        private readonly ISecurityContext context = context;
        private readonly IOptions<JwtConfiguration> jwtConfigs = jwtConfigs;

        public async Task<string> LoginUserAsync(UserLoginDTO userDTO)
        {
            var userDb = await context.ApplicationUsers
                                .FirstOrDefaultAsync(u => u.UserName.Equals(userDTO.UserName)) ?? 
                                    throw new ArgumentException("The user does not exist in the system.", nameof(userDTO));

            UserHelpers.CheckIfPasswordCorrect(userDTO.Password, userDb.Salt, userDb.PasswordHash);

            return GenerateToken(userDb);
        }

        private string GenerateToken(User applicationUser)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfigs.Value.SecretKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] {
                    new Claim(ClaimTypes.Name, applicationUser.UserName),
                    new Claim("TokenId", Guid.NewGuid().ToString()),
                }),
                Expires = DateTime.UtcNow.AddHours(jwtConfigs.Value.HoursLife),
                SigningCredentials = credentials,
                Issuer = jwtConfigs.Value.Issuer,
                Audience = jwtConfigs.Value.Audience
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}