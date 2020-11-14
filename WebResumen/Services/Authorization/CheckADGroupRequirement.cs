using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebResumen.Services.Authorization
{
    public class CheckADGroupRequirement : IAuthorizationRequirement
    {
        public string GroupName { get; private set; }

        public CheckADGroupRequirement(string groupName)
        {
            GroupName = groupName;
        }
    }
}
