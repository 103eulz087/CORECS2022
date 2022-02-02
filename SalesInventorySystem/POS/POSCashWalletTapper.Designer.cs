namespace SalesInventorySystem.POS
{
    partial class POSCashWalletTapper
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(POSCashWalletTapper));
            this.groupcashwallet = new System.Windows.Forms.Panel();
            this.txttapid = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            this.btnsubmit = new DevExpress.XtraEditors.SimpleButton();
            this.groupcashwallet.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupcashwallet
            // 
            this.groupcashwallet.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.groupcashwallet.Controls.Add(this.btnClose);
            this.groupcashwallet.Controls.Add(this.btnsubmit);
            this.groupcashwallet.Controls.Add(this.txttapid);
            this.groupcashwallet.Controls.Add(this.label8);
            this.groupcashwallet.Location = new System.Drawing.Point(3, 2);
            this.groupcashwallet.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupcashwallet.Name = "groupcashwallet";
            this.groupcashwallet.Size = new System.Drawing.Size(547, 142);
            this.groupcashwallet.TabIndex = 23;
            // 
            // txttapid
            // 
            this.txttapid.BackColor = System.Drawing.Color.SeaGreen;
            this.txttapid.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.txttapid.Location = new System.Drawing.Point(28, 44);
            this.txttapid.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txttapid.MaxLength = 50;
            this.txttapid.Name = "txttapid";
            this.txttapid.Size = new System.Drawing.Size(492, 34);
            this.txttapid.TabIndex = 11;
            this.txttapid.TextChanged += new System.EventHandler(this.txttapid_TextChanged);
            this.txttapid.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txttapid_KeyDown);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold);
            this.label8.ForeColor = System.Drawing.Color.White;
            this.label8.Location = new System.Drawing.Point(89, 12);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(347, 29);
            this.label8.TabIndex = 5;
            this.label8.Text = "(Tap your ID on the Device)";
            // 
            // btnClose
            // 
            this.btnClose.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnClose.ImageOptions.Image")));
            this.btnClose.Location = new System.Drawing.Point(392, 85);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(128, 46);
            this.btnClose.TabIndex = 15;
            this.btnClose.Text = "Close";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnsubmit
            // 
            this.btnsubmit.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnsubmit.ImageOptions.Image")));
            this.btnsubmit.Location = new System.Drawing.Point(258, 85);
            this.btnsubmit.Name = "btnsubmit";
            this.btnsubmit.Size = new System.Drawing.Size(128, 46);
            this.btnsubmit.TabIndex = 14;
            this.btnsubmit.Text = "Submit";
            this.btnsubmit.Click += new System.EventHandler(this.btnsubmit_Click);
            // 
            // POSCashWalletTapper
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(552, 148);
            this.Controls.Add(this.groupcashwallet);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "POSCashWalletTapper";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "POSCashWalletTapper";
            this.Load += new System.EventHandler(this.POSCashWalletTapper_Load);
            this.groupcashwallet.ResumeLayout(false);
            this.groupcashwallet.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel groupcashwallet;
        public System.Windows.Forms.TextBox txttapid;
        private System.Windows.Forms.Label label8;
        private DevExpress.XtraEditors.SimpleButton btnClose;
        private DevExpress.XtraEditors.SimpleButton btnsubmit;
    }
}