﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.Threading.Tasks;

namespace WebResumen.Services.Authorization
{
    public class ADGroupUsersHandler : AuthorizationHandler<ADGroupRequirement>
    {
        private readonly IConfiguration _config;
        public ADGroupUsersHandler(IConfiguration config)
        {
            _config = config;
        }
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ADGroupRequirement requirement)
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
                    if (t.Contains(requirement.GroupName))//do the check
                    {
                        context.Succeed(requirement);
                    }
                }
            }


            return Task.CompletedTask;
        }
    }
}
