using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketAhmed.Core.Models
{
    public class Produit
    {
        public int IdProduit { get; set; }
        public string Nom { get; set; }
        public string Description { get; set; }
        public string CodeBarre { get; set; }
        public int Quantite { get; set; }
        public int SeuilAlerte { get; set; }
        public bool IsActif { get; set; }
        public DateTime? DateAjout { get; set; }
        public int IdCategorie { get; set; }
        public int IdUnite { get; set; } // FK vers Unite.IdUnite
        public string? ImagePath { get; set; }      // optionnel

        // Relation vers le prix actuel (si PrixProduit est une entité séparée)
        // Assurez-vous d'avoir un IdPrixActuel si c'est une relation One-to-One ou Many-to-One
        public int? IdPrixActuel { get; set; } // Clé étrangère
        public PrixProduit PrixActuel { get; set; } // Propriété de navigation

        // 🔹 Propriétés pour afficher les noms dans le DataGridView
        public string CategorieNom { get; set; }
        public string UniteNom { get; set; }
        public decimal? PrixAchat { get; set; }
        public decimal? PrixVente { get; set; }

    }
}
