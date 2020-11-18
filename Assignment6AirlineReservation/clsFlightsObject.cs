using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment6AirlineReservation
{
    class clsFlightsObject
    {
        int flightID { get; set; }

        string flightNumber { get; set; }

        string aircraftType { get; set; }

        public clsFlightsObject(int flightID, string flightNumber, string aircraftType)
        {
            this.flightID = flightID;
            this.flightNumber = flightNumber;
            this.aircraftType = aircraftType;
        }

    }
}
