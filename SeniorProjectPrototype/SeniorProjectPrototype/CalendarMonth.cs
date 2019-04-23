using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeniorProjectPrototype
{
    public class CalendarMonth
    {
        private string Month = "";
        public List<string> distinctdays { get; set; }

        public void setMonth(string m)
        {
            MySqlManipulator mySqlManipulator = new MySqlManipulator();

            mySqlManipulator.login();

            Month = m;
            distinctdays = mySqlManipulator.getDistinctAppDayFor(Month);
        }

        public string getMonth()
        {
            return Month;
        }
    }
}
