using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Assignment6AirlineReservation
{
    public class clsUILogic
    {
        /// <summary>
        /// Creates the dataset object
        /// </summary>
        DataSet ds;

        /// <summary>
        /// The class Data object access the database
        /// </summary>
        clsDataAccess db;

        /// <summary>
        /// string for the sSQL to be sent to the database
        /// </summary>
        private string sSQL;

        /// <summary>
        /// set the num of return values
        /// </summary>
        int iNumRetValues = 0;

        public bool bNewPassangerAdded = false;

        string sPassgenerFirstName;

        string sPassgenerLastName;


        clsDataAccess clsData;

        List<clsFlightsObject> listOfFlights;

        List<clsPassangerObject> listofPassengers;

        clsSQLStatmenet sqlStatements;

        clsFlightsObject flight;

        clsPassangerObject passanger;

        public clsUILogic()
        {
            try
            { 
                sqlStatements = new clsSQLStatmenet();

                listOfFlightMethod();
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                             MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        public List<clsFlightsObject> listOfFlightMethod()
        {
            listOfFlights = new List<clsFlightsObject>();

            string sSQL = sqlStatements.returnFlightInfo();
            int iRet = 0;
            clsData = new clsDataAccess();

            //This should probably be in a new class.  Would be nice if this new class
            //returned a list of Flight objects that was then bound to the combo box
            //Also should show the flight number and aircraft type together
            ds = clsData.ExecuteSQLStatement(sSQL, ref iRet);

            for (int i = 0; i < iRet; i++)
            {
                flight = new clsFlightsObject((int)ds.Tables[0].Rows[i][0], (string)ds.Tables[0].Rows[i][1], (string)ds.Tables[0].Rows[i][2]);
                listOfFlights.Add(flight);
            }
            return listOfFlights;
        }

        public List<clsPassangerObject> passengerPull(ComboBox cbChooseFlight)
        {
            clsFlightsObject flightSeletedFlight = (clsFlightsObject)cbChooseFlight.SelectedItem;

            listofPassengers = new List<clsPassangerObject>();

            clsData = new clsDataAccess();
            int iRet = 0;

           
            string sSQL = sqlStatements.getPassengerforFlight(flightSeletedFlight.flightID);

            ds = clsData.ExecuteSQLStatement(sSQL, ref iRet);

            for (int i = 0; i < iRet; i++)
            {
                passanger = new clsPassangerObject((int)ds.Tables[0].Rows[i][0], (string)ds.Tables[0].Rows[i][1], (string)ds.Tables[0].Rows[i][2]);
                listofPassengers.Add(passanger);
            }

            return listofPassengers;
        }

        public void insertPassangerIntoDB(string firstName, string lastName)
        {
            sSQL = sqlStatements.insertAPassenger(firstName, lastName);
            this.sPassgenerFirstName = firstName;
            this.sPassgenerLastName = lastName;

            clsData = new clsDataAccess();
            int iRet = 0;
            iRet = clsData.ExecuteNonQuery(sSQL);

            bNewPassangerAdded = true;
        }


        public void insertIntoLinkTable(string sFlighID, int iSeatNumber)
        {

            int iPassengerID = getPassengerID();

            clsData = new clsDataAccess();
            sSQL = sqlStatements.insertIntoLinkTable(sFlighID, iPassengerID, iSeatNumber);

            int iRet = 0;
            iRet = clsData.ExecuteNonQuery(sSQL);

            bNewPassangerAdded = false;
        }

        public int getPassengerID()
        {
            clsData = new clsDataAccess();
            int iRet = 0;
            sSQL = sqlStatements.queryOutPassengerID(sPassgenerFirstName, sPassgenerLastName);
            ds = clsData.ExecuteSQLStatement(sSQL,ref iRet);

            return (int)ds.Tables[0].Rows[0][0];
        }

        //todo
        public void checkIfSeatsTaken(string sFlighID, int iSeatNumber)
        {

        }



        /// <summary>
        /// Handle the error.
        /// </summary>
        /// <param name="sClass">The class in which the error occurred in.</param>
        /// <param name="sMethod">The method in which the error occurred in.</param>
        private void HandleError(string sClass, string sMethod, string sMessage)
        {
            try
            {
                //Would write to a file or database here.
                MessageBox.Show(sClass + "." + sMethod + " -> " + sMessage);
            }
            catch (Exception ex)
            {
                System.IO.File.AppendAllText("C:\\Error.txt", Environment.NewLine +
                                             "HandleError Exception: " + ex.Message);
            }
        }
    }
}
