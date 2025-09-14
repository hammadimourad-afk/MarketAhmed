using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketAhmed.Core.Models
{
    public class Client
    {
        public int IdClient { get; set; }

        [Required(ErrorMessage = "Le nom est obligatoire.")]
        [StringLength(100)]
        public string Nom { get; set; }

        [Required(ErrorMessage = "Le prénom est obligatoire.")]
        [StringLength(100)]
        public string Prenom { get; set; }

        // Nouvelle propriété calculée pour afficher le nom complet
        // C'est une propriété en lecture seule qui combine Nom et Prenom.
        // Elle est très utile pour l'affichage dans les DataGridViews et ComboBox.
        public string NomComplet
        {
            get { return $"{Nom} {Prenom}"; }
        }

        [StringLength(250)]
        public string Adresse { get; set; }

        [StringLength(20)]
        public string Telephone { get; set; }

        [Required(ErrorMessage = "L'email est obligatoire.")]
        [EmailAddress(ErrorMessage = "Format d'email invalide.")]
        [StringLength(150)]
        public string Email { get; set; }

        [StringLength(50)]
        public string StatutCompte { get; set; } = "Actif"; // Valeur par défaut

        public DateTime DateAjout { get; set; } = DateTime.Now; // Valeur par défaut
        public DateTime DateDerniereModification { get; set; } = DateTime.Now; // Valeur par défaut

        // Nouveau champ pour la localisation
        // New properties for geographical data
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
    }
}