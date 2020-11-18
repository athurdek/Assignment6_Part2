using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment6AirlineReservation
{
    class clsFlightsObject
    {
        public int flightID { get; set; }

        public string flightNumber { get; set; }

        public string aircraftType { get; set; }

        public clsFlightsObject(int flightID, string flightNumber, string aircraftType)
        {
            this.flightID = flightID;
            this.flightNumber = flightNumber;
            this.aircraftType = aircraftType;
        }

        public override string ToString()
        {
            return flightNumber + " - " + aircraftType;
        }

    }
}
