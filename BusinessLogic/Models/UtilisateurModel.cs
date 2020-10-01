using DataAccess;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLogic.Models
{
    public class UtilisateurModel
    {
        private Utilisateur _utilisateur;

        public int Id { get { return _utilisateur.Id; } set { _utilisateur.Id = value; } }

        public string Nom { get { return _utilisateur.Nom; } set { _utilisateur.Nom = value; } }

        public int? age { get { return _utilisateur.Age; } set { _utilisateur.Age = value; } }

        public string Email { get { return _utilisateur.Email; } set { _utilisateur.Email = value; } }

        public int? DepartementId { get { return _utilisateur.DepartementId; } set { _utilisateur.DepartementId = value; } }

        public int? ResponsableId { get { return _utilisateur.ResponsableId; } set { _utilisateur.ResponsableId = value; } }

        public DateTime EntryDate { get { return _utilisateur.EntryDate; } set { _utilisateur.EntryDate = value; } }

        public List<RoleModel> ListeRoles { get { return listesroles; } set { listesroles = value; } }

        public DepartementModel Departement { get { return departement; } set { departement = value; } }

        public List<RoleModel> listesroles;

        public DepartementModel departement;

        public UtilisateurModel()
        {
            _utilisateur = new Utilisateur();
        }

        public UtilisateurModel(Utilisateur u)
        {
            _utilisateur = u;
            listesroles = RoleUtilisateurModel.GetRolesbyuser(u.Id);
            int v = (int)u.DepartementId;
            departement = DepartementModel.GetDepartement(v);
        }

        public static List<UtilisateurModel> GetAll()
        {
            try
            {
                return Entry.GetList<Entities, Utilisateur>().Select(o => new UtilisateurModel(o)).ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int Save()
        {
            try
            {
                if (_utilisateur.Id>0)
                {
                    foreach(var roleutil in RoleUtilisateurModel.GetRoleutilisateur(_utilisateur.Id))
                    {
                        roleutil.Delete();
                    }
                    foreach(var role in listesroles)
                    {
                        var obj=Entry.Get<Entities, RoleUtilisateur>(o=>o.RoleId==role.Id);
                        Entry.delete<Entities,RoleUtilisateur>(ref obj);
                    }
                    return Entry.update<Entities, Utilisateur>(ref _utilisateur);
                }
                else
                {
                    foreach (var role in listesroles)
                    {
                        var obj = Entry.Get<Entities, RoleUtilisateur>(o => o.RoleId == role.Id);
                        Entry.delete<Entities, RoleUtilisateur>(ref obj);
                    }
                    return Entry.add<Entities, Utilisateur>(ref _utilisateur);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int Delete()
        {
            try
            {
                foreach (var roleutil in RoleUtilisateurModel.GetRoleutilisateur(_utilisateur.Id))
                {
                    roleutil.Delete();
                }
                return Entry.delete<Entities, Utilisateur>(ref _utilisateur);
            }
            catch (Exception)
            {
                throw;
            }
        }


    }
}
