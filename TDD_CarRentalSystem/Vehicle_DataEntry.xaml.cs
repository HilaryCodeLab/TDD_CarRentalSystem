using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using MahApps.Metro.Controls;

namespace TDD_CarRentalSystem
{
    /// <summary>
    /// Interaction logic for Vehicle_DataEntry.xaml
    /// </summary>
    public partial class Vehicle_DataEntry : MetroWindow
    {
        Vehicle aVehicle;
        bool isNewCar = true;
        bool isEmpty = false;

        public Vehicle_DataEntry()
        {
            InitializeComponent();

            AddFuelTypes();
            //cbx_fuelType.ItemsSource = Vehicle.GetFuelType();

        }

        public Vehicle_DataEntry(Vehicle vehicleParameters, bool readOnly)
        {
            InitializeComponent();

            if (vehicleParameters != null)
            {
                tbx_rego.Text = vehicleParameters.RegistrationNumber;
                tbx_model.Text = vehicleParameters.Model;
                tbx_make.Text = vehicleParameters.Manufacturer;
                tbx_year.Text = vehicleParameters.MakeYear.ToString();
                cbx_fuelType.SelectedIndex = intParse(vehicleParameters.FuelChoices.ToString());
                tbx_tank.Text = vehicleParameters.TankCapacity.ToString();

                aVehicle = vehicleParameters;
                isNewCar = false;

                if (readOnly)
                {
                    tbx_rego.IsEnabled = false;
                    tbx_model.IsEnabled = false;
                    tbx_make.IsEnabled = false;
                    tbx_year.IsEnabled = false;
                    cbx_fuelType.IsEnabled = false;
                    tbx_tank.IsEnabled = false;
                    btn_Add.Visibility = Visibility.Hidden;
                }

            }
        }

        private int intParse(FuelTypes fuelChoices)
        {
            int integer;
            int.TryParse(fuelChoices.ToString(), out integer);
            return integer;
        }

        private int intParse(string text)
        {
            int integer;
            int.TryParse(text, out integer);
            return integer;

        }

        void AddFuelTypes()
        {
            cbx_fuelType.ItemsSource = Enum.GetValues(typeof(FuelTypes));
        }


        private void ValidateData()
        {
            if (string.IsNullOrEmpty(tbx_rego.Text) || string.IsNullOrEmpty(tbx_model.Text) ||
                string.IsNullOrEmpty(tbx_make.Text) || string.IsNullOrEmpty(tbx_year.Text) ||
                string.IsNullOrEmpty(cbx_fuelType.Text) || string.IsNullOrEmpty(tbx_tank.Text))

            {
                MessageBox.Show("Please fill in requied details.");

                isEmpty = true;
            }

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void btn_Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();

        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {    //validate vehicle
             // add vehicle to system

            string rego = tbx_rego.Text.Trim();
            string make = tbx_make.Text.Trim();
            string model = tbx_model.Text.Trim();
            string year = tbx_year.Text.Trim();
            string tank = tbx_tank.Text.Trim();
            string fuel = cbx_fuelType.SelectedIndex.ToString();

            ValidateData();

            if (isNewCar && isEmpty == false)
            {

                MainWindow.vehicleList.Add(new Vehicle(CarList.vehicleList.Max(x => x.Id + 1),
                                                        tbx_make.Text.Trim(),
                                                        tbx_model.Text.Trim(),
                                                        Int32.Parse(tbx_year.Text.Trim()),
                                                        tbx_rego.Text.Trim(),
                                                        (FuelTypes)cbx_fuelType.SelectedIndex,
                                                        Int32.Parse(tbx_tank.Text.Trim())
                                                        ));

            }
            else if (!isNewCar && isEmpty == false)
            {

                aVehicle = MainWindow.vehicleList.Where(x => x.Id == aVehicle.Id).FirstOrDefault();
                aVehicle.RegistrationNumber = tbx_rego.Text;
                aVehicle.Model = tbx_model.Text;
                aVehicle.Manufacturer = tbx_make.Text;
                aVehicle.MakeYear = Int32.Parse(tbx_year.Text);
                aVehicle.FuelChoices = (FuelTypes)cbx_fuelType.SelectedIndex;
                aVehicle.TankCapacity = Int32.Parse(tbx_tank.Text);

                MainWindow.vehicleList.ToArray().SetValue(aVehicle, 0);

            }

            Close();

        }

    }
}
