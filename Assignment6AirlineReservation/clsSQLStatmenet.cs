using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment6AirlineReservation
{
    class clsSQLStatmenet
    {

        /// <summary>
        /// General string to return items
        /// </summary>
        private string sSQLTOReturn;


        public clsSQLStatmenet()
        {
        }

        /// <summary>
        ///  Get the flights 
        /// </summary>
        /// <returns></returns>
        public string returnFlightInfo()
        {
            return "SELECT Flight_ID, Flight_Number, Aircraft_Type FROM FLIGHT";
        }

        /// <summary>
        ///Get the passengers for Flight ID
        /// </summary>
        /// <param name="flightID"></param>
        /// <returns></returns>
        public string getPassengerforFlight(int flightID)
        {
            sSQLTOReturn = "SELECT PASSENGER.Passenger_ID, First_Name, Last_Name, Seat_Number " +
               "FROM FLIGHT_PASSENGER_LINK, FLIGHT, PASSENGER " +
           "WHERE FLIGHT.FLIGHT_ID = FLIGHT_PASSENGER_LINK.FLIGHT_ID AND " +
           "FLIGHT_PASSENGER_LINK.PASSENGER_ID = PASSENGER.PASSENGER_ID AND " +
           "FLIGHT.FLIGHT_ID = " + flightID.ToString();

            return sSQLTOReturn;
        }

        /// <summary>
        /// Updating seat numbers
        /// </summary>
        /// <param name="seatNumber"></param>
        /// <param name="flightID"></param>
        /// <param name="passengerID"></param>
        /// <returns></returns>
        public string updateSeatNum(int seatNumber, int flightID, int passengerID)
        {
            sSQLTOReturn = "UPDATE FLIGHT_PASSENGER_LINK " +
                    "SET Seat_Number = '" + seatNumber.ToString() + "' " +
                    "WHERE FLIGHT_ID = " + flightID.ToString() + " AND PASSENGER_ID = " + passengerID.ToString();

            return sSQLTOReturn;
        }

        /// <summary>
        /// Inserting a passenger
        /// </summary>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <returns></returns>
        public string insertAPassenger(string firstName, string lastName)
        {
            return "INSERT INTO PASSENGER(First_Name, Last_Name) VALUES('"+ firstName +"','" + lastName + "')";
        }

        //Insert into the link table
        //need to figure out how this will work first
        //public string insertIntoLinkTable(string sFlighID, int iPassengerID, int iSeatNumber)
        //{
        //    return "INSERT INTO Flight_Passenger_Link("+ sFlighID +", "+ iPassengerID+", "+ iSeatNumber+") " +
        //       "VALUES( 1 , 6 , 3)";
        //}





        ////Deleting the link
        //string sSQL = "Delete FROM FLIGHT_PASSENGER_LINK " +
        //           "WHERE FLIGHT_ID = 1 AND " +
        //           "PASSENGER_ID = 6";


        //        //Delete the passenger
        //        sSQL  = "Delete FROM PASSENGER " +
        //	"WHERE PASSENGER_ID = 6";


        ////To Insert a new passenger
        //   INSERT INTO PASSENGER(First_Name, Last_Name) VALUES('FirstName','LastName');



        //        //Then you need to query back out the Passenger_ID by executing the statement:
        //        //Get the passenger's ID
        //        sSQL = "SELECT Passenger_ID from Passenger where First_Name = 'Shawn' AND Last_Name = 'Cowder'";


    }
}
