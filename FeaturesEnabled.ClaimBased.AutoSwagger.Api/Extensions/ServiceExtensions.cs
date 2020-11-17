using FeaturesEnabled.ClaimBased.AutoSwagger.Api.Models;
using FeaturesEnabled.ClaimBased.AutoSwagger.Api.PolicyProviders;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

namespace FeaturesEnabled.ClaimBased.AutoSwagger.Api.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddJwtBearerAuthentication(this IServiceCollection services)
        {
            var builder = services.AddAuthentication(o =>
            {
                o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            });

            builder.AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = true,
                    ValidateIssuer = true,
                    ValidateLifetime = true,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(TokenConstants.key)),
                    ValidIssuer = TokenConstants.Issuer,
                    ValidAudience = TokenConstants.Audience
                };
            });
            services.AddScoped<IAuthorizationHandler, ShouldBeAnAdminRequirementHandler>();
            services.AddScoped<IAuthorizationHandler, ShouldBeAReaderAuthorizationHandler>();
            return services;
        }

        public static IServiceCollection AddRolesAndPolicyAuthorization(this IServiceCollection services)
        {
            services.AddAuthorization(
                config =>
                {
                    // Add a new Policy with requirement to check for Admin
                    config.AddPolicy("ShouldBeAnAdmin", options =>
                    {
                        options.RequireAuthenticatedUser();
                        options.AuthenticationSchemes.Add(JwtBearerDefaults.AuthenticationScheme);
                        options.Requirements.Add(new ShouldBeAnAdminRequirement());
                    });

                    config.AddPolicy("ShouldContainRole", options => options.RequireClaim(ClaimTypes.Role));

                    config.AddPolicy("ShouldBeAReader", options =>
                    {
                        options.RequireClaim(ClaimTypes.Role);
                        options.RequireRole("Reader");
                        options.RequireAuthenticatedUser();
                        options.AuthenticationSchemes.Add(JwtBearerDefaults.AuthenticationScheme);
                        options.Requirements.Add(new ShouldBeAReaderRequirement());
                    });
                });

            return services;
        }
    }
}