using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketAhmed.Core.Models
{

    public enum StatutCommande
    {
        EnAttente,
        EnCoursDeTraitement,
        Expediee,
        Livree,
        Annulee
    }

    public class Commande
    {
        public int IdCommande { get; set; }
        public DateTime DateCommande { get; set; } // Changé en DateTime
        public StatutCommande Statut { get; set; } // Changé en enum
        public decimal MontantTotal { get; set; } // Changé en decimal
        public string AdresseLivraison { get; set; } // Pourrait être un type Adresse complexe
        public string AdresseFacturation { get; set; } // Optionnel
        public int IdClient { get; set; }

        // Propriétés de navigation
        public Client Client { get; set; } // Supposant que vous avez un modèle Client
        public ICollection<CommandeDetail> Details { get; set; } = new List<CommandeDetail>(); // Collection des détails de la commande

        public DateTime? DateModification { get; set; } // Pour le suivi des mises à jour
    }
}
