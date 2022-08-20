using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.IdentityModel.Tokens;
using SenseHome.Common.Enums;
using SenseHome.DataTransferObjects.Authentication;
using SenseHome.Services.Configurations;

namespace SenseHome.Services.UserExtension
{
    public class UserExtensionService : IUserExtensionService
    {
        private readonly AuthenticationConfiguration authenticationConfiguration;

        public UserExtensionService(AuthenticationConfiguration authenticationConfiguration)
        {
            this.authenticationConfiguration = authenticationConfiguration;
        }

        public bool CheckIfUserPasswordIsCorrect(string rawPassword, string hashedPassowrd)
        {
            return GetUserHashedPassword(rawPassword) == hashedPassowrd;
        }

        public TokenDto GenerateUserAccessToken(DomainModels.User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(authenticationConfiguration.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                    new Claim(ClaimTypes.Role, user.Type.ToIntegerString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return new TokenDto { Bearer = tokenHandler.WriteToken(token) };
        }

        public string GetUserHashedPassword(string rawPassword)
        {
            var salt = Encoding.ASCII.GetBytes(authenticationConfiguration.Secret.Substring(0, 16));
            // derive a 256-bit subkey (use HMACSHA1 with 10,000 iterations)
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: rawPassword,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));
            return hashed;
        }
    }
}
