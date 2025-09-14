using System;
using System.Collections.Generic;
using MarketAhmed.Core.Interfaces;
using MarketAhmed.Core.Models;

namespace MarketAhmed.Core.Services
{
    public class PrixProduitService
    {
        private readonly IPrixProduitRepository _repo;

        public PrixProduitService(IPrixProduitRepository repo)
        {
            _repo = repo;
        }

        public IEnumerable<PrixProduit> HistoriquePrix(int idProduit)
        {
            return _repo.GetHistorique(idProduit);
        }

        public PrixProduit GetCurrentPrix(int idProduit)
        {
            return _repo.GetCurrentPrix(idProduit);
        }


        public void AjouterPrix(int idProduit, decimal prixAchat, decimal prixVente)
        {
            var nouveauPrix = new PrixProduit
            {
                IdProduit = idProduit,
                PrixAchat = prixAchat,
                PrixVente = prixVente,
                DateDebut = DateTime.Now,
                DateFin = null
            };

            // fermer l'ancien prix
            _repo.CloseCurrentPrix(idProduit, DateTime.Now);
            // insérer le nouveau
            _repo.Insert(nouveauPrix);
        }

        // ✅ Supprimer un prix
        public void SupprimerPrix(int idPrixProduit)
        {
            _repo.SupprimerPrix(idPrixProduit);
        }
        // ✅ Nouvelle méthode
        public void ModifierPrix(int idPrixProduit, decimal prixAchat, decimal prixVente)
        {
            _repo.ModifierPrix(idPrixProduit, prixAchat, prixVente);
        }

    }
}
   