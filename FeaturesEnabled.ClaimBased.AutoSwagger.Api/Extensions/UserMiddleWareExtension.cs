using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace FeaturesEnabled.ClaimBased.AutoSwagger.Api.Extensions
{
    public static class UserMiddleWareExtension
    {
        public static IApplicationBuilder UseMiddleWares(this IApplicationBuilder app, IWebHostEnvironment env)
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
            return app;
        }
    }
}