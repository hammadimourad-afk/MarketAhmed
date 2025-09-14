    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;


    namespace MarketAhmed.Core.Models
    {
        public class PrixProduit
        {
            public int IdPrixProduit { get; set; }
            public int IdProduit { get; set; }
            public decimal PrixAchat { get; set; }
            public decimal PrixVente { get; set; }
            public DateTime DateDebut { get; set; }
            public DateTime? DateFin { get; set; }
        }
    }
