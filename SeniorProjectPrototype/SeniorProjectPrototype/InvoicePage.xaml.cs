using System;
using System.Collections.Generic;
using System.IO;
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
using System.Globalization;

namespace SeniorProjectPrototype
{
    /// <summary>
    /// Interaction logic for InvoicePage.xaml
    /// </summary>
    public partial class InvoicePage : Page
    {
        DateTime localDate = new DateTime();
        List<Appointment> appointments = new List<Appointment>();
        Appointment selectedAppointment = new Appointment();
        string invoiceID = "";

        public InvoicePage()
        {
            InitializeComponent();

            datePicker.SelectedDate = DateTime.Today;

            loadAppointments();
        }

        public void loadAppointments()
        {
            Invoice_TextBox.Text = "";
            appointments_ListView.Items.Clear();

            Customer customer = new Customer();
            Employee employee = new Employee(); 
            MySqlManipulator mySqlManipulator = new MySqlManipulator();

            mySqlManipulator.login();

            appointments = mySqlManipulator.getAppointmentsFor(localDate.Month.ToString(), localDate.Day.ToString());
            if (appointments != null || appointments.Count != 0)
            {
                foreach (Appointment app in appointments)
                {
                    customer = mySqlManipulator.getCustomer(app.customerID);
                    app.customerName = customer.FName + " " + customer.LName;
                    employee = mySqlManipulator.getEmployee(app.employeeID);
                    app.employeeName = employee.FName + " " + employee.LName;
                    app.date = app.getDateTime().ToString();
                    appointments_ListView.Items.Add(app);
                }
            }

        }

        private void Appointments_ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Customer customer = new Customer();
            List<Service> services = new List<Service>();
            Service aService = new Service();
            MySqlManipulator mySqlManipulator = new MySqlManipulator();
            string newRow = "";
            double total = 0;

            mySqlManipulator.login();

            try
            {
                selectedAppointment = (Appointment)e.AddedItems[0];
            }
            catch { }

            customer = mySqlManipulator.getCustomer(selectedAppointment.customerID);
            services = mySqlManipulator.getServicesFor(selectedAppointment.appointmentID);
            invoiceID = mySqlManipulator.getInvoiceID(selectedAppointment.appointmentID);
            string text = File.ReadAllText("Invoices\\InvoiceTemplate.txt");

            text = text.Replace("{INVOICEID}", invoiceID);
            text = text.Replace("{SERVICEDATE}", selectedAppointment.getDateTime().ToString());
            text = text.Replace("{VIN}", selectedAppointment.vin);
            
            text = text.Replace("{CUSTOMERNAME}", selectedAppointment.customerName);
            text = text.Replace("{STNUM}", customer.StreetNum);
            text = text.Replace("{STNAME}", customer.StreetName);
            text = text.Replace("{CITY}", customer.City);
            text = text.Replace("{STATE}", customer.State);
            text = text.Replace("{ZIP}", customer.Zip);

            foreach (Service service in services) {
                newRow = new string(' ', 150);
                aService = mySqlManipulator.getService(service.service);
                newRow = newRow.Insert(19, service.quantity.ToString());
                newRow = newRow.Insert(44, service.service);
                newRow = newRow.Insert(89, "$" + aService.price.ToString());
                newRow = newRow.Insert(127, "$" + (service.quantity * aService.price).ToString());
                total += (service.quantity * aService.price);

                text += newRow;
                text += "\n";
            }
            text += "\n\n";
            newRow = "";
            newRow = "\t\t\t\t\t\t\tTotal: $" + total + "\n";
            text += newRow;
            double tax = total * .087;

            newRow = "";
            newRow = "\t\t\t\t\t\t\tSales Tax (8.875): " + tax.ToString("C", CultureInfo.CurrentCulture) + "\n";
            text += newRow;

            newRow = "";
            newRow = "\t\t\t\t\t\t\tGRAND TOTAL: " + (total + tax).ToString("C",CultureInfo.CurrentCulture);
            text += newRow;


            Invoice_TextBox.Text = text;
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            localDate = Convert.ToDateTime(datePicker.SelectedDate);
        }

        private void DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            Invoice_TextBox.Text = "";
            appointments_ListView.Items.Clear();
            localDate = Convert.ToDateTime(datePicker.SelectedDate);
            loadAppointments();
        }

        private void Button_Save_Click(object sender, RoutedEventArgs e)
        {
            string filename = "Invoice_" + invoiceID + ".txt";
            
            if (Invoice_TextBox.Text.IndexOf("INVOICE") != -1)
            {
                MessageBoxResult result = MessageBox.Show("Would you like to save this invoice?", "Confirm Save", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    FileStream fs1 = new FileStream("Invoices\\" + filename, FileMode.OpenOrCreate, FileAccess.Write);
                    StreamWriter writer = new StreamWriter(fs1);
                    writer.Write(Invoice_TextBox.Text);
                    writer.Close();

                    MessageBox.Show("\tSaved succesfully\nThe file is located in the Invoices folder\n\tThe title of the file is:\n\t\"" + filename + "\"", "Succesful Save", MessageBoxButton.OK);
                }
            }
            else
            {
                MessageBox.Show("Please select an appointment!", "No Appointment was Selected", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
