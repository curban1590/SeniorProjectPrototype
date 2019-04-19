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
    /// Interaction logic for EmailPage.xaml
    /// </summary>
    public partial class EmailPage : Page
    {
        private Customer selectedCustomer;

        public EmailPage()
        {
            InitializeComponent();
        }

        private void EditCustomer_Loaded(object sender, RoutedEventArgs e)
        {
            MySqlManipulator mySqlManipulator = new MySqlManipulator();

            mySqlManipulator.login();

            List<Customer> customers = mySqlManipulator.allCustomers();

            foreach (Customer cus in customers)
            {
                customer_ListView.Items.Add(cus);
            }
        }

        private void Search_Textbox_TextChanged(object sender, TextChangedEventArgs e)
        {
            customer_ListView.Items.Clear();

            MySqlManipulator mySqlManipulator = new MySqlManipulator();

            mySqlManipulator.login();

            List<Customer> customers;

            customers = mySqlManipulator.searchCustomers(search_Textbox.Text);

            foreach (Customer cus in customers)
            {
                customer_ListView.Items.Add(cus);
            }
        }

        /*private void Customer_ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                selectedCustomer = (Customer)e.AddedItems[0];

                emailTargetTextBox.Text = selectedCustomer.Email;                
            }
            catch
            {

            }
        }*/

        private void SendEmail_Button_Click(object sender, RoutedEventArgs e)
        {
            if (selectedCustomer != null)
            {
                MessageBoxResult result = MessageBox.Show("Are you sure you would like to send this Email?", "Confirm Update", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    Customer customer = selectedCustomer;

                    //Put email sending thing here
                    MySqlManipulator mySqlManipulator = new MySqlManipulator();

                    mySqlManipulator.login();

                    

                    

                    MessageBox.Show("Email Sent!", "Succesful Update", MessageBoxButton.OK);
                }
            }
            else
            {
                MessageBox.Show("Please select a Customer!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }












        private void ClearTextBoxes(){
            emailSubjectTextBox.Clear();
            emailBodyTextBox.Clear();
            //emailTargetTextBox.Clear();
        }
    }
}
