namespace SalesInventorySystem.POS
{
    partial class POSHistoryCaption
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.txtamounttenderedcap = new System.Windows.Forms.Label();
            this.txtamountchangecap = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 20.25F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.Gray;
            this.label1.Location = new System.Drawing.Point(28, 14);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(314, 40);
            this.label1.TabIndex = 1;
            this.label1.Text = "Amount Tendered:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 40.25F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(10, 65);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(308, 79);
            this.label2.TabIndex = 2;
            this.label2.Text = "Change:";
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.LightBlue;
            this.button1.FlatAppearance.BorderColor = System.Drawing.Color.Crimson;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F);
            this.button1.Location = new System.Drawing.Point(24, 158);
            this.button1.Margin = new System.Windows.Forms.Padding(4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(632, 53);
            this.button1.TabIndex = 3;
            this.button1.Text = "Press Enter to Continue Next Transaction";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtamounttenderedcap
            // 
            this.txtamounttenderedcap.AutoSize = true;
            this.txtamounttenderedcap.Font = new System.Drawing.Font("Arial", 25.25F, System.Drawing.FontStyle.Bold);
            this.txtamounttenderedcap.ForeColor = System.Drawing.Color.Gray;
            this.txtamounttenderedcap.Location = new System.Drawing.Point(367, 7);
            this.txtamounttenderedcap.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.txtamounttenderedcap.Name = "txtamounttenderedcap";
            this.txtamounttenderedcap.Size = new System.Drawing.Size(106, 51);
            this.txtamounttenderedcap.TabIndex = 4;
            this.txtamounttenderedcap.Text = "0.00";
            // 
            // txtamountchangecap
            // 
            this.txtamountchangecap.AutoSize = true;
            this.txtamountchangecap.Font = new System.Drawing.Font("Arial", 40.25F, System.Drawing.FontStyle.Bold);
            this.txtamountchangecap.ForeColor = System.Drawing.Color.Yellow;
            this.txtamountchangecap.Location = new System.Drawing.Point(340, 65);
            this.txtamountchangecap.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.txtamountchangecap.Name = "txtamountchangecap";
            this.txtamountchangecap.Size = new System.Drawing.Size(167, 79);
            this.txtamountchangecap.TabIndex = 5;
            this.txtamountchangecap.Text = "0.00";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Black;
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.txtamountchangecap);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.txtamounttenderedcap);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(682, 231);
            this.panel1.TabIndex = 6;
            // 
            // POSHistoryCaption
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(689, 237);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "POSHistoryCaption";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Latest Transaction";
            this.Load += new System.EventHandler(this.POSHistoryCaption_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button1;
        public System.Windows.Forms.Label txtamountchangecap;
        public System.Windows.Forms.Label txtamounttenderedcap;
        private System.Windows.Forms.Panel panel1;
    }
}