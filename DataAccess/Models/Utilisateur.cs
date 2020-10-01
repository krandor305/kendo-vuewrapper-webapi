using System;
using System.Collections.Generic;

namespace DataAccess.Models
{
    public partial class Utilisateur
    {
        public Utilisateur()
        {
            InverseResponsable = new HashSet<Utilisateur>();
        }

        public int Id { get; set; }
        public string Nom { get; set; }
        public int? Age { get; set; }
        public string Email { get; set; }
        public int? DepartementId { get; set; }
        public int? ResponsableId { get; set; }
        public DateTime EntryDate { get; set; }

        public virtual Utilisateur Responsable { get; set; }
        public virtual ICollection<Utilisateur> InverseResponsable { get; set; }
    }
}
