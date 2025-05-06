using CoworkingReservationSystem.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CoworkingReservationSystem.Utils
{
    public class JwtService
    {
        public string GenerateToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(EnvironmentHelper.GetJwtSecret()));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Name, user.FirstName + " " + user.LastName)
            };

            var token = new JwtSecurityToken(
                issuer: EnvironmentHelper.GetJwtIssuer(),
                audience: EnvironmentHelper.GetJwtAudience(),
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(EnvironmentHelper.GetJwtExpiryMinutes()),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}