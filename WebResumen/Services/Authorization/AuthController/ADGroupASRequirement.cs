using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
