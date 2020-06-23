using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Car_Rental_Program;

namespace Car_Rental_Program
{


    public enum RentalType
    {

        [Description("Per Day (D)")]
        PerDay,

        [Description("Per KM (K)")]
        PerKM,

    }

    public class Rental : Object, IDataErrorInfo
    {

        private int id;
        private int startOdo;
        private DateTime startDate;
        private DateTime endDate;
        private RentalType rentalChoice;
        private double rentalCost;

        public string Error { get { return null; } }

        public Dictionary<string, string> ErrorCollection { get; private set; } = new Dictionary<string, string>();

        private static string clusterFolder = "C4ProgS2_TDD_AS2_Lists";
        private static string mainFolder = "Car_Rental_System";
        private static string Rental_FileName = "rentalList.json";
        
        // Getters and Setters
        
        public int Id
        {

            get { return id; }
            set => OnPropertyChanged(ref id, value);

        }

        public int StartOdo
        {

            get { return startOdo; }
            set => OnPropertyChanged(ref startOdo, value);

        }

        public DateTime StartDate
        {
            get { return startDate; }
            set => OnPropertyChanged(ref startDate, value);
        }

        public DateTime EndDate
        {

            get { return endDate; }
            set => OnPropertyChanged(ref endDate, value);

        }

        public RentalType RentalChoice
        {

            get { return rentalChoice; }
            set => OnPropertyChanged(ref rentalChoice, value);

        }

        public double RentalCost
        {

            get { return rentalCost; }
            set => OnPropertyChanged(ref rentalCost, value);

        }


        //Rental class constructor
        public Rental(int id, int startOdo, DateTime startdate, DateTime endDate, RentalType rentalChoice, double rentalCost)
        {

            Id = id;
            StartOdo = startOdo;
            StartDate = startDate;
            EndDate = endDate;
            RentalChoice = rentalChoice;
            RentalCost = rentalCost;

        }

        public string this[string vehicle]
        {
            get
            {
                string result = null;
                switch (vehicle)
                {
                    case "Id":
                        if (string.IsNullOrEmpty(Id.ToString()))
                            result = "It cannot be empty";
                        break;


                    case "StartOdo":
                        if (string.IsNullOrEmpty(StartOdo.ToString()))
                            result = "It cannot be empty";
                        break;

                    case "StartDate":
                        if (string.IsNullOrEmpty(StartDate.ToString()))
                            result = "It cannot be empty";
                        break;

                    case "EndDate":
                        if (string.IsNullOrEmpty(EndDate.ToString()))
                            result = "It cannot be empty";
                        break;

                    case "RentalCost":
                        if (string.IsNullOrEmpty(RentalCost.ToString()))
                            result = "It cannot be empty";
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
        
        // Locate rentalList Json file, if it doesn't exist one is made
        public static string getFileNamePath()
        {
            string sReturn = "";
            string rentalFilePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\" + @clusterFolder
                                        + "\\" + @mainFolder;
            try
            {
                if (!Directory.Exists(rentalFilePath))
                {
                    Directory.CreateDirectory(rentalFilePath);
                }

                sReturn = rentalFilePath + "\\" + Rental_FileName;
            }

            catch (IOException e)
            {
                Console.WriteLine(e);
            }
            return sReturn;
        }

        //Save 
        //Write booking list to file, check if file exists, if not create one
        public static void SaveRental(List<Rental> rentalList)
        {
            using (StreamWriter file = File.CreateText(getFileNamePath()))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, rentalList);
            }

        }
        
        //Load Rental List from documents file
        public static List<Rental> LoadRental()
        {

            List<Rental> rentalList = new List<Rental>();
            if (File.Exists(getFileNamePath()))
            {
                using (StreamReader file = File.OpenText(getFileNamePath()))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    rentalList = (List<Rental>)serializer.Deserialize(file, typeof(List<Rental>));

                }
            }
            else
            {
                rentalList.Add(new Rental(1, 42000, DateTime.Now, DateTime.Now, RentalType.PerDay, 400));
            }

            return rentalList;

        }

    }

}

