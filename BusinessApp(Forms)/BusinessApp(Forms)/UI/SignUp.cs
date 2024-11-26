using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using BusinessApp_Forms_;
using System.Text;
using TechStream.BL;
using TechStream.DLInterfaces;
using TechStream.DL.DB;
using TechStream.DL.FH;
using TechStream.Utils;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using BusinessApp_Forms_.Utils;

namespace BusinessApp_Forms_.UI
{
    public partial class SignUp : Form
    {
        public SignUp()
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

            IUserDL Iuser = ObjectHandler.GetUserDL();
            string name = textBox1.Text;
            int checkResult = Iuser.CheckUserName(name);
            if (string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrEmpty(textBox2.Text) || string.IsNullOrEmpty(textBox3.Text) || string.IsNullOrEmpty(comboBox1.Text))

            {
                MessageBox.Show("Input cannot be empty.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            switch (checkResult)
            {
                case 0:
                    MessageBox.Show("UserName Already Taken", "SignUp", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                case 1:
                    MessageBox.Show("Invalid character in username. Use only letters and numbers.", "SignUp", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                case 3:
                    MessageBox.Show("Invalid username. Username must contain at least 3 letters.", "SignUp", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                case 4:
                    MessageBox.Show("Invalid username. Username must be at least 6 characters long.", "SignUp", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                default:
                    break;
            }
            string password = textBox2.Text;
            if (password.Length < 8)
            {
                MessageBox.Show("Password should be at least 8 characters long", "SignUp", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (comboBox1.Text.ToUpper() == "ADMIN")
            {
                Iuser.SaveUserData(new Admin(name, password, "ADMIN"));
            } else
            {
                string account = textBox3.Text;
                if(account.Length != 13)
                {

                    MessageBox.Show("Account Number must be equal to 13 DIGITS", "SignUp", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else if(ObjectHandler.GetUserDL().AccountExisted(account))
                {
                    MessageBox.Show("Account Number cannot be repeated", "SignUp", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                Iuser.SaveUserData(new Customer(name, password, "CUSTOMER", textBox3.Text));
            }
            MessageBox.Show("You are registered successfully", "SignUp", MessageBoxButtons.OK, MessageBoxIcon.Information);
            ObjectHandler.GetUserDL().StoreAllUsers();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.Text == "Admin")
            {
                textBox3.Enabled= false;
                textBox3.Text = string.Empty;
            }
            else
                textBox3.Enabled = true;
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
        }


        private void SignUp_Load(object sender, EventArgs e)
        {

        }
        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
