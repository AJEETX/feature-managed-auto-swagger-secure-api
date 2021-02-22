using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FeaturesEnabled.ClaimBased.AutoSwagger.Api.Extensions
{
    public static partial class FaceBookAuthorizationExtension
    {
        public static IServiceCollection AddFaceBookAuthorization(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication().AddFacebook(facebookOptions =>
            {
                facebookOptions.AppId = configuration["Authentication:Facebook:AppId"];
                facebookOptions.AppSecret = configuration["Authentication:Facebook:AppSecret"];
            });
            return services;
        }
    }
}