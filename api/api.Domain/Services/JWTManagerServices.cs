﻿using api.Domain.Interfaces;
using api.Domain.Model.Poco;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace api.Domain.Services
{
    public class JWTManagerServices : IJWTManager
    {
        private readonly IConfiguration iconfiguration;

        public JWTManagerServices(IConfiguration iconfiguration)
        {
            this.iconfiguration = iconfiguration;
        }

        public Tokens GenerateRefreshToken(string userEmail)
        {
            return GenerateJWTTokens(userEmail);
        }

        public Tokens GenerateToken(string userEmail)
        {
            return GenerateJWTTokens(userEmail);
        }

        public Tokens GenerateJWTTokens(string userName)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenKey = Encoding.UTF8.GetBytes(iconfiguration["JWT:Key"]);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[] 
                        {
                            new Claim(ClaimTypes.Name, userName)
                        }),
                    Expires = DateTime.Now.AddMinutes(1),
                    SigningCredentials = new SigningCredentials(
                        new SymmetricSecurityKey(tokenKey), 
                        SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                var refreshToken = GenerateRefreshToken();
                
                return new Tokens 
                { 
                    AccessToken = tokenHandler.WriteToken(token), 
                    RefreshToken = refreshToken 
                };
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var key = Encoding.UTF8.GetBytes(iconfiguration["JWT:Key"]);
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ClockSkew = TimeSpan.Zero
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(
                token, tokenValidationParameters, out SecurityToken securityToken);
            JwtSecurityToken jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(
                SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            {
                throw new SecurityTokenException("Invalid token");
            }

            return principal;
        }

        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                
                return Convert.ToBase64String(randomNumber);
            }
        }
    }
}
