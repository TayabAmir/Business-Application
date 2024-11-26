using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechStream.Utils;

namespace TechStream.BL
{
    public class Admin : User
    {
        public Admin(string name, string password, string role) : base(name, password, role)
        {
        }
        public override void StoreInDB()
        {
                string ConnectionString = ConString.GetConnectionString();
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("INSERT INTO [Users] ([Username], [Userpassword], [Userrole], [Accountno], [Accountmoney],[Feedback]) values(@Username,@Password, @Role, null, null,null)", conn);
                    cmd.Parameters.AddWithValue("@Username", Name);
                    cmd.Parameters.AddWithValue("@Password", Password);
                    cmd.Parameters.AddWithValue("@Role", Role);
                    cmd.ExecuteNonQuery();
                }
        }
    }
}
