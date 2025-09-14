using System;
using System.Collections.Generic;
using MarketAhmed.Core.Models;


using System.Collections.Generic;
using MarketAhmed.Core.Models;

namespace MarketAhmed.Core.Interfaces
{
    public interface IProduitRepository
    {
        IEnumerable<Produit> GetAll();
        Produit GetById(int id);
        int Insert(Produit produit);
        Produit GetByCodeBarre(string codeBarre);

        void Update(Produit produit);
        void Delete(int id);
    }
}
