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
            Employee selectedEmployee;
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
    }
}
