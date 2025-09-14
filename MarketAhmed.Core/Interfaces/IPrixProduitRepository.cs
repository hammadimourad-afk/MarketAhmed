using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarketAhmed.Core.Models;

namespace MarketAhmed.Core.Interfaces
{
    public interface IPrixProduitRepository
    {
        PrixProduit GetCurrentPrix(int idProduit);
        IEnumerable<PrixProduit> GetHistorique(int idProduit);
        int Insert(PrixProduit prix);
        void CloseCurrentPrix(int idProduit, DateTime dateFin);
        // ✅ Nouvelle méthode
        void SupprimerPrix(int idPrixProduit);
        // ✅ Nouvelle méthode
        void ModifierPrix(int idPrixProduit, decimal prixAchat, decimal prixVente); 
    }
}
