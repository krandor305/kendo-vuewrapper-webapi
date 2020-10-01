using DataAccess;
using DataAccess.Models;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLogic.Models
{
    public class DepartementModel
    {
        private Departement _departement;

        public int Id { get {return _departement.Id; } set { _departement.Id = value; } }

        public string Nom { get { return _departement.NomDepartement; } set { _departement.NomDepartement = value; } }

        public DepartementModel()
        {
            _departement = new Departement();
        }

        public DepartementModel(Departement d)
        {
            _departement = d;
        }

        public static List<DepartementModel>GetDepartements()
        {
            var deps = Entry.GetList<Entities, Departement>().Select(o=>new DepartementModel(o)).ToList();
            return deps;
        }

        public static DepartementModel GetDepartement(int id)
        {
            var deps = Entry.Get<Entities, Departement>(o=>o.Id==id);
            return new DepartementModel(deps);
        }
    }
}
