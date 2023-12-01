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
using System.Text.RegularExpressions;

namespace SwitchTesterApi.Services
{
    public class SegurityService(ISegurityContext context, IOptions<JwtConfiguration> jwtConfigs) : ISegurityService
    {
        private readonly ISegurityContext context = context;
        private readonly IOptions<JwtConfiguration> jwtConfigs = jwtConfigs;

        public async Task CreateUserAsync(ApplicationUserCreateDTO userDTO)
        {
            CheckUserNameFormat(userDTO.UserName);
            CheckPasswordFormat(userDTO.Password);

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
            CheckPasswordFormat(userDTO.NewPassword);

            var userDb = await context.ApplicationUsers
                                      .FirstOrDefaultAsync(u => u.UserName.Equals(userDTO.UserName)) 
                                        ?? throw new ArgumentException("The user with the assigned credentials does not exist.", nameof(userDTO));

            CheckIfPasswordCorrect(userDTO.OldPassword, userDb.Salt, userDb.PasswordHash);

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

            CheckIfPasswordCorrect(userDTO.Password, userDb.Salt, userDb.PasswordHash);

            return GenerateToken(userDb);
        }

        private static void CheckIfPasswordCorrect(string enteredPassword, byte[] storedSalt, byte[] storedPasswordHash)
        {
            var enteredPassHash = GeneratePasswordHash(enteredPassword, storedSalt);

            if(!storedPasswordHash.SequenceEqual(enteredPassHash))
                throw new ArgumentException("The password assigned to perform the operation is incorrect.", nameof(enteredPassword));
        }

        private static void CheckUserNameFormat(string userName)
        {
            // The username only contains lowercase letters, uppercase letters, 
            // numbers and the special character _, and has a minimum length of 5 
            // and a maximum of 30

            if (!Regex.IsMatch(userName, "^[a-zA-Z0-9_]{5,30}$"))
                throw new ArgumentException("The username does not comply with established policies", nameof(userName));
        }

        private static void CheckPasswordFormat(string userPassword)
        {
            // This expression requires at least one lowercase letter, 
            // one uppercase letter, one number, and one special character, 
            // and must be at least 8 characters long

            if (!Regex.IsMatch(userPassword, "^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[^a-zA-Z\\d]).{8,}$"))
                throw new ArgumentException("The password does not comply with established policies", nameof(userPassword));
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