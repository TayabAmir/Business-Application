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
    public partial class BuyEarbuds : Form
    {
        public static double PaidAmount=0;
        public BuyEarbuds()
        {
            InitializeComponent();
            List<Device> devices = ObjectHandler.GetDeviceDL().GetDevices();
            ShowEarbuds(devices);
            var uniqueCompanies = devices
                .Where(d => d.GetDeviceType().Equals("EARBUD", StringComparison.OrdinalIgnoreCase))
                .Select(d => d.GetCompany().ToUpper())
                .Distinct();
            dataGridView1.Columns["Company"].Width = 105;
            dataGridView1.Columns["Model"].Width = 180;
            dataGridView1.Columns["Price"].Width = 90;
            foreach (string companyName in uniqueCompanies)
                company.Items.Add(companyName);
        }
        private void ShowEarbuds(List<Device> devices)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Company", typeof(string));
            dataTable.Columns.Add("Model", typeof(string));
            dataTable.Columns.Add("Price", typeof(int));

            foreach (Device device in devices)
                if (device.GetDeviceType() == "EARBUD")
                    dataTable.Rows.Add(device.GetCompany(), device.GetModel(), device.GetModelPrice());
            dataGridView1.DataSource = dataTable;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string buyModel = model.Text.ToUpper();
            IDeviceDL Idevice = ObjectHandler.GetDeviceDL();
            IUserDL Iuser = ObjectHandler.GetUserDL();
            Customer user = SignIn.GetCurrentUser();
            if (string.IsNullOrEmpty(model.Text)|| string.IsNullOrEmpty(company.Text))
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
                    MessageBox.Show("The selected EARBUD (" + buyModel + ") is within your budget and is bought successfully.", "Buying EARBUD", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Console.WriteLine("Price: " + devicePrice);
                    PaidAmount += devicePrice;
                    user.SetAccountMoney(Iuser.AccountMoney(user) - devicePrice);
                    Iuser.SaveUserDevice(user, device);
                    ObjectHandler.GetDeviceDL().DeviceSold(user.GetName(), device.GetModel(), device.GetModelPrice());
                }
                else if (devicePrice > Iuser.AccountMoney(user))
                {
                    MessageBox.Show("The selected EARBUD (" + buyModel + ")  is out of your budget.", "Buying EARBUD", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            else
            {
                MessageBox.Show("Wrong Model Name", "Buying EARBUD", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void company_SelectedIndexChanged(object sender, EventArgs e)
        {
            model.Items.Clear();
            string selectedItem = company.SelectedItem.ToString();
            List<Device> devices = ObjectHandler.GetDeviceDL().GetDevices();
            foreach (Device device in devices)
            {
                if (device.GetCompany() == selectedItem && device.GetDeviceType() == "EARBUD")
                {
                    model.Items.Add(device.GetModel());
                }
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            Hide();
            Form form = new CustomerUI();
            form.ShowDialog();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
