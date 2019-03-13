using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
    /// Interaction logic for AddEmployeePage.xaml
    /// </summary>
    public partial class AddEmployeePage : Page
    {
        public AddEmployeePage()
        {
            InitializeComponent();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Employee employee = new Employee();

            #region Accesing and Error Checking textboxes
            if (firstNameTextBox.Text == "")
            {
                MessageBox.Show("First name not entered", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Hand);
                return;
            }
            else
            {
                employee.FName = firstNameTextBox.Text;
            }

            if (lastNameTextBox.Text == "")
            {
                MessageBox.Show("Last name not entered", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Hand);
                return;
            }
            else
            {
                employee.LName = lastNameTextBox.Text;
            }

            if (phoneNumberTextBox.Text == "")
            {
                MessageBox.Show("Phone number not entered", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Hand);
                return;
            }
            else
            {
                employee.Phone = phoneNumberTextBox.Text;
            }

            if (jobTitleBox.Text == "")
            {
                MessageBox.Show("Job titled not selected", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Hand);
                return;
            }
            else
            {
                employee.JobTitle = jobTitleBox.Text;
            }

            if (streetNumTextBox.Text == "")
            {
                MessageBox.Show("Street Number not entered", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Hand);
                return;
            }
            else
            {
                employee.StreetNum = streetNumTextBox.Text;
            }

            if (streetNameTextBox.Text == "")
            {
                MessageBox.Show("Street name not entered", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Hand);
                return;
            }
            else
            {
                employee.StreetName = streetNameTextBox.Text;
            }

            if (cityTextBox.Text == "")
            {
                MessageBox.Show("City not entered", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Hand);
                return;
            }
            else
            {
                employee.City = cityTextBox.Text;
            }

            if (stateTextBox.Text == "")
            {
                MessageBox.Show("State not entered", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Hand);
                return;
            }
            else
            {
                employee.State = stateTextBox.Text;
            }

            if (zipTextBox.Text == "")
            {
                MessageBox.Show("Zip not entered", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Hand);
                return;
            }
            else
            {
                employee.Zip = zipTextBox.Text;
            }
            #endregion
            
            MySqlManipulator mySqlManipulator = new MySqlManipulator();

            mySqlManipulator.login();

            mySqlManipulator.addToTable(employee);

            MessageBox.Show(employee.FName + " " + employee.LName + " was added successfully!", "Successful", MessageBoxButton.OK, MessageBoxImage.None);
            clearBoxes();
        }

        private void clearBoxes()
        {
            firstNameTextBox.Clear();
            lastNameTextBox.Clear();
            phoneNumberTextBox.Clear();
            streetNameTextBox.Clear();
            streetNumTextBox.Clear();
            cityTextBox.Clear();
            stateTextBox.Clear();
            zipTextBox.Clear();
        }

    }
}
