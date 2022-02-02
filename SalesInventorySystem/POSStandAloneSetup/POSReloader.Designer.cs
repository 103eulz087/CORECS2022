namespace SalesInventorySystem.POSStandAloneSetup
{
    partial class POSReloader
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
            this.txtcreditlimit = new DevExpress.XtraEditors.SpinEdit();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.txtcustname = new DevExpress.XtraEditors.TextEdit();
            ((System.ComponentModel.ISupportInitialize)(this.txtcreditlimit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtcustname.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // txtcreditlimit
            // 
            this.txtcreditlimit.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtcreditlimit.Location = new System.Drawing.Point(280, 20);
            this.txtcreditlimit.Margin = new System.Windows.Forms.Padding(7, 7, 7, 7);
            this.txtcreditlimit.Name = "txtcreditlimit";
            this.txtcreditlimit.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtcreditlimit.Properties.Appearance.Options.UseFont = true;
            this.txtcreditlimit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtcreditlimit.Size = new System.Drawing.Size(233, 56);
            this.txtcreditlimit.TabIndex = 459;
            // 
            // labelControl7
            // 
            this.labelControl7.Appearance.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl7.Appearance.Options.UseFont = true;
            this.labelControl7.Location = new System.Drawing.Point(28, 27);
            this.labelControl7.Margin = new System.Windows.Forms.Padding(7, 7, 7, 7);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(218, 41);
            this.labelControl7.TabIndex = 458;
            this.labelControl7.Text = "Enter Amount:";
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Location = new System.Drawing.Point(28, 94);
            this.labelControl1.Margin = new System.Windows.Forms.Padding(7, 7, 7, 7);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(231, 41);
            this.labelControl1.TabIndex = 460;
            this.labelControl1.Text = "Tap Card Here:";
            // 
            // txtcustname
            // 
            this.txtcustname.Location = new System.Drawing.Point(280, 87);
            this.txtcustname.Margin = new System.Windows.Forms.Padding(7, 7, 7, 7);
            this.txtcustname.Name = "txtcustname";
            this.txtcustname.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 11.25F);
            this.txtcustname.Properties.Appearance.Options.UseFont = true;
            this.txtcustname.Size = new System.Drawing.Size(546, 56);
            this.txtcustname.TabIndex = 461;
            // 
            // POSReloader
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(14F, 29F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(875, 261);
            this.Controls.Add(this.txtcustname);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.txtcreditlimit);
            this.Controls.Add(this.labelControl7);
            this.Margin = new System.Windows.Forms.Padding(7, 7, 7, 7);
            this.Name = "POSReloader";
            this.Text = "POSReloader";
            this.Load += new System.EventHandler(this.POSReloader_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtcreditlimit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtcustname.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.SpinEdit txtcreditlimit;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit txtcustname;
    }
}