namespace SalesInventorySystem.AccountingDevEx
{
    partial class CancelledCheckVoucher
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CancelledCheckVoucher));
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.txtvoucherid = new DevExpress.XtraEditors.TextEdit();
            this.txtsuppid = new DevExpress.XtraEditors.TextEdit();
            this.txtrefno = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.txtreason = new System.Windows.Forms.RichTextBox();
            this.btnsearch = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtvoucherid.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtsuppid.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtrefno.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.btnsearch);
            this.panelControl1.Controls.Add(this.txtreason);
            this.panelControl1.Controls.Add(this.labelControl1);
            this.panelControl1.Controls.Add(this.txtrefno);
            this.panelControl1.Controls.Add(this.txtsuppid);
            this.panelControl1.Controls.Add(this.txtvoucherid);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(523, 172);
            this.panelControl1.TabIndex = 0;
            // 
            // txtvoucherid
            // 
            this.txtvoucherid.Location = new System.Drawing.Point(50, 181);
            this.txtvoucherid.Name = "txtvoucherid";
            this.txtvoucherid.Size = new System.Drawing.Size(125, 22);
            this.txtvoucherid.TabIndex = 0;
            this.txtvoucherid.Visible = false;
            // 
            // txtsuppid
            // 
            this.txtsuppid.Location = new System.Drawing.Point(197, 181);
            this.txtsuppid.Name = "txtsuppid";
            this.txtsuppid.Size = new System.Drawing.Size(125, 22);
            this.txtsuppid.TabIndex = 1;
            this.txtsuppid.Visible = false;
            // 
            // txtrefno
            // 
            this.txtrefno.Location = new System.Drawing.Point(341, 181);
            this.txtrefno.Name = "txtrefno";
            this.txtrefno.Size = new System.Drawing.Size(125, 22);
            this.txtrefno.TabIndex = 2;
            this.txtrefno.Visible = false;
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 9.8F);
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Location = new System.Drawing.Point(12, 52);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(57, 19);
            this.labelControl1.TabIndex = 3;
            this.labelControl1.Text = "Reason:";
            // 
            // txtreason
            // 
            this.txtreason.Location = new System.Drawing.Point(75, 18);
            this.txtreason.Name = "txtreason";
            this.txtreason.Size = new System.Drawing.Size(424, 99);
            this.txtreason.TabIndex = 4;
            this.txtreason.Text = "";
            this.txtreason.TextChanged += new System.EventHandler(this.richTextBox1_TextChanged);
            // 
            // btnsearch
            // 
            this.btnsearch.Appearance.Font = new System.Drawing.Font("Tahoma", 8.5F);
            this.btnsearch.Appearance.Options.UseFont = true;
            this.btnsearch.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnsearch.ImageOptions.Image")));
            this.btnsearch.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btnsearch.Location = new System.Drawing.Point(364, 122);
            this.btnsearch.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnsearch.Name = "btnsearch";
            this.btnsearch.Size = new System.Drawing.Size(135, 35);
            this.btnsearch.TabIndex = 462;
            this.btnsearch.Text = "Submit";
            this.btnsearch.Click += new System.EventHandler(this.btnsearch_Click_1);
            // 
            // CancelledCheckVoucher
            // 
            this.ClientSize = new System.Drawing.Size(523, 172);
            this.Controls.Add(this.panelControl1);
            this.Name = "CancelledCheckVoucher";
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtvoucherid.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtsuppid.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtrefno.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        public DevExpress.XtraEditors.TextEdit txtrefno;
        public DevExpress.XtraEditors.TextEdit txtsuppid;
        public DevExpress.XtraEditors.TextEdit txtvoucherid;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private System.Windows.Forms.RichTextBox txtreason;
        private DevExpress.XtraEditors.SimpleButton btnsearch;
    }
}