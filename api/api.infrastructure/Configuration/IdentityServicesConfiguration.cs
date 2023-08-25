using System.Text;
using api.Data;
using api.Error.Exceptions;
using api.Infrastructure.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.Tokens;

namespace api.Infrastructure.Configuration
{
    public static class IdentityServicesConfiguration
    {
        public static IServiceCollection AddIdentityServices(
            this IServiceCollection services,
            ConfigurationManager configurationManager)
        {
            services.AddIdentityCore<UserEntity>(options =>
            {
                options.User.RequireUniqueEmail = true;
                options.SignIn.RequireConfirmedEmail = true;
                options.Tokens.EmailConfirmationTokenProvider = TokenOptions.DefaultEmailProvider;
            })
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<ApiContext>()
            .AddDefaultTokenProviders();
            services.AddDataProtection();
            services.AddAuthentication(x => {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(option => {
                var Key = Encoding.UTF8.GetBytes(configurationManager.GetSection("JWT:Key").Value);
                option.SaveToken = true;
                option.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false, // on production make it true
                    ValidateAudience = false, // on production make it true
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configurationManager.GetSection("JWT:Issuer")
                        .Value,
                    ValidAudience = configurationManager.GetSection("JWT:Audience")
                        .Value,
                    IssuerSigningKey = new SymmetricSecurityKey(Key),
                    ClockSkew = TimeSpan.Zero
                };
                option.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = context => {
                        if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                        {
                            context.Response.Headers.Add("IS-TOKEN-EXPIRED", "true");
                        }

                        return Task.CompletedTask;
                    },
                    OnChallenge = context =>
                    {
                        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                        throw new UnauthorizedException("Неверна попытка");
                    },
                    OnMessageReceived = context =>
                    {
                        string BearerPrefix = "Bearer ";
                        if (context.Request.Headers.TryGetValue("Authorization", out StringValues headerValue))
                        {
                            string token = headerValue;
                            if (!string.IsNullOrEmpty(token) && token.StartsWith(BearerPrefix))
                            {
                                token = token.Substring(BearerPrefix.Length);
                            }

                            context.HttpContext.Items.Add("token", token);
                        }

                        return Task.CompletedTask;
                    }
                };
            });

            return services;
        }
    }
}
