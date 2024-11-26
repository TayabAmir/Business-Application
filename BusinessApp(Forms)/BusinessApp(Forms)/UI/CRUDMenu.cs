using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BusinessApp_Forms_.UI
{
    public partial class CRUDMenu : Form
    {
        public CRUDMenu()
        {
            InitializeComponent();
            if (ObjectHandler.GetDeviceType() == "MOBILE")
                pictureBox1.BackgroundImage = Image.FromFile("D:\\Tayyab\\BusinessAppProject\\BusinessApp(Forms)\\BusinessApp(Forms)\\Mobile2.jpeg");
            else if (ObjectHandler.GetDeviceType() == "LAPTOP")
                pictureBox1.BackgroundImage = Image.FromFile("D:\\Tayyab\\BusinessAppProject\\BusinessApp(Forms)\\BusinessApp(Forms)\\laptop.jpg");
            else if (ObjectHandler.GetDeviceType() == "SMART WATCH")
                pictureBox1.BackgroundImage = Image.FromFile("D:\\Tayyab\\BusinessAppProject\\BusinessApp(Forms)\\BusinessApp(Forms)\\SW4.jpg");
            else if (ObjectHandler.GetDeviceType() == "EARBUD")
                pictureBox1.BackgroundImage = Image.FromFile("D:\\Tayyab\\BusinessAppProject\\BusinessApp(Forms)\\BusinessApp(Forms)\\ear.jpg");
            else
                pictureBox1.BackgroundImage = Image.FromFile("D:\\Tayyab\\BusinessAppProject\\BusinessApp(Forms)\\BusinessApp(Forms)\\SH.jpeg");

                pictureBox1.BackgroundImageLayout = ImageLayout.Stretch;
        }

        private void Back_Click(object sender, EventArgs e)
        {
            Hide();
            Form form = new AdminForm();
            form.ShowDialog();
        }

        private void Watch_Click(object sender, EventArgs e)
        {
            Hide();
            Form form = new Watch();
            form.ShowDialog();
        }

        private void Add_Click(object sender, EventArgs e)
        {
            Hide();
            Form form = new Add();
            form.ShowDialog();
        }

        private void Update_Click(object sender, EventArgs e)
        {
            Hide();
            Form form = new Edit();
            form.ShowDialog();
        }

        private void Delete_Click(object sender, EventArgs e)
        {
            Hide();
            Form form = new Delete();
            form.ShowDialog();
        }

        private void CRUDMenu_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
