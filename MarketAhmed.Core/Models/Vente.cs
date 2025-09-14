using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketAhmed.Core.Models
{
    public class Vente
    {
        public int IdVente { get; set; }
        public string DateVente { get; set; }
        public double MontantTotal { get; set; }
        public string Statut { get; set; }
        public string ModePaiement { get; set; }
        public int IdClient { get; set; }
        public int IdUtilisateur { get; set; }
    }
}
