using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BusinessApp_Forms_.UI;
using TechStream.BL;
namespace BusinessApp_Forms_
{
    public partial class Sales : Form
    {
        public Sales()
        {
            InitializeComponent();
            List<string> soldDevices = ObjectHandler.GetDeviceDL().GetSoldDevices();
            ShowDevices(soldDevices);
            sale.Text = "Your Total Sales: $" + ObjectHandler.GetUserDL().GetSales();
            dataGridView1.Columns["Username"].Width = 130;
            dataGridView1.Columns["Model"].Width = 240;
            dataGridView1.Columns["ModelPrice"].Width = 120;
        }
        private void ShowDevices(List<string> soldDevices)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("UserName", typeof(string));
            dataTable.Columns.Add("Model", typeof(string));
            dataTable.Columns.Add("ModelPrice", typeof(double));

            foreach (string record in soldDevices)
            {
                string[] data = record.Split(',');
                dataTable.Rows.Add(data[0], data[1], data[2]);
            }
            dataGridView1.DataSource = dataTable;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Hide();
            Form form = new AdminForm();
            form.ShowDialog();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
