using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace SeniorProjectPrototype
{
    [DataContract]
    public class JSONCalendar
    {
        [DataMember(Name = "AllAppointments")]
        public List<CalendarDate> AllAppointments = new List<CalendarDate>();

        public JSONCalendar()
        {
            List<CalendarMonth> distinctMonths = new List<CalendarMonth>();

            MySqlManipulator mySqlManipulator = new MySqlManipulator();

            mySqlManipulator.login();

            distinctMonths = mySqlManipulator.getDistinctAppMonths();

            foreach (CalendarMonth month in distinctMonths)
            {
                foreach (string day in month.distinctdays)
                {
                    CalendarDate date = new CalendarDate();
                    date.month = month.getMonth();
                    date.setDay(day);
                    AllAppointments.Add(date);
                }
            }
        }
    }
}
