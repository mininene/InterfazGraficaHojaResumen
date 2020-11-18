using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;

namespace WebResumen.Services.Authorization
{
    public class ADGroupUsersHandler : AuthorizationHandler<ADGroupRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ADGroupRequirement requirement)
        {
            //var isAuthorized = context.User.IsInRole(requirement.GroupName);

            var groups = new List<string>();//save all your groups' name
            var wi = (WindowsIdentity)context.User.Identity;
            if (wi.Groups != null)
            {
                foreach (var group in wi.Groups)
                {
                    try
                    {
                        groups.Add(group.Translate(typeof(NTAccount)).ToString());
                    }
                    catch (Exception e)
                    {
                        // ignored
                    }
                }
                if (groups.Contains(requirement.GroupName))//do the check
                {
                    context.Succeed(requirement);
                }
            }

            return Task.CompletedTask;
        }
    }
}
