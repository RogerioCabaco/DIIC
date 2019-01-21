using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IoTakers
{
    public class RenovationStockRequest
    {
        public Pill pill { get; set; }

        public int stock_days {get;set;}

        public RenovationStockRequest(Pill med_aux,int days_aux) {
            pill = med_aux;
            stock_days = days_aux;
        }
    }
}
