using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PreFlight.AI.Server.ModelsandRepositories.Employee
{
    [Flags]
    public enum Roles : byte
    {
        View = 1 << 0, // 1
        Edit = 1 << 1, // 2
        Delete = 1 << 2, // 4
        Share = 1 << 3, // 8
        Admin = (View | Edit | Delete | Share)
    }

    public class Role {
        public bool isInRole(Roles currentRole, Roles expectedRole)
        {
            return currentRole >= expectedRole;
        }       
    } 
}
