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
        private readonly IHttpContextAccessor _httpContextAccessor;

        public  ADGroupAdminsHandler(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;

            }
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ADGroupAdminsRequirement requirement)
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
                if (groups.Contains(requirement.adminsGroupName))//do the check
                {
                    context.Succeed(requirement);
                }
            }

            return Task.CompletedTask;
        }
    }
 }

