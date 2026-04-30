namespace SalesInventorySystem.HOForms
{
    partial class SetBatchCodeFrm
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
            this.txtbatchcode = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.btnsubmit = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.txtbatchcode.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // txtbatchcode
            // 
            this.txtbatchcode.Location = new System.Drawing.Point(156, 13);
            this.txtbatchcode.Name = "txtbatchcode";
            this.txtbatchcode.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.25F);
            this.txtbatchcode.Properties.Appearance.Options.UseFont = true;
            this.txtbatchcode.Size = new System.Drawing.Size(113, 24);
            this.txtbatchcode.TabIndex = 38;
            this.txtbatchcode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtbatchcode_KeyDown_1);
            this.txtbatchcode.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtbatchcode_KeyPress_1);
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 9.25F);
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Location = new System.Drawing.Point(8, 16);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(130, 18);
            this.labelControl1.TabIndex = 39;
            this.labelControl1.Text = "Enter BatchCode #:";
            // 
            // btnsubmit
            // 
            this.btnsubmit.Appearance.Font = new System.Drawing.Font("Tahoma", 7.25F);
            this.btnsubmit.Appearance.Options.UseFont = true;
            this.btnsubmit.ImageOptions.Image = global::SalesInventorySystem.Properties.Resources.Add_16x16__2_;
            this.btnsubmit.Location = new System.Drawing.Point(275, 12);
            this.btnsubmit.Name = "btnsubmit";
            this.btnsubmit.Size = new System.Drawing.Size(94, 26);
            this.btnsubmit.TabIndex = 40;
            this.btnsubmit.Text = "Submit";
            this.btnsubmit.Click += new System.EventHandler(this.btnsubmit_Click);
            // 
            // SetBatchCodeFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(388, 55);
            this.Controls.Add(this.btnsubmit);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.txtbatchcode);
            this.ForeColor = System.Drawing.Color.Black;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "SetBatchCodeFrm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SetBatchCodeFrm";
            ((System.ComponentModel.ISupportInitialize)(this.txtbatchcode.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.TextEdit txtbatchcode;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.SimpleButton btnsubmit;
    }
}