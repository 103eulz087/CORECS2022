namespace SalesInventorySystem.Branches
{
    partial class BranchGenInventoryUpdateCost
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
            this.txtcost = new DevExpress.XtraEditors.SpinEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.txtseqno = new DevExpress.XtraEditors.TextEdit();
            ((System.ComponentModel.ISupportInitialize)(this.txtcost.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtseqno.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // txtcost
            // 
            this.txtcost.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtcost.Location = new System.Drawing.Point(90, 18);
            this.txtcost.Name = "txtcost";
            this.txtcost.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10.8F);
            this.txtcost.Properties.Appearance.Options.UseFont = true;
            this.txtcost.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtcost.Size = new System.Drawing.Size(125, 28);
            this.txtcost.TabIndex = 0;
            this.txtcost.EditValueChanged += new System.EventHandler(this.spinEdit1_EditValueChanged);
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 10.8F);
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Location = new System.Drawing.Point(12, 21);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(72, 22);
            this.labelControl1.TabIndex = 1;
            this.labelControl1.Text = "Set Cost:";
            // 
            // simpleButton1
            // 
            this.simpleButton1.ImageOptions.Image = global::SalesInventorySystem.Properties.Resources.Refresh_16x16;
            this.simpleButton1.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.simpleButton1.Location = new System.Drawing.Point(90, 53);
            this.simpleButton1.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(125, 30);
            this.simpleButton1.TabIndex = 14;
            this.simpleButton1.Text = "Generate";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // txtseqno
            // 
            this.txtseqno.Location = new System.Drawing.Point(23, 104);
            this.txtseqno.Name = "txtseqno";
            this.txtseqno.Size = new System.Drawing.Size(125, 22);
            this.txtseqno.TabIndex = 15;
            this.txtseqno.Visible = false;
            // 
            // BranchGenInventoryUpdateCost
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(240, 104);
            this.Controls.Add(this.txtseqno);
            this.Controls.Add(this.simpleButton1);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.txtcost);
            this.Name = "BranchGenInventoryUpdateCost";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "BranchGenInventoryUpdateCost";
            ((System.ComponentModel.ISupportInitialize)(this.txtcost.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtseqno.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        public DevExpress.XtraEditors.SpinEdit txtcost;
        public DevExpress.XtraEditors.TextEdit txtseqno;
    }
}