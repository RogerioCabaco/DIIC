using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DIIIC.Models
{
    public class HistorialRequests
    {
        public string medicamento { get; set; }
        public int quantidade { get; set; }

        public HistorialRequests(string v1, int v2)
        {
            medicamento = v1;
            quantidade = v2;
        }
    }
}
