using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace SeniorProjectPrototype
{
    [DataContract]
    public class JSONAppointment
    {
        [DataMember(Name = "appointmentID")]
        public string appointmentID;

        private string customerID;
        [DataMember(Name = "customer")]
        public Customer customer = new Customer();
        
        [DataMember(Name = "employeeID")]
        public string employeeID;
        [DataMember(Name = "employeeName")]
        public string employeeName = "";

        [DataMember(Name = "appointmentHour")]
        public string appointmentHour;

        [DataMember(Name = "status")]
        public string status;

        [DataMember(Name = "description")]
        public string description;

        [DataMember(Name = "duration")]
        public string duration;
        
        private string vinNumber;
        [DataMember(Name = "car")]
        public Car car = new Car();

        [DataMember(Name = "services")]
        public List<Service> services;

        public JSONAppointment()
        {
            appointmentID = "";
            customerID = "";
            employeeID = "";
            appointmentHour = "";
            status = "";
            description = "";
            duration = "";
            vinNumber = "";
            services = new List<Service>();
        }

        public void setcustomerID(string id)
        {
            customerID = id;

            MySqlManipulator mySqlManipulator = new MySqlManipulator();

            mySqlManipulator.login();

            customer = mySqlManipulator.getCustomer(customerID);
        }

        public void setVinNumber(string vin)
        {
            vinNumber = vin;

            MySqlManipulator mySqlManipulator = new MySqlManipulator();

            mySqlManipulator.login();

            car = mySqlManipulator.getCar(vinNumber);
            setServices();
        }

        public void setEmployeeID(string id)
        {
            employeeID = id;

            MySqlManipulator mySqlManipulator = new MySqlManipulator();

            mySqlManipulator.login();

            Employee emp = mySqlManipulator.getEmployee(id);
            employeeName = emp.FName + " " + emp.LName;
        }

        public void setServices()
        {
            MySqlManipulator mySqlManipulator = new MySqlManipulator();

            mySqlManipulator.login();

            services = mySqlManipulator.getServicesFor(appointmentID);
        }
    }
}
