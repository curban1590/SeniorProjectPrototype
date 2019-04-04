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
    /// Interaction logic for EditVechicleWindow.xaml
    /// </summary>
    public partial class EditServicesWindow : Window
    {
        public Customer selectedCustomer { get; set; }

        // https://www.flaticon.com/packs/car-parts-2

        public EditServicesWindow()
        {
            InitializeComponent();
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem item = (ComboBoxItem)e.AddedItems[0];
            if ((String)item.Content == "Panel")
            {
                frame.NavigationService.Navigate(new Page1());
            }
            if ((String)item.Content == "Tires")
            {
                frame.NavigationService.Navigate(new Tires());
            }
        }
    }
}
