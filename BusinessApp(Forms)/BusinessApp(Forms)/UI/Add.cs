using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using TechStream.BL;
using BusinessApp_Forms_.Utils;
namespace BusinessApp_Forms_.UI
{
    public partial class Add : Form
    {
        public Add()
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
                dataGridView1.Width = 612;
            else
                dataGridView1.Width = 732;
            dataGridView1.Columns["Type"].Width = 100;
            if (ObjectHandler.GetDeviceType() != "SECOND HAND DEVICE")
                dataGridView1.Columns["Company"].Width = 120;
            dataGridView1.Columns["Model"].Width = 210;
            dataGridView1.Columns["Price"].Width = 110  ;

            if (ObjectHandler.GetDeviceType() == "SECOND HAND DEVICE")
            {
                textBox1.Enabled = false;
                textBox1.Text = "";
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrEmpty(richTextBox1.Text))
            {
                MessageBox.Show("Input cannot be empty.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if(ObjectHandler.GetDeviceDL().ModelExisted(richTextBox1.Text))
            {
                MessageBox.Show("Model already existed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!Validation.CheckOptionValidation(textBox2.Text))
            {
                MessageBox.Show("Write valid Price", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string model = richTextBox1.Text.ToUpper();
            double price = double.Parse(textBox2.Text);

            if (ObjectHandler.GetDeviceType() != "SECOND HAND DEVICE")
            {
                string company = textBox1.Text.ToUpper();
               ObjectHandler.GetDeviceDL().AddDevice(new Device(ObjectHandler.GetDeviceType(), company, model, price));
            }
            else
                ObjectHandler.GetDeviceDL().AddDevice(new Device(ObjectHandler.GetDeviceType(), null, richTextBox1.Text.ToUpper(), double.Parse(textBox2.Text)));
            MessageBox.Show("Device Model Added Successfully", "Device Crud", MessageBoxButtons.OK, MessageBoxIcon.Information);
            ShowDevice();
        }
        private void ShowDevice()
        {
            List<Device> devices = ObjectHandler.GetDeviceDL().GetDevices();
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Type", typeof(string));
            if (ObjectHandler.GetDeviceType() != "SECOND HAND DEVICE")
                dataTable.Columns.Add("Company", typeof(string));
            dataTable.Columns.Add("Model", typeof(string));
            dataTable.Columns.Add("Price", typeof(int));

            foreach (Device device in devices)
                if (device.GetDeviceType() == ObjectHandler.GetDeviceType())
                {
                    if (ObjectHandler.GetDeviceType() != "SECOND HAND DEVICE")
                        dataTable.Rows.Add(device.GetDeviceType(), device.GetCompany(), device.GetModel(), device.GetModelPrice());
                    else
                        dataTable.Rows.Add(device.GetDeviceType(), device.GetModel(), device.GetModelPrice());
                }
            dataGridView1.DataSource = dataTable;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Hide();
            Form adminUI = new CRUDMenu();
            adminUI.Show();
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
