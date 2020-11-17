using FeaturesEnabled.ClaimBased.AutoSwagger.Api.Models;
using Microsoft.AspNetCore.Authorization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FeaturesEnabled.ClaimBased.AutoSwagger.Api.PolicyProviders
{
    public class ShouldBeAnAdminRequirement : IAuthorizationRequirement
    {
    }

    public class ShouldBeAnAdminRequirementHandler : AuthorizationHandler<ShouldBeAnAdminRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ShouldBeAnAdminRequirement requirement)
        {
            if (!context.User.HasClaim(x => x.Type == ClaimTypes.Role))
                return Task.CompletedTask;

            var claim = context.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role);

            if (claim.Value == Roles.Admin)
                context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
}