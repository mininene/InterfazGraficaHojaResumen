﻿using Microsoft.AspNetCore.Authorization;

namespace WebResumen.Services.Authorization
{

    public class ADGroupAllRequirement : IAuthorizationRequirement
    {
        public string usersGroupName { get; private set; }
        public string adminsGroupName { get; private set; }
        public string supervisorsGroupName { get; private set; }

        public ADGroupAllRequirement(string UsersGroupName, string AdminsGroupName, string SupervisorsGroupName)
        {
            usersGroupName = UsersGroupName;
            adminsGroupName = AdminsGroupName;
            supervisorsGroupName = SupervisorsGroupName;
        }
    }

}
