using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;

namespace WebResumen.Services.Authorization
{
    public class ADGroupASHandler : AuthorizationHandler<ADGroupASRequirement>
    {
        private readonly IConfiguration _config;
        public ADGroupASHandler(IConfiguration config)
        {
            _config = config;
        }
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ADGroupASRequirement requirement)
        {
            IHttpContextAccessor _httpContextAccessor = new HttpContextAccessor();
            string dominio = _config["SecuritySettings:Dominio"];
            string path = _config["SecuritySettings:ADPath"];
            var result = new List<string>();
            string userName = _httpContextAccessor.HttpContext.Session.GetString("SessionUser");
            PrincipalContext ctx = new PrincipalContext(ContextType.Domain, dominio);
            UserPrincipal usuario = UserPrincipal.FindByIdentity(ctx, userName);

            using (var searcher = new DirectorySearcher(new DirectoryEntry(path)))
            {
                searcher.Filter = String.Format("(&(objectCategory=group)(member={0}))", usuario.DistinguishedName);
                searcher.SearchScope = SearchScope.Subtree;
                searcher.PropertiesToLoad.Add("cn");

                foreach (SearchResult entry in searcher.FindAll())
                    if (entry.Properties.Contains("cn"))
                        result.Add(entry.Properties["cn"][0].ToString());

                foreach (var t in result)
                {
                    Console.WriteLine(t);
                    if (t.Contains(requirement.supervisorsGroupName) || t.Contains(requirement.adminsGroupName))//do the check//do the check
                    {
                        context.Succeed(requirement);
                    }
                }
            }
            return Task.CompletedTask;
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
            //            if (g.Name.Contains(requirement.supervisorsGroupName) || g.Name.Contains(requirement.adminsGroupName))//do the check
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
            //var groups = new List<string>();//save all your groups' name
            //var wi = (WindowsIdentity)context.User.Identity;
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
            //    if (groups.Contains(requirement.supervisorsGroupName) || groups.Contains(requirement.adminsGroupName))//do the check
            //    {
            //        context.Succeed(requirement);
            //    }
            //}

            //return Task.CompletedTask;
        }
    }
    

}
