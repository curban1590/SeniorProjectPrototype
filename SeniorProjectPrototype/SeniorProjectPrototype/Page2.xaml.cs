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
            SecondWindow customerWin = new SecondWindow();
            customerWin.pageToBeLoaded = new AddCustomerPage();
            customerWin.title = "Add Customers";
            customerWin.Show();
        }

        private void Button_Add_Employee_Click(object sender, RoutedEventArgs e)
        {
            SecondWindow employeeWin = new SecondWindow();       
            employeeWin.pageToBeLoaded = new AddEmployeePage();
            employeeWin.title = "Add Employees";
            employeeWin.Show();
        }

        private void Button_Search_Click(object sender, RoutedEventArgs e)
        {
            SelectionMessageWindow selectionMessage = new SelectionMessageWindow();
            selectionMessage.Show();
        }
    }
}
