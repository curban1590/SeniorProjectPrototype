using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeniorProjectPrototype
{
    public class Service
    {
        public string service { get; set; }
        public int quantity { get; set; }
        public int duration { get; set; }
        public int price { get; set; }

        public Service()
        {
            service = "";
            quantity = 0;
            duration = 0;
            price = 0;
        }
    }
}
