using Microsoft.AspNetCore.Authorization;

namespace WebResumen.Services.Authorization
{

    public class ADGroupASRequirement : IAuthorizationRequirement
    {

        public string adminsGroupName { get; private set; }
        public string supervisorsGroupName { get; private set; }

        public ADGroupASRequirement(string AdminsGroupName, string SupervisorsGroupName)
        {

            adminsGroupName = AdminsGroupName;
            supervisorsGroupName = SupervisorsGroupName;
        }
    }

}
