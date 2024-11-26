using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TechStream.BL
{
    public abstract class User
    {
        protected string Name;
        protected string Password;
        protected string Role;
        public User(string name,string password, string role) 
        {
            Name = name;
            Password = password;
            Role = role;
        }
        public string GetName() {  return Name; }
        public string GetPassword() { return Password; }
        public string GetRole() { return Role.ToUpper(); }
        public void SetName(string name) {  Name = name; }
        public void SetPassword(string password) {  Password = password; }
        public void SetRole(string role) {  Role = role; }
        public virtual string StoreInFile()
        {
            return $"{Name},{Password},{Role}";
        }
        public abstract void StoreInDB();
    }
}
