using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechStream.DLInterfaces;
using TechStream.BL;
using System.IO;
using TechStream.DL.DB;
using System.Xml.Linq;

namespace TechStream.DL.FH
{
    public class UserFH : IUserDL
    {
        static UserFH instance = null;
        public static UserFH GetInstance()
        {
            if (instance == null)
                instance = new UserFH();
            return instance;
        }
        public static List<User> UsersList = new List<User>();
        public List<User> LoadUsers()
        {
            UsersList.Clear();
            string record;
            StreamReader file = new StreamReader("Users.txt");
            while ((record = file.ReadLine()) != null)
            {
                string[] userData = record.Split(',');
                string name = userData[0];
                string password = userData[1];
                string role = userData[2];

                if (role.ToUpper() == "ADMIN")
                {
                    UsersList.Add(new Admin(name, password, role));
                }
                else if (role.ToUpper() == "CUSTOMER")
                {
                    string accountNo = userData[3];
                    double accountMoney = double.Parse(userData[4]);
                    string feedback = userData[5];
                    List<Device> devices = new List<Device>();

                    Customer customer = new Customer(name, password, role, accountNo);
                    if (userData.Length > 6)
                    {
                        string[] devicesData = userData[6].Split(';');
                        foreach (string deviceData in devicesData)
                        {
                            IDeviceDL deviceFH = new DeviceFH();
                            customer.AddDevice(deviceFH.GetDeviceByName(deviceData));
                        }
                    }
                    customer.SetAccountMoney(accountMoney);
                    customer.SetFeedback(feedback);
                    UsersList.Add(customer);
                }
            }
            file.Close();
            return UsersList;
        }
        public List<User> GetUsers() { return UsersList; }
        public void StoreAllUsers()
        {
            using (StreamWriter file = new StreamWriter("Users.txt", false))
            {
                foreach (User userr in UsersList)
                {
                    file.WriteLine(userr.StoreInFile());
                }
            }
        }
        public List<Device> GetUserDevices(Customer customer)
        {
            return customer.GetDevices();
        }
        public void SaveUserData(User user)
        {
            UsersList.Add(user);
        }
        public void SaveUserDevice(Customer customer, Device device)
        {
            if (customer.GetDevices().Count > 0)
            {
                if (customer.GetDevices()[0] == null)
                {
                    { customer.ClearDevices(); }
                }
            }
            customer.AddDevice(device);
        }
        public bool RemoveUser(User user)
        { return UsersList.Remove(user); }
        public int CheckUserName(string word)
        {
            if (ExistedUsername(word))
            {
                return 0;
            }

            if (word.Length > 5)
            {
                int letterCount = 0;

                for (int i = 0; i < word.Length; i++)
                {
                    char c = word[i];

                    if ((c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z'))
                    {
                        letterCount++;
                    }
                    else if (!(c >= '0' && c <= '9'))
                    {
                        return 1;
                    }
                }

                if (letterCount >= 3)
                {
                    return 2;
                }
                else
                {
                    return 3;
                }
            }
            else
            {
                return 4;
            }
        }
        public bool ExistedUsername(string name)
        {
            for (int i = UsersList.Count - 1; i >= 0; i--)
            {
                if (name == UsersList[i].GetName())
                {
                    return true;
                }
            }
            return false;
        }
        public User SignIn(string name, string password)
        {
            foreach (User user in UsersList)
            {
                if (user.GetName() == name && user.GetPassword() == password)
                {
                    return user;
                }
            }
            return null;
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
        public bool AccountExisted(string account)
        {
            foreach (User user in UsersList)
            {
                if (user is Customer)
                {
                    Customer customer = (Customer)user;
                    if (account == customer.GetAccountNo())
                    {
                        return true;
                    }
                }

            }
            return false;
        }
        public void AddFeedback(Customer customer, string feedback)
        {
            customer.SetFeedback(feedback);
        }
        public bool UpdateMoney(Customer customer, double amount)
        {
            return customer.SetAccountMoney(amount);
        }
        public double AccountMoney(Customer cust)
        {
            return cust.GetAccountMoney();
        }
        public List<string> GetCurrentCustomerNames()
        {
            List<string> names = new List<string>();
            foreach (User user in UsersList)
                if (user.GetRole() == "CUSTOMER")
                    names.Add(user.GetName());
            return names;
        }
        public List<string> GetFeedbacks()
        {
            List<string> result = new List<string>();
            foreach (User user in UsersList)
            {
                if (user is Customer)
                {
                    Customer customer = (Customer)user;
                    result.Add(customer.GetFeedback());
                }
            }
            return result;
        }

        public double GetSales()
        {
            string record;
            double sales = 0;
            StreamReader file = new StreamReader("SoldDevices.txt");
            while ((record = file.ReadLine()) != null)
            {
                string[] data = record.Split(',');
                sales += Convert.ToDouble(data[2]);
            }
            file.Close();
            return sales;
        }
    }
}
