using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
    /// Interaction logic for Page1.xaml
    /// </summary>
    public partial class Page1 : Page
    {
        public Page1()
        {
            InitializeComponent();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string username = usernameTextBox.Text;
            string password = passwordTextBox.Password;
            bool isValid = false;
            
            MySqlManipulator mySqlManipulator = new MySqlManipulator();

            mySqlManipulator.login();
            if (IsDigitsOnly(username) && IsDigitsOnly(password))
            {

                if (username != "" && password != "")
                {
                    isValid = mySqlManipulator.loginCheck(username, password);
                }
                else
                {
                    MessageBox.Show("Login Required", "Invalid Login", MessageBoxButton.OK, MessageBoxImage.Hand);
                    usernameTextBox.Clear();
                    passwordTextBox.Clear();
                    return;
                }
            }

            if (!isValid)
            {
                MessageBox.Show("Not a valid Login", "Invalid Login", MessageBoxButton.OK, MessageBoxImage.Hand);
                usernameTextBox.Clear();
                passwordTextBox.Clear();
            }
            else
            {
                NavigationService.Navigate(new Page2());
            }
            
        }

        private bool IsDigitsOnly(string str)
        {
            foreach (char c in str)
            {
                if (c < '0' || c > '9')
                    return false;
            }

            return true;
        }

        private void PasswordTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                Button_Click_1(sender, new RoutedEventArgs());
            }
        }
    }
}
