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
            ((System.ComponentModel.ISupportInitialize)(this.progressBar.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // btnsync
            // 
            this.btnsync.Location = new System.Drawing.Point(21, 12);
            this.btnsync.Name = "btnsync";
            this.btnsync.Size = new System.Drawing.Size(138, 29);
            this.btnsync.TabIndex = 0;
            this.btnsync.Text = "Start Morning Sync";
            this.btnsync.Click += new System.EventHandler(this.btnsync_Click);
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(21, 57);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(246, 22);
            this.progressBar.TabIndex = 1;
            // 
            // lblStatus
            // 
            this.lblStatus.Location = new System.Drawing.Point(165, 25);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(75, 16);
            this.lblStatus.TabIndex = 2;
            this.lblStatus.Text = "labelControl1";
            // 
            // progressSpinner
            // 
            this.progressSpinner.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.progressSpinner.Appearance.Options.UseBackColor = true;
            this.progressSpinner.Caption = "Please Wait..";
            this.progressSpinner.Location = new System.Drawing.Point(21, 85);
            this.progressSpinner.Name = "progressSpinner";
            this.progressSpinner.Size = new System.Drawing.Size(246, 66);
            this.progressSpinner.TabIndex = 3;
            this.progressSpinner.Text = "progressPanel1";
            this.progressSpinner.Visible = false;
            // 
            // POSSyncAll
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(291, 175);
            this.Controls.Add(this.progressSpinner);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.btnsync);
            this.Name = "POSSyncAll";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "POSSyncAll";
            ((System.ComponentModel.ISupportInitialize)(this.progressBar.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnsync;
        private DevExpress.XtraEditors.ProgressBarControl progressBar;
        private DevExpress.XtraEditors.LabelControl lblStatus;
        private DevExpress.XtraWaitForm.ProgressPanel progressSpinner;
    }
}