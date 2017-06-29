using MusiUploaderWeb.Models.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusiUploaderWeb.Interfaces
{
    interface IRoleRepository
    {
        List<LookupRole> GetAllRoles();

        LookupRole GetRoleByName(string roleName);
    }
}
