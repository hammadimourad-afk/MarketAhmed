using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MarketAhmed.Core.Models;

namespace MarketAhmed.Core.Interfaces
{
    public interface IUniteRepository
    {
        IEnumerable<Unite> GetAll();
        Unite GetById(int id);
        int Insert(Unite unite);
        void Update(Unite unite);
        void Delete(int id);
    }
}
