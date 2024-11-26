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
    public partial class Bill : Form
    {
        public Bill()
        {
            InitializeComponent();
            UpdateAmountLabels(BuyMobile.PaidAmount + BuyLaptop.PaidAmount + BuySW.PaidAmount + BuyEarbuds.PaidAmount+Buy_Second_Hand_Device.PaidAmount, ObjectHandler.GetUserDL().AccountMoney(SignIn.currentUser));
        }
        private void UpdateAmountLabels(double paid, double remaining)
        {
            initialAmount.Text = "Your Initial Amount: $" + (paid+remaining).ToString();
            paidAmount.Text = "Your Paid Amount: $" + paid.ToString();
            remainingAmount.Text = "Your Remaining Amount: $" + remaining.ToString();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            Hide();
            Form form = new CustomerUI();
            form.ShowDialog();
        }

        private void Bill_Load(object sender, EventArgs e)
        {

        }

        private void remainingAmount_Click(object sender, EventArgs e)
        {

        }

        private void initialAmount_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
