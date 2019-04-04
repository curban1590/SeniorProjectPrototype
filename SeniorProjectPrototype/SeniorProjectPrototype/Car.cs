using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeniorProjectPrototype
{
    public class Car
    {
        public string VIN { get; set; }
        public string CustomerID { get; set; }
        public string Year { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }

        public string toSQLString()
        {
            return VIN + ", " + CustomerID + ", " + Year + ", \'" + Make + "\', \'" + Model + "\'";
        }
    }
}
