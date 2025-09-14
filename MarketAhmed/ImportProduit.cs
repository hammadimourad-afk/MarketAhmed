using System.Windows.Forms; // pour MessageBox

using MarketAhmed.Core.Interfaces;
using MarketAhmed.Core.Models;
using MarketAhmed.Core.Services;
using MarketAhmed.Data.Repositories;
using System;
using System.IO;
using System.Linq; // important !


namespace MarketAhmed.UI.Helpers
{
    public class ImportProduit
    {
        private readonly ProduitService _produitService;
        private readonly ICategorieRepository _categorieRepo;
        private readonly IUniteRepository _uniteRepo;

        public ImportProduit(ProduitService produitService, ICategorieRepository categorieRepo, IUniteRepository uniteRepo)
        {
            _produitService = produitService;
            _categorieRepo = categorieRepo;
            _uniteRepo = uniteRepo;
        }


public void ImporterDepuisFichier(string cheminFichier)
    {
        if (!File.Exists(cheminFichier))
        {
            MessageBox.Show("Fichier introuvable : " + cheminFichier, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        using var sr = new StreamReader(cheminFichier);
        string ligne;
        bool premiereLigne = true;
        int compteur = 0; // compteur de produits ajoutés

        while ((ligne = sr.ReadLine()) != null)
        {
            if (premiereLigne)
            {
                premiereLigne = false;
                continue;
            }

            var colonnes = ligne.Split('\t'); // séparateur CSV
            if (colonnes.Length < 8) continue;

            string nom = colonnes[0].Trim();
            string description = colonnes[1].Trim();
            string codeBarre = colonnes[2].Trim();
            string categorieNom = colonnes[3].Trim();
            string uniteNom = colonnes[4].Trim();
            int quantite = int.TryParse(colonnes[5], out int q) ? q : 0;
            int seuil = int.TryParse(colonnes[6], out int s) ? s : 0;
            bool actif = colonnes[7].Trim() == "1";

            // Vérifier ou créer la catégorie
            var categorie = _categorieRepo.GetAll()
                .FirstOrDefault(c => c.Nom.Equals(categorieNom, StringComparison.OrdinalIgnoreCase));
            if (categorie == null)
            {
                categorie = new Categorie { Nom = categorieNom };
                categorie.IdCategorie = _categorieRepo.Insert(categorie);
            }

            // Vérifier ou créer l’unité
            var unite = _uniteRepo.GetAll()
                .FirstOrDefault(u => u.Nom.Equals(uniteNom, StringComparison.OrdinalIgnoreCase));
            if (unite == null)
            {
                unite = new Unite { Nom = uniteNom };
                unite.IdUnite = _uniteRepo.Insert(unite);
            }

            // Créer le produit
            var produit = new Produit
            {
                Nom = nom,
                Description = description,
                CodeBarre = codeBarre,
                IdCategorie = categorie.IdCategorie,
                IdUnite = unite.IdUnite,
                Quantite = quantite,
                SeuilAlerte = seuil,
                IsActif = actif,
                DateAjout = DateTime.Now
            };

            _produitService.AjouterProduit(produit);
            compteur++; // incrémenter le compteur
        }

        // Afficher le résultat dans MessageBox
        MessageBox.Show($"{compteur} produit(s) importé(s) avec succès !",
                        "Importation terminée",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
    }
}
}
