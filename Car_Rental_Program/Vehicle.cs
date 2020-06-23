using Car_Rental_Program;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;

namespace Car_Rental_Program
{

    public class Vehicle : Object, IDataErrorInfo
    {
        private int id;
        private string manufacturer;
        private string model;
        private int makeYear;
        private string registrationNumber;
        private int odometerReading;
        private int tankCapacity;
        private int revenue;
        private int lastService;

        // DONE add Registration Number 
        // DONE add variable for OdometerReading (in KM),
        // DONE add variable for TankCapacity (in litres)

        private static List<Vehicle> _vehicleList { get { return LoadVehicles(); } }

        public static List<Vehicle> vehicleList { get { return _vehicleList; } }

        public string Error { get { return null; } }

        public Dictionary<string, string> ErrorCollection { get; private set; } = new Dictionary<string, string>();

        public string this[string vehicle]
        {
            get
            {
                string result = null;
                switch (vehicle)
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

                    case "TankCapacity":
                        if (string.IsNullOrWhiteSpace(TankCapacity.ToString()))
                            result = "it cannot be empty";
                        break;

                }
                if (ErrorCollection.ContainsKey(vehicle))
                    ErrorCollection[vehicle] = result;
                else if (result != null)
                    ErrorCollection.Add(vehicle, result);

                OnPropertyChanged("ErrorCollection");
                return result;
            }

        }

        public int Id
        {

            get { return id; }
            set { OnPropertyChanged(ref id, value); }
        }
        public string Manufacturer
        {
            get { return manufacturer; }

            set
            {
                OnPropertyChanged(ref manufacturer, value);
            }
        }

        public string Model
        {
            get { return model; }

            set
            {
                OnPropertyChanged(ref model, value);
            }
        }

        public int MakeYear
        {
            get { return makeYear; }

            set
            {
                OnPropertyChanged(ref makeYear, value);
            }
        }

        public string RegistrationNumber
        {
            get { return registrationNumber; }

            set
            {
                OnPropertyChanged(ref registrationNumber, value);
            }
        }

        public int OdometerReading
        {
            get { return odometerReading; }

            set
            {
                OnPropertyChanged(ref odometerReading, value);
            }
        }

        public int TankCapacity
        {
            get { return tankCapacity; }

            set
            {
                OnPropertyChanged(ref tankCapacity, value);
            }
        }

        public int Revenue
        {

            get { return revenue; }

            set
            {
                OnPropertyChanged(ref revenue, value);
            }

        }

        public int LastService
        {

            get { return lastService; }

            set
            {
                OnPropertyChanged(ref lastService, value);
            }

        }

        private static string clusterFolder = "C4ProgS2_TDD_AS2_Lists";
        private static string mainFolder = "Car_Rental_System";

        private static string vehicles_FileName = "vehiclesList.json";
        public static bool vListChanged = false;

        public Vehicle() { }



        /**
         * Class constructor specifying name of make (manufacturer), model and year
         * of make.
         * @param manufacturer
         * @param model
         * @param makeYear
         * @param rego
         * @param tank
         * @param revenue
         * @param lastService
         */
        public Vehicle(int id, string manufacturer, string model, int makeYear, string rego, int tank, int revenue, int lastService )
        {

            Id = id;
            Manufacturer = manufacturer;
            Model = model;
            MakeYear = makeYear;
            RegistrationNumber = rego;
            TankCapacity = tank;
            Revenue = revenue;
            LastService = lastService;

        }

        public static void AddVehicle(int id, string manufacturer, string model, int makeYear, string rego, int tank, int lastService, int revenue)//id,make,model,year,rego,tank(L),lastService(KM),revenue($)
        {
            List<Vehicle> vehicleList = _vehicleList;
            id = (vehicleList.Count > 0 ? vehicleList.Last().Id + 1 : 1);
            //var id = _vehicleList.Max(x => x.Id + 1);
            vehicleList.Add(new Vehicle(id, manufacturer, model, makeYear, rego, tank, revenue, lastService));
        }

        //convert JSon to string and write string to a file
        public static void SaveVehicles(List<Vehicle> vehicleList) 
        {
            using (StreamWriter file = File.CreateText(GetFilePath()))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, vehicleList);
            }

        }

        // convert JSON file to objects and put into list
        public static List<Vehicle> LoadVehicles() 
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

                

            }

            return vehicleList;

            // DONE Add missing getter and setter methods

            /**
             * Prints details for {@link Vehicle}
             
            void printDetails()
            {
                Console.WriteLine("Vehicle: " + makeYear + " " + manufacturer + " " + model + " " + registrationNumber + " " + revenue + " " + lastService);
                // DONE Display additional information about this vehicle
            }

            */

             

        }

        // DONE Create an addKilometers method which takes a parameter for distance travelled 
        // and adds it to the odometer reading.

        private void addOdometer(int distanceTravelled)
        {

            odometerReading += distanceTravelled;

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
                Console.WriteLine("File path invalid", error);
            }

            return sReturn;

        }

        /*
        public void showDetails()
        {
            Console.WriteLine
        }
        */

    }

}
