using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BusinessApp_Forms_.UI
{
    public partial class CustomerUI : Form
    {
        public CustomerUI()
        {
            InitializeComponent();
        }
        private void button10_Click(object sender, EventArgs e)
        {
            ObjectHandler.GetUserDL().StoreAllUsers();
            Hide();
            Form form = new Sign();
            form.ShowDialog();
        }
        private void setForm(Form form)
        {
            panel_main.Controls.Clear();
            form.TopLevel = false;
            form.FormBorderStyle = FormBorderStyle.None;
            panel_main.Controls.Add(form);
            form.Dock = DockStyle.Fill;
            panel_main.Tag = form;
            form.Show();
        }

        private void Money_Click(object sender, EventArgs e)
        {
            Form form = new AddMoney();
            setForm(form);
        }

        private void Mobile_Click(object sender, EventArgs e)
        {
            Form form = new BuyMobile();
            setForm(form);
        }

        private void Laptop_Click(object sender, EventArgs e)
        {
            Form form = new BuyLaptop();
            setForm(form);
        }

        private void SW_Click(object sender, EventArgs e)
        {
            Form form = new BuySW();
            setForm(form);
        }

        private void SHDevice_Click(object sender, EventArgs e)
        {
            Form form = new Buy_Second_Hand_Device();
            setForm(form);
        }

        private void Bill_Click(object sender, EventArgs e)
        {
            Form form = new Bill();
            setForm(form);
        }

        private void Feedback_Click(object sender, EventArgs e)
        {
            Form form = new GiveFeedback();
            setForm(form);
        }

        private void Devices_Click(object sender, EventArgs e)
        {
            Form form = new UserDevices();
            setForm(form);
        }

        private void Earbuds_Click(object sender, EventArgs e)
        {
            Form form = new BuyEarbuds();
            setForm(form);
        }
        private void button1_Click(object sender, EventArgs e)
        {
        }
        private void button2_Click(object sender, EventArgs e)
        {
        }
        private void button3_Click(object sender, EventArgs e)
        {
        }
        private void button4_Click(object sender, EventArgs e)
        {
        }
        private void button5_Click(object sender, EventArgs e)
        {
        }
        private void button6_Click(object sender, EventArgs e)
        {
        }
        private void button7_Click(object sender, EventArgs e)
        {
        }
        private void button11_Click(object sender, EventArgs e){}
        private void pictureBox1_Click(object sender, EventArgs e){}
        private void button8_Click(object sender, EventArgs e)
        {
        }
        private void button9_Click(object sender, EventArgs e)
        {
        }
    }
}
