using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DIIIC
{
    public class RenovationStockRequest
    {
        public Pill pill { get; set; }
        public int quantidade { get; set; }

        public RenovationStockRequest(Pill med_aux, int quant_aux) {
            pill = med_aux;
            quantidade = quant_aux;
        }
    }
}
