using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace SeniorProjectPrototype
{
    public static class WindowsManeger
    {
        public static List<Window> openWindows = new List<Window>();

        public static void OpenAddClient()
        {
            string title = "Add Customer";
            if (!CheckIfOpen(title))
            {
                SecondWindow customerWin = new SecondWindow();
                customerWin.pageToBeLoaded = new AddCustomerPage();
                customerWin.title = title;
                customerWin.Show();
                openWindows.Add(customerWin);
            }
        }

        public static void OpenAddEmployee()
        {
            string title = "Add Employee";
            if (!CheckIfOpen(title))
            {
                SecondWindow employeeWin = new SecondWindow();
                employeeWin.pageToBeLoaded = new AddEmployeePage();
                employeeWin.title = title;
                employeeWin.Show();
                openWindows.Add(employeeWin);
            }
        }

        public static void OpenSearch()
        {
            string title = "Employee/Customer Selection";
            if (!CheckIfOpen(title))
            {
                SelectionMessageWindow selectionMessage = new SelectionMessageWindow();
                selectionMessage.Show();
                openWindows.Add(selectionMessage);
            }
        }

        public static void OpenCustomerEdit()
        {
            string title = "Search/Edit Customers";
            if (!CheckIfOpen(title))
            {
                SecondWindow customerWin = new SecondWindow();
                customerWin.pageToBeLoaded = new EditCustomerPage();
                customerWin.title = title;
                customerWin.Show();
                openWindows.Add(customerWin);
            }
        }

        public static void OpenEmployeeEdit()
        {
            string title = "Search/Edit Employees";
            if (!CheckIfOpen(title))
            {
                SecondWindow employeeWin = new SecondWindow();
                employeeWin.pageToBeLoaded = new EditEmployeePage();
                employeeWin.title = title;
                employeeWin.Show();
                openWindows.Add(employeeWin);
            }
        }

        internal static void OpenAddAppointment()
        {
            string title = "Add Appointment";
            if (!CheckIfOpen(title))
            {
                SecondWindow appointmentWin = new SecondWindow();
                appointmentWin.ResizeMode = ResizeMode.NoResize;
                appointmentWin.pageToBeLoaded = new AddAppointmentPage();
                appointmentWin.title = title;
                appointmentWin.Show();
                openWindows.Add(appointmentWin);
            }
        }

        public static void OpenAppConfirm(Appointment newAppointment)
        {
            string title = "Confirm Appointment";
            if (!CheckIfOpen(title))
            {
                AppointmentConfirmationWindow appointmentWin = new AppointmentConfirmationWindow();
                appointmentWin.appointment = newAppointment;
                appointmentWin.Show();
                openWindows.Add(appointmentWin);
            }
        }

        public static void CloseAllWindows()
        {
            try
            {
                foreach (Window w in openWindows)
                {
                    w.Close();
                }
            }
            catch
            {

            }
        }

        public static void CloseWindow(string title)
        {
            try
            {
                foreach (Window w in openWindows)
                {
                    if (w.Title == title)
                    {
                        w.Close();
                        openWindows.Remove(w);
                    }
                }
            }
            catch
            {

            }
        }

        public static void WindowClosing(string title)
        {
            try
            {
                foreach (Window w in openWindows)
                {
                    if (w.Title == title)
                    {
                        openWindows.Remove(w);
                    }
                }
            }
            catch
            {

            }
        }

        public static bool CheckIfOpen(string title)
        {
            foreach (Window w in openWindows)
            {
                if (w.Title == title)
                {
                    w.Focus();
                    return true;
                }
            }
            return false;
        }
    }
}
