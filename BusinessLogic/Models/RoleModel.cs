using DataAccess;
using DataAccess.Models;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLogic.Models
{
    public class RoleModel
    {
        private Role _role;

        public int Id { get {return _role.Id; } set { _role.Id = value; } }

        public string Nom { get { return _role.Nom; } set { _role.Nom = value; } }

        public RoleModel()
        {
            _role = new Role();
        }

        public RoleModel(Role r)
        {
            _role = r;
        }

        public static List<RoleModel>GetRoles()
        {
            var roles = Entry.GetList<Entities, Role>().Select(o=>new RoleModel(o)).ToList();
            return roles;
        }
    }
}
