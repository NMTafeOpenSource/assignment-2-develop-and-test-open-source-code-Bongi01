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


namespace Car_Rental_Program
{
    /// <summary>
    /// Interaction logic for Rental_List.xaml
    /// </summary>
    public partial class RentalList : Window
    {
        public static List<Rental> rentalList = new List<Rental>();

        public RentalList()
        {
            InitializeComponent();
            rentalList = MainWindow.rentalList;
            UpdateList();
            txtFileNameLabel.Text = Rental.getFileNamePath();
        }

        private void UpdateList(int index = 0)
        {
            lvRentalList.ItemsSource = rentalList;
            lvRentalList.Items.Refresh();
            txtRecordNumber.Text = string.Format("Record {0} of {1}", index + 1, rentalList.Count);
        }

        private void BtnNew_Click(object sender, RoutedEventArgs e)
        {
            RentalDataEntry newWin = new RentalDataEntry();
            newWin.ShowDialog();
            UpdateList();
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
            Rental rental = button.DataContext as Rental;
            int index = lvRentalList.Items.IndexOf(rental);
            RentalDataEntry win = new RentalDataEntry(rental, false);
            win.ShowDialog();
            UpdateList(index);

        }

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {

            Button button = sender as Button;
            Rental rental = button.DataContext as Rental;
            int index = lvRentalList.Items.IndexOf(rental);
            rentalList.Remove(rental);
            UpdateList(index);


        }

        private void TxtSearch_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (MessageBox.Show("Do you want to save this rental list to external file?", "Save File", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
            {
                Rental.SaveRental(rentalList);
            }

        }
    }
}