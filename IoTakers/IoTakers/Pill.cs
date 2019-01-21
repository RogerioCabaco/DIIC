using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IoTakers
{
    public class Pill
    {
        public string Name { get; set; }

        public Pill(string med)
        {
            Name = med;
        }
    }
}
