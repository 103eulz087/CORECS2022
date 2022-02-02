namespace SalesInventorySystem.POSDevEx
{
    partial class ExecuteTicketOptionFrmDevEx
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
            this.label4 = new System.Windows.Forms.Label();
            this.tickettype = new System.Windows.Forms.ComboBox();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.SuspendLayout();
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F);
            this.label4.Location = new System.Drawing.Point(40, 41);
            this.label4.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(185, 29);
            this.label4.TabIndex = 35;
            this.label4.Text = "Process Option:";
            // 
            // tickettype
            // 
            this.tickettype.FormattingEnabled = true;
            this.tickettype.Items.AddRange(new object[] {
            "LIST",
            "UPDATE"});
            this.tickettype.Location = new System.Drawing.Point(232, 41);
            this.tickettype.Margin = new System.Windows.Forms.Padding(5);
            this.tickettype.Name = "tickettype";
            this.tickettype.Size = new System.Drawing.Size(199, 31);
            this.tickettype.TabIndex = 34;
            // 
            // simpleButton1
            // 
            this.simpleButton1.Location = new System.Drawing.Point(443, 41);
            this.simpleButton1.Margin = new System.Windows.Forms.Padding(5);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(125, 41);
            this.simpleButton1.TabIndex = 36;
            this.simpleButton1.Text = "Select";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // ExecuteTicketOptionFrmDevEx
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(635, 120);
            this.Controls.Add(this.simpleButton1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.tickettype);
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "ExecuteTicketOptionFrmDevEx";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ExecuteTicketOptionFrmDevEx";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox tickettype;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
    }
}