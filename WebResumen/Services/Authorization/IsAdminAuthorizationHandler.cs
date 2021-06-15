using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;

namespace WebResumen.Services.Authorization
{
    public class IsAdminAuthorizationHandler : AuthorizationHandler<ADGroupRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ADGroupRequirement requirement)
        {
            if (context.User.IsInRole("ADMIN"))
            {
                context.Succeed(requirement);
            }
            return Task.FromResult(0);
        }
    }
}
