using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketAhmed.Core.Models
{
    internal class MouvementStock
    {
        public int IdMouvement { get; set; }
        public string TypeMouvement { get; set; }
        public int Quantite { get; set; }
        public string DateMouvement { get; set; }
        public string Commentaire { get; set; }
        public int IdProduit { get; set; }
        public int IdUtilisateur { get; set; }
    }
}
