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
    /// Interaction logic for Page2.xaml
    /// </summary>
    public partial class Page2 : Page
    {
        public List<Mechanic> mechanics = new List<Mechanic>();
        public Mechanic aMechanic = new Mechanic();

        public static RoutedCommand MyComandClient = new RoutedCommand();
        public static RoutedCommand MyComandEmployee = new RoutedCommand();
        public static RoutedCommand MyComandSearch = new RoutedCommand();
        public static RoutedCommand MyComandLogout = new RoutedCommand();
        public static RoutedCommand MyComandExit = new RoutedCommand();
        public static RoutedCommand MyComandAbout = new RoutedCommand();

        public Page2()
        {
            InitializeComponent();

            MyComandClient.InputGestures.Add(new KeyGesture(Key.C, ModifierKeys.Control));
            CommandBindings.Add(new CommandBinding(MyComandClient, Button_Add_Client_Click));

            MyComandEmployee.InputGestures.Add(new KeyGesture(Key.E, ModifierKeys.Control));
            CommandBindings.Add(new CommandBinding(MyComandEmployee, Button_Add_Employee_Click));

            MyComandSearch.InputGestures.Add(new KeyGesture(Key.S, ModifierKeys.Control));
            CommandBindings.Add(new CommandBinding(MyComandSearch, Button_Search_Click));

            MyComandLogout.InputGestures.Add(new KeyGesture(Key.L, ModifierKeys.Control));
            CommandBindings.Add(new CommandBinding(MyComandLogout, Logout_MenuItem_Click));

            MyComandExit.InputGestures.Add(new KeyGesture(Key.Q, ModifierKeys.Control));
            CommandBindings.Add(new CommandBinding(MyComandExit, Exit_MenuItem_Click));

            MyComandAbout.InputGestures.Add(new KeyGesture(Key.A, ModifierKeys.Control));
            CommandBindings.Add(new CommandBinding(MyComandAbout, About_MenuItem_Click));

            setMechanics();
            button_Add_Employee.Focus();
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

            Mechanincs_ComboBox.Items.Add("All");
        }

        private void Button_Add_Client_Click(object sender, RoutedEventArgs e)
        {
            WindowsManeger.OpenAddClient();
        }

        private void Button_Add_Employee_Click(object sender, RoutedEventArgs e)
        {
            WindowsManeger.OpenAddEmployee();
        }

        private void Button_Search_Click(object sender, RoutedEventArgs e)
        {
            WindowsManeger.OpenSearch();
        }

        private void AddClient_MenuItem_Click(object sender, RoutedEventArgs e)
        {
            Button_Add_Client_Click(sender, e);
        }

        private void AddEmployees_MenuItem_Click(object sender, RoutedEventArgs e)
        {
            Button_Add_Employee_Click(sender, e);
        }

        private void Edit_MenuItem_Click(object sender, RoutedEventArgs e)
        {
            Button_Search_Click(sender, e);
        }

        private void Logout_MenuItem_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult boxResult = MessageBox.Show("Are you sure you would like to log out?\nLOGIN OUT WILL CLOSE ALL OPEN WINDOWS FOR THIS SESSION!", "Confirm Logout", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (boxResult == MessageBoxResult.Yes)
            {
                WindowsManeger.CloseAllWindows();
                this.NavigationService.Navigate(new Page1());
            }
        }

        private void Exit_MenuItem_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult boxResult = MessageBox.Show("Are you sure you would like to exit?", "Confirm Exit", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (boxResult == MessageBoxResult.Yes)
            {
                System.Windows.Application.Current.Shutdown();
            }
        }

        private void About_MenuItem_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("TJCTJ Systems \nCreators:\n    Jason Diaz \n    Thomas Holzmacher \n    Twinkle Patel\n    James Pina\n    Conner Urban", "About");
        }
        
        private void Email_Click(object sender, RoutedEventArgs e)
        {
            MailMessage message = new MailMessage("tjcjtsystems@gmail.com", "jasondiaz1130@gmail.com", "This is it", "Hellooooooo");

            System.Net.Mail.Attachment attachment;
            attachment = new System.Net.Mail.Attachment(@"Attachments/logo.png");
            message.Attachments.Add(attachment);

            message.IsBodyHtml = true;

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
                threadSendMails = new Thread(delegate()
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
                Debug.WriteLine("EMail Error: " + ex.Message);
            }
        }

        private void Mechanincs_ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            { 
                aMechanic = mechanics.Find(x => x.employeeID == ((Mechanic)Mechanincs_ComboBox.SelectedItem).employeeID);
                FillTheDataGrid();
            }
            catch
            {

            }
        }

        private void Button_Add_Appointment_Click(object sender, RoutedEventArgs e)
        {
            WindowsManeger.OpenAddAppointment();
        }

        private void FillTheDataGrid()
        {
            List<SomeClass> list = new List<SomeClass>();
            string labelStr = "";

            for (int i = 0; i < 9; i++)
            {
                if (i < 3)
                {
                    labelStr = " AM";
                }
                else
                {
                    labelStr = " PM";
                }

                string myTime = aMechanic.timeSlots[i * 4].time.ToString();
                string startOfTime = myTime.Substring(0, myTime.Length - 2);
                int currentHour = (((Convert.ToInt32(startOfTime)) - 1) % 12) + 1;
                string endOfTime = myTime.Substring(myTime.Length - 2);

                list.Add(new SomeClass() { Time = currentHour + labelStr, Description = aMechanic.timeSlots[i * 4].appointmentDescription });

                List<Appointment> appointments = aMechanic.checkHour(aMechanic.timeSlots[i * 4].time);

                if (appointments != null)
                {

                    foreach (Appointment app in appointments)
                    {
                        myTime = app.time.ToString();
                        startOfTime = myTime.Substring(0, myTime.Length - 2);
                        currentHour = (((Convert.ToInt32(startOfTime)) - 1) % 12) + 1;
                        endOfTime = myTime.Substring(myTime.Length - 2);

                        list.Add(new SomeClass() { Time = "\t" + currentHour + ":" + endOfTime + labelStr, Description = app.appointmentDescription });
                    }
                }
            }
            dataGrid.ItemsSource = list;
        }

        private void dataGrid_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            Dispatcher.BeginInvoke(DispatcherPriority.Render, new Action(() => AlterRow(e)));
        }

        private void AlterRow(DataGridRowEventArgs e)
        {
            var cell = GetCell(dataGrid, e.Row, 1);
            if (cell == null) return;

            var item = e.Row.Item as SomeClass;
            if (item == null) return;

            var value = item.Description;

            Random rnd = new Random();
            int num = rnd.Next(7);

            num = (item.Description.Length) % 7;

            //if (value == "Betsie Rose") cell.Background = Brushes.Red;
            if (value != "")
            {
                switch (num)
                {
                    case 0:
                        cell.Background = Brushes.LightBlue;
                        break;
                    case 1:
                        cell.Background = Brushes.LightCoral;
                        break;
                    case 2:
                        cell.Background = Brushes.LightCyan;
                        break;
                    case 3:
                        cell.Background = Brushes.LightGray;
                        break;
                    case 4:
                        cell.Background = Brushes.LightGreen;
                        break;
                    case 5:
                        cell.Background = Brushes.LightPink;
                        break;
                    case 6:
                        cell.Background = Brushes.LightYellow;
                        break;
                }
            }
            else cell.Background = Brushes.White;
            //else cell.Background = Brushes.Green;
        }

        public static DataGridRow GetRow(DataGrid grid, int index)
        {
            var row = grid.ItemContainerGenerator.ContainerFromIndex(index) as DataGridRow;

            if (row == null)
            {
                // may be virtualized, bring into view and try again
                grid.ScrollIntoView(grid.Items[index]);
                row = (DataGridRow)grid.ItemContainerGenerator.ContainerFromIndex(index);
            }
            return row;
        }

        public static T GetVisualChild<T>(Visual parent) where T : Visual
        {
            T child = default(T);
            int numVisuals = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < numVisuals; i++)
            {
                var v = (Visual)VisualTreeHelper.GetChild(parent, i);
                child = v as T ?? GetVisualChild<T>(v);
                if (child != null)
                {
                    break;
                }
            }
            return child;
        }

        public static DataGridCell GetCell(DataGrid host, DataGridRow row, int columnIndex)
        {
            if (row == null) return null;

            var presenter = GetVisualChild<DataGridCellsPresenter>(row);
            if (presenter == null) return null;

            // try to get the cell but it may possibly be virtualized
            var cell = (DataGridCell)presenter.ItemContainerGenerator.ContainerFromIndex(columnIndex);
            if (cell == null)
            {
                // now try to bring into view and retreive the cell
                host.ScrollIntoView(row, host.Columns[columnIndex]);
                cell = (DataGridCell)presenter.ItemContainerGenerator.ContainerFromIndex(columnIndex);
            }
            return cell;
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // http://dotnetpattern.com/wpf-datagrid-selectionmode
            List<SomeClass> employees = new List<SomeClass>();

            IList items = dataGrid.SelectedItems;
            foreach (object item in items)
            {
                employees.Add(item as SomeClass);
            }

            foreach (SomeClass some in employees)
            {
                Console.WriteLine(some.toString());
            }
        }

        private void Calendar_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            foreach(Mechanic mec in mechanics)
            {
                mec.setDate(Convert.ToDateTime(calendar.SelectedDate));
            }

            LoadMechanicsComboBox();
            Mechanincs_ComboBox.SelectedItem = aMechanic;
            FillTheDataGrid();
        }
    }
}




        /** INVOICING
        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = System.IO.Directory.GetCurrentDirectory();

            if (saveFileDialog.ShowDialog() == true)
            {
                using (StreamWriter sw = new StreamWriter(saveFileDialog.FileName))
                {
                    sw.WriteLine(emp.id);
                    sw.WriteLine(emp.department);
                }
            }
        }
        **/
    
