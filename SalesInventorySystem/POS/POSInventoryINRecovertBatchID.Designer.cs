namespace SalesInventorySystem.POS
{
    partial class POSInventoryINRecovertBatchID
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
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.txtbatchid = new DevExpress.XtraEditors.TextEdit();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.txtbatchid.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // labelControl6
            // 
            this.labelControl6.Appearance.Font = new System.Drawing.Font("Tahoma", 9.8F);
            this.labelControl6.Appearance.Options.UseFont = true;
            this.labelControl6.Location = new System.Drawing.Point(36, 36);
            this.labelControl6.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(121, 35);
            this.labelControl6.TabIndex = 12;
            this.labelControl6.Text = "Batch ID:";
            // 
            // txtbatchid
            // 
            this.txtbatchid.Location = new System.Drawing.Point(181, 29);
            this.txtbatchid.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.txtbatchid.Name = "txtbatchid";
            this.txtbatchid.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.8F);
            this.txtbatchid.Properties.Appearance.Options.UseFont = true;
            this.txtbatchid.Size = new System.Drawing.Size(268, 50);
            this.txtbatchid.TabIndex = 13;
            // 
            // simpleButton1
            // 
            this.simpleButton1.ImageOptions.Image = global::SalesInventorySystem.Properties.Resources.Save_16x16__5_;
            this.simpleButton1.Location = new System.Drawing.Point(181, 91);
            this.simpleButton1.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(268, 58);
            this.simpleButton1.TabIndex = 69;
            this.simpleButton1.Text = "Submit";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // POSInventoryINRecovertBatchID
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(14F, 29F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(533, 198);
            this.Controls.Add(this.simpleButton1);
            this.Controls.Add(this.labelControl6);
            this.Controls.Add(this.txtbatchid);
            this.Name = "POSInventoryINRecovertBatchID";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "POSInventoryINRecovertBatchID";
            ((System.ComponentModel.ISupportInitialize)(this.txtbatchid.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        public DevExpress.XtraEditors.TextEdit txtbatchid;
    }
}