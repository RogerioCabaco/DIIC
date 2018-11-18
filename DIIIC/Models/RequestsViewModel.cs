using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DIIIC.Models
{
    public class RequestsViewModel
    {
        public List<HistorialRequests> historialRequests { get; set; }
        public List<Pill> pillsRegistered { get; set; }
        public List<RenovationStockRequest> renovationRequests { get; set; }

        public RequestsViewModel() {
            // Manual config
            historialRequests = new List<HistorialRequests>();
            historialRequests.Add(new HistorialRequests("Strefan", 3));
            historialRequests.Add(new HistorialRequests("Strefan", 2));
            historialRequests.Add(new HistorialRequests("Benuron", 5));
            historialRequests.Add(new HistorialRequests("Bruffen", 2));
            historialRequests.Add(new HistorialRequests("Benuron", 3));
            // Manual config
            pillsRegistered = new List<Pill>(); 
            pillsRegistered.Add(new Pill("Bruffen"));
            pillsRegistered.Add(new Pill("Benuron"));
            pillsRegistered.Add(new Pill("Strefan"));
            // Manual config
            renovationRequests = new List<RenovationStockRequest>();
            renovationRequests.Add(new RenovationStockRequest(new Pill("Bruffen"),3));
        }
    }
}
