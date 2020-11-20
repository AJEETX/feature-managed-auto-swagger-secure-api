using FeaturesEnabled.ClaimBased.AutoSwagger.Api.Core.Models;
using FeaturesEnabled.ClaimBased.AutoSwagger.Api.PolicyProviders;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace FeaturesEnabled.ClaimBased.AutoSwagger.Api.Extensions
{
    public static partial class AddAuthenticationExtension
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
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Constants.key)),
                    ValidIssuer = Constants.Issuer,
                    ValidAudience = Constants.Audience
                };
            });
            services.AddScoped<IAuthorizationHandler, ShouldBeAnAdminRequirementHandler>();
            services.AddScoped<IAuthorizationHandler, ShouldBeAReaderAuthorizationHandler>();
            return services;
        }
    }
}