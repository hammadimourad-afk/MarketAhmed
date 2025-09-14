using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
    
using MarketAhmed.Core.Models;

namespace MarketAhmed.Core.Interfaces
{
    public interface ICategorieRepository
    {
        IEnumerable<Categorie> GetAll();
        Categorie GetById(int id);
        int Insert(Categorie categorie);
        void Update(Categorie categorie);
        void Delete(int id);
    }
}
