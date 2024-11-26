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
    public partial class SignIn : Form
    {
        public static Customer currentUser;
        public static Customer GetCurrentUser() {  return currentUser; }
        public SignIn()
        {
            InitializeComponent();
        }
        private void home_Click(object sender, EventArgs e)
        {
            Hide();
            Form form = new Sign();
            form.ShowDialog();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string Name = richTextBox1.Text;
            string Password = richTextBox2.Text;
            if (string.IsNullOrEmpty(Name) || string.IsNullOrEmpty(Password))
            {
                MessageBox.Show("Input cannot be empty.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            IUserDL Iuser = ObjectHandler.GetUserDL();
            List<string> users = Iuser.GetCurrentCustomerNames();
            User user = Iuser.SignIn(Name, Password);
            if (user != null && user.GetRole() == "ADMIN")
            {
                MessageBox.Show("You have successfully logged in as Admin", "SignIn", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Hide();
                Form adminUI = new AdminForm();
                adminUI.ShowDialog();
            }
            else if (user != null && user.GetRole() == "CUSTOMER")
            {
                currentUser = (Customer)user;
                MessageBox.Show("You have successfully logged in as Customer", "SignIn", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Hide();
                CustomerUI customerUI = new CustomerUI();
                customerUI.ShowDialog();
            }
            else
            {
                MessageBox.Show("You are not registered yet.", "SignIn", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Hide();
            Form form = new SignUp();
            form.ShowDialog();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}
