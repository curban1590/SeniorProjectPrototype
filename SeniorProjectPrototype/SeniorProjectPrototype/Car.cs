using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace SeniorProjectPrototype
{
    [DataContract]
    public class Car
    {
        [DataMember(Name = "VIN")]
        public string VIN { get; set; }
        [DataMember(Name = "CustomerID")]
        public string CustomerID { get; set; }
        [DataMember(Name = "Year")]
        public string Year { get; set; }
        [DataMember(Name = "Make")]
        public string Make { get; set; }
        [DataMember(Name = "Model")]
        public string Model { get; set; }

        public string toSQLString()
        {
            return VIN + ", " + CustomerID + ", " + Year + ", \'" + Make + "\', \'" + Model + "\'";
        }
    }
}
