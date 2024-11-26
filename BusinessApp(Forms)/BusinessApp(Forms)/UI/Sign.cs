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
    public partial class Sign : Form
    {
        public Sign()
        {
            InitializeComponent();
        }

        private void login_Click(object sender, EventArgs e)
        {
            Hide();
            Form form = new SignIn();
            form.ShowDialog();
        }

        private void register_Click(object sender, EventArgs e)
        {
            Hide();
            Form form = new SignUp();
            form.ShowDialog();
        }

        private void Exit_Click(object sender, EventArgs e)
        {
            ObjectHandler.GetUserDL().StoreAllUsers();
            Close();
        }
    }
}
