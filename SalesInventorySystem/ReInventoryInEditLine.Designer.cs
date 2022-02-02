namespace SalesInventorySystem
{
    partial class ReInventoryInEditLine
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
            this.txtqty1 = new System.Windows.Forms.TextBox();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.txtcost = new System.Windows.Forms.TextBox();
            this.txtprodname = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // txtqty1
            // 
            this.txtqty1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F);
            this.txtqty1.Location = new System.Drawing.Point(190, 63);
            this.txtqty1.Name = "txtqty1";
            this.txtqty1.Size = new System.Drawing.Size(121, 23);
            this.txtqty1.TabIndex = 81;
            this.txtqty1.Text = "0";
            this.txtqty1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtqty1_KeyDown);
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 11.75F, System.Drawing.FontStyle.Bold);
            this.labelControl1.Location = new System.Drawing.Point(108, 64);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(76, 19);
            this.labelControl1.TabIndex = 80;
            this.labelControl1.Text = "Quantity:";
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Tahoma", 11.75F, System.Drawing.FontStyle.Bold);
            this.labelControl2.Location = new System.Drawing.Point(108, 92);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(42, 19);
            this.labelControl2.TabIndex = 82;
            this.labelControl2.Text = "Cost:";
            // 
            // txtcost
            // 
            this.txtcost.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F);
            this.txtcost.Location = new System.Drawing.Point(190, 92);
            this.txtcost.Name = "txtcost";
            this.txtcost.Size = new System.Drawing.Size(121, 23);
            this.txtcost.TabIndex = 83;
            this.txtcost.Text = "0";
            this.txtcost.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtcost_KeyDown);
            // 
            // txtprodname
            // 
            this.txtprodname.Font = new System.Drawing.Font("Tahoma", 10.25F, System.Drawing.FontStyle.Bold);
            this.txtprodname.Location = new System.Drawing.Point(12, 12);
            this.txtprodname.MaxLength = 90;
            this.txtprodname.Multiline = true;
            this.txtprodname.Name = "txtprodname";
            this.txtprodname.ReadOnly = true;
            this.txtprodname.Size = new System.Drawing.Size(406, 35);
            this.txtprodname.TabIndex = 84;
            this.txtprodname.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // ReInventoryInEditLine
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(438, 131);
            this.Controls.Add(this.txtprodname);
            this.Controls.Add(this.txtcost);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.txtqty1);
            this.Controls.Add(this.labelControl1);
            this.Name = "ReInventoryInEditLine";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ReInventoryInEditLine";
            this.Load += new System.EventHandler(this.ReInventoryInEditLine_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.TextBox txtqty1;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        public System.Windows.Forms.TextBox txtcost;
        public System.Windows.Forms.TextBox txtprodname;
    }
}