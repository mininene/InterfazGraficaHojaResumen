using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
