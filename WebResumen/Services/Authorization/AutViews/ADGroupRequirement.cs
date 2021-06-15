using Microsoft.AspNetCore.Authorization;

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
