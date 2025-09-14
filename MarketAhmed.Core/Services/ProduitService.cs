using System;
using System.Collections.Generic;
using System.Linq;
using MarketAhmed.Core.Interfaces;
using MarketAhmed.Core.Models;

namespace MarketAhmed.Core.Services
{
    public class ProduitService
    {
        private readonly IProduitRepository _produitRepo;
        private readonly ICategorieRepository _categorieRepo;
        private readonly IUniteRepository _uniteRepo;
        private readonly IPrixProduitRepository _prixRepo;

        public ProduitService(IProduitRepository produitRepo,
                              ICategorieRepository categorieRepo,
                              IUniteRepository uniteRepo,
                              IPrixProduitRepository prixRepo)
        {
            _produitRepo = produitRepo;
            _categorieRepo = categorieRepo;
            _uniteRepo = uniteRepo;
            _prixRepo = prixRepo;
        }

        // ⚡ Exposer publiquement les repositories (utile pour UI)
        public ICategorieRepository CategorieRepo => _categorieRepo;
        public IUniteRepository UniteRepo => _uniteRepo;

        #region Produits
        public IEnumerable<Produit> GetAllProduits()
        {
            var produits = _produitRepo.GetAll().ToList();
            var categories = _categorieRepo.GetAll().ToList();
            var unites = _uniteRepo.GetAll().ToList();

            foreach (var p in produits)
            {
                p.CategorieNom = categories.FirstOrDefault(c => c.IdCategorie == p.IdCategorie)?.Nom ?? "";
                p.UniteNom = unites.FirstOrDefault(u => u.IdUnite == p.IdUnite)?.Nom ?? "";

                var prix = _prixRepo.GetCurrentPrix(p.IdProduit);
                p.PrixAchat = prix?.PrixAchat ?? 0;
                p.PrixVente = prix?.PrixVente ?? 0;
            }

            return produits;
        }

        // Nouvelle méthode pour récupérer un produit par son ID
        public Produit GetProduitById(int idProduit)
        {
            try
            {
                var produit = _produitRepo.GetById(idProduit);
                if (produit != null)
                {
                    produit.CategorieNom = _categorieRepo.GetById(produit.IdCategorie)?.Nom ?? "";
                    produit.UniteNom = _uniteRepo.GetById(produit.IdUnite)?.Nom ?? "";

                    var prix = _prixRepo.GetCurrentPrix(produit.IdProduit);
                    produit.PrixAchat = prix?.PrixAchat ?? 0;
                    produit.PrixVente = prix?.PrixVente ?? 0;
                }
                return produit;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erreur lors de la récupération du produit avec l'ID {idProduit} : " + ex.Message);
            }
        }


        public void AjouterProduit(Produit produit)
        {
            try
            {
                _produitRepo.Insert(produit);
            }
            catch (Exception ex)
            {
                throw new Exception("Erreur lors de l'ajout du produit : " + ex.Message);
            }
        }

        public void UpdateProduit(Produit produit)
        {
            try
            {
                _produitRepo.Update(produit);
            }
            catch (Exception ex)
            {
                throw new Exception("Erreur lors de la modification du produit : " + ex.Message);
            }
        }

        public void SupprimerProduit(int idProduit)
        {
            try
            {
                _produitRepo.Delete(idProduit);
            }
            catch (Exception ex)
            {
                throw new Exception("Erreur lors de la suppression du produit : " + ex.Message);
            }
        }
        #endregion

        #region Catégories
        public IEnumerable<Categorie> GetAllCategories()
        {
            try
            {
                return _categorieRepo.GetAll();
            }
            catch (Exception ex)
            {
                throw new Exception("Erreur lors du chargement des catégories : " + ex.Message);
            }
        }
        #endregion

        #region Unités
        public IEnumerable<Unite> GetAllUnites()
        {
            try
            {
                return _uniteRepo.GetAll();
            }
            catch (Exception ex)
            {
                throw new Exception("Erreur lors du chargement des unités : " + ex.Message);
            }
        }
        #endregion

        #region Prix
        public PrixProduit GetPrixActuel(int idProduit)
        {
            try
            {
                return _prixRepo.GetCurrentPrix(idProduit);
            }
            catch (Exception ex)
            {
                throw new Exception("Erreur lors de la récupération du prix actuel : " + ex.Message);
            }
        }

        // ⭐ NOUVELLE MÉTHODE : GetProduitByCodeBarre
        public Produit GetProduitByCodeBarre(string codeBarre)
        {
            try
            {
                var produit = _produitRepo.GetByCodeBarre(codeBarre);
                if (produit != null)
                {
                    produit.CategorieNom = _categorieRepo.GetById(produit.IdCategorie)?.Nom ?? "";
                    produit.UniteNom = _uniteRepo.GetById(produit.IdUnite)?.Nom ?? "";

                    var prix = _prixRepo.GetCurrentPrix(produit.IdProduit);
                    produit.PrixAchat = prix?.PrixAchat ?? 0;
                    produit.PrixVente = prix?.PrixVente ?? 0;
                }
                return produit;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erreur lors de la récupération du produit avec le code-barres '{codeBarre}' : " + ex.Message);
            }
        }

        public int AjouterPrix(int idProduit, decimal prixAchat, decimal prixVente)
        {
            try
            {
                var actuel = _prixRepo.GetCurrentPrix(idProduit);
                if (actuel != null)
                {
                    _prixRepo.CloseCurrentPrix(idProduit, DateTime.Now);
                }

                var nouveau = new PrixProduit
                {
                    IdProduit = idProduit,
                    PrixAchat = prixAchat,
                    PrixVente = prixVente,
                    DateDebut = DateTime.Now
                };

                return _prixRepo.Insert(nouveau);
            }
            catch (Exception ex)
            {
                throw new Exception("Erreur lors de l'ajout du prix : " + ex.Message);
            }
        }

        public IEnumerable<PrixProduit> HistoriquePrix(int idProduit)
        {
            try
            {
                return _prixRepo.GetHistorique(idProduit);
            }
            catch (Exception ex)
            {
                throw new Exception("Erreur lors de la récupération de l'historique des prix : " + ex.Message);
            }
        }
        #endregion
    }
}