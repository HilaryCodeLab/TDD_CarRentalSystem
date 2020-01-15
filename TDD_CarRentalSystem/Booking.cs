using Microsoft.Office.Interop.Excel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDD_CarRentalSystem
{
    public enum BookingType
    {
        PerDay = 0,
        PerKm = 1
    }

    public class Booking : ObservableObject, IDataErrorInfo
    {
        private int _id;
        private BookingType _bookingChoice;
        private int _startOdo;
        private DateTime _startDate;
        private DateTime _endDate;

        public string Error { get { return null; } }
        public Dictionary<string, string> ErrorCollection { get; private set; } = new Dictionary<string, string>();

        private static string clusterFolder = "C4ProgS2_TDD_AS2";
        private static string mainFolder = "CarRental_Management";
        private static string Bookings_FileName = "BookingList.json";

        public int Id
        {
            get { return _id; }
            set => OnPropertyChanged(ref _id, value);
        }

        public BookingType BookingChoice
        {
            get { return _bookingChoice; }
            set => OnPropertyChanged(ref _bookingChoice, value);
        }

        public int StartOdo
        {
            get { return _startOdo; }
            set => OnPropertyChanged(ref _startOdo, value);
        }

        public DateTime StartDate
        {
            get { return _startDate; }
            set => OnPropertyChanged(ref _startDate, value);
        }

        public DateTime EndDate
        {
            get { return _endDate; }
            set => OnPropertyChanged(ref _endDate, value);
        }

        //constructor
        public Booking(int id, BookingType bookingChoice, int startOdo, DateTime startDate, DateTime endDate)
        {
            Id = id;
            BookingChoice = bookingChoice;
            StartOdo = startOdo;
            StartDate = startDate;
            EndDate = endDate;
        }

        public string this [string name]
        {
            get
            {
               string result = null;
                switch (name)
                {
                    case "Id":
                        if (string.IsNullOrEmpty(Id.ToString()))
                            result = "It cannot be empty";
                        break;

                    case "BookingChoice":
                        if(string.IsNullOrEmpty(BookingChoice.ToString()))
                            result = "It cannot be empty";
                        break;

                    case "StartOdo":
                        if(string.IsNullOrEmpty(StartOdo.ToString()))
                            result = "It cannot be empty";
                        break;

                    case "StartDate":
                        if(string.IsNullOrEmpty(StartDate.ToString()))
                            result = "It cannot be empty";
                        break;

                    case "EndDate":
                        if (string.IsNullOrEmpty(EndDate.ToString()))
                            result = "It cannot be empty";
                        break;
                }
                
                if (ErrorCollection.ContainsKey(name))
                    ErrorCollection[name] = result;
                else if (result != null)
                    ErrorCollection.Add(name, result);

                OnPropertyChanged("ErrorCollection");
                return result;
            }
            
            
        }

        //getFileNamePath
        public static string getFileNamePath()
        {
            string sReturn = "";
            string bookingsFilePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\" + @clusterFolder
                                        + "\\" + @mainFolder;
            try
            {
                if (!Directory.Exists(bookingsFilePath))
                {
                    Directory.CreateDirectory(bookingsFilePath);
                }

                sReturn = bookingsFilePath + "\\" + Bookings_FileName;
            }

            catch (IOException e)
            {
                Console.WriteLine(e);
            }
            return sReturn;
        }

        //Save Company
        //Write booking list to file, check if file exists, if not create one
        public static void SaveBooking(List<Booking> bookingList)
        {
            using (StreamWriter file = File.CreateText(Bookings_FileName))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, bookingList);
            }

        }

        //Load Company
        public static List<Booking> LoadBooking()
        {

            List<Booking> bookingList = new List<Booking>();
            if (File.Exists(getFileNamePath()))
            {
                using (StreamReader file = File.OpenText(getFileNamePath()))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    bookingList = (List<Booking>)serializer.Deserialize(file, typeof(List<Booking>));

                }
            }
            else
            {
                bookingList.Add(new Booking(1, BookingType.PerDay, 42000, DateTime.Now, DateTime.Now));
            }

            return bookingList;

        }

    }
}
