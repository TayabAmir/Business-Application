﻿using BusinessApp_Forms_.Utils;
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
using TechStream.BL;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace BusinessApp_Forms_.UI
{
    public partial class Delete : Form
    {
        public Delete()
        {
            InitializeComponent();
            ShowDevice();
            if (ObjectHandler.GetDeviceType() == "MOBILE")
                BackgroundImage = Image.FromFile("D:\\Tayyab\\BusinessAppProject\\BusinessApp(Forms)\\BusinessApp(Forms)\\Mobile2.jpeg");
            else if (ObjectHandler.GetDeviceType() == "LAPTOP")
                BackgroundImage = Image.FromFile("D:\\Tayyab\\BusinessAppProject\\BusinessApp(Forms)\\BusinessApp(Forms)\\laptop.jpg");
            else if (ObjectHandler.GetDeviceType() == "SMART WATCH")
                BackgroundImage = Image.FromFile("D:\\Tayyab\\BusinessAppProject\\BusinessApp(Forms)\\BusinessApp(Forms)\\SW4.jpg");
            else if (ObjectHandler.GetDeviceType() == "EARBUD")
                BackgroundImage = Image.FromFile("D:\\Tayyab\\BusinessAppProject\\BusinessApp(Forms)\\BusinessApp(Forms)\\ear.jpg");
            else
                BackgroundImage = Image.FromFile("D:\\Tayyab\\BusinessAppProject\\BusinessApp(Forms)\\BusinessApp(Forms)\\SH.jpeg");

            BackgroundImageLayout = ImageLayout.Stretch;

            dataGridView1.Columns["Type"].Width = 100;
            if (ObjectHandler.GetDeviceType() != "SECOND HAND DEVICE")
                dataGridView1.Columns["Company"].Width = 120;
            dataGridView1.Columns["Model"].Width = 180;
            dataGridView1.Columns["Price"].Width = 100;


            List<Device> devices = ObjectHandler.GetDeviceDL().GetDevices();
            dataGridView1.CellClick += dataGridView1_CellContentClick;
            foreach (Device device in devices)
                if (device.GetDeviceType() == "MOBILE")
                    company.Items.Add(device.GetCompany());
            if (ObjectHandler.GetDeviceType() == "SECOND HAND DEVICE")
            {
                company.Enabled = false;
                company.Text = "";
            }
        }
        private void ShowDevice()
        {
            List<Device> devices = ObjectHandler.GetDeviceDL().GetDevices();
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Type", typeof(string));
            if (ObjectHandler.GetDeviceType() != "SECOND HAND DEVICE")
                dataTable.Columns.Add("Company", typeof(string));
            dataTable.Columns.Add("Model", typeof(string));
            dataTable.Columns.Add("Price", typeof(int));

            foreach (Device device in devices)
                if (device.GetDeviceType() == ObjectHandler.GetDeviceType())
                {
                    if (ObjectHandler.GetDeviceType() != "SECOND HAND DEVICE")
                        dataTable.Rows.Add(device.GetDeviceType(), device.GetCompany(), device.GetModel(), device.GetModelPrice());
                    else
                        dataTable.Rows.Add(device.GetDeviceType(), device.GetModel(), device.GetModelPrice());
                }
            dataGridView1.DataSource = dataTable;
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                if (ObjectHandler.GetDeviceType() != "SECOND HAND DEVICE")
                    company.Text = row.Cells["Company"].Value.ToString();
                model.Text = row.Cells["Model"].Value.ToString();
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(company.Text) || string.IsNullOrEmpty(model.Text))
            {
                MessageBox.Show("Input cannot be empty.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (ObjectHandler.GetDeviceType() != "SECOND HAND DEVICE")
                ObjectHandler.GetDeviceDL().DeleteModel(company.Text, model.Text);
            else
                ObjectHandler.GetDeviceDL().DeleteModel(null, model.Text);
                    MessageBox.Show("Device Model Deleted Successfully", "Device Crud", MessageBoxButtons.OK, MessageBoxIcon.Information);

            ShowDevice();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Hide();
            CRUDMenu cRUD = new CRUDMenu();
            cRUD.ShowDialog();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            model.Items.Clear();
            string selectedItem = company.SelectedItem.ToString();
            List<Device> devices = ObjectHandler.GetDeviceDL().GetDevices();
            foreach (Device device in devices)
            {
                if (device.GetCompany() == selectedItem)
                {
                    model.Items.Add(device.GetModel());
                }
            }
        }
    }
}
