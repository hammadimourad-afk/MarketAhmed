using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketAhmed.Core.Models
{
    internal class JournalCaisse
    {
        public int IdOpCaisse { get; set; }
        public string DateOp { get; set; }
        public string TypeOp { get; set; }
        public double Montant { get; set; }
        public string Commentaire { get; set; }
        public int IdUtilisateur { get; set; }
    }
}
