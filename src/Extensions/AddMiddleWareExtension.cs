using Microsoft.Extensions.DependencyInjection;
using Microsoft.FeatureManagement;

namespace FeaturesEnabled.ClaimBased.AutoSwagger.Api.Extensions
{
    public static partial class AddMiddleWareExtension
    {
        public static IServiceCollection AddMiddleWares(this IServiceCollection services)
        {
            services.AddCors().AddCustomServices().AddJwtBearerAuthentication().AddRolesAndPolicyAuthorization().AddOpenApiDoc().AddApiVersion();
            services.AddFeatureManagement();
            services.AddMvc().ConfigureApiBehaviorOptions(options =>
            {
                options.SuppressMapClientErrors = true;
            });
            services.AddRouting();

            services.AddControllers();

            return services;
        }
    }
}