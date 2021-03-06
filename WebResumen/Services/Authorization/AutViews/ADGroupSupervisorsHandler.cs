﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.DirectoryServices.AccountManagement;
using System.Security.Principal;
using System.Threading.Tasks;

namespace WebResumen.Services.Authorization
{
    public class ADGroupSupervisorsHandler : AuthorizationHandler<ADGroupRequirement>
    {
        private readonly IConfiguration _config;
        public ADGroupSupervisorsHandler(IConfiguration config)
        {
            _config = config;
        }
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ADGroupRequirement requirement)
        {

            IHttpContextAccessor _httpContextAccessor = new HttpContextAccessor();
            var group = new List<string>();//save all your groups' name
            var wi = (WindowsIdentity)context.User.Identity;
            string dominio = _config["SecuritySettings:Dominio"];
            UserPrincipal user = null;
            string userName = _httpContextAccessor.HttpContext.Session.GetString("SessionUser");
            PrincipalContext ctx = new PrincipalContext(ContextType.Domain, dominio);
            UserPrincipal usuario = UserPrincipal.FindByIdentity(ctx, userName);

            if (usuario != null)
            {
                //PrincipalSearchResult<Principal> groups = user.GetGroups();
                PrincipalSearchResult<Principal> groups = usuario.GetAuthorizationGroups();
                foreach (GroupPrincipal g in groups)
                {
                    try
                    {
                        Console.WriteLine(g.Name);
                        if (g.Name.Contains(requirement.GroupName))//do the check
                        {
                            context.Succeed(requirement);
                        }

                        // group.Add(group.ToString());
                    }
                    catch (Exception e)
                    {
                        // ignored
                    }
                }

                //if (group.Contains(requirement.GroupName))//do the check
                //{
                //    context.Succeed(requirement);
                //}
            }


            return Task.CompletedTask;
        }
    }
}
