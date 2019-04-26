using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace SeniorProjectPrototype
{
    /*
     * This class will be used to convert our database into a JSON String
     */
    [DataContract]
    public class CalendarDate
    {
        [DataMember(Name = "month")]
        public string month = "";

        [DataMember(Name = "day")]
        public string day;

        [DataMember(Name = "appointments")]
        public List<JSONAppointment> appointments = new List<JSONAppointment>();
        
        public void setDay(string d)
        {
            day = d;

            if(month != "")
            {
                MySqlManipulator mySqlManipulator = new MySqlManipulator();

                mySqlManipulator.login();

                appointments = mySqlManipulator.getJSONAppointmentsFor(month, day);
            }
        }
    }
}
