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
    /// Interaction logic for Page2.xaml
    /// </summary>
    public partial class Page2 : Page
    {
        public Page2()
        {
            InitializeComponent();
        }

        private void Button_Add_Client_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Would you like to add a new customer? \n (Selecting no allows you to edit customers)","Add New/Edit", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
            SecondWindow customerWin = new SecondWindow();

            switch(result)
            {
                case MessageBoxResult.Yes:
                    {
                        customerWin.pageToBeLoaded = new AddCustomerPage();
                        customerWin.title = "Add Customers";
                        customerWin.Show();
                        break;
                    }
                case MessageBoxResult.No:
                    {
                        customerWin.pageToBeLoaded = new EditCustomerPage();
                        customerWin.title = "Edit Customers";
                        customerWin.Show();
                        break;
                    }
                case MessageBoxResult.Cancel:
                    {
                        break;
                    }

                default:
                    break;
            }




        }

        private void Button_Add_Employee_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Would you like to add a new employee? \n (Selecting no allows you to edit employees)", "Add New/Edit", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);

            SecondWindow employeeWin = new SecondWindow();

            switch (result)
            {
                case MessageBoxResult.Yes:
                    {
                        employeeWin.pageToBeLoaded = new AddEmployeePage();
                        employeeWin.title = "Add Employees";
                        employeeWin.Show();
                        break;
                    }
                case MessageBoxResult.No:
                    {
                        employeeWin.pageToBeLoaded = new EditEmployeePage();
                        employeeWin.title = "Edit Employees";
                        employeeWin.Show();
                        break;
                    }
                case MessageBoxResult.Cancel:
                    {
                        break;
                    }

                default:
                    break;
            }

        }
    }
}
