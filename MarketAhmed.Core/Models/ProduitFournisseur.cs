using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketAhmed.Core.Models
{
    internal class ProduitFournisseur
    {
        public int IdProduit { get; set; }
        public int IdFournisseur { get; set; }
        public double PrixAchat { get; set; }
        public string DelaiLivraison { get; set; }
    }
}
