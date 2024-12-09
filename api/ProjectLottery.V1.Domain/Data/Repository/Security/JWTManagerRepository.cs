using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ProjectLottery.V1.Domain.Data.Abstractions;
using ProjectLottery.V1.Domain.DTOs.Auth;
using ProjectLottery.V1.Entities.Security;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ProjectLottery.V1.Domain.Data.Repository.Security
{
    public class JWTManagerRepository : IJWTManagerRepository
    {
        private readonly IConfiguration _configuration;

        public JWTManagerRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<Tokens> GetToken(User datos)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenValue = _configuration.GetSection("JWT:Key").Value;
            var expHours = string.IsNullOrEmpty(_configuration.GetSection("JWT:ExpirationHours").Value) ? "4" : _configuration.GetSection("JWT:ExpirationHours").Value;
            int expirationHours;
            int.TryParse(expHours, out expirationHours);
            var tokenKey = Encoding.UTF8.GetBytes(tokenValue);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("FullName", $"{datos.FirstName} {datos.LastName}"),
                    new Claim("Name", datos.FirstName),
                    new Claim("Email", datos.Email),
                    new Claim("Profile", datos.ProfileName.ToString()),
                    new Claim("UserId", datos.Id.ToString()),
                    new Claim("isFirstLogin", datos.isFirstLogin.ToString()),
                }),
                Expires = DateTime.UtcNow.AddHours(expirationHours),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };

            SecurityToken? token = null;

            await Task.Run(() =>
            {
                token = tokenHandler.CreateToken(tokenDescriptor);
            });

            return new Tokens { Token = tokenHandler.WriteToken(token) };
        }
    }
}