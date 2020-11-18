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
    class clsUILogic
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
