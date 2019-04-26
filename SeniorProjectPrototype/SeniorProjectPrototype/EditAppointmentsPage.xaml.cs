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
    /// Interaction logic for EditAppointmentsPage.xaml
    /// </summary>
    public partial class EditAppointmentsPage : Page
    {
        private List<Appointment> appointments = new List<Appointment>();
        private Appointment selectedAppointment;

        public EditAppointmentsPage()
        {
            InitializeComponent();
        }

        public void setAppointments(List<Appointment> argAppointment)
        {
            Customer customer = new Customer();
            Employee employee = new Employee();

            appointments = argAppointment;

            MySqlManipulator mySqlManipulator = new MySqlManipulator();

            mySqlManipulator.login();

            foreach (Appointment app in appointments)
            {
                customer = mySqlManipulator.getCustomer(app.customerID);
                app.customerName = customer.FName + " " + customer.LName;
                employee = mySqlManipulator.getEmployee(app.employeeID);
                app.employeeName = employee.FName + " " + employee.LName;
                app.date = app.getDateTime().ToString();
            }
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            appointments_ListView.ItemsSource = appointments;
        }

        private void Appointments_ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                selectedAppointment = (Appointment)e.AddedItems[0];
            }
            catch { }
        }

        private void Delete_Button_Click(object sender, RoutedEventArgs e)
        {
            if(selectedAppointment == null)
            {
                MessageBox.Show("No appointment was selected!", "Please select appointment", MessageBoxButton.OK, MessageBoxImage.Stop);
            }
            else
            {
                MessageBoxResult result = MessageBox.Show("Are you sure you would like to delete the appointment for " + selectedAppointment.customerName + "?", "Confirm Appointment Cancelation", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    MySqlManipulator mySqlManipulator = new MySqlManipulator();

                    mySqlManipulator.login();

                    mySqlManipulator.Delete(selectedAppointment);
                    appointments.Remove(selectedAppointment);
                    MessageBox.Show("Appointment deleted!", "Success", MessageBoxButton.OK);
                    appointments_ListView.SelectedItem = null;
                    appointments_ListView.Items.Refresh();
                }
                else
                {
                    return;
                }
            }
        }
    }
}
