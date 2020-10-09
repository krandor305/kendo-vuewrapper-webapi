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

        public int? DepartementId { 
            get { 
                return _utilisateur.DepartementId;
            } 
            set { 
                _utilisateur.DepartementId = value;
            } }

        public int? ResponsableId { get { return _utilisateur.ResponsableId; } set { _utilisateur.ResponsableId = value; } }

        public DateTime EntryDate { get { return _utilisateur.EntryDate; } set { _utilisateur.EntryDate = value; } }

        public List<RoleModel> ListeRoles { get { return listesroles; } set { listesroles = value; } }

        public List<RoleModel> listesroles;
        public DepartementModel Departement { get { return departementfield; } set { departementfield = value;DepartementId = value.Id;  } }

        public DepartementModel departementfield;



        public UtilisateurModel()
        {
            _utilisateur = new Utilisateur();
            /*met la propriété ce qui permet de lié departementId à departement*/
            Departement = new DepartementModel();
            listesroles = new List<RoleModel>();
        }

        public UtilisateurModel(Utilisateur u)
        {
            _utilisateur = u;
            ListeRoles = RoleUtilisateurModel.GetRolesbyuser(u.Id);
            Departement = DepartementModel.GetDepartement(u.DepartementId);//always put properties here
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
                    var arr = RoleUtilisateurModel.GetRoleutilisateur(_utilisateur.Id);/*.OrderBy(o=>o.)*/;
                    foreach (var roleutil in arr)
                    {
                        if(!listesroles.Any(o=>o.Id==roleutil.RoleId))//si le role n'existe pas dans la liste réele l'enlever
                        {
                            roleutil.Delete();
                        }
                    }
                    foreach (var role in listesroles)
                    {
                        if(!arr.Any(o=>o.RoleId==role.Id))//si le role existe dans la base de donnée le laisser pour eviter d'ajouter un duplicata
                        {
                            var obj = new RoleUtilisateurModel(role.Id, _utilisateur.Id);
                            obj.Save();
                        }
                    }
                    return Entry.update<Entities, Utilisateur>(ref _utilisateur);
                }
                else
                {

                    var ret=Entry.add<Entities, Utilisateur>(ref _utilisateur);
                    if (listesroles!=null)
                    {
                        foreach (var role in listesroles)
                        {
                            var obj = new RoleUtilisateurModel(role.Id, _utilisateur.Id);
                            obj.Save();
                        }
                    }
                    return ret;
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
