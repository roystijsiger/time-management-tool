﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace TimeManagementTool.Data
{
    class Database
    {
        private SqlConnection connection;

        public Database()
        {
            string sConnection = "Data Source=process-time-management.database.windows.net; Initial Catalog=process_time_management; User ID=process-time-management; Password=Zxcasdqwe123";

            this.connection = new SqlConnection(sConnection);

            try
            {
                this.connection.Open();
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public bool InsertCategory(String category)
        {
            string sQuery = "INSERT INTO Category(Title) VALUES('" + category + "')";

            try
            {
                var command = new SqlCommand(sQuery, this.connection);
                command.ExecuteNonQuery();
                command.Dispose();
                return true;
            }
            catch(Exception e)
            {
                return false;
            }
        }
    }
}