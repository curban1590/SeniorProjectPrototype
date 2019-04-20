﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using MySql.Data.MySqlClient;
using System.Security.Cryptography;

namespace SeniorProjectPrototype
{
    class MySqlManipulator
    {
        private MySqlConnection connection;
        private string server;
        private string database;
        private string user;
        private string password;
        private string port;
        private string connectionString;
        private string sslM;

        public void login()
        {

            server = "remotemysql.com";
            database = "iwszrpTbK9";
            user = "iwszrpTbK9";
            password = "YRVCNclT6P";
            port = "3306";
            sslM = "none";

            connectionString = String.Format("server={0};port={1};user id={2}; password={3}; database={4}; SslMode={5}", server, port, user, password, database, sslM);

            connection = new MySqlConnection(connectionString);
            conexion();
        }

        private void conexion()
        {
            try
            {
                connection.Open();

                //MessageBox.Show("Successful Connection To Database");

                connection.Close();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message + connectionString);
            }
        }

        public bool loginCheck(string id, string zip)
        {
            bool isValid = false;

            string command = "SELECT EmployeeID, EmployeeZip FROM tblEmployee WHERE EmployeeID = " + id + " AND EmployeeZip = " + zip + ";";

            MySqlCommand myCommand = new MySqlCommand(command, connection);
            connection.Open();

            MySqlDataReader reader = myCommand.ExecuteReader();

            while (reader.Read())
            {
                if (id == reader.GetString("EmployeeID") && zip == Convert.ToString(reader[1]))
                {
                    isValid = true;
                    break;
                }
            }

            connection.Close();
            return isValid;
        }

        public void addToTable(Employee employee)
        {
            string command = "SELECT MAX(EmployeeID) FROM tblEmployee";
            int id;
            string stringID = "";

            MySqlCommand myCommand = new MySqlCommand(command, connection);
            connection.Open();

            MySqlDataReader reader = myCommand.ExecuteReader();
            while (reader.Read())
            {
                id = Convert.ToInt32(reader.GetString(0));
                id++;
                stringID = id.ToString();
            }

            connection.Close();

            command = "INSERT INTO tblEmployee VALUES (" + stringID + ", " + employee.toSQLString() + ")";

            myCommand = new MySqlCommand(command, connection);
            connection.Open();
            myCommand.ExecuteNonQuery();

            connection.Close();
        }

        public void addToTable(Customer customer)
        {
            string command = "SELECT MAX(CustomerID) FROM tblCustomer";
            int id;
            string stringID = "";

            MySqlCommand myCommand = new MySqlCommand(command, connection);
            connection.Open();

            MySqlDataReader reader = myCommand.ExecuteReader();
            while (reader.Read())
            {
                id = Convert.ToInt32(reader.GetString(0));
                id++;
                stringID = id.ToString();
            }

            connection.Close();

            command = "INSERT INTO tblCustomer VALUES (" + stringID + ", " + customer.toSQLString() + ", \'" + GeneratePassword() + "\')";

            myCommand = new MySqlCommand(command, connection);
            connection.Open();
            myCommand.ExecuteNonQuery();

            connection.Close();
        }

        public void addToTable(Car car)
        {
            string command = "INSERT INTO tblCar VALUES (" + car.toSQLString() + ")";

            MySqlCommand myCommand = new MySqlCommand(command, connection);

            connection.Open();
            myCommand.ExecuteNonQuery();

            connection.Close();
        }

        public void addToTable(Appointment appointment)
        {
            string command = "SELECT MAX(AppointmentID) FROM tblAppointment";
            int id;
            string stringID = "";

            MySqlCommand myCommand = new MySqlCommand(command, connection);
            connection.Open();

            MySqlDataReader reader = myCommand.ExecuteReader();
            while (reader.Read())
            {
                id = Convert.ToInt32(reader.GetString(0));
                id++;
                stringID = id.ToString();
            }

            connection.Close();

            DateTime tempTime = appointment.getDateTime();
            string myTime = appointment.time.ToString();
            string startOfTime = myTime.Substring(0, myTime.Length - 2);
            string endOfTime = myTime.Substring(myTime.Length - 2);

            command = "INSERT INTO tblAppointment VALUES ('" + stringID + "', '" + appointment.customerID + "', '" + appointment.employeeID + "', '" +
                tempTime.Year + "-" + tempTime.Month + "-" + tempTime.Day + " " + startOfTime + ":" + endOfTime + ":00', 'Scheduled', '" +
                appointment.description + "', '" + appointment.duration + "');";

            myCommand = new MySqlCommand(command, connection);
            connection.Open();
            myCommand.ExecuteNonQuery();

            connection.Close();

            command = "SELECT MAX(InvoiceID) FROM tblInvoice";
            string InvoiceID = "";

            myCommand = new MySqlCommand(command, connection);
            connection.Open();

            reader = myCommand.ExecuteReader();
            while (reader.Read())
            {
                id = Convert.ToInt32(reader.GetString(0));
                id++;
                InvoiceID = id.ToString();
            }

            connection.Close();

            int InvoiceTotal = 0;
            foreach(Service service in appointment.services)
            {
                InvoiceTotal += service.price;
            }

            command = "INSERT INTO tblInvoice VALUES (" + InvoiceID + ", " + InvoiceTotal + ", 0, " + stringID + ")";

            myCommand = new MySqlCommand(command, connection);
            connection.Open();
            myCommand.ExecuteNonQuery();

            connection.Close();

            command = "INSERT INTO tblInvoiceServices VALUES ";

            foreach (Service service in appointment.services)
            {

                command += "(" + InvoiceID + ", " + service.getServiceID() + "), ";

            }
            command = command.Remove(command.Length-2, 2);
            command += ";";

            myCommand = new MySqlCommand(command, connection);
            connection.Open();
            myCommand.ExecuteNonQuery();

            connection.Close();
        }

        public List<Employee> allEmployees()
        {
            List<Employee> employees = new List<Employee>();
            Employee employee;

            string command = "SELECT * FROM tblEmployee";
            MySqlCommand myCommand = new MySqlCommand(command, connection);
            connection.Open();

            MySqlDataReader reader = myCommand.ExecuteReader();
            while (reader.Read())
            {
                employee = new Employee();
                employee.ID = reader.GetInt32(0).ToString();
                employee.FName = reader.GetString(1);
                employee.LName = reader.GetString(2);
                employee.Phone = reader.GetString(3);
                employee.StreetNum = reader.GetUInt32(4).ToString();
                employee.StreetName = reader.GetString(5);
                employee.City = reader.GetString(6);
                employee.State = reader.GetString(7);
                employee.Zip = reader.GetUInt32(8).ToString();
                employee.JobTitle = reader.GetString(9);
                employees.Add(employee);
            }
            connection.Close();
            return employees;
        }

        public List<Customer> allCustomers()
        {
            List<Customer> customers = new List<Customer>();
            Customer customer;

            string command = "SELECT * FROM tblCustomer";
            MySqlCommand myCommand = new MySqlCommand(command, connection);
            connection.Open();

            MySqlDataReader reader = myCommand.ExecuteReader();
            while (reader.Read())
            {
                customer = new Customer();
                customer.ID = reader.GetInt32(0).ToString();
                customer.FName = reader.GetString(1);
                customer.LName = reader.GetString(2);
                customer.StreetNum = reader.GetString(3);
                customer.StreetName = reader.GetString(4);
                customer.City = reader.GetString(5);
                customer.State = reader.GetString(6);
                customer.Zip = reader.GetString(7);
                customer.PhoneNum = reader.GetString(8);
                customer.Email = reader.GetString(9);
                customers.Add(customer);
            }
            connection.Close();
            return customers;
        }

        public List<Car> allCarsFor(Customer customer)
        {
            List<Car> cars = new List<Car>();
            Car car;
           
            string command = "SELECT * FROM tblCar WHERE CustomerID = " + customer.ID;
            MySqlCommand myCommand = new MySqlCommand(command, connection);
            connection.Open();

            MySqlDataReader reader = myCommand.ExecuteReader();
            while (reader.Read())
            {
                car = new Car();
                car.VIN = reader.GetInt32(0).ToString();
                car.CustomerID = reader.GetInt32(1).ToString();
                car.Year = reader.GetString(2);
                car.Make = reader.GetString(3);
                car.Model = reader.GetString(4);
                cars.Add(car);
            }
            connection.Close();
            return cars;
        }

        public List<Mechanic> allMechanics()
        {
            List<Mechanic> mechanics = new List<Mechanic>();
            Mechanic mechanic;

            string command = "SELECT EmployeeID, EmployeeFName, EmployeeLName FROM tblEmployee WHERE JobTitle = 'Mechanic'";
            MySqlCommand myCommand = new MySqlCommand(command, connection);
            connection.Open();

            MySqlDataReader reader = myCommand.ExecuteReader();
            while (reader.Read())
            {
                mechanic = new Mechanic();
                mechanic.employeeID = reader.GetInt32(0).ToString();
                mechanic.employeeName = reader.GetString(1) + " " + reader.GetString(2);
                mechanics.Add(mechanic);
            }

            return mechanics;

        }

        public List<Employee> searchEmployees(string search)
        {
            List<Employee> employees = new List<Employee>();
            Employee employee;
            string command = "SELECT * FROM tblEmployee WHERE EmployeeID LIKE '%" + search +
                "%' OR EmployeeFName LIKE '%" + search +
                "%' OR EmployeeLName LIKE '%" + search +
                "%' OR EmployeePhoneNum LIKE '%" + search +
                "%' OR EmployeeStreetNum LIKE '%" + search +
                "%' OR EmployeeStreetName LIKE '%" + search +
                "%' OR EmployeeCity LIKE '%" + search +
                "%' OR EmployeeState LIKE '%" + search +
                "%' OR EmployeeZip LIKE '%" + search +
                "%' OR JobTitle LIKE '%" + search + "%'";

            MySqlCommand myCommand = new MySqlCommand(command, connection);
            connection.Open();

            MySqlDataReader reader = myCommand.ExecuteReader();
            while (reader.Read())
            {
                employee = new Employee();
                employee.ID = reader.GetInt32(0).ToString();
                employee.FName = reader.GetString(1);
                employee.LName = reader.GetString(2);
                employee.Phone = reader.GetString(3);
                employee.StreetNum = reader.GetUInt32(4).ToString();
                employee.StreetName = reader.GetString(5);
                employee.City = reader.GetString(6);
                employee.State = reader.GetString(7);
                employee.Zip = reader.GetUInt32(8).ToString();
                employee.JobTitle = reader.GetString(9);
                employees.Add(employee);
            }
            connection.Close();
            return employees;
        }

        public List<Customer> searchCustomers(string search)
        {
            List<Customer> customers = new List<Customer>();
            Customer customer;
            string command = "SELECT * FROM tblCustomer WHERE CustomerID LIKE '%" + search +
                "%' OR CustomerFName LIKE '%" + search +
                "%' OR CustomerLName LIKE '%" + search +
                "%' OR CustomerStreetNum LIKE '%" + search +
                "%' OR CustomerStreetName LIKE '%" + search +
                "%' OR CustomerCity LIKE '%" + search +
                "%' OR CustomerState LIKE '%" + search +
                "%' OR CustomerZip LIKE '%" + search +
                "%' OR CustomerPhone LIKE '%" + search +
                "%' OR CustomerEmail LIKE '%" + search + "%'";

            MySqlCommand myCommand = new MySqlCommand(command, connection);
            connection.Open();

            MySqlDataReader reader = myCommand.ExecuteReader();
            while (reader.Read())
            {
                customer = new Customer();
                customer.ID = reader.GetInt32(0).ToString();
                customer.FName = reader.GetString(1);
                customer.LName = reader.GetString(2);
                customer.StreetNum = reader.GetString(3);
                customer.StreetName = reader.GetString(4);
                customer.City = reader.GetString(5);
                customer.State = reader.GetString(6);
                customer.Zip = reader.GetString(7);
                customer.PhoneNum = reader.GetString(8);
                customer.Email = reader.GetString(9);
                customers.Add(customer);
            }
            connection.Close();
            return customers;
        }

        public List<Appointment> getAppointment(string employeeID, DateTime date)
        {
            List<Appointment> appointments = new List<Appointment>();
            Appointment appointment;

            string command = "SELECT * FROM tblAppointment WHERE EmployeeID = " + employeeID + " AND Month(AppointmentTime) = " + date.Month + " AND DAY(AppointmentTime) = " + date.Day;
            MySqlCommand myCommand = new MySqlCommand(command, connection);
            connection.Open();

            MySqlDataReader reader = myCommand.ExecuteReader();
            while (reader.Read())
            {
                appointment = new Appointment();
                appointment.appointmentID = reader.GetInt32(0).ToString();
                appointment.customerID = reader.GetInt32(1).ToString();
                appointment.employeeID = reader.GetInt32(2).ToString();
                appointment.setDateTime(reader.GetDateTime(3));
                appointment.appointmentDescription = reader.GetString(5);
                appointment.duration = reader.GetInt32(6);
                appointments.Add(appointment);
            }
            connection.Close();
            return appointments;
        }

        public Service GetService(String serviceName)
        {
            Service service = new Service();
            service.service = serviceName;

            string command = "SELECT ServiceTime, ServicePrice FROM tblService WHERE ServiceName LIKE '" + serviceName + "'";

            MySqlCommand myCommand = new MySqlCommand(command, connection);
            connection.Open();

            MySqlDataReader reader = myCommand.ExecuteReader();
            while (reader.Read())
            {
                service.duration = reader.GetInt32(0);
                service.price = Convert.ToInt32(reader.GetFloat(1));
            }
            return service;
        }

        public bool VINExists(string search)
        {
            bool extists = false;
            int result = 0;

            string command = "SELECT COUNT(VinNumber) FROM tblCar WHERE VinNumber = " + search;

            MySqlCommand myCommand = new MySqlCommand(command, connection);
            connection.Open();
            MySqlDataReader reader = myCommand.ExecuteReader();

            while (reader.Read())
            {
                result = reader.GetInt32(0);
            }

            if (result == 1)
            {
                extists = true;
            }

            connection.Close();
            return extists;

        }

        public void Update(Employee employee)
        {
            string command = "UPDATE tblEmployee SET EmployeeFName = \'" + employee.FName + "\', " +
            "EmployeeLName = \'" + employee.LName + "\', " +
            "EmployeePhoneNum = \'" + employee.Phone + "\', " +
            "EmployeeStreetNum = \'" + employee.StreetNum + "\', " +
            "EmployeeStreetName = \'" + employee.StreetName + "\', " +
            "EmployeeCity = \'" + employee.City + "\', " +
            "EmployeeState = \'" + employee.State + "\', " +
            "EmployeeZip = \'" + employee.Zip + "\' " +
            "WHERE EmployeeID = " + employee.ID;

            MySqlCommand myCommand = new MySqlCommand(command, connection);
            connection.Open();
            myCommand.ExecuteNonQuery();
            connection.Close();
        }

        public void Update(Customer customer)
        {
            string command = "UPDATE tblCustomer SET CustomerFName = \'" + customer.FName + "\', " +
            "CustomerLName = \'" + customer.LName + "\', " +
            "CustomerStreetNum = \'" + customer.StreetNum + "\', " +
            "CustomerStreetName = \'" + customer.StreetName + "\', " +
            "CustomerCity = \'" + customer.City + "\', " +
            "CustomerState = \'" + customer.State + "\', " +
            "CustomerZip = \'" + customer.Zip + "\', " +
            "CustomerPhone = \'" + customer.PhoneNum + "\', " +
            "CustomerEmail = \'" + customer.Email + "\' " +
            "WHERE CustomerID = " + customer.ID;

            MySqlCommand myCommand = new MySqlCommand(command, connection);
            connection.Open();
            myCommand.ExecuteNonQuery();
            connection.Close();
        }

        public void Update(Car car)
        {
            string command = "UPDATE tblCar SET Year = " + car.Year + ", " +
            "Make = \'" + car.Make + "\', " +
            "Model = \'" + car.Model + "\' " +
            "WHERE VinNumber = " + car.VIN;

            MySqlCommand myCommand = new MySqlCommand(command, connection);
            connection.Open();
            myCommand.ExecuteNonQuery();
            connection.Close();
        }
        
        public void UpdatePassword(Customer customer)
        {
            string command = "UPDATE tblCustomer SET CustomerPassword = \'" + GeneratePassword() + "\' " +
                "WHERE CustomerID = " + customer.ID;

            MySqlCommand myCommand = new MySqlCommand(command, connection);
            connection.Open();
            myCommand.ExecuteNonQuery();
            connection.Close();
        }

        public void Delete(Employee employee)
        {
            string command = "DELETE FROM tblEmployee WHERE EmployeeID = " + employee.ID;
            MySqlCommand myCommand = new MySqlCommand(command, connection);
            connection.Open();
            myCommand.ExecuteNonQuery();
            connection.Close();
        }

        public void Delete(Customer customer)
        {
            string command = "DELETE FROM tblCustomer WHERE CustomerID = " + customer.ID;
            MySqlCommand myCommand = new MySqlCommand(command, connection);
            connection.Open();
            myCommand.ExecuteNonQuery();
            connection.Close();
        }

        public void Delete(Car car)
        {
            string command = "DELETE FROM tblCar WHERE VinNumber = " + car.VIN;
            MySqlCommand myCommand = new MySqlCommand(command, connection);
            connection.Open();
            myCommand.ExecuteNonQuery();
            connection.Close();
        }

        private string GeneratePassword()
        {
            int length = 4;
            string password;
            RNGCryptoServiceProvider cryptRNG = new RNGCryptoServiceProvider();
            byte[] tokenBuffer = new byte[length];
            cryptRNG.GetBytes(tokenBuffer);

            password = Convert.ToBase64String(tokenBuffer);

            const string alphanumericCharacters = "!@#$%^&*()-_=+/";

            StringBuilder res = new StringBuilder();
            var aStringBuilder = new StringBuilder(password);
            aStringBuilder.Remove(6, 2);

            Random rnd = new Random();
            length = 2;
            while (0 < length--)
            {
                res.Append(alphanumericCharacters[rnd.Next(alphanumericCharacters.Length)]);
            }

            res.ToString();
            aStringBuilder.Insert(3, res);
            password = aStringBuilder.ToString();

            rnd = new Random();

            var list = new SortedList<int, char>();
            foreach (var c in password)
            {
                list.Add(rnd.Next(), c);
            }

            password = new string(list.Values.ToArray());

            return password;
        }



    }
    }

