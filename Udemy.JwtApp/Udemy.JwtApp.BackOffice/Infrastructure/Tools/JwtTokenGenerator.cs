using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Udemy.JwtApp.BackOffice.Core.Application.Dtos;

namespace Udemy.JwtApp.BackOffice.Infrastructure.Tools
{
    //TOKEN OLUŞTURMA İŞLEMİ YAPACAĞIZ BURDA
    public class JwtTokenGenerator
    {
        public static JwtTokenResponse GenerateToken(CheckUserResponseDto dto)
        {
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();

            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Role, dto.Role));
            claims.Add(new Claim(ClaimTypes.Name, dto.Username));
            claims.Add(new Claim(ClaimTypes.NameIdentifier, dto.Id.ToString()));

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtTokenSettings.Key));

            SigningCredentials credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            //token sonlanma tarihi bunu frontend'de göstermek istiyorum.
            var expireDate = DateTime.UtcNow.AddMinutes(JwtTokenSettings.Expire);

            JwtSecurityToken token = new JwtSecurityToken(issuer: JwtTokenSettings.Issuer, audience: JwtTokenSettings.Audience, claims: claims, notBefore: DateTime.UtcNow, expires: expireDate, signingCredentials: credentials);

            return new JwtTokenResponse(handler.WriteToken(token), expireDate);
        }
    }
}
