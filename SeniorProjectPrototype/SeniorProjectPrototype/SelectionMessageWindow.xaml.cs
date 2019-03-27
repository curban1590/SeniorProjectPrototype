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
    /// Interaction logic for SelectionMessageWindow.xaml
    /// </summary>
    public partial class SelectionMessageWindow : Window
    {

        public SelectionMessageWindow()
        {
            InitializeComponent();
        }

        private void Customer_Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            SecondWindow customerWin = new SecondWindow();

            customerWin.pageToBeLoaded = new EditCustomerPage();
            customerWin.title = "Search/Edit Customers";
            customerWin.Show();
        }

        private void Employee_Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            SecondWindow employeeWin = new SecondWindow();

            employeeWin.pageToBeLoaded = new EditEmployeePage();
            employeeWin.title = "Search/Edit Employees";
            employeeWin.Show();
        }
    }
}
