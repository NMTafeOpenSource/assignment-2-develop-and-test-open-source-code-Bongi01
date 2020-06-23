using Car_Rental_Program;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

namespace Car_Rental_Program
{
    public partial class MainWindow : Window
    {
        public static List<Vehicle> vehicleList;
        public static List<Rental> rentalList;

        public MainWindow()
        {

            InitializeComponent();


            vehicleList = Vehicle.LoadVehicles();


            rentalList = Rental.LoadRental();

        }


        private void btn_toVehicleList_Click(object sender, RoutedEventArgs e)
        {
            VehicleList winCarList = new VehicleList();
            winCarList.ShowDialog();

        }

        private void btn_Add_Click(object sender, RoutedEventArgs e)
        {
            VehicleDataEntry winAddVehicle = new VehicleDataEntry();
            winAddVehicle.ShowDialog();
        }

        private void Quit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void VehiclesInformationCard_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            VehicleList winList = new VehicleList();
            winList.ShowDialog();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (MessageBox.Show("Do you want to save to external file?", "Save File", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
            {

                Vehicle.SaveVehicles(vehicleList);

            }
        }

        private void BookingInformationCard_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            RentalList rentalList = new RentalList();
            rentalList.ShowDialog();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Rental.SaveRental(rentalList);
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {

            RentalList rlist = new RentalList();

            rlist.Show();

        }

        private void btn_toVehicles_Click(object sender, RoutedEventArgs e)
        {

            VehicleList toCars = new VehicleList();

            toCars.ShowDialog();
            
        }

        private void btn_toRentals_Click(object sender, RoutedEventArgs e)
        {

            RentalList toRentals = new RentalList();

            toRentals.ShowDialog();

        }
    }
}
