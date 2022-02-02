namespace SalesInventorySystem.Branches
{
    partial class InventoryBranchQtyAdj
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
            this.panel2 = new System.Windows.Forms.Panel();
            this.raddeduct = new System.Windows.Forms.RadioButton();
            this.radadd = new System.Windows.Forms.RadioButton();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.txtqtyadj = new DevExpress.XtraEditors.TextEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.txtprodcode = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.txtdesc = new DevExpress.XtraEditors.TextEdit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtqtyadj.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtprodcode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtdesc.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.raddeduct);
            this.panel2.Controls.Add(this.radadd);
            this.panel2.Location = new System.Drawing.Point(140, 13);
            this.panel2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(234, 32);
            this.panel2.TabIndex = 92;
            // 
            // raddeduct
            // 
            this.raddeduct.AutoSize = true;
            this.raddeduct.Font = new System.Drawing.Font("Tahoma", 9.25F);
            this.raddeduct.Location = new System.Drawing.Point(69, 4);
            this.raddeduct.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.raddeduct.Name = "raddeduct";
            this.raddeduct.Size = new System.Drawing.Size(92, 23);
            this.raddeduct.TabIndex = 83;
            this.raddeduct.TabStop = true;
            this.raddeduct.Text = "DEDUCT";
            this.raddeduct.UseVisualStyleBackColor = true;
            // 
            // radadd
            // 
            this.radadd.AutoSize = true;
            this.radadd.Font = new System.Drawing.Font("Tahoma", 9.25F);
            this.radadd.Location = new System.Drawing.Point(2, 4);
            this.radadd.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.radadd.Name = "radadd";
            this.radadd.Size = new System.Drawing.Size(63, 23);
            this.radadd.TabIndex = 82;
            this.radadd.Text = "ADD";
            this.radadd.UseVisualStyleBackColor = true;
            // 
            // labelControl7
            // 
            this.labelControl7.Appearance.Font = new System.Drawing.Font("Tahoma", 10F);
            this.labelControl7.Appearance.Options.UseFont = true;
            this.labelControl7.Location = new System.Drawing.Point(17, 56);
            this.labelControl7.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(62, 21);
            this.labelControl7.TabIndex = 104;
            this.labelControl7.Text = "Product:";
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Tahoma", 10F);
            this.button2.Location = new System.Drawing.Point(236, 155);
            this.button2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(87, 36);
            this.button2.TabIndex = 103;
            this.button2.Text = "Cancel";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Tahoma", 10F);
            this.button1.Location = new System.Drawing.Point(142, 155);
            this.button1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(87, 36);
            this.button1.TabIndex = 102;
            this.button1.Text = "Save";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtqtyadj
            // 
            this.txtqtyadj.EditValue = "0";
            this.txtqtyadj.Location = new System.Drawing.Point(140, 119);
            this.txtqtyadj.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtqtyadj.Name = "txtqtyadj";
            this.txtqtyadj.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtqtyadj.Properties.Appearance.Options.UseFont = true;
            this.txtqtyadj.Size = new System.Drawing.Size(126, 28);
            this.txtqtyadj.TabIndex = 100;
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Tahoma", 10F);
            this.labelControl3.Appearance.Options.UseFont = true;
            this.labelControl3.Location = new System.Drawing.Point(15, 122);
            this.labelControl3.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(123, 21);
            this.labelControl3.TabIndex = 96;
            this.labelControl3.Text = "Qty Adjustment:";
            // 
            // txtprodcode
            // 
            this.txtprodcode.Location = new System.Drawing.Point(140, 54);
            this.txtprodcode.Name = "txtprodcode";
            this.txtprodcode.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.8F);
            this.txtprodcode.Properties.Appearance.Options.UseFont = true;
            this.txtprodcode.Size = new System.Drawing.Size(125, 26);
            this.txtprodcode.TabIndex = 105;
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 10F);
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Location = new System.Drawing.Point(17, 88);
            this.labelControl1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(90, 21);
            this.labelControl1.TabIndex = 106;
            this.labelControl1.Text = "Description:";
            // 
            // txtdesc
            // 
            this.txtdesc.Location = new System.Drawing.Point(140, 86);
            this.txtdesc.Name = "txtdesc";
            this.txtdesc.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.8F);
            this.txtdesc.Properties.Appearance.Options.UseFont = true;
            this.txtdesc.Size = new System.Drawing.Size(234, 26);
            this.txtdesc.TabIndex = 107;
            // 
            // InventoryBranchQtyAdj
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(398, 208);
            this.Controls.Add(this.txtdesc);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.txtprodcode);
            this.Controls.Add(this.labelControl7);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txtqtyadj);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.panel2);
            this.Name = "InventoryBranchQtyAdj";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "InventoryBranchQtyAdj";
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtqtyadj.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtprodcode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtdesc.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.RadioButton raddeduct;
        private System.Windows.Forms.RadioButton radadd;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        public DevExpress.XtraEditors.TextEdit txtqtyadj;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        public DevExpress.XtraEditors.TextEdit txtprodcode;
        public DevExpress.XtraEditors.TextEdit txtdesc;
    }
}