using MarketAhmed.Core.Interfaces;
using MarketAhmed.Core.Models;

using System;
using System.Collections.Generic;
using System.Linq;

namespace MarketAhmed.Core.Services
{
    public class CommandeService
    {
        private readonly ICommandeRepository _commandeRepo;
        private readonly ICommandeDetailRepository _commandeDetailRepo;
        private readonly IProduitRepository _produitRepo; // Pour obtenir les détails du produit comme le prix actuel

        public CommandeService(ICommandeRepository commandeRepo,
                               ICommandeDetailRepository commandeDetailRepo,
                               IProduitRepository produitRepo)
        {
            _commandeRepo = commandeRepo;
            _commandeDetailRepo = commandeDetailRepo;
            _produitRepo = produitRepo;
        }

        public IEnumerable<Commande> GetAllCommandes()
        {
            return _commandeRepo.GetAll();
        }

        public Commande GetCommandeById(int id)
        {
            var commande = _commandeRepo.GetById(id);
            if (commande != null)
            {
                // Charger les détails associés
                commande.Details = _commandeDetailRepo.GetDetailsByCommandeId(id).ToList();
                // Assurez-vous d'obtenir les noms de produits pour l'affichage si nécessaire
                foreach (var detail in commande.Details)
                {
                    var produit = _produitRepo.GetById(detail.IdProduit);
                    if (produit != null)
                    {
                        detail.NomProduit = produit.Nom;
                    }
                }
            }
            return commande;
        }

        public int AddCommande(Commande commande)
        {
            // Validation de base
            if (commande.IdClient <= 0 || string.IsNullOrWhiteSpace(commande.AdresseLivraison))
            {
                throw new ArgumentException("Le client et l'adresse de livraison sont requis pour une nouvelle commande.");
            }
            if (!commande.Details.Any())
            {
                throw new ArgumentException("Une commande doit avoir au moins un détail de produit.");
            }

            commande.DateCommande = DateTime.Now;
            commande.Statut = StatutCommande.EnAttente; // Statut par défaut

            // Calculer le montant total à partir des détails avant de sauvegarder l'en-tête
            commande.MontantTotal = commande.Details.Sum(d => d.TotalLigne);

            _commandeRepo.Add(commande); // Cela devrait populer commande.IdCommande

            foreach (var detail in commande.Details)
            {
                detail.IdCommande = commande.IdCommande;
                _commandeDetailRepo.Add(detail);
            }
            return commande.IdCommande;
        }

        public bool UpdateCommande(Commande commande)
        {
            var existingCommande = _commandeRepo.GetById(commande.IdCommande);
            if (existingCommande == null) return false;

            // Assurez-vous que MontantTotal est recalculé lors de la mise à jour
            commande.MontantTotal = commande.Details.Sum(d => d.TotalLigne);
            commande.DateModification = DateTime.Now; // Mettre à jour la date de modification

            _commandeRepo.Update(commande);

            // Synchroniser les détails:
            // 1. Obtenir les détails existants pour cette commande
            var existingDetails = _commandeDetailRepo.GetDetailsByCommandeId(commande.IdCommande).ToList();

            // 2. Supprimer les détails qui ne sont plus dans les details de la commande mise à jour
            foreach (var detail in existingDetails.Where(ed => !commande.Details.Any(nd => nd.IdCommandeDetail == ed.IdCommandeDetail)))
            {
                _commandeDetailRepo.Delete(detail.IdCommandeDetail);
            }

            // 3. Ajouter ou mettre à jour les détails
            foreach (var detail in commande.Details)
            {
                detail.IdCommande = commande.IdCommande; // Assurez-vous de la FK correcte
                if (detail.IdCommandeDetail == 0) // Nouveau détail
                {
                    _commandeDetailRepo.Add(detail);
                }
                else // Détail existant
                {
                    _commandeDetailRepo.Update(detail);
                }
            }
            return true;
        }

        public bool DeleteCommande(int idCommande)
        {
            var existingCommande = _commandeRepo.GetById(idCommande);
            if (existingCommande == null) return false;

            // Supprimer d'abord tous les détails associés
            var details = _commandeDetailRepo.GetDetailsByCommandeId(idCommande);
            foreach (var detail in details)
            {
                _commandeDetailRepo.Delete(detail.IdCommandeDetail);
            }

            _commandeRepo.Delete(idCommande);
            return true;
        }

        // Méthodes pour gérer les détails de commande individuels
        public CommandeDetail GetCommandeDetailById(int idCommandeDetail)
        {
            return _commandeDetailRepo.GetById(idCommandeDetail);
        }

        public void AddCommandeDetail(CommandeDetail detail)
        {
            _commandeDetailRepo.Add(detail);
            // Optionnellement, mettre à jour le MontantTotal de la Commande parent ici ou dans UpdateCommande
        }

        public void UpdateCommandeDetail(CommandeDetail detail)
        {
            _commandeDetailRepo.Update(detail);
            // Optionnellement, mettre à jour le MontantTotal de la Commande parent ici ou dans UpdateCommande
        }

        public void DeleteCommandeDetail(int idCommandeDetail)
        {
            _commandeDetailRepo.Delete(idCommandeDetail);
            // Optionnellement, mettre à jour le MontantTotal de la Commande parent ici ou dans UpdateCommande
        }

        public decimal GetCurrentProduitSellingPrice(int idProduit)
        {
            // Cela utiliserait l'IProduitRepository pour obtenir le prix actuel
            var produit = _produitRepo.GetById(idProduit);
            if (produit?.PrixActuel != null)
            {
                return produit.PrixActuel.PrixVente;
            }
            // Gérer le cas où le prix n'est pas trouvé ou l'objet PrixActuel est null
            // Peut-être lancer une exception ou retourner 0m si c'est acceptable
            throw new InvalidOperationException($"Le prix actuel pour le produit {idProduit} est introuvable.");
        }
    }
}