using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechStream.BL;
using TechStream.DL.FH;
using TechStream.DLInterfaces;
using TechStream.Utils;

namespace TechStream.DL.DB
{
    public class DeviceDB : IDeviceDL
    {
        static DeviceDB instance = null;
        public static DeviceDB GetInstance()
        {
            if(instance == null)
                instance = new DeviceDB();
            return instance;
        }
        public List<Device> GetDevices()
        { return LoadDeviceData(); }

        public Device GetDeviceByName(string name)
        {
            string ConnectionString = ConString.GetConnectionString();
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                string query = "SELECT * FROM Device WHERE model = @Model";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Model", name);

                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        string deviceType = Convert.ToString(reader["devicetype"]);
                        string company = Convert.ToString(reader["company"]);
                        string model = Convert.ToString(reader["model"]);
                        double modelPrice = Convert.ToDouble(reader["modelPrice"]);

                        return new Device(deviceType, company, model, modelPrice);
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }
        public void AddDevice(Device device)
        {
            string ConnectionString = ConString.GetConnectionString();
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                string query = "INSERT INTO Device (devicetype, company, model, modelPrice) VALUES (@DeviceType, @Company, @Model, @ModelPrice)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    command.Parameters.AddWithValue("@DeviceType", device.GetDeviceType());
                    command.Parameters.AddWithValue("@Company", device.GetCompany());
                    command.Parameters.AddWithValue("@Model", device.GetModel());
                    command.Parameters.AddWithValue("@ModelPrice", device.GetModelPrice());

                    command.ExecuteNonQuery();
                }
            }
        }
        public List<Device> LoadDeviceData()
        {
            List<Device> devices = new List<Device>();

            string ConnectionString = ConString.GetConnectionString();
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                string query = "SELECT devicetype, company, model, modelPrice FROM Device";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        string deviceType = Convert.ToString(reader["devicetype"]);
                        string company = Convert.ToString(reader["company"]);
                        string model = Convert.ToString(reader["model"]);
                        double modelPrice = Convert.ToDouble(reader["modelPrice"]);

                        Device device = new Device(deviceType, company, model, modelPrice);
                        devices.Add(device);
                    }

                    reader.Close();
                }
            }
            return devices;
        }
        public bool EditPrice(string deviceType,string company, string model, double modelPrice)
        {
            string ConnectionString = ConString.GetConnectionString();
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                string query = "UPDATE Device SET modelPrice = @ModelPrice WHERE company = @Company AND model = @Model";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ModelPrice", modelPrice);
                    command.Parameters.AddWithValue("@Company", company);
                    command.Parameters.AddWithValue("@Model", model);

                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }
        public bool DeleteModel(string company, string model)
        {
            string ConnectionString = ConString.GetConnectionString();
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                string query = "DELETE FROM Device WHERE company = @Company AND model = @Model";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Company", company);
                    command.Parameters.AddWithValue("@Model", model);

                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();

                    return rowsAffected > 0;
                }
            }
        }

        public bool ModelExisted(string buyModel)
        {
            string ConnectionString = ConString.GetConnectionString();
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                string query = "SELECT COUNT(*) FROM Device WHERE model = @Model";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Model", buyModel);

                    connection.Open();
                    int count = Convert.ToInt32(command.ExecuteScalar());

                    return count > 0;
                }
            }
        }

        public double GetDevicePrice(string buyModel)
        {
            string ConnectionString = ConString.GetConnectionString();
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                string query = "SELECT modelPrice FROM Device WHERE model = @Model";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    command.Parameters.AddWithValue("@Model", buyModel);

                    object result = command.ExecuteScalar();

                    if (result != null )
                    {
                        return Convert.ToDouble(result);
                    }
                    else
                    {
                        return 0.0;
                    }
                }
            }
        }

        public void DeviceSold(string name, string model, double modelPrice)
        {
            string ConnectionString = ConString.GetConnectionString();
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                string query = "INSERT INTO SoldDevices (Username, Model, ModelPrice) VALUES (@Name, @Model, @ModelPrice)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    command.Parameters.AddWithValue("@Name", name);
                    command.Parameters.AddWithValue("@Model", model);
                    command.Parameters.AddWithValue("@ModelPrice", modelPrice);
                    command.ExecuteNonQuery();
                }
            }
        }

        public List<string> GetSoldDevices()
        {
            List<string> devices = new List<string>();
            string ConnectionString = ConString.GetConnectionString();
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                string query = "SELECT Username, Model, ModelPrice FROM SoldDevices";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        string Username = Convert.ToString(reader["Username"]);
                        string model = Convert.ToString(reader["Model"]);
                        string modelPrice = Convert.ToString(reader["ModelPrice"]);

                        devices.Add($"{Username},{model},{modelPrice}");
                    }
                    reader.Close();
                }
            }
            return devices;
        }
    }
}
