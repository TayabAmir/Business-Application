using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using TechStream.BL;
using TechStream.DL.DB;
using TechStream.DLInterfaces;
using TechStream.Utils;

namespace TechStream.DL.DB
{
    public class UserDB : IUserDL
    {
        static UserDB instance = null;
        public static UserDB GetInstance()
        {
            if (instance == null)
                instance = new UserDB();
            return instance;
        }
        public List<User> LoadUsers()
        {
            string ConnectionString = ConString.GetConnectionString();
            List<User> users = new List<User>();

            string query = "SELECT * FROM Users";

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string role = Convert.ToString(reader["Userrole"]);

                            if (role == "ADMIN")
                            {
                                Admin admin = new Admin(
                                    Convert.ToString(reader["Username"]),
                                    Convert.ToString(reader["Userpassword"]),
                                    role
                                );
                                users.Add(admin);
                            }
                            else if (role == "CUSTOMER")
                            {
                                Customer customer = new Customer(
                                    Convert.ToString(reader["Username"]),
                                    Convert.ToString(reader["Userpassword"]),
                                    role,
                                    Convert.ToString(reader["Accountno"])
                                );
                                customer.SetAccountMoney(Convert.ToDouble(reader["Accountmoney"]));
                                customer.SetFeedback(Convert.ToString(reader["Feedback"]));
                                users.Add(customer);
                            }
                        }
                    }
                }
            }
                return users;
        }
        public List<User> GetUsers() { return LoadUsers(); }
        public void StoreAllUsers()
        {
            return;
        }
        public void SaveUserData(User user)
        {
                user.StoreInDB();
        }
        public void SaveUserDevice(Customer customer,Device device)
        {
            string connectionString = ConString.GetConnectionString();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string sql = "INSERT INTO UserDevice (Username, Model) VALUES (@Username, @Model)";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Username", customer.GetName());
                    cmd.Parameters.AddWithValue("@Model", device.GetModel());
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public bool RemoveUser(User user)
        {
            string connectionString = ConString.GetConnectionString();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string sql = "DELETE FROM Users WHERE Username = @Username";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Username", user.GetName());
                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                        return true;
                    else
                        return false;
                }
            }
        }
        public User SignIn(string name,string password)
        {
            List<User> UsersList = LoadUsers();

            foreach(User user in UsersList)
            {
                if (user.GetName()==name && user.GetPassword()==password)
                {
                    return user;
                }
            }
            return null;
        }
        public int CheckUserName(string word)
        {
            // Check if the username already exists
            if (ExistedUsername(word))
            {
                return 0;
            }
            // Check the length of username to be 6 or greater than 6
            if (word.Length > 5)
            {
                int letterCount = 0;
                // Count the number of letters in the username                
                for (int i = 0; i < word.Length; i++)
                {
                    char c = word[i];

                    if ((c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z'))
                    {
                        letterCount++;
                    }
                    else if (!(c >= '0' && c <= '9'))
                    {
                        return 1; // Username contains non-alphanumeric characters
                    }
                }

                // Determine the validity of the username based on letter count
                if (letterCount >= 3)
                {
                    return 2; // Only true case for username
                }
                else
                {
                    return 3; // Username does not contain 3 characters
                }
            }
            else
            {
                return 4; // Username is less than 6 characters
            }
        }
        public bool ExistedUsername(string name)
        {
            string ConnectionString = ConString.GetConnectionString();
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM Users WHERE Username = @Name", connection))
                {
                    command.Parameters.AddWithValue("@Name", name);
                    int count = (int)command.ExecuteScalar();
                    return count > 0;
                }
            }
        }
        public List<Device> GetUserDevices(Customer customer)
        {
            List<Device> userDevices = new List<Device>();

            string connectionString = ConString.GetConnectionString();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string sql = "SELECT model FROM UserDevice WHERE Username = @Username";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Username", customer.GetName());
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            IDeviceDL deviceDL = new DeviceDB();
                            userDevices.Add(deviceDL.GetDeviceByName(Convert.ToString(reader["model"])));
                        }
                    }
                }
            }
            return userDevices;
        }
        public bool AccountExisted(string account)
        {
            string ConnectionString = ConString.GetConnectionString();
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM Users WHERE Accountno = @account", connection))
                {
                    command.Parameters.AddWithValue("@account", account);
                    int count = (int)command.ExecuteScalar();
                    return count > 0;
                }
            }
        }
        public bool CheckAccount(string aNo, string userAccount)
        {
            for (int i = 0; i < aNo.Length; i++)
            {
                if (aNo[i] != userAccount[i])
                {
                    return false;
                }
            }
            return true;
        }
        public void AddFeedback(Customer customer, string feedback)
        {
            string connectionString = ConString.GetConnectionString();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string sql = "UPDATE Users SET Feedback = @Feedback WHERE Username = @Username";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Feedback", feedback);
                    cmd.Parameters.AddWithValue("@Username", customer.GetName());
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public bool UpdateMoney(Customer customer, double amount)
        {
            if(customer.GetAccountMoney()+amount > 400000)
                return false;
            string ConnectionString = ConString.GetConnectionString();
            using(SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                string query = "Update Users Set AccountMoney = @AccountMoney where Username = @Username";
                using(SqlCommand cmd = new SqlCommand(query,conn))
                {
                    cmd.Parameters.AddWithValue("@Username", customer.GetName());
                    cmd.Parameters.AddWithValue("@AccountMoney", amount);
                    cmd.ExecuteNonQuery();
                }
            }
            return true;
        }
        public double AccountMoney(Customer cust)
        {
            double accountMoney = 0.0;
            string connectionString = ConString.GetConnectionString();
            string query = "SELECT AccountMoney FROM Users WHERE Username = @Username";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    command.Parameters.AddWithValue("@Username", cust.GetName());
                    object result = command.ExecuteScalar();
                    if (result != null)
                    {
                        double.TryParse(result.ToString(), out accountMoney);
                    }
                }
            }
            return accountMoney;
        }
        public List<string> GetCurrentCustomerNames()
        {

            string ConnectionString = ConString.GetConnectionString();
            List<string> names = new List<string>();

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                string query = "SELECT Username FROM Users WHERE Userrole = 'CUSTOMER'";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        string name = reader.GetString(reader.GetOrdinal("Username"));
                        names.Add(name);
                    }
                }
            }
            return names;
        }
        public List<string> GetFeedbacks()
        {
            string ConnectionString = ConString.GetConnectionString();
            List<string> feedbacks = new List<string>();

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                string query = "SELECT feedback FROM Users WHERE feedback IS NOT NULL";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        string feedback = reader.GetString(reader.GetOrdinal("feedback"));
                        feedbacks.Add(feedback);
                    }
                }
            }

            return feedbacks;
        }

        public double GetSales()
        {
            double sales = 0.0;
            string connectionString = ConString.GetConnectionString();
            string query = "SELECT SUM(ModelPrice) FROM SoldDevices";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    object result = command.ExecuteScalar();
                    if (result != null)
                    {
                        sales = (double)result;
                    }
                }
            }
            return sales;
        }
    }
}
