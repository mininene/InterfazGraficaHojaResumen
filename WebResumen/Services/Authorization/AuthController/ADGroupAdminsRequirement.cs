using Microsoft.AspNetCore.Authorization;

namespace WebResumen.Services.Authorization
{

    public class ADGroupAdminsRequirement : IAuthorizationRequirement
    {

        public string adminsGroupName { get; private set; }


        public ADGroupAdminsRequirement(string AdminsGroupName)
        {

            adminsGroupName = AdminsGroupName;

        }
    }

}
