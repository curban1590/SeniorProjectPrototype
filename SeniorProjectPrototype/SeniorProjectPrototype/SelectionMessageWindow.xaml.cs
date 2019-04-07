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
            WindowsManeger.CloseWindow(Title);
            WindowsManeger.OpenCustomerEdit();
        }

        private void Employee_Button_Click(object sender, RoutedEventArgs e)
        {
            WindowsManeger.CloseWindow(Title);
            WindowsManeger.OpenEmployeeEdit();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            WindowsManeger.WindowClosing(Title);
        }
    }
}
