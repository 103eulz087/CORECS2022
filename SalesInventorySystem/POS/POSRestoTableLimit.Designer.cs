namespace SalesInventorySystem.POS
{
    partial class POSRestoTableLimit
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
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.txtlimitamount = new DevExpress.XtraEditors.SpinEdit();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.txttableno = new DevExpress.XtraEditors.TextEdit();
            ((System.ComponentModel.ISupportInitialize)(this.txtlimitamount.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txttableno.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 10.25F);
            this.labelControl1.Appearance.ForeColor = System.Drawing.Color.Red;
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Appearance.Options.UseForeColor = true;
            this.labelControl1.Location = new System.Drawing.Point(43, 69);
            this.labelControl1.Margin = new System.Windows.Forms.Padding(6);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(168, 33);
            this.labelControl1.TabIndex = 13;
            this.labelControl1.Text = "Limit Amount:";
            // 
            // txtlimitamount
            // 
            this.txtlimitamount.EditValue = new decimal(new int[] {
            15000,
            0,
            0,
            0});
            this.txtlimitamount.Location = new System.Drawing.Point(223, 63);
            this.txtlimitamount.Margin = new System.Windows.Forms.Padding(6, 4, 6, 4);
            this.txtlimitamount.Name = "txtlimitamount";
            this.txtlimitamount.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.8F);
            this.txtlimitamount.Properties.Appearance.Options.UseFont = true;
            this.txtlimitamount.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtlimitamount.Properties.MaxLength = 6;
            this.txtlimitamount.Size = new System.Drawing.Size(249, 46);
            this.txtlimitamount.TabIndex = 14;
            // 
            // btnSave
            // 
            this.btnSave.ImageOptions.Image = global::SalesInventorySystem.Properties.Resources.Save_16x16__5_;
            this.btnSave.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btnSave.Location = new System.Drawing.Point(223, 120);
            this.btnSave.Margin = new System.Windows.Forms.Padding(5, 7, 5, 7);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(249, 51);
            this.btnSave.TabIndex = 42;
            this.btnSave.Text = "SAVE";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Tahoma", 10.25F);
            this.labelControl2.Appearance.ForeColor = System.Drawing.Color.Red;
            this.labelControl2.Appearance.Options.UseFont = true;
            this.labelControl2.Appearance.Options.UseForeColor = true;
            this.labelControl2.Location = new System.Drawing.Point(43, 15);
            this.labelControl2.Margin = new System.Windows.Forms.Padding(6);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(124, 33);
            this.labelControl2.TabIndex = 43;
            this.labelControl2.Text = "Table No.:";
            // 
            // txttableno
            // 
            this.txttableno.Location = new System.Drawing.Point(223, 16);
            this.txttableno.Name = "txttableno";
            this.txttableno.Properties.ReadOnly = true;
            this.txttableno.Size = new System.Drawing.Size(249, 40);
            this.txttableno.TabIndex = 44;
            // 
            // POSRestoTableLimit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(524, 213);
            this.Controls.Add(this.txttableno);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.txtlimitamount);
            this.Name = "POSRestoTableLimit";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "POSRestoTableLimit";
            ((System.ComponentModel.ISupportInitialize)(this.txtlimitamount.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txttableno.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        public DevExpress.XtraEditors.SpinEdit txtlimitamount;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        public DevExpress.XtraEditors.TextEdit txttableno;
    }
}