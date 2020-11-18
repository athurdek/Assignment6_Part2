using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment6AirlineReservation
{
    class clsPassangerObject
    {

        public int iPassengerID { get; set; }

        public string sFirstName { get; set; }

        public string sLastName { get; set; }

        public clsPassangerObject(int iPassengerID, string sFirstName, string sLastName)
        {
            this.sFirstName = sFirstName;
            this.sLastName = sLastName;
            this.iPassengerID = iPassengerID;
        }

        public override string ToString()
        {
            return sFirstName + " " + sLastName;
        }


    }
}
