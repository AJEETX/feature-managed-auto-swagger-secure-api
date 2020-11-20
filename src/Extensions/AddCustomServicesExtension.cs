using FeaturesEnabled.ClaimBased.AutoSwagger.Api.Core.Domain;
using Microsoft.Extensions.DependencyInjection;

namespace FeaturesEnabled.ClaimBased.AutoSwagger.Api.Extensions
{
    public static partial class AddCustomServicesExtension
    {
        public static IServiceCollection AddCustomServices(this IServiceCollection services)
        {
            services.AddScoped<IAuthorizeService, AuthorizeService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IProductService, ProductService>();
            return services;
        }
    }
}