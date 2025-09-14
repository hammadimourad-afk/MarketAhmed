using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketAhmed.Core.Models
{
    public class Utilisateur
    {
        public int IdUtilisateur { get; set; }
        public string Nom { get; set; } = "";
        public string MotDePasse { get; set; } = "";
        public string Role { get; set; } = "Utilisateur";
        public bool Actif { get; set; } = true;
        public DateTime DateAjout { get; set; } = DateTime.Now;
    }
}
