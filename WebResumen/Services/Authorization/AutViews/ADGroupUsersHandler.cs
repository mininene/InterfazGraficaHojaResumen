using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.DirectoryServices.AccountManagement;
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
            IHttpContextAccessor _httpContextAccessor = new HttpContextAccessor();
            var group = new List<string>();//save all your groups' name
            var wi = (WindowsIdentity)context.User.Identity;
            UserPrincipal user = null;
            string userName = _httpContextAccessor.HttpContext.Session.GetString("SessionName");
            PrincipalContext ctx = new PrincipalContext(ContextType.Domain, "somesite.org", "DC = somesite, DC = org");

            if ((user = UserPrincipal.FindByIdentity(ctx, userName)) != null)
            {
                PrincipalSearchResult<Principal> groups = user.GetGroups();
                foreach (GroupPrincipal g in groups)
                {
                    Console.WriteLine(g.Name);
                    try
                    {
                        group.Add(group.ToString());
                    }
                    catch (Exception e)
                    {
                        // ignored
                    }
                }
                if (group.Contains(requirement.GroupName))//do the check
                {
                    context.Succeed(requirement);
                }
            }
            //if (wi.Groups != null)
            //{
            //    foreach (var group in wi.Groups)
            //    {
            //        try
            //        {
            //            groups.Add(group.Translate(typeof(NTAccount)).ToString());
            //        }
            //        catch (Exception e)
            //        {
            //            // ignored
            //        }
            //    }
            //    if (groups.Contains(requirement.GroupName))//do the check
            //    {
            //        context.Succeed(requirement);
            //    }
            //}

            return Task.CompletedTask;
        }
    }
}
