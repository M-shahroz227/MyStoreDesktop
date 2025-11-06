using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace MyStoreDesktop
{
    public class DatabaseHelper
    {
        private readonly string _conn;

        public DatabaseHelper()
        {
            _conn = ConfigurationManager.ConnectionStrings["MyStoreDB"].ConnectionString;
        }

        public bool ValidateUser(string username, string password, out string role)
        {
            role = "";
            using (SqlConnection conn = new SqlConnection(_conn))
            using (SqlCommand cmd = new SqlCommand("SELECT Role FROM Users WHERE Username=@u AND Password=@p", conn))
            {
                cmd.Parameters.AddWithValue("@u", username);
                cmd.Parameters.AddWithValue("@p", password);
                conn.Open();
                var r = cmd.ExecuteScalar();
                if (r != null)
                {
                    role = r.ToString();
                    return true;
                }
                return false;
            }
        }

        public bool RegisterUser(string username, string password, string fullname)
        {
            using (SqlConnection conn = new SqlConnection(_conn))
            using (SqlCommand cmd = new SqlCommand(
                "INSERT INTO Users(Username, Password, FullName, Role) VALUES(@u,@p,@n,'Cashier')", conn))
            {
                cmd.Parameters.AddWithValue("@u", username);
                cmd.Parameters.AddWithValue("@p", password);
                cmd.Parameters.AddWithValue("@n", fullname);
                conn.Open();
                try
                {
                    cmd.ExecuteNonQuery();
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }
    }
}
