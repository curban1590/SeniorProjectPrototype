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

        private List<Mechanic> mechanics;
        Appointment selectedAppointment = new Appointment();

        Mechanic selectedMechanic;
        bool allMechanics = false;
        Customer selectedCustomer = new Customer();
        Car selectedCar;
        DateTime selectedDateTime;

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
                if (cus.isActive)
                {
                    customer_ListView.Items.Add(cus);
                }
            }

            Fuse_Slider.IsEnabled = false;
            Door_Slider.IsEnabled = false;
            Tire_Slider.IsEnabled = false;

            setMechanics();
        }

        private void setMechanics()
        {
            MySqlManipulator mySqlManipulator = new MySqlManipulator();

            mySqlManipulator.login();

            mechanics = mySqlManipulator.allMechanics();

            LoadMechanicsComboBox();
        }

        private void LoadMechanicsComboBox()
        {
            Mechanincs_ComboBox.Items.Clear();

            foreach (Mechanic mec in mechanics)
            {
                mec.loadAppointments();
                Mechanincs_ComboBox.Items.Add(mec);
            }

            Mechanincs_ComboBox.Items.Add("All Avaliable");
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
                if (cus.isActive)
                {
                    customer_ListView.Items.Add(cus);
                }
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
            selectVehicle_Label.Visibility = Visibility.Visible;

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
            selectedCar = new Car();
            selectedCar = (Car)e.AddedItems[0];
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

        #region Services Button Methods
        private void Diagnosis_Button_Click(object sender, RoutedEventArgs e)
        {
            if (selectedCar != null)
            {
                if (Diagnosis_Button.Background == null)
                {
                    Diagnosis_Button.Background = Brushes.LimeGreen;

                    MySqlManipulator mySqlManipulator = new MySqlManipulator();

                    mySqlManipulator.login();

                    diagnosisService = mySqlManipulator.getService("Diagnosis");
                    diagnosisService.quantity = 1;
                    diagnosisService.setServiceID("8");

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
                tabControl.SelectedItem = tabControl.Items[0];
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

                    acService = mySqlManipulator.getService("Air Filter Change");
                    acService.quantity = 1;
                    acService.setServiceID("2");

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
                tabControl.SelectedItem = tabControl.Items[0];
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

                    fuseService = mySqlManipulator.getService("Dashboard Fuse");
                    fuseService.quantity = Convert.ToInt32(Fuse_Slider.Value);
                    fuseService.setServiceID("5");

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
                tabControl.SelectedItem = tabControl.Items[0];
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

                    doorService = mySqlManipulator.getService("Door Repair");
                    doorService.quantity = Convert.ToInt32(Door_Slider.Value);
                    doorService.setServiceID("6");

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
                tabControl.SelectedItem = tabControl.Items[0];
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

                    tireService = mySqlManipulator.getService("Tire Replacement");
                    tireService.quantity = Convert.ToInt32(Tire_Slider.Value);
                    tireService.setServiceID("4");

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
                tabControl.SelectedItem = tabControl.Items[0];
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

                    engineService = mySqlManipulator.getService("Engine Flush");
                    engineService.quantity = 1;
                    engineService.setServiceID("12");

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
                tabControl.SelectedItem = tabControl.Items[0];
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

                    autostartService = mySqlManipulator.getService("Auto-start Install");
                    autostartService.quantity = 1;
                    autostartService.setServiceID("13");

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
                tabControl.SelectedItem = tabControl.Items[0];
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

                    gearShiftService = mySqlManipulator.getService("Gear Shift Repair");
                    gearShiftService.quantity = 1;
                    gearShiftService.setServiceID("7");

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
                tabControl.SelectedItem = tabControl.Items[0];
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

                    oilService = mySqlManipulator.getService("Oil Change");
                    oilService.quantity = 1;
                    oilService.setServiceID("1");

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
                tabControl.SelectedItem = tabControl.Items[0];
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

                    seatbeltRepairService = mySqlManipulator.getService("Seat-belt Repair");
                    seatbeltRepairService.quantity = 1;
                    seatbeltRepairService.setServiceID("11");

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
                tabControl.SelectedItem = tabControl.Items[0];
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

                    coolantService = mySqlManipulator.getService("Coolant Flush");
                    coolantService.quantity = 1;
                    coolantService.setServiceID("10");

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
                tabControl.SelectedItem = tabControl.Items[0];
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

                    wiperService = mySqlManipulator.getService("Wiper Replacement");
                    wiperService.quantity = 1;
                    wiperService.setServiceID("3");

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
                tabControl.SelectedItem = tabControl.Items[0];
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

                    batteryService = mySqlManipulator.getService("Battery Change");
                    batteryService.quantity = 1;
                    batteryService.setServiceID("9");

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
                tabControl.SelectedItem = tabControl.Items[0];
            }
        }
        #endregion

        private void updatePrice()
        {
            totalPrice = 0;

            foreach (Service service in Services_Listview.Items)
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

            while (minutes >= 60)
            {
                minutes -= 60;
                hours += 1;
            }

            if (hours == 0)
            {
                myTime = minutes + " min.";
            }
            else
            {
                if (minutes == 0)
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

            string minutesSTR;
            if (minutes == 0)
            {
                minutesSTR = "00";
            }
            else
            {
                minutesSTR = minutes.ToString();
            }
            string durationStr = hours.ToString() + minutesSTR;
            totalDuration = Convert.ToInt32(durationStr);
        }

        private void SelectDate_Button_Checked(object sender, RoutedEventArgs e)
        {
            calendar.Visibility = Visibility.Visible;
            nextAavaliable_Button.IsChecked = false;
            rectangle.Visibility = Visibility.Hidden;
        }

        private void NextAavaliable_Button_Checked(object sender, RoutedEventArgs e)
        {
            calendar.Visibility = Visibility.Hidden;
            selectDate_Button.IsChecked = false;
            rectangle.Visibility = Visibility.Hidden;
        }

        private void Mechanincs_ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Mechanincs_ComboBox.SelectedItem.ToString() == "All Avaliable")
            {
                allMechanics = true;
            }
            else
            {
                allMechanics = false;
                selectedMechanic = mechanics.Find(x => x.employeeID == ((Mechanic)Mechanincs_ComboBox.SelectedItem).employeeID);
            }
        }

        private void Search_Button_Click(object sender, RoutedEventArgs e)
        {
            appointmentListView.Items.Clear();
            Book_Button.Visibility = Visibility.Hidden;

            if (selectDate_Button.IsChecked == false && nextAavaliable_Button.IsChecked == false)
            {
                MessageBox.Show("Please select a date option!", "Date Required!", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                rectangle.Visibility = Visibility.Visible;
                return;
            }

            DateTime tempTime = Convert.ToDateTime(calendar.SelectedDate);

            if (tempTime.DayOfWeek == DayOfWeek.Saturday || tempTime.DayOfWeek == DayOfWeek.Sunday)
            {
                MessageBox.Show("Closed on the weekends!\nPlease select a weekday!", "Invalid Day", MessageBoxButton.OK, MessageBoxImage.Hand);
                return;
            }
            if (tempTime.Date < DateTime.Today && selectDate_Button.IsChecked == true)
            {
                MessageBox.Show("Cannot schedule appointment for prior day!", "Invalid Day", MessageBoxButton.OK, MessageBoxImage.Hand);
                return;
            }

            if (selectedMechanic == null && allMechanics == false)
            {
                MessageBox.Show("Please select a mechanic or all!", "Mechanic Required!", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }

            if (Services_Listview.Items.Count == 0)
            {
                MessageBox.Show("Please select atleast one service!", "Service Required!", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                tabControl.SelectedItem = tabControl.Items[1];
                return;
            }
            if (totalDuration > 800)
            {
                MessageBox.Show("Too many services selected!", "Too many services!", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }

            if (allMechanics && nextAavaliable_Button.IsChecked == true)
            {
                List<Mechanic> localMechanics = mechanics;
                bool foundDate = false;

                foreach (Mechanic mec in localMechanics)
                {
                    while (!foundDate)
                    {
                        for (int i = 0; i < mec.timeSlots.Length; i++)
                        {
                            if (mec.canAdd(mec.timeSlots[i].time, totalDuration))
                            {
                                Appointment appointment = new Appointment();

                                appointment.customerID = selectedCustomer.ID;
                                appointment.customerName = selectedCustomer.FName + " " + selectedCustomer.LName;
                                appointment.employeeID = mec.employeeID;
                                appointment.employeeName = mec.employeeName;
                                appointment.setDateTime(mec.getDate());
                                appointment.date = (mec.getDate()).ToShortDateString();
                                appointment.time = mec.timeSlots[i].time;
                                appointment.duration = totalDuration;

                                foreach (Service service in Services_Listview.Items)
                                {
                                    appointment.services.Add(service);
                                }

                                appointmentListView.Items.Add(appointment);

                                foundDate = true;
                                i = mec.timeSlots.Length;
                            }
                        }
                        
                        DateTime mecDate = mec.getDate();

                        if (mecDate.DayOfWeek == DayOfWeek.Friday)
                        {
                            mec.setDate(mecDate.AddDays(3));
                        }
                        else
                        {
                            mec.setDate(mecDate.AddDays(1));
                        }
                    }
                    foundDate = false;
                }
            }
            else if (allMechanics && selectedDateTime != null)
            {
                List<Mechanic> localMechanics = mechanics;

                foreach (Mechanic mec in localMechanics)
                {
                    mec.setDate(selectedDateTime);

                    for (int i = 0; i < mec.timeSlots.Length; i++)
                    {
                        if (mec.canAdd(mec.timeSlots[i].time, totalDuration))
                        {
                            Appointment appointment = new Appointment();

                            appointment.customerID = selectedCustomer.ID;
                            appointment.customerName = selectedCustomer.FName + " " + selectedCustomer.LName;
                            appointment.employeeID = mec.employeeID;
                            appointment.employeeName = mec.employeeName;
                            appointment.setDateTime(mec.getDate());
                            appointment.date = (mec.getDate()).ToShortDateString();
                            appointment.time = mec.timeSlots[i].time;
                            appointment.duration = totalDuration;

                            foreach (Service service in Services_Listview.Items)
                            {
                                appointment.services.Add(service);
                            }

                            appointmentListView.Items.Add(appointment);

                            i = mec.timeSlots.Length;
                        }
                    }
                }
            }
            else if (!allMechanics && nextAavaliable_Button.IsChecked == true)
            {
                List<Mechanic> localMechanics = mechanics;
                Mechanic mec = localMechanics.Find(x => x.employeeID == ((Mechanic)Mechanincs_ComboBox.SelectedItem).employeeID);
                int count = 0;
                while (count < 15)
                {
                    for (int i = 0; i < mec.timeSlots.Length && count < 15; i++)
                    {
                        if (mec.canAdd(mec.timeSlots[i].time, totalDuration))
                        {
                            Appointment appointment = new Appointment();

                            appointment.customerID = selectedCustomer.ID;
                            appointment.customerName = selectedCustomer.FName + " " + selectedCustomer.LName;
                            appointment.employeeID = mec.employeeID;
                            appointment.employeeName = mec.employeeName;
                            appointment.setDateTime(mec.getDate());
                            appointment.date = (mec.getDate()).ToShortDateString();
                            appointment.time = mec.timeSlots[i].time;
                            appointment.duration = totalDuration;

                            foreach (Service service in Services_Listview.Items)
                            {
                                appointment.services.Add(service);
                            }

                            appointmentListView.Items.Add(appointment);

                            count++;
                        }
                    }

                    DateTime mecDate = mec.getDate();

                    if (mecDate.DayOfWeek == DayOfWeek.Friday)
                    {
                        mec.setDate(mecDate.AddDays(3));
                    }
                    else
                    {
                        mec.setDate(mecDate.AddDays(1));
                    }
                }
            }
            else
            {
                List<Mechanic> localMechanics = mechanics;
                Mechanic mec = localMechanics.Find(x => x.employeeID == ((Mechanic)Mechanincs_ComboBox.SelectedItem).employeeID);
                mec.setDate(selectedDateTime);
                int count = 0;
                while (count < 15)
                {
                    for (int i = 0; i < mec.timeSlots.Length && count < 15; i++)
                    {
                        if (mec.canAdd(mec.timeSlots[i].time, totalDuration))
                        {
                            Appointment appointment = new Appointment();

                            appointment.customerID = selectedCustomer.ID;
                            appointment.customerName = selectedCustomer.FName + " " + selectedCustomer.LName;
                            appointment.employeeID = mec.employeeID;
                            appointment.employeeName = mec.employeeName;
                            appointment.setDateTime(mec.getDate());
                            appointment.date = (mec.getDate()).ToShortDateString();
                            appointment.time = mec.timeSlots[i].time;
                            appointment.duration = totalDuration;

                            foreach (Service service in Services_Listview.Items)
                            {
                                appointment.services.Add(service);
                            }

                            appointmentListView.Items.Add(appointment);

                            count++;
                        }
                    }
                    DateTime mecDate = mec.getDate();

                    if (mecDate.DayOfWeek == DayOfWeek.Friday)
                    {
                        mec.setDate(mecDate.AddDays(3));
                    }
                    else
                    {
                        mec.setDate(mecDate.AddDays(1));
                    }
                }
            }
        }

        private void Calendar_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            DateTime tempTime = Convert.ToDateTime(calendar.SelectedDate);

            if (tempTime.DayOfWeek == DayOfWeek.Saturday || tempTime.DayOfWeek == DayOfWeek.Sunday)
            {
                MessageBox.Show("Closed on the weekends!\nPlease select a weekday!", "Invalid Day", MessageBoxButton.OK, MessageBoxImage.Hand);
                return;
            }
            if(tempTime < DateTime.Today)
            {
                MessageBox.Show("Cannot schedule appointment for prior day!", "Invalid Day", MessageBoxButton.OK, MessageBoxImage.Hand);
                return;
            }
            selectedDateTime = Convert.ToDateTime(calendar.SelectedDate);
            appointmentListView.Items.Clear();
        }

        private void AppointmentListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Book_Button.Visibility = Visibility.Visible;

            try
            {
                selectedAppointment = (Appointment)e.AddedItems[0];
            }
            catch
            {

            }
        }

        private void Book_Button_Click(object sender, RoutedEventArgs e)
        {
            appointmentListView.Items.Clear();
            selectedAppointment.car = selectedCar;
            WindowsManeger.OpenAppConfirm(selectedAppointment);
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (tabControl.SelectedIndex == 0 || tabControl.SelectedIndex == 1)
            {
                appointmentListView.Items.Clear();
            }
        }
    }
}
