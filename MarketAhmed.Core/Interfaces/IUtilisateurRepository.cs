using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarketAhmed.Core.Models;


namespace MarketAhmed.Core.Interfaces
{
    public interface IUtilisateurRepository
    {
        // Récupérer tous les utilisateurs
        IEnumerable<Utilisateur> GetAll();

        // Récupérer un utilisateur par son ID
        Utilisateur? GetById(int id);

        // Récupérer un utilisateur par son nom
        Utilisateur? GetByNom(string nom);

        // Ajouter un nouvel utilisateur
        int Inserer(Utilisateur utilisateur);

        // Mettre à jour un utilisateur existant
        bool Modifier(Utilisateur utilisateur);

        // Supprimer un utilisateur par son ID
        bool Supprimer(int id);
    }
}
