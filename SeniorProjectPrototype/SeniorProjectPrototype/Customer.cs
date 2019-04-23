using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace SeniorProjectPrototype
{
    [DataContract]
    public class Customer
    {
        [DataMember(Name = "ID")]
        public string ID { get; set; }
        [DataMember(Name = "FName")]
        public string FName { get; set; }
        [DataMember(Name = "LName")]
        public string LName { get; set; }

        public string StreetNum { get; set; }
        public string StreetName { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }

        [DataMember(Name = "PhoneNum")]
        public string PhoneNum { get; set; }
        [DataMember(Name = "Email")]
        public string Email { get; set; }
        public string Password { get; set; }

        public string toSQLString()
        {
            return " \'" + FName + "\', \'" + LName + "\', \'" + StreetNum + "\', \'" +
                StreetName + "\', \'" + City + "\', \'" + State + "\', \'" + Zip + "\', \'" +
                PhoneNum + "\', \'" + Email + "\'";
        }
    }
}
