namespace BusinessApp_Forms_.UI
{
    partial class Bill
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Bill));
            this.initialAmount = new System.Windows.Forms.Label();
            this.paidAmount = new System.Windows.Forms.Label();
            this.remainingAmount = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // initialAmount
            // 
            this.initialAmount.AutoSize = true;
            this.initialAmount.Font = new System.Drawing.Font("Segoe Script", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.initialAmount.ForeColor = System.Drawing.Color.SteelBlue;
            this.initialAmount.Location = new System.Drawing.Point(82, 71);
            this.initialAmount.Name = "initialAmount";
            this.initialAmount.Size = new System.Drawing.Size(388, 48);
            this.initialAmount.TabIndex = 7;
            this.initialAmount.Text = "Your Initial Amount: $";
            this.initialAmount.Click += new System.EventHandler(this.initialAmount_Click);
            // 
            // paidAmount
            // 
            this.paidAmount.AutoSize = true;
            this.paidAmount.Font = new System.Drawing.Font("Segoe Script", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.paidAmount.ForeColor = System.Drawing.Color.SteelBlue;
            this.paidAmount.Location = new System.Drawing.Point(109, 180);
            this.paidAmount.Name = "paidAmount";
            this.paidAmount.Size = new System.Drawing.Size(361, 48);
            this.paidAmount.TabIndex = 8;
            this.paidAmount.Text = "Your Paid Amount: $";
            // 
            // remainingAmount
            // 
            this.remainingAmount.AutoSize = true;
            this.remainingAmount.Font = new System.Drawing.Font("Segoe Script", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.remainingAmount.ForeColor = System.Drawing.Color.SteelBlue;
            this.remainingAmount.Location = new System.Drawing.Point(3, 285);
            this.remainingAmount.Name = "remainingAmount";
            this.remainingAmount.Size = new System.Drawing.Size(467, 48);
            this.remainingAmount.TabIndex = 9;
            this.remainingAmount.Text = "Your Remaining Amount: $";
            this.remainingAmount.Click += new System.EventHandler(this.remainingAmount_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(1, 1);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1312, 823);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 26;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.remainingAmount);
            this.panel1.Controls.Add(this.paidAmount);
            this.panel1.Controls.Add(this.initialAmount);
            this.panel1.Location = new System.Drawing.Point(52, 167);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(693, 446);
            this.panel1.TabIndex = 27;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(276, 1);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(257, 190);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 28;
            this.pictureBox2.TabStop = false;
            // 
            // Bill
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1315, 822);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Bill";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Bill";
            this.Load += new System.EventHandler(this.Bill_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label initialAmount;
        private System.Windows.Forms.Label paidAmount;
        private System.Windows.Forms.Label remainingAmount;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pictureBox2;
    }
}