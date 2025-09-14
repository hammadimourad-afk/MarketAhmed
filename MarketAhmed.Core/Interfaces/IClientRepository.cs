using MarketAhmed.Core.Models;
using MarketAhmed.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketAhmed.Core.Interfaces
{
    public interface IClientRepository
    {
        IEnumerable<Client> GetAll();

        Client GetById(int id);
        void Add(Client client);
        void Update(Client client);
        void Delete(int id);
        Client GetByEmail(string email); // Utile pour vérifier l'unicité
    }
}