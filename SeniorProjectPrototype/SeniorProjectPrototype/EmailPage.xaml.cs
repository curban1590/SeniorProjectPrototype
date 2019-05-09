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
using System.Net.Mail;
using System.Net;
using System.Threading;
using System.Diagnostics;
using System.Collections;
using System.Windows.Controls.Primitives;
using System.Windows.Threading;

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

        private void Customer_ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                selectedCustomer = (Customer)e.AddedItems[0];

                emailTargetTextBox.Text = selectedCustomer.Email;                
            }
            catch
            {

            }
        }
        private void ShowAll_Button_Click(object sender, RoutedEventArgs e)
        {
            customer_ListView.Items.Clear();

            MySqlManipulator mySqlManipulator = new MySqlManipulator();

            mySqlManipulator.login();

            List<Customer> customers;

            customers = mySqlManipulator.allCustomers();

            foreach (Customer cus in customers)
            {
                customer_ListView.Items.Add(cus);
            }
        }



        private void SendEmail_Button_Click(object sender, RoutedEventArgs e)
        {
          
            if (selectedCustomer != null && emailSubjectTextBox.Text != "" && emailBodyTextBox.Text != "")
            {
                MessageBoxResult result = MessageBox.Show("Are you sure you would like to send this Email?", "Confirm Update", MessageBoxButton.YesNo, MessageBoxImage.Question);
                
                if (result == MessageBoxResult.Yes)
                {
                    

                    MailMessage message = new MailMessage("tjcjtsystems@gmail.com", selectedCustomer.Email, escapeQuotes(emailSubjectTextBox.Text), escapeQuotes(emailBodyTextBox.Text));

                    System.Net.Mail.Attachment attachment;
                    attachment = new System.Net.Mail.Attachment(@"Attachments/logo.png");
                    message.Attachments.Add(attachment);

                    //message.IsBodyHtml = true;

                    try
                    {
                        SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
                        //client.Timeout = 2000;
                        client.EnableSsl = true;
                        client.DeliveryMethod = SmtpDeliveryMethod.Network;
                        //client.Credentials = CredentialCache.DefaultNetworkCredentials;
                        client.UseDefaultCredentials = false;
                        client.Credentials = new System.Net.NetworkCredential("tjcjtsystems@gmail.com", "Asdf1123abcd");

                        Thread threadSendMails;
                        threadSendMails = new Thread(delegate ()
                        {                    
                            client.Send(message);
                            MessageBox.Show("Email Sent");
                            message.Dispose();
                            client.Dispose();
                        });

                        threadSendMails.IsBackground = true;
                        threadSendMails.Start();
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine("Email Error: " + ex.Message);
                    }

                    MessageBox.Show("Email Sent!", "Succesful Update", MessageBoxButton.OK);
                }
            }
            else if (emailSubjectTextBox.Text == "")
            {
                MessageBox.Show("Please enter a Subject!", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Hand);
                return;
            }
            else if (emailBodyTextBox.Text != "")
            {
                MessageBox.Show("Please enter an email body!", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Hand);
                return;
            }            
            else
            {
                MessageBox.Show("Please select a Customer!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private string escapeQuotes(string s)
        {
            s = s.Replace("\"", "\\\"");
            s = s.Replace("\'", "\\\'");
            return s;
        }
        private string noQuotes(string s)
        {
            s = s.Replace("\"", "");
            s = s.Replace("\'", "");
            return s;
        }

        private void ClearTextBoxes(){
            emailSubjectTextBox.Clear();
            emailBodyTextBox.Clear();
            emailTargetTextBox.Clear();
        }
    }
}
