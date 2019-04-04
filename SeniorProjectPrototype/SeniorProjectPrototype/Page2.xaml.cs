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
        public static RoutedCommand MyComandClient = new RoutedCommand();
        public static RoutedCommand MyComandEmployee = new RoutedCommand();
        public static RoutedCommand MyComandSearch = new RoutedCommand();
        public static RoutedCommand MyComandLogout = new RoutedCommand();
        public static RoutedCommand MyComandExit = new RoutedCommand();
        public static RoutedCommand MyComandAbout = new RoutedCommand();

        private List<Window> openWindows = new List<Window>();

        public Page2()
        {
            InitializeComponent();

            MyComandClient.InputGestures.Add(new KeyGesture(Key.C, ModifierKeys.Control));
            CommandBindings.Add(new CommandBinding(MyComandClient, Button_Add_Client_Click));

            MyComandEmployee.InputGestures.Add(new KeyGesture(Key.E, ModifierKeys.Control));
            CommandBindings.Add(new CommandBinding(MyComandEmployee, Button_Add_Employee_Click));

            MyComandSearch.InputGestures.Add(new KeyGesture(Key.S, ModifierKeys.Control));
            CommandBindings.Add(new CommandBinding(MyComandSearch, Button_Search_Click));

            MyComandLogout.InputGestures.Add(new KeyGesture(Key.L, ModifierKeys.Control));
            CommandBindings.Add(new CommandBinding(MyComandLogout, Logout_MenuItem_Click));

            MyComandExit.InputGestures.Add(new KeyGesture(Key.Q, ModifierKeys.Control));
            CommandBindings.Add(new CommandBinding(MyComandExit, Exit_MenuItem_Click));

            MyComandAbout.InputGestures.Add(new KeyGesture(Key.A, ModifierKeys.Control));
            CommandBindings.Add(new CommandBinding(MyComandAbout, About_MenuItem_Click));
        }

        private void Button_Add_Client_Click(object sender, RoutedEventArgs e)
        {
            SecondWindow customerWin = new SecondWindow();
            customerWin.pageToBeLoaded = new AddCustomerPage();
            customerWin.title = "Add Customers";
            customerWin.Show();
            openWindows.Add(customerWin);
        }

        private void Button_Add_Employee_Click(object sender, RoutedEventArgs e)
        {
            SecondWindow employeeWin = new SecondWindow();
            employeeWin.pageToBeLoaded = new AddEmployeePage();
            employeeWin.title = "Add Employees";
            employeeWin.Show();
            openWindows.Add(employeeWin);
        }

        private void Button_Search_Click(object sender, RoutedEventArgs e)
        {
            SelectionMessageWindow selectionMessage = new SelectionMessageWindow();
            selectionMessage.Show();
            openWindows.Add(selectionMessage);
        }

        private void AddClient_MenuItem_Click(object sender, RoutedEventArgs e)
        {
            Button_Add_Client_Click(sender, e);
        }

        private void AddEmployees_MenuItem_Click(object sender, RoutedEventArgs e)
        {
            Button_Add_Employee_Click(sender, e);
        }

        private void Edit_MenuItem_Click(object sender, RoutedEventArgs e)
        {
            Button_Search_Click(sender, e);
        }

        private void Logout_MenuItem_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult boxResult = MessageBox.Show("Are you sure you would like to log out?", "Confirm Logout", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (boxResult == MessageBoxResult.Yes)
            {
                foreach (Window w in openWindows)
                {
                    w.Close();
                }   
                this.NavigationService.Navigate(new Page1());  
            }
        }

        private void Exit_MenuItem_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult boxResult = MessageBox.Show("Are you sure you would like to exit?", "Confirm Exit", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (boxResult == MessageBoxResult.Yes)
            {
                System.Windows.Application.Current.Shutdown();
            }
        }

        private void About_MenuItem_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("TJCTJ Systems \nCreators:\n    Jason Diaz \n    Thomas Holzmacher \n    Twinkle Patel\n    James Pina\n    Conner Urban", "About");
        }

        /** INVOICING
        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = System.IO.Directory.GetCurrentDirectory();

            if (saveFileDialog.ShowDialog() == true)
            {
                using (StreamWriter sw = new StreamWriter(saveFileDialog.FileName))
                {
                    sw.WriteLine(emp.id);
                    sw.WriteLine(emp.department);
                }
            }
        }
        **/
    }
}
