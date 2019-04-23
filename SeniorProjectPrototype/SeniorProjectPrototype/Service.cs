using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace SeniorProjectPrototype
{
    [DataContract]
    public class Service
    {
        [DataMember(Name = "service")]
        public string service { get; set; }
        public string description { get; set; }
        [DataMember(Name = "quantity")]
        public int quantity { get; set; }
        public int duration { get; set; }
        public int price { get; set; }

        private string serviceID;

        public Service()
        {
            serviceID = "";
            service = "";
            description = "";
            quantity = 0;
            duration = 0;
            price = 0;
        }

        public string getServiceID()
        {
            return serviceID;
        }

        public void setServiceID(string id)
        {
            serviceID = id;

            if(service == "")
            {
                MySqlManipulator mySqlManipulator = new MySqlManipulator();

                mySqlManipulator.login();

                service = mySqlManipulator.getServiceFor(id).service;

            }
        }
    }
}
