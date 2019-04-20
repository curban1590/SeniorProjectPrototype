using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SeniorProjectPrototype
{
    public class Mechanic
    {
        public string employeeID { get; set; }
        public string employeeName { get; set; }
        private DateTime date;
        public Appointment[] timeSlots = new Appointment[33];

        public Mechanic()
        {
            date = DateTime.Today;
            employeeID = "";
            employeeName = "";
            timeSlotsSetup();
        }

        private void timeSlotsSetup()
        {
            // Start Hour
            int currentHour = 900;
            // How many hours in the work day
            int workHours = 8;

            int index = 0;

            for (int i = 0; i < workHours; i++)
            {
                timeSlots[index] = new Appointment();
                timeSlots[index].time = currentHour;
                for (int j = 15; j < 60; j += 15)
                {
                    index += 1;
                    timeSlots[index] = new Appointment();
                    timeSlots[index].time = currentHour + j;
                }
                index += 1;
                currentHour += 100;
            }
            timeSlots[index] = new Appointment();
            timeSlots[index].time = currentHour;
        }


        public void loadAppointments()
        {
            timeSlotsSetup();

            MySqlManipulator mySqlManipulator = new MySqlManipulator();

            mySqlManipulator.login();

            List<Appointment> appointments = mySqlManipulator.getAppointment(employeeID, date);

            foreach(Appointment app in appointments)
            {
                add(app.time, app.duration, app.appointmentID, app.appointmentDescription);
            }
        }

        public void setDate(DateTime setDate)
        {
            date = setDate;
            loadAppointments();
        }

        public DateTime getDate()
        {
            return date;
        }

        public void add(int startTime, int duration, string appID, string description)
        {
            // search for next appointID 
            foreach (Appointment app in timeSlots)
            {
                if (app.time >= startTime && app.time < startTime + duration)
                {
                    app.booked = true;
                    app.appointmentID = appID;
                    app.appointmentDescription = description;
                }
            }

            // Call MySqlManip to add to database
        }

        public bool canAdd(int startTime, int duration)
        {
            bool canBook = true;
            string durationStr = Convert.ToString(duration);
            string startOfTime = durationStr.Substring(0, durationStr.Length - 2);
            string endOfTime = durationStr.Substring(durationStr.Length - 2);
            int hour = Convert.ToInt32(startOfTime);
            int minutes = Convert.ToInt32(endOfTime);
            int totalDurationCount = 0;

            List<Appointment> appointmentsToCheck = new List<Appointment>();
            foreach (Appointment app in timeSlots)
            {
                if (app.time >= startTime && app.time < startTime + duration)
                {
                    appointmentsToCheck.Add(app);
                }
            }

            totalDurationCount += hour * 4;
            totalDurationCount += minutes / 15;

            if (totalDurationCount <= appointmentsToCheck.Count)
            {
                foreach (Appointment app in appointmentsToCheck)
                {
                    if (app.booked)
                    {
                        canBook = false;
                    }
                }
            }
            else
            {
                canBook = false;
            }

            return canBook;
        }

        public List<Appointment> checkHour(int hour)
        {
            List<Appointment> appointments = new List<Appointment>();
            foreach (Appointment app in timeSlots)
            {
                if (app.booked && app.time > hour && app.time < hour + 100)
                {
                    appointments.Add(app);
                }
            }
            return appointments;
        }

        public string printTimeSlots()
        {
            string str = "";

            foreach (Appointment app in timeSlots)
            {
                str += app.time +  " " + app.booked + " " + app.appointmentDescription + "\n";
            }
            return str;
        }

        public override string ToString()
        {
            return employeeName;
        }
    }
}
