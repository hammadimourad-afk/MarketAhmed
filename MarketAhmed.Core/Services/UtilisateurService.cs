using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarketAhmed.Core.Interfaces;
using MarketAhmed.Core.Models;
using System.Security.Cryptography;


using System;
using System.Text;
using System.Security.Cryptography;
using MarketAhmed.Core.Models;
using MarketAhmed.Core.Interfaces;

namespace MarketAhmed.Core.Services
{
    public class UtilisateurService
    {
        private readonly IUtilisateurRepository _repo;

        public UtilisateurService(IUtilisateurRepository repo)
        {
            _repo = repo;
        }

        /// <summary>
        /// Authentifie un utilisateur avec son nom et mot de passe
        /// </summary>
        public Utilisateur? Authentifier(string nom, string motDePasse)
        {
            var utilisateur = _repo.GetByNom(nom);
            if (utilisateur == null) return null;

            string motDePasseHache = HacherMotDePasse(motDePasse);
            return utilisateur.MotDePasse == motDePasseHache ? utilisateur : null;
        }

        /// <summary>
        /// Hash un mot de passe en utilisant SHA256
        /// </summary>
        public static string HacherMotDePasse(string motDePasse)
        {
            using var sha256 = SHA256.Create();
            byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(motDePasse));
            return Convert.ToBase64String(bytes);
        }
    }
}
