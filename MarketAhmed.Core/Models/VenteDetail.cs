using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketAhmed.Core.Models
{
    internal class VenteDetail
    {
        public int IdVenteDetail { get; set; }
        public int Quantite { get; set; }
        public double PrixUnitaire { get; set; }
        public int IdVente { get; set; }
        public int IdProduit { get; set; }
    }
}
