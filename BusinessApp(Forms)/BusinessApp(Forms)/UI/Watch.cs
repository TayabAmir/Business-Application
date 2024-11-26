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
using System.Data.SqlClient;
namespace BusinessApp_Forms_.UI
{
    public partial class Watch : Form
    {
        public Watch()
        {
            InitializeComponent();
            ShowDevice();
            if (ObjectHandler.GetDeviceType() == "MOBILE")
                BackgroundImage = Image.FromFile("D:\\Tayyab\\BusinessAppProject\\BusinessApp(Forms)\\BusinessApp(Forms)\\Mobile2.jpeg");
            else if (ObjectHandler.GetDeviceType() == "LAPTOP")
                BackgroundImage = Image.FromFile("D:\\Tayyab\\BusinessAppProject\\BusinessApp(Forms)\\BusinessApp(Forms)\\laptop.jpg");
            else if (ObjectHandler.GetDeviceType() == "SMART WATCH")
                BackgroundImage = Image.FromFile("D:\\Tayyab\\BusinessAppProject\\BusinessApp(Forms)\\BusinessApp(Forms)\\SW4.jpg");
            else if (ObjectHandler.GetDeviceType() == "EARBUD")
                BackgroundImage = Image.FromFile("D:\\Tayyab\\BusinessAppProject\\BusinessApp(Forms)\\BusinessApp(Forms)\\ear.jpg");
            else
                BackgroundImage = Image.FromFile("D:\\Tayyab\\BusinessAppProject\\BusinessApp(Forms)\\BusinessApp(Forms)\\SH.jpeg");

            BackgroundImageLayout = ImageLayout.Stretch;

            if (ObjectHandler.GetDeviceType() != "SECOND HAND DEVICE")
                dataGridView1.Columns["Company"].Width = 130;
            dataGridView1.Columns["Model"].Width = 220;
            dataGridView1.Columns["Price"].Width = 120;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Hide();
            Form form = new CRUDMenu();
            form.ShowDialog();
        }
        private void ShowDevice()
        {
            List<Device> devices = ObjectHandler.GetDeviceDL().GetDevices();
            DataTable dataTable = new DataTable();
            if (ObjectHandler.GetDeviceType() != "SECOND HAND DEVICE")
                dataTable.Columns.Add("Company", typeof(string));
            dataTable.Columns.Add("Model", typeof(string));
            dataTable.Columns.Add("Price", typeof(int));

            foreach (Device device in devices)
                if (device.GetDeviceType() == ObjectHandler.GetDeviceType())
                {
                    if (ObjectHandler.GetDeviceType() != "SECOND HAND DEVICE")
                        dataTable.Rows.Add(device.GetCompany(), device.GetModel(), device.GetModelPrice());
                    else
                        dataTable.Rows.Add(device.GetModel(), device.GetModelPrice());
                }
            dataGridView1.DataSource = dataTable;
        }

        private void button8_Click(object sender, EventArgs e)
        {
        }
        private void WatchDevices_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
