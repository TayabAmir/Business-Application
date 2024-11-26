using BusinessApp_Forms_.Utils;
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
using TechStream.DLInterfaces;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace BusinessApp_Forms_.UI
{
    public partial class Buy_Second_Hand_Device : Form
    {
        public static double PaidAmount=0;
        public Buy_Second_Hand_Device()
        {
            InitializeComponent();
            List<Device> devices = ObjectHandler.GetDeviceDL().GetDevices();
            ShowSH(devices);
            dataGridView1.Columns["Model"].Width = 220;
            dataGridView1.Columns["Price"].Width = 100;
            foreach (Device device in devices)
                if (device.GetDeviceType() == "SECOND HAND DEVICE")
                    model.Items.Add(device.GetModel());
        }
        private void ShowSH(List<Device> devices)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Model", typeof(string));
            dataTable.Columns.Add("Price", typeof(int));

            foreach (Device device in devices)
                if (device.GetDeviceType() == "SECOND HAND DEVICE")
                    dataTable.Rows.Add(device.GetModel(), device.GetModelPrice());
            dataGridView1.DataSource = dataTable;
        }
        private void button10_Click(object sender, EventArgs e)
        {
            Hide();
            Form form = new CustomerUI();
            form.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string buyModel = model.Text.ToUpper();
            IDeviceDL Idevice = ObjectHandler.GetDeviceDL();
            IUserDL Iuser = ObjectHandler.GetUserDL();
            Customer user = SignIn.GetCurrentUser();
            if (string.IsNullOrEmpty(model.Text))
            {
                MessageBox.Show("Input cannot be empty.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (Idevice.ModelExisted(buyModel))
            {
                double devicePrice = Idevice.GetDevicePrice(buyModel);

                if (devicePrice > 0 && devicePrice <= Iuser.AccountMoney(user))
                {
                    Device device = Idevice.GetDeviceByName(buyModel);
                    MessageBox.Show("The selected SECOND HAND DEVICE (" + buyModel + ") is within your budget and is bought successfully.", "Buying SECOND HAND DEVICE", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Console.WriteLine("Price: " + devicePrice);
                    PaidAmount += devicePrice;
                    user.SetAccountMoney(Iuser.AccountMoney(user) - devicePrice);
                    Iuser.SaveUserDevice(user, device);
                    ObjectHandler.GetDeviceDL().DeviceSold(user.GetName(), device.GetModel(), device.GetModelPrice());
                }
                else if (devicePrice > Iuser.AccountMoney(user))
                {
                    MessageBox.Show("The selected SECOND HAND DEVICE (" + buyModel + ")  is out of your budget.", "Buying SECOND HAND DEVICE", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            else
            {
                MessageBox.Show("Wrong Model Name", "Buying SECOND HAND DEVICE", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void company_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
