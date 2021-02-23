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
    public class ADGroupAdminsHandler : AuthorizationHandler<ADGroupAdminsRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ADGroupAdminsRequirement requirement)
        {

            //IHttpContextAccessor _httpContextAccessor = new HttpContextAccessor();
            //var group = new List<string>();//save all your groups' name
            //var wi = (WindowsIdentity)context.User.Identity;
            //string dominio = @"global.baxter.com";
            //UserPrincipal user = null;
            //string userName = _httpContextAccessor.HttpContext.Session.GetString("SessionUser");
            //PrincipalContext ctx = new PrincipalContext(ContextType.Domain, dominio);
            //UserPrincipal usuario = UserPrincipal.FindByIdentity(ctx, userName);

            //if (usuario != null)
            //{
            //    //PrincipalSearchResult<Principal> groups = user.GetGroups();
            //    PrincipalSearchResult<Principal> groups = usuario.GetAuthorizationGroups();
            //    foreach (GroupPrincipal g in groups)
            //    {
            //        try
            //        {
            //            Console.WriteLine(g.Name);
            //            if (g.Name.Contains(requirement.adminsGroupName))//do the check
            //            {
            //                context.Succeed(requirement);
            //            }

            //            // group.Add(group.ToString());
            //        }
            //        catch (Exception e)
            //        {
            //            // ignored
            //        }
            //    }


            //}
            //return Task.CompletedTask;
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
                if (groups.Contains(requirement.adminsGroupName))//do the check
                {
                    context.Succeed(requirement);
                }
            }

            return Task.CompletedTask;
        }
    }
 }

