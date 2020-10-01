using DataAccess;
using DataAccess.Models;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLogic.Models
{
    public class RoleUtilisateurModel
    {
        private RoleUtilisateur _roleutilisateur;


        public int Id { get { return _roleutilisateur.Id; } set { _roleutilisateur.Id = value; } }
        public int? UtilisateurId { get { return _roleutilisateur.UtilisateurId; } set { _roleutilisateur.UtilisateurId = value; } }
        public int? RoleId { get { return _roleutilisateur.RoleId; } set { _roleutilisateur.RoleId = value; } }

        public RoleUtilisateurModel()
        {
            _roleutilisateur = new RoleUtilisateur();
        }

        public RoleUtilisateurModel(RoleUtilisateur r)
        {
            _roleutilisateur = r;
        }

        public static List<RoleUtilisateurModel>GetRoleutilisateur()
        {
            var roles = Entry.GetList<Entities, RoleUtilisateur>().Select(o=>new RoleUtilisateurModel(o)).ToList();
            return roles;
        }

        public static List<RoleUtilisateurModel> GetRoleutilisateur(int userid)
        {
            var roles = Entry.GetList<Entities, RoleUtilisateur>(o=>o.UtilisateurId==userid).Select(o => new RoleUtilisateurModel(o)).ToList();
            return roles;
        }

            public static List<RoleModel> GetRolesbyuser(int userid)
        {
            var roleutils = Entry.GetList<Entities, RoleUtilisateur>(o => o.UtilisateurId == userid).Select(o => new RoleUtilisateurModel(o).RoleId).ToList();
            var roles = Entry.GetList<Entities, Role>(o => roleutils.Contains(o.Id)).Select(o=>new RoleModel(o)).ToList();
            return roles;
            //à ajouter au get des user
        }

    public int Delete()
        {
            try
            {
                return Entry.delete<Entities, RoleUtilisateur>(ref _roleutilisateur);
            }
            catch (Exception)
            {
                throw;
            }
        }

    
}
}
