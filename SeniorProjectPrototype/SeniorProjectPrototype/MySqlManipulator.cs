using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using MySql.Data.MySqlClient;

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
            
                server = "sql9.freemysqlhosting.net";
                database = "sql9280619";
                user = "sql9280619";
                password = "c1R8qVLfl1";
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

                    MessageBox.Show("successful connection");

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

            string command = "SELECT EmployeeID, EmployeeID FROM Employee WHERE EmployeeID = " + id; // + " AND EmployeeZip = " + zip + ";";

            MySqlCommand myCommand = new MySqlCommand(command, connection);
            connection.Open();

            MySqlDataReader reader = myCommand.ExecuteReader();
            
            while (reader.Read())
            {
                if (id == reader.GetString("EmployeeID")) // && zip == reader.GetString("EmployeeZip"))
                {
                    isValid = true;
                    break;
                }
            }

            connection.Close();
            return isValid;
        }

        }

    }

