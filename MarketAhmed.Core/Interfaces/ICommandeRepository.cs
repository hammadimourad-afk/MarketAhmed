using MarketAhmed.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketAhmed.Core.Interfaces
{
    public interface ICommandeRepository : IGenericRepository<Commande>
    {
        // Ajoutez des méthodes spécifiques à Commande si nécessaire, au-delà de IGenericRepository
        // ex: GetCommandesByClientId(int clientId);
    }

    public interface ICommandeDetailRepository : IGenericRepository<CommandeDetail>
    {
        IEnumerable<CommandeDetail> GetDetailsByCommandeId(int commandeId);
    }

 
    // Supposant que IGenericRepository existe et ressemble à ceci:
    public interface IGenericRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T GetById(int id);
        void Add(T entity);
        void Update(T entity);
        void Delete(int id);
    }
}