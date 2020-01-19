using System;
using System.Linq;
using System.Reflection;
using System.Windows;
using DocumentFormat.OpenXml.Drawing;
using MahApps.Metro.Controls;

namespace TDD_CarRentalSystem
{
    /// <summary>
    /// Interaction logic for Booking_DataEntry.xaml
    /// </summary>
    public partial class Booking_DataEntry : MetroWindow
    {
        bool isNewBooking = true;
        bool isEmpty = false;
        Booking aBooking;


        public Booking_DataEntry()
        {
            InitializeComponent();
        }

        public Booking_DataEntry(Booking bookingParameter, bool readOnly)
        {
            InitializeComponent();

            if(bookingParameter != null)
            {
                txtCode.Text = bookingParameter.Id.ToString();
                cmBookingType.SelectedEnumeration = bookingParameter.BookingChoice.ToString();
                txtBookingStart.SelectedDate = bookingParameter.StartDate;
                txtBookingEnd.SelectedDate = bookingParameter.EndDate;
                txtStartOdo.Text =bookingParameter.StartOdo.ToString();

                aBooking = bookingParameter;
                isNewBooking = false;

                if (readOnly)
                {
                    txtCode.IsEnabled = false;
                    cmBookingType.IsEnabled = false;
                    txtBookingStart.IsEnabled = false;
                    txtBookingEnd.IsEnabled = false;
                    txtStartOdo.IsEnabled = false;
                }
                
            }
        }

        //convert string to int
        private int intParse(string text)
        {
            int integer;
            int.TryParse(text, out integer);
            return integer;
        }

        private void ValidateData()
        {
            if(string.IsNullOrEmpty(txtCode.Text) ||
               string.IsNullOrEmpty(txtBookingStart.Text) || string.IsNullOrEmpty(txtBookingEnd.Text) ||
               string.IsNullOrEmpty(txtStartOdo.Text))
            {
                MessageBox.Show("Please fill in requied details.");
                isEmpty = true;
            }
        }


        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            txtCode.Text = Booking_List.bookingList.Max(x => x.Id + 1).ToString();
            ValidateData();
            if (isNewBooking && isEmpty == false)
            {
                MainWindow.bookingList.Add(new Booking(Int32.Parse(txtCode.Text),
                    Int32.Parse(txtStartOdo.Text.Trim()), (DateTime)txtBookingStart.SelectedDate, (DateTime)txtBookingEnd.SelectedDate,
                     (BookingType)Enum.Parse(typeof(BookingType), cmBookingType.SelectedEnumeration.ToString())));

            }
            else if (!isNewBooking && isEmpty == false)
            {
                aBooking = MainWindow.bookingList.Where(x => x.Id == aBooking.Id).FirstOrDefault();
                aBooking.BookingChoice = (BookingType)cmBookingType.SelectedEnumeration;
                aBooking.StartOdo = Int32.Parse(txtStartOdo.Text);
                aBooking.StartDate = (DateTime)txtBookingStart.SelectedDate;
                aBooking.EndDate = (DateTime)txtBookingEnd.SelectedDate;
               
            }

            Close();

        }


        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

       
    }
}
