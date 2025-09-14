using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketAhmed.Core.Models
{
    public class CommandeDetail
    {
        public int IdCommandeDetail { get; set; }
        public int Quantite { get; set; }
        public decimal PrixUnitaire { get; set; } // Changé en decimal
        public int IdCommande { get; set; }
        public int IdProduit { get; set; }
        // Ajoutez cette propriété pour stocker le nom du produit pour l'affichage
        // Ce n'est pas une colonne de la DB, mais une propriété de commodité pour le chargement
        public string NomProduit { get; set; }

        // Propriétés de navigation
        public Commande Commande { get; set; }
        public Produit Produit { get; set; } // Supposant que vous avez un modèle Produit

        // Propriété calculée (peut être faite dans le code métier ou comme une propriété en lecture seule)
        public decimal TotalLigne => Quantite * PrixUnitaire;
    }
}
