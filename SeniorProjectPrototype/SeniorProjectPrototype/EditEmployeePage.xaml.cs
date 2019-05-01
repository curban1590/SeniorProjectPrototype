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
    /// Interaction logic for EditEmployeePage.xaml
    /// </summary>
    public partial class EditEmployeePage : Page
    {
        private Employee selectedEmployee;

        public EditEmployeePage()
        {
            InitializeComponent();
        }

        private void EditEmployee_Loaded(object sender, RoutedEventArgs e)
        {
            MySqlManipulator mySqlManipulator = new MySqlManipulator();

            mySqlManipulator.login();

            List<Employee> employees = mySqlManipulator.allEmployees();

            foreach (Employee emp in employees)
            {
                employee_ListView.Items.Add(emp);
            }
        }

        private void ShowAll_Button_Click(object sender, RoutedEventArgs e)
        {
            employee_ListView.Items.Clear();

            MySqlManipulator mySqlManipulator = new MySqlManipulator();

            mySqlManipulator.login();

            List<Employee> employees;

            employees = mySqlManipulator.allEmployees();

            foreach (Employee emp in employees)
            {
                employee_ListView.Items.Add(emp);
            }
        }

        private void Search_Textbox_TextChanged(object sender, TextChangedEventArgs e)
        {
            employee_ListView.Items.Clear();

            MySqlManipulator mySqlManipulator = new MySqlManipulator();
   
            mySqlManipulator.login();

            List<Employee> employees;

            employees = mySqlManipulator.searchEmployees(search_Textbox.Text);

            foreach (Employee emp in employees)
            {
                employee_ListView.Items.Add(emp);
            }

        }

        private void Employee_ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                selectedEmployee = (Employee)e.AddedItems[0];

                firstNameTextBox.Text = selectedEmployee.FName;
                lastNameTextBox.Text = selectedEmployee.LName;
                phoneNumberTextBox.Text = selectedEmployee.Phone;
                jobTitleTextBox.Text = selectedEmployee.JobTitle;
                streetNumTextBox.Text = selectedEmployee.StreetNum;
                streetNameTextBox.Text = selectedEmployee.StreetName;
                cityTextBox.Text = selectedEmployee.City;
                stateTextBox.Text = selectedEmployee.State;
                zipTextBox.Text = selectedEmployee.Zip;
            }
            catch
            {

            }    
        }

        private void Update_Button_Click(object sender, RoutedEventArgs e)
        {
            if (selectedEmployee != null)
            {
                Employee employee = selectedEmployee;

                #region Accesing and Error Checking textboxes
                if (firstNameTextBox.Text == "")
                {
                    MessageBox.Show("First name not entered", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Hand);
                    return;
                }
                else
                {
                    employee.FName = escapeQuotes(firstNameTextBox.Text);
                }

                if (lastNameTextBox.Text == "")
                {
                    MessageBox.Show("Last name not entered", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Hand);
                    return;
                }
                else
                {
                    employee.LName = escapeQuotes(lastNameTextBox.Text);
                }

                if (phoneNumberTextBox.Text == "")
                {
                    MessageBox.Show("Phone number not entered", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Hand);
                    return;
                }
                else
                {
                    employee.Phone = noQuotes(phoneNumberTextBox.Text);
                }

                if (streetNumTextBox.Text == "")
                {
                    MessageBox.Show("Street Number not entered", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Hand);
                    return;
                }
                else
                {
                    employee.StreetNum = noQuotes(streetNumTextBox.Text);
                }

                if (streetNameTextBox.Text == "")
                {
                    MessageBox.Show("Street name not entered", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Hand);
                    return;
                }
                else
                {
                    employee.StreetName = escapeQuotes(streetNameTextBox.Text);
                }

                if (cityTextBox.Text == "")
                {
                    MessageBox.Show("City not entered", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Hand);
                    return;
                }
                else
                {
                    employee.City = escapeQuotes(cityTextBox.Text);
                }

                if (stateTextBox.Text == "")
                {
                    MessageBox.Show("State not entered", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Hand);
                    return;
                }
                else
                {
                    employee.State = escapeQuotes(stateTextBox.Text);
                }

                if (zipTextBox.Text == "")
                {
                    MessageBox.Show("Zip not entered", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Hand);
                    return;
                }
                else
                {
                    employee.Zip = noQuotes(zipTextBox.Text);
                }
                #endregion

                MessageBoxResult result = MessageBox.Show("Are you sure you would like to update this employee?", "Confirm Update", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    MySqlManipulator mySqlManipulator = new MySqlManipulator();

                    mySqlManipulator.login();

                    mySqlManipulator.Update(employee);

                    selectedEmployee = employee;

                    MessageBox.Show("Employee Updated Succesfully", "Succesful Update", MessageBoxButton.OK);

                    employee_ListView.Items.Refresh();
                }
            }
            else
            {
                MessageBox.Show("Please select an Employee!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void Delete_Button_Click(object sender, RoutedEventArgs e)
        {
            if (selectedEmployee != null)
            {
                MessageBoxResult result = MessageBox.Show("Are you sure you would like to DELETE this employee?", "Confirm Update", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    Employee employee = selectedEmployee;

                    MySqlManipulator mySqlManipulator = new MySqlManipulator();

                    mySqlManipulator.login();

                    mySqlManipulator.Delete(employee);

                    selectedEmployee = null;

                    ClearTextBoxes();
                    search_Textbox.Clear();
                    MessageBox.Show("Employee Deleted Succesfully", "Succesful Delete", MessageBoxButton.OK);

                    ShowAll_Button_Click(sender, e);
                }
            }
            else
            {
                MessageBox.Show("Please select an Employee!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ClearTextBoxes()
        {
            firstNameTextBox.Clear();
            lastNameTextBox.Clear();
            phoneNumberTextBox.Clear();
            jobTitleTextBox.Clear();
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
