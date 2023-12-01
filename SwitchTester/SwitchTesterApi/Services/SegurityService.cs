using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SwitchTesterApi.DTOs;
using SwitchTesterApi.Models;
using SwitchTesterApi.Models.Contexts;
using SwitchTesterApi.Settings;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace SwitchTesterApi.Services
{
    public class SegurityService(ISegurityContext context, IOptions<JwtConfiguration> jwtConfigs) : ISegurityService
    {
        private readonly ISegurityContext context = context;
        private readonly IOptions<JwtConfiguration> jwtConfigs = jwtConfigs;

        public async Task CreateUserAsync(ApplicationUserCreateDTO userDTO)
        {
            var salt = GenerateSalt();
            var passwordHash = GeneratePasswordHash(userDTO.Password, salt);

            var newAppUser = new ApplicationUser()
            {
                UserName = userDTO.UserName,
                PasswordHash = passwordHash,
                Salt = salt
            };

            context.ApplicationUsers.Add(newAppUser);

            await context.SaveChangesAsync();
        }

        public async Task UpdateUserAsync(ApplicationUserUpdateDTO userDTO)
        {
            var userDb = await context.ApplicationUsers
                                      .FirstOrDefaultAsync(u => u.UserName.Equals(userDTO.UserName)) 
                                        ?? throw new ArgumentException("The user with the assigned credentials does not exist.", nameof(userDTO));

            if (!IsPasswordCorrect(userDTO.OldPassword, userDb.Salt, userDb.PasswordHash))
                throw new ArgumentException("The password assigned to perform the operation is incorrect.", nameof(userDTO));
            
            var newSalt = GenerateSalt();
            var newPassHash = GeneratePasswordHash(userDTO.NewPassword, newSalt);

            userDb.Salt = newSalt;
            userDb.PasswordHash = newPassHash;

            await context.SaveChangesAsync();
        }

        public async Task<string> LoginUserAsync(ApplicationUserLoginDTO userDTO)
        {
            var userDb = await context.ApplicationUsers
                                .FirstOrDefaultAsync(u => u.UserName.Equals(userDTO.UserName)) ?? 
                                    throw new ArgumentException("The user does not exist in the system.", nameof(userDTO));

            var validPassword = IsPasswordCorrect(userDTO.Password, userDb.Salt, userDb.PasswordHash);

            return validPassword ? GenerateToken(userDb)
                                    : throw new ArgumentException("User credentials are incorrect.", nameof(userDTO));
        }

        private static bool IsPasswordCorrect(string enteredPassword, byte[] storedSalt, byte[] storedPasswordHash)
        {
            var enteredPassHash = GeneratePasswordHash(enteredPassword, storedSalt);

            return storedPasswordHash.SequenceEqual(enteredPassHash);
        }

        private static byte[] GenerateSalt()
        {
            byte[] salt = new byte[16];

            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(salt);

            return salt;
        }

        private static byte[] GeneratePasswordHash(string password, byte[] salt)
        {
            using var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000, HashAlgorithmName.SHA256);

            return pbkdf2.GetBytes(32);
        }

        private string GenerateToken(ApplicationUser applicationUser)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfigs.Value.SecretKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] {
                    new Claim(ClaimTypes.Name, applicationUser.UserName),
                }),
                Expires = DateTime.UtcNow.AddHours(2),
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