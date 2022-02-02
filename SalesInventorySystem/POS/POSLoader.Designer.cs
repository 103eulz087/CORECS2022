namespace SalesInventorySystem.POS
{
    partial class POSLoader
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
            this.Label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txttapid = new System.Windows.Forms.TextBox();
            this.txtamount = new System.Windows.Forms.TextBox();
            this.btnreload = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.Label1.Location = new System.Drawing.Point(13, 19);
            this.Label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(70, 28);
            this.Label1.TabIndex = 442;
            this.Label1.Text = "Tap ID:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.label2.Location = new System.Drawing.Point(13, 62);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(87, 28);
            this.label2.TabIndex = 443;
            this.label2.Text = "Amount:";
            // 
            // txttapid
            // 
            this.txttapid.BackColor = System.Drawing.Color.White;
            this.txttapid.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.txttapid.Location = new System.Drawing.Point(104, 16);
            this.txttapid.Margin = new System.Windows.Forms.Padding(4);
            this.txttapid.MaxLength = 50;
            this.txttapid.Name = "txttapid";
            this.txttapid.Size = new System.Drawing.Size(492, 34);
            this.txttapid.TabIndex = 446;
            this.txttapid.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txttapid_KeyDown);
            // 
            // txtamount
            // 
            this.txtamount.BackColor = System.Drawing.Color.White;
            this.txtamount.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.txtamount.Location = new System.Drawing.Point(104, 59);
            this.txtamount.Margin = new System.Windows.Forms.Padding(4);
            this.txtamount.MaxLength = 50;
            this.txtamount.Name = "txtamount";
            this.txtamount.Size = new System.Drawing.Size(492, 34);
            this.txtamount.TabIndex = 447;
            this.txtamount.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtamount_KeyDown);
            this.txtamount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtamount_KeyPress);
            // 
            // btnreload
            // 
            this.btnreload.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.btnreload.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.btnreload.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnreload.Location = new System.Drawing.Point(104, 101);
            this.btnreload.Margin = new System.Windows.Forms.Padding(4);
            this.btnreload.Name = "btnreload";
            this.btnreload.Size = new System.Drawing.Size(492, 40);
            this.btnreload.TabIndex = 448;
            this.btnreload.Text = "RELOAD";
            this.btnreload.UseVisualStyleBackColor = false;
            this.btnreload.Click += new System.EventHandler(this.btnreload_Click);
            // 
            // POSLoader
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(611, 148);
            this.Controls.Add(this.btnreload);
            this.Controls.Add(this.txtamount);
            this.Controls.Add(this.txttapid);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.Label1);
            this.Name = "POSLoader";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "POSLoader";
            this.Load += new System.EventHandler(this.POSLoader_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.Label label2;
        public System.Windows.Forms.TextBox txttapid;
        public System.Windows.Forms.TextBox txtamount;
        internal System.Windows.Forms.Button btnreload;
    }
}