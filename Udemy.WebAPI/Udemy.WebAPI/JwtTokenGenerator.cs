using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace Udemy.WebAPI
{
    //amacımız bir token üretmek
    public class JwtTokenGenerator
    {
        public string GenerateToken()
        {
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();

            //aslında şu yorumu yapabiliriz
            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Yavuzyavuzyavuz1."));
            //aşağıdaki metot benden bir SecurityKey ve Algoritma istiyor
            SigningCredentials credentials = new SigningCredentials(key,SecurityAlgorithms.HmacSha256);

            //writeToken bizden securityToken istiyor. JwtSecurityToken'da SecurityToken classından kalıtırlmıştır.
            //role bilgili yapmak istersem buraya claims ekliyorum
            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Role, "Member"));

            JwtSecurityToken token = new JwtSecurityToken(issuer: "http://localhost", claims:null ,audience: "http://localhost",notBefore:DateTime.Now,expires:DateTime.Now.AddMinutes(1), signingCredentials: credentials);

            return handler.WriteToken(token);
        }
    }
}
