using MusiUploaderWeb.Interfaces;
using MusiUploaderWeb.Models.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MusiUploaderWeb.Models.Repository
{
    public class RoleRepository : IRoleRepository
    {
        private readonly MusicEntities context = new MusicEntities();
        public List<LookupRole> GetAllRoles()
        {
            var roles = context.LookupRoles.ToList();
            return roles;
        }

        public LookupRole GetRoleByName(string roleName)
        {
            var role = context.LookupRoles.Where(r => r.RoleName == roleName).FirstOrDefault();
            return role;
        }
    }
}