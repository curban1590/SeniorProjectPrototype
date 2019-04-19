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
    /// Interaction logic for AddAppointmentPage.xaml
    /// </summary> 
    public partial class AddAppointmentPage : Page
    {
        int totalPrice;
        int totalDuration;

        Customer selectedCustomer = new Customer();
        Car selectedCar;

        Service acService;
        Service oilService;
        Service fuseService;
        Service doorService;
        Service tireService;
        Service wiperService;
        Service engineService;
        Service coolantService;
        Service batteryService;
        Service diagnosisService;
        Service autostartService;
        Service gearShiftService;
        Service seatbeltRepairService;

        public AddAppointmentPage()
        {
            InitializeComponent();
        }

        private void AddAppointment_Loaded(object sender, RoutedEventArgs e)
        {
            MySqlManipulator mySqlManipulator = new MySqlManipulator();

            mySqlManipulator.login();

            List<Customer> customers = mySqlManipulator.allCustomers();

            foreach (Customer cus in customers)
            {
                customer_ListView.Items.Add(cus);
            }

            Fuse_Slider.IsEnabled = false;
            Door_Slider.IsEnabled = false;
            Tire_Slider.IsEnabled = false;
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

        private void Customer_ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                selectedCar = null;
                selectedCustomer = (Customer)e.AddedItems[0];

                ShowVehicles();
            }
            catch
            {

            }
        }

        private void Vehicle_ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                selectedCar = (Car)e.AddedItems[0];
            }
            catch
            {

            }
        }

        private void ShowVehicles()
        {
            vehicle_ListView.Items.Clear();

            MySqlManipulator mySqlManipulator = new MySqlManipulator();

            mySqlManipulator.login();

            List<Car> cars;

            cars = mySqlManipulator.allCarsFor(selectedCustomer);

            foreach (Car car in cars)
            {
                vehicle_ListView.Items.Add(car);
            }
        }

        private void Diagnosis_Button_Click(object sender, RoutedEventArgs e)
        {
            if (selectedCar != null)
            {
                if (Diagnosis_Button.Background == null)
                {
                    Diagnosis_Button.Background = Brushes.LimeGreen;

                    MySqlManipulator mySqlManipulator = new MySqlManipulator();

                    mySqlManipulator.login();

                    diagnosisService = mySqlManipulator.GetService("Diagnosis");
                    diagnosisService.quantity = 1;

                    Services_Listview.Items.Add(diagnosisService);
                }
                else
                {
                    Diagnosis_Button.Background = null;
                    Services_Listview.Items.Remove(diagnosisService);
                    Services_Listview.Items.Refresh();
                }
                updatePrice();
                updateDuration();
            }
            else
            {
                MessageBox.Show("Please select a customer and a vehicle!", "Customer and Vechile Required", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        private void AC_Button_Click(object sender, RoutedEventArgs e)
        {
            if (selectedCar != null)
            {
                if (AC_Button.Background == null)
                {
                    AC_Button.Background = Brushes.LimeGreen;

                    MySqlManipulator mySqlManipulator = new MySqlManipulator();

                    mySqlManipulator.login();

                    acService = mySqlManipulator.GetService("Air Filter Change");
                    acService.quantity = 1;

                    Services_Listview.Items.Add(acService);
                }
                else
                {
                    AC_Button.Background = null;
                    Services_Listview.Items.Remove(acService);
                    Services_Listview.Items.Refresh();
                }
                updatePrice();
                updateDuration();
            }
            else
            {
                MessageBox.Show("Please select a customer and a vehicle!", "Customer and Vechile Required", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        private void Fuse_Button_Click(object sender, RoutedEventArgs e)
        {
            if (selectedCar != null)
            {
                if (Fuse_Button.Background == null)
                {
                    Fuse_Slider.IsEnabled = true;
                    Fuse_Button.Background = Brushes.LimeGreen;

                    MySqlManipulator mySqlManipulator = new MySqlManipulator();

                    mySqlManipulator.login();

                    fuseService = mySqlManipulator.GetService("Dashboard Fuse");
                    fuseService.quantity = Convert.ToInt32(Fuse_Slider.Value);

                    Services_Listview.Items.Add(fuseService);
                }
                else
                {
                    Fuse_Slider.IsEnabled = false;
                    Fuse_Button.Background = null;
                    Services_Listview.Items.Remove(fuseService);
                    Services_Listview.Items.Refresh();
                }
                updatePrice();
                updateDuration();
            }
            else
            {
                MessageBox.Show("Please select a customer and a vehicle!", "Customer and Vechile Required", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        private void Fuse_Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (Fuse_Slider.IsEnabled && fuseService != null)
            {
                fuseService.quantity = Convert.ToInt32(Fuse_Slider.Value);
                fuseService.duration = fuseService.quantity * 30;
                fuseService.price = fuseService.quantity * 20;
                Services_Listview.Items.Refresh();
            }
            updatePrice();
            updateDuration();
        }

        private void Door_Button_Click(object sender, RoutedEventArgs e)
        {
            if (selectedCar != null)
            {
                if (Door_Button.Background == null)
                {
                    Door_Slider.IsEnabled = true;
                    Door_Button.Background = Brushes.LimeGreen;

                    MySqlManipulator mySqlManipulator = new MySqlManipulator();

                    mySqlManipulator.login();

                    doorService = mySqlManipulator.GetService("Door Repair");
                    doorService.quantity = Convert.ToInt32(Door_Slider.Value);

                    Services_Listview.Items.Add(doorService);
                }
                else
                {
                    Door_Slider.IsEnabled = false;
                    Door_Button.Background = null;
                    Services_Listview.Items.Remove(doorService);
                    Services_Listview.Items.Refresh();
                }
                updatePrice();
                updateDuration();
            }
            else
            {
                MessageBox.Show("Please select a customer and a vehicle!", "Customer and Vechile Required", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        private void Door_Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (Door_Slider.IsEnabled && doorService != null)
            {
                doorService.quantity = Convert.ToInt32(Door_Slider.Value);
                doorService.duration = doorService.quantity * 60;
                doorService.price = doorService.quantity * 65;
                Services_Listview.Items.Refresh();
            }
            updatePrice();
            updateDuration();
        }

        private void Tire_Button_Click(object sender, RoutedEventArgs e)
        {
            if (selectedCar != null)
            {
                if (Tire_Button.Background == null)
                {
                    Tire_Slider.IsEnabled = true;
                    Tire_Button.Background = Brushes.LimeGreen;

                    MySqlManipulator mySqlManipulator = new MySqlManipulator();

                    mySqlManipulator.login();

                    tireService = mySqlManipulator.GetService("Tire Replacement");
                    tireService.quantity = Convert.ToInt32(Tire_Slider.Value);

                    Services_Listview.Items.Add(tireService);
                }
                else
                {
                    Tire_Slider.IsEnabled = false;
                    Tire_Button.Background = null;
                    Services_Listview.Items.Remove(tireService);
                    Services_Listview.Items.Refresh();
                }
                updatePrice();
                updateDuration();
            }
            else
            {
                MessageBox.Show("Please select a customer and a vehicle!", "Customer and Vechile Required", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        private void Tire_Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (Tire_Slider.IsEnabled && tireService != null)
            {
                tireService.quantity = Convert.ToInt32(Tire_Slider.Value);
                tireService.duration = tireService.quantity * 30;
                tireService.price = tireService.quantity * 130;
                Services_Listview.Items.Refresh();
            }
            updatePrice();
            updateDuration();
        }

        private void Engine_Button_Click(object sender, RoutedEventArgs e)
        {
            if (selectedCar != null)
            {
                if (Engine_Button.Background == null)
                {
                    Engine_Button.Background = Brushes.LimeGreen;

                    MySqlManipulator mySqlManipulator = new MySqlManipulator();

                    mySqlManipulator.login();

                    engineService = mySqlManipulator.GetService("Engine Flush");
                    engineService.quantity = 1;

                    Services_Listview.Items.Add(engineService);
                }
                else
                {
                    Engine_Button.Background = null;
                    Services_Listview.Items.Remove(engineService);
                    Services_Listview.Items.Refresh();
                }
                updatePrice();
                updateDuration();
            }
            else
            {
                MessageBox.Show("Please select a customer and a vehicle!", "Customer and Vechile Required", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        private void Key_Button_Click(object sender, RoutedEventArgs e)
        {
            if (selectedCar != null)
            {
                if (Key_Button.Background == null)
                {
                    Key_Button.Background = Brushes.LimeGreen;

                    MySqlManipulator mySqlManipulator = new MySqlManipulator();

                    mySqlManipulator.login();

                    autostartService = mySqlManipulator.GetService("Auto-start Install");
                    autostartService.quantity = 1;

                    Services_Listview.Items.Add(autostartService);
                }
                else
                {
                    Key_Button.Background = null;
                    Services_Listview.Items.Remove(autostartService);
                    Services_Listview.Items.Refresh();
                }
                updatePrice();
                updateDuration();
            }
            else
            {
                MessageBox.Show("Please select a customer and a vehicle!", "Customer and Vechile Required", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }
        
        private void Gear_Button_Click(object sender, RoutedEventArgs e)
        {
            if (selectedCar != null)
            {
                if (Gear_Button.Background == null)
                {
                    Gear_Button.Background = Brushes.LimeGreen;

                    MySqlManipulator mySqlManipulator = new MySqlManipulator();

                    mySqlManipulator.login();

                    gearShiftService = mySqlManipulator.GetService("Gear Shift Repair");
                    gearShiftService.quantity = 1;

                    Services_Listview.Items.Add(gearShiftService);
                }
                else
                {
                    Gear_Button.Background = null;
                    Services_Listview.Items.Remove(gearShiftService);
                    Services_Listview.Items.Refresh();
                }
                updatePrice();
                updateDuration();
            }
            else
            {
                MessageBox.Show("Please select a customer and a vehicle!", "Customer and Vechile Required", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        private void Oil_Button_Click(object sender, RoutedEventArgs e)
        {
            if (selectedCar != null)
            {
                if (Oil_Button.Background == null)
                {
                    Oil_Button.Background = Brushes.LimeGreen;

                    MySqlManipulator mySqlManipulator = new MySqlManipulator();

                    mySqlManipulator.login();

                    oilService = mySqlManipulator.GetService("Oil Change");
                    oilService.quantity = 1;

                    Services_Listview.Items.Add(oilService);
                }
                else
                {
                    Oil_Button.Background = null;
                    Services_Listview.Items.Remove(oilService);
                    Services_Listview.Items.Refresh();
                }
                updatePrice();
                updateDuration();
            }
            else
            {
                MessageBox.Show("Please select a customer and a vehicle!", "Customer and Vechile Required", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        private void Seatbelt_Button_Click(object sender, RoutedEventArgs e)
        {
            if (selectedCar != null)
            {
                if (Seatbelt_Button.Background == null)
                {
                    Seatbelt_Button.Background = Brushes.LimeGreen;

                    MySqlManipulator mySqlManipulator = new MySqlManipulator();

                    mySqlManipulator.login();

                    seatbeltRepairService = mySqlManipulator.GetService("Seat-belt Repair");
                    seatbeltRepairService.quantity = 1;

                    Services_Listview.Items.Add(seatbeltRepairService);
                }
                else
                {
                    Seatbelt_Button.Background = null;
                    Services_Listview.Items.Remove(seatbeltRepairService);
                    Services_Listview.Items.Refresh();
                }
                updatePrice();
                updateDuration();
            }
            else
            {
                MessageBox.Show("Please select a customer and a vehicle!", "Customer and Vechile Required", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }
        
        private void Coolant_Button_Click(object sender, RoutedEventArgs e)
        {
            if (selectedCar != null)
            {
                if (Coolant_Button.Background == null)
                {
                    Coolant_Button.Background = Brushes.LimeGreen;

                    MySqlManipulator mySqlManipulator = new MySqlManipulator();

                    mySqlManipulator.login();

                    coolantService = mySqlManipulator.GetService("Coolant Flush");
                    coolantService.quantity = 1;

                    Services_Listview.Items.Add(coolantService);
                }
                else
                {
                    Coolant_Button.Background = null;
                    Services_Listview.Items.Remove(coolantService);
                    Services_Listview.Items.Refresh();
                }
                updatePrice();
                updateDuration();
            }
            else
            {
                MessageBox.Show("Please select a customer and a vehicle!", "Customer and Vechile Required", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }
        
        private void Wiper_Button_Click(object sender, RoutedEventArgs e)
        {
            if (selectedCar != null)
            {
                if (Wiper_Button.Background == null)
                {
                    Wiper_Button.Background = Brushes.LimeGreen;

                    MySqlManipulator mySqlManipulator = new MySqlManipulator();

                    mySqlManipulator.login();

                    wiperService = mySqlManipulator.GetService("Wiper Replacement");
                    wiperService.quantity = 1;

                    Services_Listview.Items.Add(wiperService);
                }
                else
                {
                    Wiper_Button.Background = null;
                    Services_Listview.Items.Remove(wiperService);
                    Services_Listview.Items.Refresh();
                }
                updatePrice();
                updateDuration();
            }
            else
            {
                MessageBox.Show("Please select a customer and a vehicle!", "Customer and Vechile Required", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        private void Battery_Button_Click(object sender, RoutedEventArgs e)
        {
            if (selectedCar != null)
            {
                if (Battery_Button.Background == null)
                {
                    Battery_Button.Background = Brushes.LimeGreen;

                    MySqlManipulator mySqlManipulator = new MySqlManipulator();

                    mySqlManipulator.login();

                    batteryService = mySqlManipulator.GetService("Battery Change");
                    batteryService.quantity = 1;

                    Services_Listview.Items.Add(batteryService);
                }
                else
                {
                    Battery_Button.Background = null;
                    Services_Listview.Items.Remove(batteryService);
                    Services_Listview.Items.Refresh();
                }
                updatePrice();
                updateDuration();
            }
            else
            {
                MessageBox.Show("Please select a customer and a vehicle!", "Customer and Vechile Required", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        private void updatePrice()
        {
            totalPrice = 0;

            foreach(Service service in Services_Listview.Items)
            {
                totalPrice += service.price;
            }
            if (Total_Label != null)
            {
                Total_Label.Text = totalPrice.ToString();
            }
        }

        private void updateDuration()
        {
            totalDuration = 0;
            int hours = 0;
            int minutes = 0;
            string myTime = "";

            foreach (Service service in Services_Listview.Items)
            {
                totalDuration += service.duration;
            }

            minutes = totalDuration;

            while(minutes >= 60)
            {
                minutes -= 60;
                hours += 1;
            }

            if(hours == 0)
            {
                myTime = minutes + " min.";
            }
            else
            {
                if(minutes == 0)
                {
                    myTime = hours + ":00";
                }
                else
                {
                    myTime = hours + ":" + minutes;
                }
            }

            if (Duration_Label != null)
            {
                Duration_Label.Content = myTime;
            }
        }
    }
}
