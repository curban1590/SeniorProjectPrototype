using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SeniorProjectPrototype
{
    public class Appointment
    {
        public string appointmentID { get; set; }
        public string customerName { get; set; }
        public string customerID { get; set; }
        public string employeeName { get; set; }
        public string employeeID { get; set; }
        public string status { get; set; }
        public string vin { get; set; }
        public int time { get; set; }
        public bool booked { get; set; }
        public string appointmentDescription { get; set; }
        public int duration { get; set; }
        public string date { get; set; }
        public List<Service> services { get; set; }
        public string description { get; set; }
        public Car car { get; set; }
        
        private DateTime dateTime;       

        public Appointment()
        {
            status = "";
            description = "";
            date = "";
            appointmentID = "";
            customerID = "";
            employeeID = "";
            time = 0;
            booked = false;
            appointmentDescription = "";
            duration = 0;
            dateTime = new DateTime();
            car = new Car();
            employeeName = "";
            customerName = "";
            vin = "";
            services = new List<Service>();
        }

        public void setDateTime(DateTime setDateTime)
        {
            dateTime = setDateTime;
            string minutes = dateTime.Minute.ToString();

            if(dateTime.Minute == 0)
            {
                minutes = "00";
            }

            string combinedTime = dateTime.Hour.ToString() + minutes;

            time = Convert.ToInt32(combinedTime);
        }

        public DateTime getDateTime()
        {
            return dateTime;
        }
    }
}
