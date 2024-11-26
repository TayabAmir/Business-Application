using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechStream.BL;

namespace TechStream.DLInterfaces
{
    public interface IUserDL
    {
        void SaveUserData(User user);
        void SaveUserDevice(Customer customer, Device device);
        List<User> LoadUsers();
        List<User> GetUsers();
        void StoreAllUsers();
        List<Device> GetUserDevices(Customer customer);
        int CheckUserName(string name);
        bool ExistedUsername(string name);
        User SignIn(string name, string password);
        bool CheckAccount(string aNo, string userAccount);
        void AddFeedback(Customer customer, string feedback);
        bool UpdateMoney(Customer customer, double amount);
        bool AccountExisted(string account);
        bool RemoveUser(User user);
        double AccountMoney(Customer cust);
        double GetSales();
        List<string> GetCurrentCustomerNames();
        List<string> GetFeedbacks();
    }
}