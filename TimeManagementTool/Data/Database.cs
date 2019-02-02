using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using TimeManagementTool.Models;

namespace TimeManagementTool.Data
{
    class Database
    {
        private SqlConnection connection;

        public Database()
        {
            string sConnection = "Data Source=**********; Initial Catalog=***********; User ID=*********; Password=********";

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
            string sQuery = "INSERT INTO Category(Title) VALUES('@TITLE')";

            try
            {
                var command = new SqlCommand(sQuery, this.connection);
                command.Parameters.Add("@TITLE", SqlDbType.NVarChar);
                command.Parameters["@TITLE"].Value = category;
                command.ExecuteNonQuery();
                command.Dispose();
                return true;
            }
            catch(Exception e)
            {
                return false;
            }
        }

        public List<Category> GetCategories()
        {
            string sQuery = "SELECT * FROM Category";

            List<Category> categories = new List<Category>();
            try
            {
                //example: https://www.codeproject.com/Articles/837599/Using-Csharp-to-Connect-to-and-Query-from-a-SQL-Da
                var command = new SqlCommand(sQuery, this.connection);
                SqlDataReader sqlDataReader = command.ExecuteReader(){
                    if (sqlDataReader.HasRows)
                    {
                        while (sqlDataReader.Read())
                        {
                           
                            Category c = new Category();
                            c.Id = sqlDataReader.GetInt32(sqlDataReader.GetOrdinal("Id"));
                            c.Title = sqlDataReader.GetString(sqlDataReader.GetOrdinal("Title"));

                            categories.Add(c);
                            
                        }

                    }
                }
      
                command.ExecuteNonQuery();

            }
            catch(Exception e)
            {
                
            }
            return categories;

        }
    }
}
