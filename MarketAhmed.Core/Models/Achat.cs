using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketAhmed.Core.Models
{
    internal class Achat
    {
        public int IdAchat { get; set; }
        public string DateAchat { get; set; }
        public double MontantTotal { get; set; }
        public string Statut { get; set; }
        public int IdFournisseur { get; set; }
        public int IdUtilisateur { get; set; }
    }
}
