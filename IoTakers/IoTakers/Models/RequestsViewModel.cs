using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IoTakers.Models
{
    public class RequestsViewModel
    {
        public List<HistorialRequests> historialRequests { get; set; }
        public List<Pill> pillsRegistered { get; set; }
        public List<RenovationStockRequest> renovationRequests { get; set; }

        public bool arduinoRequest { get; set; }
    }
}