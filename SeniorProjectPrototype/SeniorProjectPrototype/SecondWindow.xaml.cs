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
    /// Interaction logic for SecondWindow.xaml
    /// </summary>
    public partial class SecondWindow : Window
    {
        public Page pageToBeLoaded { get; set; }
        public string title { get; set; }

        public SecondWindow()
        {
            InitializeComponent();
        } 

        private void Frame2_Loaded(object sender, RoutedEventArgs e)
        {
            this.Title = title;
            frame2.NavigationService.Navigate(pageToBeLoaded);
            
        }

    }
}
