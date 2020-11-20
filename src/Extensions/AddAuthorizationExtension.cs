﻿using FeaturesEnabled.ClaimBased.AutoSwagger.Api.PolicyProviders;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using System.Security.Claims;

namespace FeaturesEnabled.ClaimBased.AutoSwagger.Api.Extensions
{
    public static partial class AddAuthorizationExtension
    {
        public static IServiceCollection AddRolesAndPolicyAuthorization(this IServiceCollection services)
        {
            services.AddAuthorization(
                config =>
                {
                    config.AddPolicy("ShouldBeAnAdmin", options =>
                    {
                        options.RequireAuthenticatedUser();
                        options.AuthenticationSchemes.Add(JwtBearerDefaults.AuthenticationScheme);
                        options.Requirements.Add(new ShouldBeAnAdminRequirement());
                    });

                    config.AddPolicy("ShouldBeAnEditor", options =>
                    {
                        options.RequireClaim(ClaimTypes.Role);
                        options.RequireRole("Reader");
                        options.RequireAuthenticatedUser();
                        options.AuthenticationSchemes.Add(JwtBearerDefaults.AuthenticationScheme);
                        options.Requirements.Add(new ShouldBeAReaderRequirement());
                    });

                    config.AddPolicy("ShouldBeAReader", options =>
                    {
                        options.RequireClaim(ClaimTypes.Role);
                        options.RequireRole("Reader");
                        options.RequireAuthenticatedUser();
                        options.AuthenticationSchemes.Add(JwtBearerDefaults.AuthenticationScheme);
                        options.Requirements.Add(new ShouldBeAReaderRequirement());
                    });
                    config.AddPolicy("ShouldContainRole", options => options.RequireClaim(ClaimTypes.Role));
                });

            return services;
        }
    }
}