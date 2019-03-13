using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeniorProjectPrototype
{
    class Customer
    {
        public string FName { get; set; }
        public string LName { get; set; }
        public string StreetNum { get; set; }
        public string StreetName { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string PhoneNum { get; set; }
        public string Email { get; set; }

        public string toSQLString()
        {
            return " \'" + FName + "\', \'" + LName + "\', " + StreetNum + ", \'" +
                StreetName + "\', \'" + City + "\', \'" + State + "\', \'" + Zip + "\', \'" +
                PhoneNum + "\', \'" + Email + "\'";
        }
    }
}
