using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MahApps.Metro.Controls;

namespace TDD_CarRentalSystem
{
    /// <summary>
    /// Interaction logic for Booking_List.xaml
    /// </summary>
    public partial class Booking_List : MetroWindow
    {
        public static List<Booking> bookingList = new List<Booking>();

        public Booking_List()
        {
            InitializeComponent();
            bookingList = MainWindow.bookingList;
            UpdateList();
            txtFileNameLabel.Text = Booking.getFileNamePath();
        }

        private void UpdateList (int index = 0)
        {
            lvBookingList.ItemsSource = bookingList;
            lvBookingList.Items.Refresh();
            txtRecordNumber.Text = string.Format("Record {0} of {1}", index + 1, bookingList.Count);
        }

        private void BtnNew_Click(object sender, RoutedEventArgs e)
        {
            Booking_DataEntry newWin = new Booking_DataEntry();
            newWin.ShowDialog();
            UpdateList();
        }

        private void MetroWindow_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private void BtnClearSearch_Click(object sender, RoutedEventArgs e)
        {

        }

        private void lvBookingList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void lvBookingList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }

        private void GridViewColumnHeader_Click(object sender, RoutedEventArgs e)
        {

        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            Booking booking = button.DataContext as Booking;
            int index = lvBookingList.Items.IndexOf(booking);
            Booking_DataEntry win = new Booking_DataEntry(booking, false);
            win.ShowDialog();
            UpdateList(index);

        }

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void TxtSearch_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private void MetroWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if(MessageBox.Show("Do you want to save booking list to external file?", "Save File", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
            {
                Booking.SaveBooking(bookingList);
            }
            
        }
    }
}
