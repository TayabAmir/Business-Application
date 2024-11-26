using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using TechStream.BL;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;
using TechStream.DL.FH;
using TechStream.DLInterfaces;

namespace BusinessApp_Forms_.UI
{
    public partial class UserDevices : Form
    {
        public UserDevices()
        {
            InitializeComponent();
            List<Device> devicesBought = ObjectHandler.GetUserDL().GetUserDevices(SignIn.GetCurrentUser());
            if (devicesBought.Count > 0)
                if (devicesBought[0] != null)
                {
                    ShowDevices(devicesBought);
                    dataGridView1.Columns["Model"].Width = 230;
                    dataGridView1.Columns["Price"].Width = 120;
                }
        }
        private void ShowDevices(List<Device> devicesBought)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Model");
            dataTable.Columns.Add("Price");
            foreach (Device device in devicesBought)
            {
                dataTable.Rows.Add(device.GetModel(), device.GetModelPrice());
            }
            dataGridView1.DataSource = dataTable;
        }

        private void UserDevices_Load(object sender, EventArgs e)
        {

        }

        private void button10_Click(object sender, EventArgs e)
        {
            Hide();
            Form form = new CustomerUI();
            form.ShowDialog();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
