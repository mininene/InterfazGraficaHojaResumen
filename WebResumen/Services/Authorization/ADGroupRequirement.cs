using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebResumen.Services.Authorization
{
    public class ADGroupRequirement : IAuthorizationRequirement
    {
        public string GroupName { get; private set; }

        public ADGroupRequirement(string groupName)
        {
            GroupName = groupName;
        }
    }
}
