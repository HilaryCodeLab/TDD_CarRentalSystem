using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDD_CarRentalSystem
{
    class Booking
    {
        public int Id { get; set; }
        public string BookingType { get; set; }
        public int StartOdo { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        private static string clusterFolder = "C4ProgS2_TDD_AS2";
        private static string mainFolder = "CarRental_Management";
        private static string Bookings_FileName = "BookingList.json";

        //constructor
        public Booking(int id, string bookingType, int startOdo, DateTime startDate, DateTime endDate)
        {
            Id = id;
            BookingType = bookingType;
            StartOdo = startOdo;
            StartDate = startDate;
            EndDate = endDate;
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
        public static void saveCompany(List<Booking> bookingList)
        {
            using (StreamWriter file = File.CreateText(Bookings_FileName))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, bookingList);
            }

        }

        //Load Company
        public static List<Booking> loadCompany()
        {

            List<Booking> bookingList = new List<Booking>();
            if (!File.Exists(getFileNamePath()))
            {
                using (StreamReader file = File.OpenText(Bookings_FileName))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    bookingList = (List<Booking>)serializer.Deserialize(file, typeof(List<Booking>));


                }
            }
            else
            {
                bookingList.Add(new Booking(1, "per Day", 42000, DateTime.Now, DateTime.Now));
            }

            return bookingList;

        }

    }
}
