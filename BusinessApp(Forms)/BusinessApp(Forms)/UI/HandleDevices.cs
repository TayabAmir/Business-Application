using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TechStream.BL;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace BusinessApp_Forms_.UI
{
    public partial class HandleDevices : Form
    {
        public HandleDevices()
        {
            InitializeComponent();
            ShowDevice();
            dataGridView1.CellClick += dataGridView1_CellContentClick;
        }
        private void ShowDevice()
        {
            List<Device> devices = ObjectHandler.GetDeviceDL().GetDevices();
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Type", typeof(string));
            dataTable.Columns.Add("Company", typeof(string));
            dataTable.Columns.Add("Model", typeof(string));
            dataTable.Columns.Add("Price", typeof(int));

            foreach (Device device in devices)
                dataTable.Rows.Add(device.GetDeviceType(), device.GetCompany(), device.GetModel(), device.GetModelPrice());
            dataGridView1.DataSource = dataTable;
        }
        private void button8_Click(object sender, EventArgs e)
        {
            ObjectHandler.GetDeviceDL().AddDevice(new Device(comboBox1.Text, textBox1.Text.ToUpper(), richTextBox1.Text.ToUpper(), double.Parse(textBox2.Text)));
            MessageBox.Show("Device Model Added Successfully", "Device Crud", MessageBoxButtons.OK, MessageBoxIcon.Information);
            ShowDevice();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ObjectHandler.GetDeviceDL().EditPrice(comboBox1.Text, textBox1.Text, richTextBox1.Text, double.Parse(textBox2.Text));
            MessageBox.Show("Device Price Updated Successfully", "Device Crud", MessageBoxButtons.OK, MessageBoxIcon.Information);
            ShowDevice();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(ObjectHandler.GetDeviceDL().DeleteModel(textBox1.Text, richTextBox1.Text))
                MessageBox.Show("Device Model Deleted Successfully", "Device Crud", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show("Device Model cannot Deleted", "Device Crud", MessageBoxButtons.OK, MessageBoxIcon.Error);
                
            ShowDevice();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                comboBox1.Text = row.Cells["Type"].Value.ToString();
                textBox1.Text = row.Cells["Company"].Value.ToString();
                richTextBox1.Text = row.Cells["Model"].Value.ToString();
                textBox2.Text = row.Cells["Price"].Value.ToString();
            }
        }
    }
}
