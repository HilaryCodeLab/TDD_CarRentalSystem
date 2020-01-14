using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using MahApps.Metro.Controls;

namespace TDD_CarRentalSystem
{
    public partial class CarList : MetroWindow
    {
        public static List<Vehicle> vehicleList = new List<Vehicle>();
        public GridViewColumnHeader listViewSortCol = null;
        public SortAdorner listViewSortAdorner = null;


        public CarList()
        {
            InitializeComponent();
            vehicleList = MainWindow.vehicleList;
            txtFileNameLabel.Text = Vehicle.GetFilePath();
            UpdateList();



        }

        private void UpdateList(int index = 0)
        {
            lvVehicleList.ItemsSource = vehicleList;
            lvVehicleList.Items.Refresh();
            txtRecordNumber.Text = string.Format("Record {0} of {1}", index + 1, vehicleList.Count);
        }


        private void BtnNew_Click(object sender, RoutedEventArgs e)
        {
            Vehicle_DataEntry winAdd = new Vehicle_DataEntry();
            winAdd.ShowDialog();
            UpdateList();
        }

        private void BtnClearSearch_Click(object sender, RoutedEventArgs e)
        {

        }

        private void lvVehicleList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lvVehicleList.SelectedIndex != -1)
            {
                txtRecordNumber.Text = string.Format("Record {0} of {1}", lvVehicleList.SelectedIndex + 1, vehicleList.Count);
            }
        }

        private void lvVehicleList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (lvVehicleList.SelectedItem != null)
            {
                Vehicle_DataEntry clickIn = new Vehicle_DataEntry((Vehicle)lvVehicleList.SelectedItem, true);
                clickIn.ShowDialog();
            }
        }

        private void EditCompanyButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            Vehicle vehicle = button.DataContext as Vehicle;
            int index = lvVehicleList.Items.IndexOf(vehicle);
            Vehicle_DataEntry window = new Vehicle_DataEntry(vehicle, false);

            window.ShowDialog();
            UpdateList(index);

        }


        private void DeleteVehicleBtn_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            Vehicle vehicle = button.DataContext as Vehicle;
            int index = lvVehicleList.Items.IndexOf(vehicle);
            vehicleList.Remove(vehicle);
            UpdateList(index);

        }


        private void GridViewColumnHeader_Click(object sender, RoutedEventArgs e)
        {
            GridViewColumnHeader column = (sender as GridViewColumnHeader);
            string sortBy = column.Tag.ToString();
            if (listViewSortCol != null)
            {
                AdornerLayer.GetAdornerLayer(listViewSortCol).Remove(listViewSortAdorner);
                lvVehicleList.Items.SortDescriptions.Clear();
            }

            ListSortDirection newDir = ListSortDirection.Ascending;
            if (listViewSortCol == column && listViewSortAdorner.Direction == newDir)
                newDir = ListSortDirection.Descending;

            listViewSortCol = column;
            listViewSortAdorner = new SortAdorner(listViewSortCol, newDir);
            AdornerLayer.GetAdornerLayer(listViewSortCol).Add(listViewSortAdorner);
            lvVehicleList.Items.SortDescriptions.Add(new SortDescription(sortBy, newDir));
        }

        public class SortAdorner : Adorner
        {
            public ListSortDirection Direction { get; set; }

            public SortAdorner(UIElement element, ListSortDirection dir)
                : base(element) => this.Direction = dir;
        }

        private void TxtSearch_KeyUp(object sender, KeyEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            vehicleList = MainWindow.vehicleList;
            if (!string.IsNullOrEmpty(textBox.Text))
            {
                vehicleList = vehicleList.Where(x => x.Manufacturer.ToUpper().Contains(textBox.Text.ToUpper())).ToList();
            }

            UpdateList();
        }
    }
}
