using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using TechStream.BL;
using TechStream.DLInterfaces;
using TechStream.DL.DB;
using TechStream.DL.FH;
using TechStream.Utils;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BusinessApp_Forms_.UI
{
    public partial class WatchUsers : Form
    {
        public WatchUsers()
        {
            InitializeComponent();
            List<User> users = ObjectHandler.GetUserDL().GetUsers();
            ShowRegisteredUsers(users);
            dataGridView1.Columns["Username"].Width = 170;
            dataGridView1.Columns["Role"].Width = 120;
        }
        private void ShowRegisteredUsers(List<User> users) 
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("UserName",typeof(string));
            dataTable.Columns.Add("Role",typeof(string));
            
            foreach(User user in users)
            {
                dataTable.Rows.Add(user.GetName(),user.GetRole());
            }
            dataGridView1.DataSource = dataTable;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Hide();
            Form form = new AdminForm();
            form.ShowDialog();
        }

        private void WatchUsers_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
