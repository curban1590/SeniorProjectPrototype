﻿using System;
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
            //Page page2 = new Page2();
            //page2.();
            NavigationService.Navigate(new Page2());

            /*
            string username = usernameTextBox.Text;
            string password = passwordTextBox.Password;
            
            MySqlManipulator mySqlManipulator = new MySqlManipulator();

            mySqlManipulator.login();

            bool isValid = mySqlManipulator.loginCheck(username, password);

            // TESTING CODE            
            Console.WriteLine(isValid);

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
            */
        }
    }
}
