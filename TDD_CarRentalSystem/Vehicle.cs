using GalaSoft.MvvmLight;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;

namespace TDD_CarRentalSystem
{
    public enum FuelTypes
    {
        Petrol = 0,
        Diesel = 1,
        Electric = 2,
        Gas = 3
    }

    public class Vehicle : ObservableObject, IDataErrorInfo
    {
        //new code

        private string _manufacturer;
        private int _id;
        private string _model;
        private int _makeYear;
        private string _registrationNumber;
        private int _odometerReading;
        private int _tankCapacity;
        private FuelTypes _fuelChoices;
        private static List<Vehicle> _vehicleList { get { return LoadVehicles(); } }
        public static List<Vehicle> vehicleList { get { return _vehicleList; } }

        public string Error { get { return null; } }
        public Dictionary<string, string> ErrorCollection { get; private set; } = new Dictionary<string, string>();

        //old code 
        //public int Id { get; set; }
        //public string Manufacturer { get; set; }
        //public string Model { get; set; }
        //public int MakeYear { get; set; }
        //public string RegistrationNumber { get; set; }
        //public int OdometerReading { get; set; }
        //public int TankCapacity { get; set; }
        //public FuelTypes FuelChoices { get; set; }

        //new code ii

        public string this[string name]
        {
            get
            {
                string result = null;
                switch (name)
                {

                    case "RegistrationNumber":
                        if (string.IsNullOrEmpty(RegistrationNumber))
                            result = "It cannot be empty";
                        break;

                    case "Model":
                        if (string.IsNullOrEmpty(Model))
                            result = "It cannot be empty";
                        break;

                    case "Manufacturer":
                        if (string.IsNullOrWhiteSpace(Manufacturer))
                            result = "it cannot be empty";
                        else if (Manufacturer.Length < 3)
                            result = "it must be minimum of 3 characters";
                        break;

                    case "MakeYear":
                        if (string.IsNullOrWhiteSpace(MakeYear.ToString()))
                            result = "it cannot be empty";
                        break;

                    case "FuelChoices":
                        if (string.IsNullOrWhiteSpace(FuelChoices.ToString()))
                            result = "it cannot be empty";
                        break;

                    case "TankCapacity":
                        if (string.IsNullOrWhiteSpace(TankCapacity.ToString()))
                            result = "it cannot be empty";
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

        public string Manufacturer
        {
            get { return _manufacturer; }

            set
            {
                OnPropertyChanged(ref _manufacturer, value);
            }
        }

        public int Id
        {
            get { return _id; }

            set
            {
                OnPropertyChanged(ref _id, value);
            }
        }

        public string Model
        {
            get { return _model; }

            set
            {
                OnPropertyChanged(ref _model, value);
            }
        }

        public int MakeYear
        {
            get { return _makeYear; }

            set
            {
                OnPropertyChanged(ref _makeYear, value);
            }
        }

        public string RegistrationNumber
        {
            get { return _registrationNumber; }

            set
            {
                OnPropertyChanged(ref _registrationNumber, value);
            }
        }

        public int OdometerReading
        {
            get { return _odometerReading; }

            set
            {
                OnPropertyChanged(ref _odometerReading, value);
            }
        }

        public int TankCapacity
        {
            get { return _tankCapacity; }

            set
            {
                OnPropertyChanged(ref _tankCapacity, value);
            }
        }

        public FuelTypes FuelChoices
        {
            get { return _fuelChoices; }

            set
            {
                OnPropertyChanged(ref _fuelChoices, value);
            }
        }
        //Service vehicleService = new Service();

        //created at
        //updated at

        private static string clusterFolder = "C4ProgS2_TDD_AS2";
        private static string mainFolder = "CarRental_Management";
        private static string vehicles_FileName = "vehiclesList.json";
        public static bool vListChanged = false;

        public Vehicle() { }


        /**
         * Class constructor specifying name of make (manufacturer), model and year
         * of make.
         * @param manufacturer
         * @param model
         * @param makeYear
         */


        public Vehicle(int id, string manufacturer, string model, int makeYear, string rego, FuelTypes fuel, int tank)
        {
            Id = id;
            Manufacturer = manufacturer;
            Model = model;
            MakeYear = makeYear;
            RegistrationNumber = rego;
            FuelChoices = fuel;
            TankCapacity = tank;

        }

        public static void AddVehicle(string manufacturer, string model, int makeYear, string rego, FuelTypes fuel, int tank)//id,make,model,year,rego,fueltype,tank
        {
            List<Vehicle> vehicleList = _vehicleList;
            var id = (vehicleList.Count > 0 ? vehicleList.Last().Id + 1 : 1);
            //var id = _vehicleList.Max(x => x.Id + 1);
            vehicleList.Add(new Vehicle(id, manufacturer, model, makeYear, rego, fuel, tank));
        }

        public static void SaveVehicles(List<Vehicle> vehicleList) //convert JSon to string and write string to a file
        {
            using (StreamWriter file = File.CreateText(GetFilePath()))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, vehicleList);
            }

        }


        public static List<Vehicle> LoadVehicles() // convert JSON file to objects and put into list
        {
            List<Vehicle> vehicleList = new List<Vehicle>();
            if (File.Exists(GetFilePath()))
            {
                using (StreamReader file = File.OpenText(GetFilePath()))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    vehicleList = (List<Vehicle>)serializer.Deserialize(file, typeof(List<Vehicle>));
                }

            }
            else
            {
                vehicleList.Add(new Vehicle() { Id = 1, Manufacturer = "Ariel", Model = "Atom 4", MakeYear = 2019, RegistrationNumber = "1ARI444", FuelChoices = FuelTypes.Petrol, TankCapacity = 40 });
                vehicleList.Add(new Vehicle() { Id = 2, Manufacturer = "Ford", Model = "Ranger XL", MakeYear = 2015, RegistrationNumber = "1GVL526", FuelChoices = FuelTypes.Diesel, TankCapacity = 80 });
                vehicleList.Add(new Vehicle() { Id = 3, Manufacturer = "Tesla", Model = "Roadster", MakeYear = 2008, RegistrationNumber = "8HDZ576", FuelChoices = FuelTypes.Electric, TankCapacity = 0 });
                vehicleList.Add(new Vehicle() { Id = 4, Manufacturer = "Holden", Model = "Commodore LT", MakeYear = 2018, RegistrationNumber = "1GXI000", FuelChoices = FuelTypes.Diesel, TankCapacity = 61 });

            }

            return vehicleList;
        }


        public static string GetFilePath()//Locate vehicleList json file
        {
            string sReturn = "";
            string directoryPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\" + @clusterFolder + "\\" + @mainFolder;

            try
            {
                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }
                sReturn = directoryPath + "\\" + vehicles_FileName;
            }
            catch (Exception error)
            {
                Console.WriteLine("issues with file path", error);
            }

            return sReturn;

        }

      
        //public override string ToString()
        //{
        //    return Enum.GetName(typeof(FuelTypes), FuelChoices);
        //}



    }
}
