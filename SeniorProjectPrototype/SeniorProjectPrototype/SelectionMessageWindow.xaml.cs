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
        private bool isAdd = false;

        public SelectionMessageWindow()
        {
            InitializeComponent();
        }

        public void editIsAdd(bool add)
        {
            isAdd = add;
            if (isAdd)
            {
                search_Label.Visibility = Visibility.Hidden;
                add_Label.Visibility = Visibility.Visible;
            }
        }

        private void Customer_Button_Click(object sender, RoutedEventArgs e)
        {
            WindowsManeger.CloseWindow(Title);
            if (isAdd)
            {
                WindowsManeger.OpenAddClient();
            }
            else
            {
                WindowsManeger.OpenCustomerEdit();
            }
        }

        private void Employee_Button_Click(object sender, RoutedEventArgs e)
        {
            WindowsManeger.CloseWindow(Title);
            if (isAdd)
            {
                WindowsManeger.OpenAddEmployee();
            }
            else
            {
                WindowsManeger.OpenEmployeeEdit();
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            WindowsManeger.WindowClosing(Title);
        }
    }
}
