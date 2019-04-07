using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SeniorProjectPrototype
{
    /// <summary>
    /// Interaction logic for AddEditVehicles.xaml
    /// </summary>
    public partial class AddEditVehicles : Page
    {
        public Customer selectedCustomer { get; set; }
        private Car selectedCar;

        public AddEditVehicles()
        {
            InitializeComponent();
            
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            ShowAll();
            main_Label.Content = "Vehicles for " + selectedCustomer.FName + " " + selectedCustomer.LName;
        }

        private void Clear_Button_Click(object sender, RoutedEventArgs e)
        {
            ClearTextBoxes();
        }

        private void AddNew_Button_Click(object sender, RoutedEventArgs e)
        {
            Car car = new Car();
            car.CustomerID = selectedCustomer.ID;

            #region Accesing and Error Checking textboxes
            if (vin_TextBox.Text == "")
            {
                MessageBox.Show("VIN not entered!", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Hand);
                return;
            }
            else
            {
                car.VIN = vin_TextBox.Text;
            }
            if (year_TextBox.Text == "")
            {
                MessageBox.Show("Year not entered!", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Hand);
                return;
            }
            if (!IsDigitsOnly(year_TextBox.Text))
            {
                MessageBox.Show("Only numbers are allowed for the Year field!", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Hand);
                return;
            }
            if ((Convert.ToInt32(year_TextBox.Text) > 10000) || (Convert.ToInt32(year_TextBox.Text) < 1900))
            {
                MessageBox.Show("Invalid Year!", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Hand);
                return;
            }
            else
            {
                car.Year = year_TextBox.Text;
            }
            if (make_TextBox.Text == "")
            {
                MessageBox.Show("Make not entered!", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Hand);
                return;
            }
            else
            {
                car.Make = make_TextBox.Text;
            }
            if (model_TextBox.Text == "")
            {
                MessageBox.Show("Model not entered!", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Hand);
                return;
            }
            else
            {
                car.Model = model_TextBox.Text;
            }
            #endregion

            MessageBoxResult boxResult = MessageBox.Show("Would you like to procced adding this car as a new car?", "Confirm", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (boxResult == MessageBoxResult.Yes)
            {
                MySqlManipulator mySqlManipulator = new MySqlManipulator();
                mySqlManipulator.login();

                if (!mySqlManipulator.VINExists(car.VIN))
                {
                    mySqlManipulator.addToTable(car);
                    ShowAll();
                    ClearTextBoxes();
                }
                else
                {
                    MessageBox.Show("VIN already exists in database", "Invalid VIN", MessageBoxButton.OK);
                }
            }
        }

        private void Update_Button_Click(object sender, RoutedEventArgs e)
        {
            if (selectedCar != null)
            {
                Car car = new Car();

                #region Accesing and Error Checking textboxes
                if (vin_TextBox.Text == "")
                {
                    MessageBox.Show("VIN not entered!", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Hand);
                    return;
                }
                else
                {
                    car.VIN = vin_TextBox.Text;
                }
                if (year_TextBox.Text == "")
                {
                    MessageBox.Show("Year not entered!", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Hand);
                    return;
                }
                if (!IsDigitsOnly(year_TextBox.Text))
                {
                    MessageBox.Show("Only numbers are allowed for the Year field!", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Hand);
                    return;
                }
                if ((Convert.ToInt32(year_TextBox.Text) > 10000) || (Convert.ToInt32(year_TextBox.Text) < 1900))
                {
                    MessageBox.Show("Invalid Year!", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Hand);
                    return;
                }
                else
                {
                    car.Year = year_TextBox.Text;
                }
                if (make_TextBox.Text == "")
                {
                    MessageBox.Show("Make not entered!", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Hand);
                    return;
                }
                else
                {
                    car.Make = make_TextBox.Text;
                }
                if (model_TextBox.Text == "")
                {
                    MessageBox.Show("Model not entered!", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Hand);
                    return;
                }
                else
                {
                    car.Model = model_TextBox.Text;
                }
                #endregion

                MessageBoxResult result = MessageBox.Show("Are you sure you would like to update this Vehicle?", "Confirm Update", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    MySqlManipulator mySqlManipulator = new MySqlManipulator();

                    mySqlManipulator.login();


                    mySqlManipulator.Update(car);

                    selectedCar = car;

                    ShowAll();

                    MessageBox.Show("Vehicle Updated Succesfully", "Succesful Update", MessageBoxButton.OK); 
                }
            }
            else
            {
                MessageBox.Show("Please select the vehicle you would like to update!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Delete_Button_Click(object sender, RoutedEventArgs e)
        {
            if (selectedCar != null)
            {
                MessageBoxResult result = MessageBox.Show("Are you sure you would like to DELETE this Vehicle?", "Confirm Update", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    Car car = selectedCar;

                    MySqlManipulator mySqlManipulator = new MySqlManipulator();

                    mySqlManipulator.login();

                    mySqlManipulator.Delete(car);

                    selectedCar = null;

                    ClearTextBoxes();
                    ShowAll();
                    MessageBox.Show("Vehicle Deleted Succesfully", "Succesful Delete", MessageBoxButton.OK);
                }
            }
            else
            {
                MessageBox.Show("Please select a Vehicle!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Vehicle_ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                selectedCar = (Car)e.AddedItems[0];

                vin_TextBox.Text = selectedCar.VIN;
                year_TextBox.Text = selectedCar.Year;
                make_TextBox.Text = selectedCar.Make;
                model_TextBox.Text = selectedCar.Model;
            }
            catch
            {

            }
        }

        private void ClearTextBoxes()
        {
            vehicle_ListView.SelectedItem = null;
            vin_TextBox.Clear();
            year_TextBox.Clear();
            make_TextBox.Clear();
            model_TextBox.Clear();
        }

        private void ShowAll()
        {
            vehicle_ListView.Items.Clear();

            MySqlManipulator mySqlManipulator = new MySqlManipulator();

            mySqlManipulator.login();

            List<Car> cars;

            cars = mySqlManipulator.allCarsFor(selectedCustomer);

            foreach (Car car in cars)
            {
                vehicle_ListView.Items.Add(car);
            }
        }

        private bool IsDigitsOnly(string str)
        {
            foreach (char c in str)
            {
                if (c < '0' || c > '9')
                    return false;
            }

            return true;
        }


    }
}
