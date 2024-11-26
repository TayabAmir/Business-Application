using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BusinessApp_Forms_.UI
{
    public partial class ShowFeedbacks : Form
    {
        public ShowFeedbacks()
        {
            InitializeComponent();
            List<string> names = ObjectHandler.GetUserDL().GetCurrentCustomerNames();
            List<string> feedbacks = ObjectHandler.GetUserDL().GetFeedbacks();
            ShowFeedbacksTable(names, feedbacks); 
            dataGridView1.Columns["Name"].Width = 120;
            dataGridView1.Columns["Feedback"].Width = 230;
        }
        private void ShowFeedbacksTable(List<string> names, List<string> feedbacks)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Name", typeof(string));
            dataTable.Columns.Add("Feedback", typeof(string));

            for(int i=0;i<names.Count;i++) { 
                dataTable.Rows.Add(names[i], feedbacks[i]);
            }
            dataGridView1.DataSource = dataTable;
        }
        private void ShowFeedbacks_Load(object sender, EventArgs e)
        {

        }

        private void home_Click(object sender, EventArgs e)
        {
            Hide();
            Form form = new AdminForm();
            form.ShowDialog();
        }
    }
}
