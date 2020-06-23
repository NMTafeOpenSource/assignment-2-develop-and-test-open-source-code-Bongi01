using System;
using System.Linq;
using System.Reflection;
using System.Windows;

namespace Car_Rental_Program
{
    /// <summary>
    /// Interaction logic for RentalDataEntry.xaml
    /// </summary>
    public partial class RentalDataEntry : Window
    {
        bool isNewRental = true;
        bool isEmpty = false;
        Rental aRental;


        public RentalDataEntry()
        {
            InitializeComponent();
        }

        public RentalDataEntry(Rental rentalParameter, bool readOnly)
        {
            InitializeComponent();

            if (rentalParameter != null)
            {
                txtCode.Text = rentalParameter.Id.ToString();
                cmRentalType.SelectedEnumeration = rentalParameter.RentalChoice.ToString();
                txtRentalStart.SelectedDate = rentalParameter.StartDate;
                txtRentalEnd.SelectedDate = rentalParameter.EndDate;
                txtStartOdo.Text = rentalParameter.StartOdo.ToString();
                txt_rentalCost.Text = rentalParameter.RentalCost.ToString();

                aRental = rentalParameter;
                isNewRental = false;

                if (readOnly)
                {
                    txtCode.IsEnabled = false;
                    cmRentalType.IsEnabled = false;
                    txtRentalStart.IsEnabled = false;
                    txtRentalEnd.IsEnabled = false;
                    txtStartOdo.IsEnabled = false;
                    txt_rentalCost.IsEnabled = false;
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
            if (string.IsNullOrEmpty(txtCode.Text) ||
               string.IsNullOrEmpty(txtRentalStart.Text) || string.IsNullOrEmpty(txtRentalEnd.Text) ||
               string.IsNullOrEmpty(txtStartOdo.Text))
            {
                MessageBox.Show("Please fill in requied details.");
                isEmpty = true;
            }
        }


        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {

            ValidateData();
            if (isNewRental && isEmpty == false)
            {
                MainWindow.rentalList.Add(new Rental(Int32.Parse(txtCode.Text),
                    Int32.Parse(txtStartOdo.Text.Trim()), (DateTime)txtRentalStart.SelectedDate, (DateTime)txtRentalEnd.SelectedDate,
                     (RentalType)Enum.Parse(typeof(RentalType), cmRentalType.SelectedEnumeration.ToString()), Int32.Parse(txt_rentalCost.Text)));

            }
            else if (!isNewRental && isEmpty == false)
            {
                aRental = MainWindow.rentalList.Where(x => x.Id == aRental.Id).FirstOrDefault();
                aRental.RentalChoice = (RentalType)cmRentalType.SelectedEnumeration;
                aRental.StartOdo = Int32.Parse(txtStartOdo.Text);
                aRental.StartDate = (DateTime)txtRentalStart.SelectedDate;
                aRental.EndDate = (DateTime)txtRentalEnd.SelectedDate;
                aRental.RentalCost = Int32.Parse(txt_rentalCost.Text);

            }

            Close();

        }


        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }

}
