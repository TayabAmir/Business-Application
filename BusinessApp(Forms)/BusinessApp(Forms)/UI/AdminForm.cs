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
    public partial class AdminForm : Form
    {
        public AdminForm()
        {
            InitializeComponent();
            HideButtons();
            this.BackColor = SystemColors.HotTrack;
        }
        private void ShowButtons()
        {
            Watch.Visible = true;
            Add.Visible = true;
            Edit.Visible = true;
            Delete.Visible = true;
        }
        private void HideButtons()
        {
            Watch.Visible = false;
            Add.Visible = false;
            Edit.Visible = false;
            Delete.Visible = false;
        }
        private void setForm(Form form)
        {
            List<Control> controlsToRemove = new List<Control>();

            foreach (Control control in panel_main.Controls)
            {
                if (control != Watch && control != Add && control != Delete && control != Edit)
                {
                    controlsToRemove.Add(control);
                }
            }
            foreach (Control controlToRemove in controlsToRemove)
            {
                panel_main.Controls.Remove(controlToRemove);
            }
            form.TopLevel = false;
            form.FormBorderStyle = FormBorderStyle.None;
            panel_main.Controls.Add(form);
            form.Dock = DockStyle.Fill;
            panel_main.Tag = form;
            form.Show();
        }
        private void Watch_Click(object sender, EventArgs e)
        {
            Form form = new Watch();
            HideButtons();
            setForm(form);
        }

        private void Add_Click(object sender, EventArgs e)
        {
            Form form = new Add();
            HideButtons();
            setForm(form);
        }

        private void Edit_Click(object sender, EventArgs e)
        {
            Form form = new Edit();
            HideButtons();
            setForm(form);
        }

        private void Delete_Click(object sender, EventArgs e)
        {
            Form form = new Delete();
            HideButtons();
            setForm(form);
        }

        private void WatchUsers_Click(object sender, EventArgs e)
        {
            Form form = new WatchUsers();
            HideButtons();
            setForm(form);
        }

        private void Mobile_Click(object sender, EventArgs e)
        {
            ObjectHandler.type = "MOBILE";
            Form form = new CRUDMenu();
            ShowButtons();
            setForm(form);
        }

        private void Laptop_Click(object sender, EventArgs e)
        {
            ObjectHandler.type = "LAPTOP";
            Form form = new CRUDMenu();
            ShowButtons();
            setForm(form);
        }

        private void SW_Click(object sender, EventArgs e)
        {
            ObjectHandler.type = "SMART WATCH";
            Form form = new CRUDMenu();
            ShowButtons();
            setForm(form);
        }

        private void Earbud_Click(object sender, EventArgs e)
        {
            ObjectHandler.type = "EARBUD";
            Form form = new CRUDMenu();
            ShowButtons();
            setForm(form);
        }

        private void SHDevices_Click(object sender, EventArgs e)
        {
            ObjectHandler.type = "SECOND HAND DEVICE";
            Form form = new CRUDMenu();
            ShowButtons();
            setForm(form);
        }

        private void Feedback_Click(object sender, EventArgs e)
        {
            Form form = new ShowFeedbacks();
            HideButtons();
            setForm(form);
        }

        private void Sales_Click(object sender, EventArgs e)
        {
            Form form = new Sales();
            HideButtons();
            setForm(form);
        }

        private void home_Click(object sender, EventArgs e)
        {
            Hide();
            Form form = new Sign();
            form.ShowDialog();
        }
    }
}
