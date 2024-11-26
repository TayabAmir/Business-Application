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

namespace BusinessApp_Forms_.UI
{
    public partial class AddMoney : Form
    {
        public AddMoney()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e) // button_1 = Back Button
        {
            Hide();
            Form form = new CustomerUI();
            form.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e) // button_2 = Add Money Button
        {
            IUserDL Iuser = ObjectHandler.GetUserDL();
            Customer user = SignIn.GetCurrentUser();
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("Input cannot be empty.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!Validation.CheckOptionValidation(textBox2.Text))
            {
                MessageBox.Show("Write valid Price", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string accountNo = textBox1.Text;
            if (Iuser.CheckAccount(accountNo, user.GetAccountNo()) && accountNo.Length == 13)
            {
                string accountMoney = textBox2.Text;
                double money;
                if (CheckValidation(accountMoney)) 
                {
                    money = double.Parse(accountMoney);
                }
                else
                {
                    MessageBox.Show("Wrong Input", "Account Money", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (Iuser.UpdateMoney(user, user.GetAccountMoney() + money))
                {
                    MessageBox.Show($"Your amount of {money} has been updated", "Account Money", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    money = user.GetAccountMoney();
                }
                else
                    MessageBox.Show("Amount cannot be upgraded to greater than 400k", "Account Money", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            MessageBox.Show("Wrong Account No", "Account No", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        static public bool CheckValidation(string input)
        {
            for (int i = 0; i < input.Length; i++)
            {
                char ch = input[i];
                if (ch < '0' || ch > '9')
                {
                    return false;
                }
            }
            return true;
        }
    }
}
