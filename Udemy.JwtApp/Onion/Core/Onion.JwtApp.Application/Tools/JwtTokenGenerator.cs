using Microsoft.IdentityModel.Tokens;
using Onion.JwtApp.Application.Dtos;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Onion.JwtApp.Application.Tools
{
    //burda amacımız token üretmek
    public static class JwtTokenGenerator
    {
        public static TokenResponseDto GenerateToken(CheckUserResponseDto dto)
        {
            var claims = new List<Claim>();
            if (!string.IsNullOrWhiteSpace(dto.Role))
            {
                claims.Add(new Claim(ClaimTypes.Role, dto.Role));
            }
            claims.Add(new Claim(ClaimTypes.NameIdentifier, dto.Id.ToString()));
            if (!string.IsNullOrWhiteSpace(dto.Username))
            {
                claims.Add(new Claim("Username", dto.Username));
            }
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtTokenDefaults.Key));
            var signinCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expireDate = DateTime.UtcNow.AddDays(JwtTokenDefaults.Expire);

            JwtSecurityToken token = new JwtSecurityToken(issuer: JwtTokenDefaults.Issuer, audience: JwtTokenDefaults.Audience, claims: claims, notBefore: DateTime.UtcNow, expires: expireDate, signingCredentials: signinCredentials);

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

            return new TokenResponseDto(tokenHandler.WriteToken(token), expireDate);
        }
    }
}
