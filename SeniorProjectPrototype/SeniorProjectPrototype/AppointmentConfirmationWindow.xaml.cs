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
using System.Windows.Shapes;

namespace SeniorProjectPrototype
{
    /// <summary>
    /// Interaction logic for AppointmentConfirmationWindow.xaml
    /// </summary>
    public partial class AppointmentConfirmationWindow : Window
    {
        public Appointment appointment = new Appointment();
        public string CustomerID { get; set; }
        public string CustomerName { get; set; }
        public string EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public DateTime AppointmentTime { get; set; }
        private string Description { get; set; }
        public int Duration { get; set; }

        public AppointmentConfirmationWindow()
        {
            InitializeComponent();
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            Customer_TextBlock.Text = appointment.customerName;
            Mechanic_TextBlock.Text = appointment.employeeName;
            Datetime_TextBlock.Text = appointment.date + " " + appointment.time;
            Duration_TextBlock.Text = durationDisplay();
        }

        private string durationDisplay()
        {
            int totalDuration = 0;
            int hours = 0;
            int minutes = 0;
            string myTime = "";

            foreach (Service service in appointment.services)
            {
                totalDuration += service.duration;
            }

            minutes = totalDuration;

            while (minutes >= 60)
            {
                minutes -= 60;
                hours += 1;
            }

            if (hours == 0)
            {
                myTime = minutes + " min.";
            }
            else
            {
                if (minutes == 0)
                {
                    myTime = hours + ":00";
                }
                else
                {
                    myTime = hours + ":" + minutes;
                }
            }

            return myTime;
        }

        private void ConfirmAppointment_Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            WindowsManeger.WindowClosing(Title);
        }

        private void Book_Button_Click(object sender, RoutedEventArgs e)
        {
            
            appointment.description = escapeQuotes(Description_TextBox.Text);

            MySqlManipulator mySqlManipulator = new MySqlManipulator();

            mySqlManipulator.login();

            if (Description_TextBox.Text == "")
            {
                MessageBoxResult result = MessageBox.Show("Description Required!\n Would you like an auto-generated description?", "Description Required!", MessageBoxButton.YesNo, MessageBoxImage.Asterisk);

                if (result == MessageBoxResult.Yes)
                {
                    appointment.description = appointment.customerName + " for " + appointment.employeeName;
                }
                if (result == MessageBoxResult.No)
                {
                    return;
                }
            }
            
            mySqlManipulator.addToTable(appointment);

            MessageBox.Show("Appointment Booked!", "Success", MessageBoxButton.OK);
            WindowsManeger.CloseWindow(Title);

        }
        private string escapeQuotes(string s)
        {
            s = s.Replace("\"", "\\\"");
            s = s.Replace("\'", "\\\'");
            return s;
        }
    }

}