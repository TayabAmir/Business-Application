using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechStream.Utils;

namespace TechStream.BL
{
    public class Customer: User
    {
        private string AccountNo;
        private double AccountMoney;
        private List<Device> Devices;
        private string Feedback;
        public Customer(string name, string password, string role, string accountNo) : base(name,password,role)
        {
            AccountNo = accountNo;
            AccountMoney = 0;
            Devices = new List<Device>();
        }
        public string GetAccountNo() {  return AccountNo; }
        public double GetAccountMoney() {  return AccountMoney; }
        public void SetAccountNo(string accountNo) {  AccountNo = accountNo; }
        public bool SetAccountMoney(double accountMoney) 
        {
            if (accountMoney > 400000) return false;
            AccountMoney = accountMoney; 
            return true;
        }
        public string GetFeedback() { return Feedback; }
        public void SetFeedback(string feedback) {  Feedback = feedback; }
        public  void AddDevice(Device device) { Devices.Add(device); }
        public List<Device> GetDevices() { return Devices; }
        public void ClearDevices() {  Devices.Clear(); }
        public override string StoreInFile()
        {
            int i = 0;
            string fileHandling = $"{Name},{Password},{Role},{AccountNo},{AccountMoney},{Feedback},";
            foreach (Device device in Devices)
            {
                if (device != null)
                {
                    fileHandling += device.GetModel();
                    if (i < Devices.Count - 1)
                        fileHandling += ";";
                    i++;
                }
            }
            return fileHandling;
        }
        public override void StoreInDB()
        {
            string ConnectionString = ConString.GetConnectionString();
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO [Users] ([Username], [Userpassword], [Userrole], [Accountno], [Accountmoney],[Feedback]) values(@Username,@Password, @Role, @AccountNo, 0,@Feedback)", conn);
                cmd.Parameters.AddWithValue("@Username", Name);
                cmd.Parameters.AddWithValue("@Password", Password);
                cmd.Parameters.AddWithValue("@Role", Role);
                cmd.Parameters.AddWithValue("@AccountNo", AccountNo);
                cmd.Parameters.AddWithValue("@Feedback", " ");
                cmd.ExecuteNonQuery();
            }
        }
    }
}