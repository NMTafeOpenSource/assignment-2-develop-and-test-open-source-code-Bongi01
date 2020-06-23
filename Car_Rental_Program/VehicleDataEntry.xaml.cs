using Car_Rental_Program;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Car_Rental_Program
{
    /// <summary>
    /// Interaction logic for Vehicle_DataEntry.xaml
    /// </summary>
    public partial class VehicleDataEntry : Window
    {
        Vehicle aVehicle;
        bool isNewCar = true;
        bool isEmpty = false;

        public VehicleDataEntry()
        {

            InitializeComponent();

            lbl_serviceNeeded.Visibility = Visibility.Hidden;

            lbl_serviceAnswer.Visibility = Visibility.Hidden;
        }

        public VehicleDataEntry(Vehicle vehicleParameters, bool readOnly)
        {
            InitializeComponent();

            if (vehicleParameters != null)
            {
                tbx_rego.Text = vehicleParameters.RegistrationNumber;
                tbx_model.Text = vehicleParameters.Model;
                tbx_make.Text = vehicleParameters.Manufacturer;
                tbx_year.Text = vehicleParameters.MakeYear.ToString();
                tbx_tank.Text = vehicleParameters.TankCapacity.ToString();
                txt_lastservice.Text = vehicleParameters.LastService.ToString();
                txt_revenue.Text = vehicleParameters.Revenue.ToString();

                if (vehicleParameters.LastService >= 10000 || Convert.ToInt32(txt_lastservice.Text) >= 10000)
                {

                    lbl_serviceAnswer.Content = "Yes";
                    MessageBox.Show("Vehicle is unavaible for rental");

                }
                else
                {

                    lbl_serviceAnswer.Content = "No";

                }

                aVehicle = vehicleParameters;
                isNewCar = false;

                if (readOnly)
                {
                    tbx_rego.IsEnabled = false;
                    tbx_model.IsEnabled = false;
                    tbx_make.IsEnabled = false;
                    tbx_year.IsEnabled = false;
                    tbx_tank.IsEnabled = false;
                    txt_lastservice.IsEnabled = false;
                    txt_revenue.IsEnabled = false;
                    btn_Add.Visibility = Visibility.Hidden;
                }

            }
        }

        //ValidateData method used to ensure that the user provides input
        private void ValidateData()
        {
            if (string.IsNullOrEmpty(tbx_rego.Text) || string.IsNullOrEmpty(tbx_model.Text) ||
                string.IsNullOrEmpty(tbx_make.Text) || string.IsNullOrEmpty(tbx_year.Text) ||
                string.IsNullOrEmpty(tbx_tank.Text) || string.IsNullOrEmpty(txt_lastservice.Text) ||
                string.IsNullOrEmpty(txt_revenue.Text))

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
            string lastservice = txt_lastservice.Text.Trim();
            string revenue = txt_revenue.Text.Trim();

            ValidateData();
            
            if (isNewCar && isEmpty == false)
            {
                Vehicle.AddVehicle(1, tbx_make.Text.Trim(),
                                    tbx_model.Text.Trim(),
                                    Int32.Parse(tbx_year.Text.Trim()),
                                    tbx_rego.Text.Trim(),
                                    Int32.Parse(tbx_tank.Text.Trim()),
                                    Int32.Parse(txt_lastservice.Text.Trim()),
                                    Int32.Parse(txt_revenue.Text.Trim())
                                    );




                MainWindow.vehicleList.Add(new Vehicle(VehicleList.vehicleList.Count + 1,
                                                     tbx_make.Text.Trim(),
                                                     tbx_model.Text.Trim(),
                                                     Int32.Parse(tbx_year.Text.Trim()),
                                                     tbx_rego.Text.Trim(),
                                                     Int32.Parse(tbx_tank.Text.Trim()),
                                                     Int32.Parse(txt_lastservice.Text.Trim()),
                                                     Int32.Parse(txt_revenue.Text.Trim())
                                                     ));
                                                     
            }
            //update vehicle
            else if (!isNewCar && isEmpty == false)
            {

                aVehicle = MainWindow.vehicleList.Where(x => x.Id == aVehicle.Id).FirstOrDefault();
                aVehicle.RegistrationNumber = tbx_rego.Text;
                aVehicle.Model = tbx_model.Text;
                aVehicle.Manufacturer = tbx_make.Text;
                aVehicle.MakeYear = Int32.Parse(tbx_year.Text);
                aVehicle.TankCapacity = Int32.Parse(tbx_tank.Text);
                aVehicle.LastService = Int32.Parse(txt_lastservice.Text);
                aVehicle.Revenue = Int32.Parse(txt_revenue.Text);

                MainWindow.vehicleList.ToArray().SetValue(aVehicle, 0);

            }

            Close();

        }

        private void btn_Cancel_Click_1(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
