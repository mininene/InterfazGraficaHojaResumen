using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Threading.Tasks;

namespace WebResumen.Services.Authorization
{
    public class ADGroupAllHandler : AuthorizationHandler<ADGroupAllRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ADGroupAllRequirement requirement)
        {





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
                if (groups.Contains(requirement.usersGroupName) || groups.Contains(requirement.supervisorsGroupName) || groups.Contains(requirement.adminsGroupName))//do the check
                {
                    context.Succeed(requirement);
                }
            }
            return Task.CompletedTask;

            // IHttpContextAccessor _httpContextAccessor = new HttpContextAccessor();
            //// List<string> group = new List<string>();//save all your groups' name
            //// var wi = (WindowsIdentity)context.User.Identity;

            // string dominio = @"global.baxter.com";
            // UserPrincipal user = null;
            // string userName = "fuentei3";
            //     //_httpContextAccessor.HttpContext.Session.GetString("SessionName");
            // PrincipalContext ctx = new PrincipalContext(ContextType.Domain,dominio, userName, "Cck1070-");

            // if ((user = UserPrincipal.FindByIdentity(ctx, userName)) != null)
            // {
            //     PrincipalSearchResult<Principal> groups = user.GetGroups();
            //     foreach (GroupPrincipal g in groups)
            //     {
            //         Console.WriteLine(g.Name);
            //         try
            //         {
            //             //group.Add(groups.ToString());
            //             if (g.Name.Contains(requirement.usersGroupName) || g.Name.Contains(requirement.supervisorsGroupName) || g.Name.Contains(requirement.adminsGroupName))//do the check
            //             {

            //                 context.Succeed(requirement);


            //             }
            //         }
            //         catch (Exception e)
            //         {
            //             // ignored
            //         }
            //     } 


            //}

            // return Task.CompletedTask;
        }
    }

}
