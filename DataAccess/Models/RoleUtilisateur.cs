using System;
using System.Collections.Generic;

namespace DataAccess.Models
{
    public partial class RoleUtilisateur
    {
        public int Id { get; set; }
        public int? UtilisateurId { get; set; }
        public int? RoleId { get; set; }
    }
}
