using FeaturesEnabled.ClaimBased.AutoSwagger.Api.Extensions;
using FeaturesEnabled.ClaimBased.AutoSwagger.Api.Core.Domain;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.FeatureManagement;
using NSwag;
using NSwag.Generation.Processors.Security;
using System.Linq;

namespace FeaturesEnabled.ClaimBased.AutoSwagger.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IAuthorizeService, AuthorizeService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddCors();
            services.AddJwtBearerAuthentication();
            services.AddFeatureManagement();
            services.AddRolesAndPolicyAuthorization();
            services.AddMvc().ConfigureApiBehaviorOptions(options =>
            {
                options.SuppressMapClientErrors = true;
            });
            services.AddRouting();

            services.AddControllers();

            services.AddOpenApiDocument(document =>
            {
                document.PostProcess = document =>
                {
                    document.Info.Version = "v1";
                    document.Info.Title = "FeaturesEnabled Claim-based Autogenerated Swagger REST Api";
                    document.Info.Description = "Autogenerated swagger json file with Nswag for REST API.";
                };
                document.AddSecurity("JWT", Enumerable.Empty<string>(), new NSwag.OpenApiSecurityScheme
                {
                    Type = OpenApiSecuritySchemeType.ApiKey,
                    Name = "Authorization",
                    In = OpenApiSecurityApiKeyLocation.Header,
                    Description = "Type into the text box: Bearer {your JWT token}."
                });

                document.OperationProcessors.Add(
                    new AspNetCoreOperationSecurityScopeProcessor("JWT"));
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCors();

            app.UseAuthentication();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
            app.UseOpenApi();
            app.UseSwaggerUi3();
        }
    }
}