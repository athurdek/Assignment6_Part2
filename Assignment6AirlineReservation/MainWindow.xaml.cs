using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Assignment6AirlineReservation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        wndAddPassenger wndAddPass;
        clsUILogic UILogic;

        public MainWindow()
        {
            try
            {
                InitializeComponent();
                Application.Current.ShutdownMode = ShutdownMode.OnMainWindowClose;

                UILogic = new clsUILogic();

                List<clsFlightsObject> flights = UILogic.listOfFlightMethod();

                cbChooseFlight.ItemsSource = flights;
                colorSeats();
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        //TODO
        private void colorSeats()
        {
            throw new NotImplementedException();
        }

        private void cbChooseFlight_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                cbChoosePassenger.IsEnabled = true;
                gPassengerCommands.IsEnabled = true;

                if (cbChooseFlight.SelectedIndex == 1)
                {
                    CanvasA380.Visibility = Visibility.Hidden;
                    Canvas767.Visibility = Visibility.Visible;
                }
                else
                {
                    Canvas767.Visibility = Visibility.Hidden;
                    CanvasA380.Visibility = Visibility.Visible;
                }

                cbChoosePassenger.ItemsSource = UILogic.passengerPull(cbChooseFlight);

            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        private void cmdAddPassenger_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                wndAddPass = new wndAddPassenger(UILogic);
                wndAddPass.ShowDialog();
                UILogic = wndAddPass.uILogic;
                if (UILogic.bNewPassangerAdded)
                {
                    cbChooseFlight.IsEnabled = false;
                    cbChoosePassenger.IsEnabled = false;
                    cmdAddPassenger.IsEnabled = false;
                    cmdChangeSeat.IsEnabled = false;
                    cmdDeletePassenger.IsEnabled = false;
                }

            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        private void HandleError(string sClass, string sMethod, string sMessage)
        {
            try
            {
                MessageBox.Show(sClass + "." + sMethod + " -> " + sMessage);
            }
            catch (System.Exception ex)
            {
                System.IO.File.AppendAllText(@"C:\Error.txt", Environment.NewLine + "HandleError Exception: " + ex.Message);
            }
        }

        private void seatClick(object sender, MouseButtonEventArgs e)
        {
            Label holdLabel = (Label)sender;
            
            //todo make background the check to see if a passenger should be added
            //if (holdLabel = red)
            //{

            //}

            clsFlightsObject holdFlightObject = (clsFlightsObject)cbChooseFlight.SelectedItem;


            

            UILogic.insertIntoLinkTable(holdFlightObject.flightID.ToString(), Convert.ToInt32(holdLabel.Content));

            cbChoosePassenger.ItemsSource = UILogic.passengerPull(cbChooseFlight);

            cbChooseFlight.IsEnabled = true;
            cbChoosePassenger.IsEnabled = true;
            cmdAddPassenger.IsEnabled = true;
            cmdChangeSeat.IsEnabled = true;
            cmdDeletePassenger.IsEnabled = true;
        }
    }
}
