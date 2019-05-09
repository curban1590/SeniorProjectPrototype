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
    /// Interaction logic for EditCustomerPage.xaml
    /// </summary>
    public partial class EditCustomerPage : Page
    {
        private Customer selectedCustomer;

        public EditCustomerPage()
        {
            InitializeComponent();
        }

        private void EditCustomer_Loaded(object sender, RoutedEventArgs e)
        {
            MySqlManipulator mySqlManipulator = new MySqlManipulator();

            mySqlManipulator.login();

            List<Customer> customers = mySqlManipulator.allCustomers();

            foreach (Customer cus in customers)
            {
                if (cus.isActive)
                {
                    customer_ListView.Items.Add(cus);
                }
            }
        }

        private void ShowAll_Button_Click(object sender, RoutedEventArgs e)
        {
            customer_ListView.Items.Clear();

            MySqlManipulator mySqlManipulator = new MySqlManipulator();

            mySqlManipulator.login();

            List<Customer> customers;

            customers = mySqlManipulator.allCustomers();

            foreach (Customer cus in customers)
            {
                if (cus.isActive)
                {
                    customer_ListView.Items.Add(cus);
                }
            }
        }

        private void Search_Textbox_TextChanged(object sender, TextChangedEventArgs e)
        {
            customer_ListView.Items.Clear();

            MySqlManipulator mySqlManipulator = new MySqlManipulator();

            mySqlManipulator.login();

            List<Customer> customers;

            customers = mySqlManipulator.searchCustomers(search_Textbox.Text);

            foreach (Customer cus in customers)
            {
                if (cus.isActive)
                {
                    customer_ListView.Items.Add(cus);
                }
            }
        }

        private void Customer_ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                selectedCustomer = (Customer)e.AddedItems[0];

                firstNameTextBox.Text = selectedCustomer.FName;
                lastNameTextBox.Text = selectedCustomer.LName;
                phoneNumberTextBox.Text = selectedCustomer.PhoneNum;
                emailTextBox.Text = selectedCustomer.Email;
                streetNumTextBox.Text = selectedCustomer.StreetNum;
                streetNameTextBox.Text = selectedCustomer.StreetName;
                cityTextBox.Text = selectedCustomer.City;
                stateTextBox.Text = selectedCustomer.State;
                zipTextBox.Text = selectedCustomer.Zip;
            }
            catch
            {

            }
        }

        private void Update_Button_Click(object sender, RoutedEventArgs e)
        {
            if (selectedCustomer != null)
            {
                Customer customer = selectedCustomer;

                #region Accesing and Error Checking textboxes
                if (firstNameTextBox.Text == "")
                {
                    MessageBox.Show("First name not entered", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Hand);
                    return;
                }
                else
                {
                    customer.FName = escapeQuotes(firstNameTextBox.Text);
                }

                if (lastNameTextBox.Text == "")
                {
                    MessageBox.Show("Last name not entered", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Hand);
                    return;
                }
                else
                {
                    customer.LName = escapeQuotes(lastNameTextBox.Text);
                }

                if (phoneNumberTextBox.Text == "")
                {
                    MessageBox.Show("Phone number not entered", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Hand);
                    return;
                }
                else
                {
                    customer.PhoneNum = noQuotes(phoneNumberTextBox.Text);
                }

                if (emailTextBox.Text == "")
                {
                    MessageBox.Show("Job titled not selected", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Hand);
                    return;
                }
                else
                {
                    customer.Email = noQuotes(emailTextBox.Text);
                }

                if (streetNumTextBox.Text == "")
                {
                    MessageBox.Show("Street Number not entered", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Hand);
                    return;
                }
                else
                {
                    customer.StreetNum = noQuotes(streetNumTextBox.Text);
                }

                if (streetNameTextBox.Text == "")
                {
                    MessageBox.Show("Street name not entered", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Hand);
                    return;
                }
                else
                {
                    customer.StreetName = escapeQuotes(streetNameTextBox.Text);
                }

                if (cityTextBox.Text == "")
                {
                    MessageBox.Show("City not entered", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Hand);
                    return;
                }
                else
                {
                    customer.City = escapeQuotes(cityTextBox.Text);
                }

                if (stateTextBox.Text == "")
                {
                    MessageBox.Show("State not entered", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Hand);
                    return;
                }
                else
                {
                    customer.State = noQuotes(stateTextBox.Text);
                }

                if (zipTextBox.Text == "")
                {
                    MessageBox.Show("Zip not entered", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Hand);
                    return;
                }
                else
                {
                    customer.Zip = noQuotes(zipTextBox.Text);
                }
                #endregion

                MessageBoxResult result = MessageBox.Show("Are you sure you would like to update this customer?", "Confirm Update", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                { 
                    MySqlManipulator mySqlManipulator = new MySqlManipulator();

                    mySqlManipulator.login();

                    mySqlManipulator.Update(customer);

                    selectedCustomer = customer;

                    MessageBox.Show("Customer Updated Succesfully", "Succesful Update", MessageBoxButton.OK);

                    customer_ListView.Items.Refresh();
                }
            }
            else
            {
                MessageBox.Show("Please select a Customer!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Delete_Button_Click(object sender, RoutedEventArgs e)
        {
            if (selectedCustomer != null)
            {
                MessageBoxResult result = MessageBox.Show("Are you sure you would like to DELETE this customer?", "Confirm Update", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    Customer customer = selectedCustomer;

                    MySqlManipulator mySqlManipulator = new MySqlManipulator();

                    mySqlManipulator.login();

                    mySqlManipulator.Delete(customer);

                    selectedCustomer = null;

                    ClearTextBoxes();
                    search_Textbox.Clear();
                    MessageBox.Show("Customer Deleted Succesfully", "Succesful Delete", MessageBoxButton.OK);

                    ShowAll_Button_Click(sender, e);
                }
            }
            else
            {
                MessageBox.Show("Please select a Customer!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void EditVehicles_Button_Click(object sender, RoutedEventArgs e)
        {
            if (selectedCustomer != null)
            {
                AddEditVehicles vehiclePage = new AddEditVehicles();
                vehiclePage.selectedCustomer = selectedCustomer;

                SecondWindow vehicleWin = new SecondWindow();
                vehicleWin.title = "Add/Edit Vechicles for " + selectedCustomer.FName + " " + selectedCustomer.LName;
                vehicleWin.pageToBeLoaded = vehiclePage;
                vehicleWin.Show();
                WindowsManeger.openWindows.Add(vehicleWin);
            }
            else
            {
                MessageBox.Show("Please select a Customer!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ResetPasword_Button_Click(object sender, RoutedEventArgs e)
        {
            if (selectedCustomer != null)
            {
                MessageBoxResult result = MessageBox.Show("Are you sure you would like to Reset this customers Password?", "Confirm Update", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    Customer customer = selectedCustomer;

                    MySqlManipulator mySqlManipulator = new MySqlManipulator();

                    mySqlManipulator.login();

                    mySqlManipulator.UpdatePassword(customer);

                    selectedCustomer.Password = customer.Password;

                    MessageBox.Show("Customer password succesfully updated!\nThe customers password is ready to be sent to them", "Succesful Update", MessageBoxButton.OK);
                }
                }
            else
            {
                MessageBox.Show("Please select a Customer!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

            private void ClearTextBoxes()
        {
            firstNameTextBox.Clear();
            lastNameTextBox.Clear();
            phoneNumberTextBox.Clear();
            emailTextBox.Clear();
            streetNumTextBox.Clear();
            streetNameTextBox.Clear();
            cityTextBox.Clear();
            stateTextBox.Clear();
            zipTextBox.Clear();
        }
        private string escapeQuotes(string s)
        {
            s = s.Replace("\"", "\\\"");
            s = s.Replace("\'", "\\\'");
            return s;
        }
        private string noQuotes(string s)
        {
            s = s.Replace("\"", "");
            s = s.Replace("\'", "");
            return s;
        }
    }
}
