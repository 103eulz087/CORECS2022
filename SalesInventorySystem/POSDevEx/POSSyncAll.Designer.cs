namespace SalesInventorySystem.POSDevEx
{
    partial class POSSyncAll
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
            this.btnsync = new DevExpress.XtraEditors.SimpleButton();
            this.progressBar = new DevExpress.XtraEditors.ProgressBarControl();
            this.lblStatus = new DevExpress.XtraEditors.LabelControl();
            this.progressSpinner = new DevExpress.XtraWaitForm.ProgressPanel();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.txtcombotables = new DevExpress.XtraEditors.ComboBoxEdit();
            ((System.ComponentModel.ISupportInitialize)(this.progressBar.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtcombotables.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // btnsync
            // 
            this.btnsync.Location = new System.Drawing.Point(21, 59);
            this.btnsync.Name = "btnsync";
            this.btnsync.Size = new System.Drawing.Size(138, 29);
            this.btnsync.TabIndex = 0;
            this.btnsync.Text = "Start Morning Sync";
            this.btnsync.Click += new System.EventHandler(this.btnsync_Click);
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(21, 104);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(246, 22);
            this.progressBar.TabIndex = 1;
            // 
            // lblStatus
            // 
            this.lblStatus.Location = new System.Drawing.Point(165, 66);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(0, 16);
            this.lblStatus.TabIndex = 2;
            // 
            // progressSpinner
            // 
            this.progressSpinner.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.progressSpinner.Appearance.Options.UseBackColor = true;
            this.progressSpinner.Caption = "Please Wait..";
            this.progressSpinner.Location = new System.Drawing.Point(21, 132);
            this.progressSpinner.Name = "progressSpinner";
            this.progressSpinner.Size = new System.Drawing.Size(246, 66);
            this.progressSpinner.TabIndex = 3;
            this.progressSpinner.Text = "progressPanel1";
            this.progressSpinner.Visible = false;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(21, 25);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(76, 16);
            this.labelControl1.TabIndex = 4;
            this.labelControl1.Text = "Select Table:";
            // 
            // txtcombotables
            // 
            this.txtcombotables.Location = new System.Drawing.Point(103, 22);
            this.txtcombotables.Name = "txtcombotables";
            this.txtcombotables.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtcombotables.Properties.Items.AddRange(new object[] {
            "Sync ALL Data",
            "Products",
            "Users",
            "Customers",
            "Branches",
            "Categories & Types"});
            this.txtcombotables.Size = new System.Drawing.Size(164, 22);
            this.txtcombotables.TabIndex = 5;
            // 
            // POSSyncAll
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(294, 214);
            this.Controls.Add(this.txtcombotables);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.progressSpinner);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.btnsync);
            this.Name = "POSSyncAll";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "POSSyncAll";
            this.Load += new System.EventHandler(this.POSSyncAll_Load);
            ((System.ComponentModel.ISupportInitialize)(this.progressBar.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtcombotables.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnsync;
        private DevExpress.XtraEditors.ProgressBarControl progressBar;
        private DevExpress.XtraEditors.LabelControl lblStatus;
        private DevExpress.XtraWaitForm.ProgressPanel progressSpinner;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.ComboBoxEdit txtcombotables;
    }
}