using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeniorProjectPrototype
{
    public class SomeClass
    {
        public string Time { get; set; }
        public string Description { get; set; }

        public string toString()
        {
            return Time + " " + Description;
        }
    }
}
