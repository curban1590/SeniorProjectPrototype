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
using System.IO;
using System.Runtime.Serialization.Json;
using System.Runtime.Serialization;

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
        public static RoutedCommand MyComandEmail = new RoutedCommand();
        public static RoutedCommand MyComandInvoice = new RoutedCommand();
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

            MyComandEmail.InputGestures.Add(new KeyGesture(Key.M, ModifierKeys.Control));
            CommandBindings.Add(new CommandBinding(MyComandEmail, Button_Email_Click));

            MyComandInvoice.InputGestures.Add(new KeyGesture(Key.I, ModifierKeys.Control));
            CommandBindings.Add(new CommandBinding(MyComandInvoice, Button_Invoice_Click));

            MyComandLogout.InputGestures.Add(new KeyGesture(Key.L, ModifierKeys.Control));
            CommandBindings.Add(new CommandBinding(MyComandLogout, Logout_MenuItem_Click));

            MyComandExit.InputGestures.Add(new KeyGesture(Key.Q, ModifierKeys.Control));
            CommandBindings.Add(new CommandBinding(MyComandExit, Exit_MenuItem_Click));

            MyComandAbout.InputGestures.Add(new KeyGesture(Key.A, ModifierKeys.Control));
            CommandBindings.Add(new CommandBinding(MyComandAbout, About_MenuItem_Click));

            setMechanics();
            Add_Person_Button.Focus();
            calendar.SelectedDate = DateTime.Today;
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

        private void Add_Person_Button_Click(object sender, RoutedEventArgs e)
        {
            if (WindowsManeger.loggedInEmployee.JobTitle == "Admin")
            {
                WindowsManeger.OpenSearch(true);
            }
            else
            {
                MessageBox.Show("Must be logged in as Admin to use this feature", "Permission Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Button_Add_Client_Click(object sender, RoutedEventArgs e)
        {
            if (WindowsManeger.loggedInEmployee.JobTitle == "Admin")
            {
                WindowsManeger.OpenAddClient();
            }
            else
            {
                MessageBox.Show("Must be logged in as Admin to use this feature", "Permission Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Button_Add_Employee_Click(object sender, RoutedEventArgs e)
        {
            if (WindowsManeger.loggedInEmployee.JobTitle == "Admin")
            {
                WindowsManeger.OpenAddEmployee();
            }
            else
            {
                MessageBox.Show("Must be logged in as Admin to use this feature", "Permission Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Button_Search_Click(object sender, RoutedEventArgs e)
        {
            if (WindowsManeger.loggedInEmployee.JobTitle == "Admin" || WindowsManeger.loggedInEmployee.JobTitle == "Technician")
            {
                WindowsManeger.OpenSearch(false);
            }
            else
            {
                MessageBox.Show("Must be logged in as a Admin or Technician to use this feature", "Permission Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void AddClient_MenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (WindowsManeger.loggedInEmployee.JobTitle == "Admin")
            {
                Button_Add_Client_Click(sender, e);
            }
            else
            {
                MessageBox.Show("Must be logged in as Admin to use this feature", "Permission Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void AddEmployees_MenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (WindowsManeger.loggedInEmployee.JobTitle == "Admin")
            {
                Button_Add_Employee_Click(sender, e);
            }
            else
            {
                MessageBox.Show("Must be logged in as Admin to use this feature", "Permission Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
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
            AboutWindow Win = new AboutWindow();
            Win.Show();
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
                if (Mechanincs_ComboBox.SelectedItem.ToString() != "All")
                {
                    aMechanic = mechanics.Find(x => x.employeeID == ((Mechanic)Mechanincs_ComboBox.SelectedItem).employeeID);
                    FillTheDataGrid();
                }
                else
                {
                    MySqlManipulator mySqlManipulator = new MySqlManipulator();

                    mySqlManipulator.login();

                    List<Appointment> localApp = new List<Appointment>();
                    foreach(Mechanic mechanic in mechanics)
                    {
                        localApp = mySqlManipulator.getAppointment(mechanic.employeeID, Convert.ToDateTime(calendar.SelectedDate));
                        foreach(Appointment app in localApp)
                        {
                            app.description = "Booked";
                            aMechanic.add(app.time, app.duration, "1", "1", app.description);
                        }
                    }
                    FillTheDataGrid();
                }
            }
            catch
            {

            }
        }

        private void Button_Add_Appointment_Click(object sender, RoutedEventArgs e)
        {
            WindowsManeger.OpenAddAppointment();
        }

        private void Button_Edit_Appointments_Click(object sender, RoutedEventArgs e)
        {
            MySqlManipulator mySqlManipulator = new MySqlManipulator();

            mySqlManipulator.login();

            DateTime selectedDate = Convert.ToDateTime(calendar.SelectedDate);

            List<Appointment> appointments = mySqlManipulator.getAppointmentsFor(selectedDate.Month.ToString(), selectedDate.Day.ToString());

            if(appointments == null || appointments.Count == 0)
            {
                MessageBox.Show("There are no appointments for the selected day!", "No Appointments", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else
            {
                WindowsManeger.OpenEditAppointments(appointments);
            }
        }

        private void Button_Email_Click(object sender, RoutedEventArgs e)
        {
            WindowsManeger.OpenEmail();
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
                        cell.Background = Brushes.Red;
                        break;
                }
            }
            else cell.Background = Brushes.White;
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

        private void DataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // http://dotnetpattern.com/wpf-datagrid-selectionmode
            if (Mechanincs_ComboBox.SelectedItem.ToString() != "All")
            {
                List<SomeClass> employees = new List<SomeClass>();
                string display = "";

                IList items = dataGrid.SelectedItems;


                foreach (object item in items)
                {
                    employees.Add(item as SomeClass);
                }

                foreach (Appointment appointment in aMechanic.timeSlots)
                {
                    if (appointment.appointmentDescription == employees[0].Description)
                    {
                        MySqlManipulator mySqlManipulator = new MySqlManipulator();

                        mySqlManipulator.login();

                        Appointment localAppointment = mySqlManipulator.getAppointment(appointment.appointmentID);

                        string durationStr = Convert.ToString(localAppointment.duration);
                        string startOfTime = durationStr.Substring(0, durationStr.Length - 2);
                        string endOfTime = durationStr.Substring(durationStr.Length - 2);
                        int hour = 0;
                        if (localAppointment.duration >= 100)
                        {
                            hour = Convert.ToInt32(startOfTime);
                        }
                        int minutes = Convert.ToInt32(endOfTime);

                        DateTime endTime = localAppointment.getDateTime().AddHours(hour);
                        endTime.AddMinutes(minutes);

                        display += "Customer: " + mySqlManipulator.getCustomer(appointment.customerID).FName + " " + mySqlManipulator.getCustomer(appointment.customerID).LName + "\n"
                            + "Phone: " + mySqlManipulator.getCustomer(appointment.customerID).PhoneNum + "\n"
                            + "Start: " + localAppointment.getDateTime() + "\n"
                            + "End : " + endTime + "\n"
                            + "Status: " + localAppointment.status + "\n"
                            + "Description: " + appointment.appointmentDescription;

                        MessageBox.Show(display, "Booked Appointment", MessageBoxButton.OK);
                        return;
                    }
                }
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

        private void JSON_MenuItem_Click(object sender, RoutedEventArgs e)
        {
            string filename = "Appointments.json";
            JSONCalendar calendar = new JSONCalendar();


            System.IO.FileStream writer = new FileStream(filename, FileMode.Create, FileAccess.Write);

            DataContractJsonSerializer ser;
            ser = new DataContractJsonSerializer(typeof(JSONCalendar));

            ser.WriteObject(writer, calendar);
            writer.Close();

            MessageBox.Show("JSON Created");
        }

        private void Button_Invoice_Click(object sender, RoutedEventArgs e)
        {
            WindowsManeger.OpenInvoice();
        }
    }
}




/** INVOICING
 * string filename = "Invoice\\Invoice.docx";
    //System.IO.FileStream writer = new FileStream(filename, FileMode.Create, FileAccess.Write);



    FileStream fs1 = new FileStream(filename, FileMode.OpenOrCreate, FileAccess.Write);
    StreamWriter writer = new StreamWriter(fs1);
    writer.Write("Hello Welcome");
    writer.
    writer.Close();



    ser.WriteObject(writer, calendar);

    string fileName = "c://msdn.bmp";  //the picture file to be inserted
    Object oMissed = doc.Paragraphs[2].Range; //the position you want to insert
    Object oLinkToFile = false;  //default
    Object oSaveWithDocument = true;//default
    wir.InlineShapes.AddPicture(fieldName, ref oLinkToFile, ref oSaveWithDocument, ref oMissed);
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

