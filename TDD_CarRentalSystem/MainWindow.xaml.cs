using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using MahApps.Metro.Controls;
using LiveCharts;
using LiveCharts.Wpf;

namespace TDD_CarRentalSystem
{
    public partial class MainWindow : MetroWindow
    {
        public static List<Vehicle> vehicleList;
        public SeriesCollection lineData;


        public MainWindow()
        {
            InitializeComponent();


            vehicleList = Vehicle.LoadVehicles();
            VehiclesInformationCard.Title = "Vehicles";
            VehiclesInformationCard.BackgroundColour = "Purple";
            VehiclesInformationCard.ValueProperty = vehicleList.Count.ToString();

            BookingInformationCard.Title = "Booking";
            BookingInformationCard.BackgroundColour = "Green";
            //BookingInformationCard.ValueProperty = vehicleList.Count.ToString();

            lineData = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "Series 2",
                    Values = new ChartValues<double> { 6, 7, 3, 4 ,6 },
                    PointGeometry = null

                }
            };

            VehiclesInformationCard.SeriesData = lineData;

        }



        private void btn_toVehicleList_Click(object sender, RoutedEventArgs e)
        {
            CarList winCarList = new CarList();
            winCarList.ShowDialog();

        }

        private void btn_Add_Click(object sender, RoutedEventArgs e)
        {
            Vehicle_DataEntry winAddVehicle = new Vehicle_DataEntry();
            winAddVehicle.ShowDialog();
        }

        private void Quit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void VehiclesInformationCard_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            CarList winList = new CarList();
            winList.ShowDialog();
        }

        private void MetroWindow_MouseMove(object sender, MouseEventArgs e)
        {
            VehiclesInformationCard.ValueProperty = vehicleList.Count.ToString();
        }

        private void MetroWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (MessageBox.Show("Do you want to save to external file?", "Save File", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
            {
                Vehicle.SaveVehicles(vehicleList);
            }
        }

        private void BookingInformationCard_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Booking_List bookingList = new Booking_List();
            bookingList.ShowDialog();
        }
    }
}
