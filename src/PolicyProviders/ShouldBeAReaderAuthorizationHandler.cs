using FeaturesEnabled.ClaimBased.AutoSwagger.Api.Core.Models;
using Microsoft.AspNetCore.Authorization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FeaturesEnabled.ClaimBased.AutoSwagger.Api.PolicyProviders
{
    public class ShouldBeAReaderRequirement : IAuthorizationRequirement
    {
    }

    public class ShouldBeAReaderAuthorizationHandler : AuthorizationHandler<ShouldBeAReaderRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ShouldBeAReaderRequirement requirement)
        {
            if (!context.User.HasClaim(x => x.Type == ClaimTypes.Email)) return Task.CompletedTask;

            var claim = context.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role);

            if (claim.Value == Roles.Reader)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}