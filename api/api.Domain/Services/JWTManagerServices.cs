﻿using api.Domain.Command;
using api.Domain.Interfaces;
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

        public JWTManagerServices(
            IConfiguration iconfiguration)
        {
            this.iconfiguration = iconfiguration;
        }

        public UserTokensData GenerateRefreshToken(string userEmail)
        {
            return GenerateJWTTokens(userEmail);
        }

        public UserTokensData GenerateToken(string userEmail)
        {
            return GenerateJWTTokens(userEmail);
        }

        public UserTokensData GenerateJWTTokens(string userEmail)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.UTF8.GetBytes(iconfiguration["JWT:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] 
                {
                    new Claim(ClaimTypes.Email, userEmail)
                }),
                Expires = DateTime.Now.AddMinutes(2),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(tokenKey),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var refreshToken = GenerateRefreshToken();
                
            return new UserTokensData
            {
                AccessToken = tokenHandler.WriteToken(token),
                RefreshToken = refreshToken
            };
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
                throw new SecurityTokenException("Недопустимый токен");
            }

            return principal;
        }

        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);

            return Convert.ToBase64String(randomNumber);
        }
    }
}
