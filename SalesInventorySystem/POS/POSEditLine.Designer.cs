namespace SalesInventorySystem.POS
{
    partial class POSEditLine
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
            this.txtprodname = new System.Windows.Forms.TextBox();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.txtuprice = new System.Windows.Forms.TextBox();
            this.txttotal = new System.Windows.Forms.TextBox();
            this.btnsubmit = new DevExpress.XtraEditors.SimpleButton();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.txtqty1 = new System.Windows.Forms.TextBox();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.txtspecialprice = new DevExpress.XtraEditors.SpinEdit();
            this.percentagedisc = new DevExpress.XtraEditors.SpinEdit();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.txtnewtotal = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.txtspecialprice.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.percentagedisc.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 11.75F, System.Drawing.FontStyle.Bold);
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Location = new System.Drawing.Point(31, 65);
            this.labelControl1.Margin = new System.Windows.Forms.Padding(4);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(94, 24);
            this.labelControl1.TabIndex = 19;
            this.labelControl1.Text = "Quantity:";
            // 
            // txtprodname
            // 
            this.txtprodname.Font = new System.Drawing.Font("Tahoma", 10.25F, System.Drawing.FontStyle.Bold);
            this.txtprodname.Location = new System.Drawing.Point(16, 15);
            this.txtprodname.Margin = new System.Windows.Forms.Padding(4);
            this.txtprodname.MaxLength = 90;
            this.txtprodname.Multiline = true;
            this.txtprodname.Name = "txtprodname";
            this.txtprodname.ReadOnly = true;
            this.txtprodname.Size = new System.Drawing.Size(540, 42);
            this.txtprodname.TabIndex = 69;
            this.txtprodname.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Tahoma", 11.75F, System.Drawing.FontStyle.Bold);
            this.labelControl2.Appearance.Options.UseFont = true;
            this.labelControl2.Location = new System.Drawing.Point(17, 107);
            this.labelControl2.Margin = new System.Windows.Forms.Padding(4);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(106, 24);
            this.labelControl2.TabIndex = 70;
            this.labelControl2.Text = "Unit Price:";
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Tahoma", 11.75F, System.Drawing.FontStyle.Bold);
            this.labelControl3.Appearance.Options.UseFont = true;
            this.labelControl3.Location = new System.Drawing.Point(67, 147);
            this.labelControl3.Margin = new System.Windows.Forms.Padding(4);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(57, 24);
            this.labelControl3.TabIndex = 71;
            this.labelControl3.Text = "Total:";
            // 
            // txtuprice
            // 
            this.txtuprice.Font = new System.Drawing.Font("Tahoma", 10.25F);
            this.txtuprice.Location = new System.Drawing.Point(140, 106);
            this.txtuprice.Margin = new System.Windows.Forms.Padding(4);
            this.txtuprice.MaxLength = 6;
            this.txtuprice.Name = "txtuprice";
            this.txtuprice.Size = new System.Drawing.Size(160, 28);
            this.txtuprice.TabIndex = 73;
            this.txtuprice.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtuprice_KeyDown);
            // 
            // txttotal
            // 
            this.txttotal.Font = new System.Drawing.Font("Tahoma", 10.25F);
            this.txttotal.Location = new System.Drawing.Point(140, 145);
            this.txttotal.Margin = new System.Windows.Forms.Padding(4);
            this.txttotal.MaxLength = 6;
            this.txttotal.Name = "txttotal";
            this.txttotal.ReadOnly = true;
            this.txttotal.Size = new System.Drawing.Size(160, 28);
            this.txttotal.TabIndex = 74;
            // 
            // btnsubmit
            // 
            this.btnsubmit.Appearance.BackColor = System.Drawing.Color.RoyalBlue;
            this.btnsubmit.Appearance.Font = new System.Drawing.Font("Tahoma", 12.25F);
            this.btnsubmit.Appearance.ForeColor = System.Drawing.Color.White;
            this.btnsubmit.Appearance.Options.UseBackColor = true;
            this.btnsubmit.Appearance.Options.UseFont = true;
            this.btnsubmit.Appearance.Options.UseForeColor = true;
            this.btnsubmit.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Flat;
            this.btnsubmit.Location = new System.Drawing.Point(16, 271);
            this.btnsubmit.Margin = new System.Windows.Forms.Padding(4);
            this.btnsubmit.Name = "btnsubmit";
            this.btnsubmit.Size = new System.Drawing.Size(284, 44);
            this.btnsubmit.TabIndex = 75;
            this.btnsubmit.Text = "Submit";
            this.btnsubmit.Click += new System.EventHandler(this.btnsubmit_Click);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F);
            this.checkBox1.Location = new System.Drawing.Point(316, 70);
            this.checkBox1.Margin = new System.Windows.Forms.Padding(4);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(205, 24);
            this.checkBox1.TabIndex = 76;
            this.checkBox1.Text = "Enable SpecialPrice (F1)";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // txtqty1
            // 
            this.txtqty1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F);
            this.txtqty1.Location = new System.Drawing.Point(140, 68);
            this.txtqty1.Margin = new System.Windows.Forms.Padding(4);
            this.txtqty1.MaxLength = 8;
            this.txtqty1.Name = "txtqty1";
            this.txtqty1.Size = new System.Drawing.Size(160, 27);
            this.txtqty1.TabIndex = 79;
            this.txtqty1.Text = "0";
            this.txtqty1.TextChanged += new System.EventHandler(this.txtqty1_TextChanged);
            this.txtqty1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtqty1_KeyDown);
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.labelControl4.Appearance.Options.UseFont = true;
            this.labelControl4.Location = new System.Drawing.Point(316, 111);
            this.labelControl4.Margin = new System.Windows.Forms.Padding(4);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(99, 19);
            this.labelControl4.TabIndex = 81;
            this.labelControl4.Text = "Percentage:";
            // 
            // txtspecialprice
            // 
            this.txtspecialprice.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtspecialprice.Location = new System.Drawing.Point(431, 146);
            this.txtspecialprice.Name = "txtspecialprice";
            this.txtspecialprice.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.8F);
            this.txtspecialprice.Properties.Appearance.Options.UseFont = true;
            this.txtspecialprice.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtspecialprice.Size = new System.Drawing.Size(125, 26);
            this.txtspecialprice.TabIndex = 82;
            this.txtspecialprice.EditValueChanged += new System.EventHandler(this.txtspecialprice_EditValueChanged);
            // 
            // percentagedisc
            // 
            this.percentagedisc.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.percentagedisc.Location = new System.Drawing.Point(431, 108);
            this.percentagedisc.Name = "percentagedisc";
            this.percentagedisc.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.8F);
            this.percentagedisc.Properties.Appearance.Options.UseFont = true;
            this.percentagedisc.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.percentagedisc.Properties.MaxLength = 6;
            this.percentagedisc.Size = new System.Drawing.Size(125, 26);
            this.percentagedisc.TabIndex = 83;
            this.percentagedisc.EditValueChanged += new System.EventHandler(this.percentagedisc_EditValueChanged);
            this.percentagedisc.KeyDown += new System.Windows.Forms.KeyEventHandler(this.percentagedisc_KeyDown);
            // 
            // labelControl5
            // 
            this.labelControl5.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.labelControl5.Appearance.Options.UseFont = true;
            this.labelControl5.Location = new System.Drawing.Point(316, 149);
            this.labelControl5.Margin = new System.Windows.Forms.Padding(4);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(108, 19);
            this.labelControl5.TabIndex = 84;
            this.labelControl5.Text = "Disc Amount:";
            // 
            // labelControl6
            // 
            this.labelControl6.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.labelControl6.Appearance.Options.UseFont = true;
            this.labelControl6.Location = new System.Drawing.Point(316, 187);
            this.labelControl6.Margin = new System.Windows.Forms.Padding(4);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(90, 19);
            this.labelControl6.TabIndex = 85;
            this.labelControl6.Text = "New Total:";
            // 
            // txtnewtotal
            // 
            this.txtnewtotal.Font = new System.Drawing.Font("Tahoma", 10.25F);
            this.txtnewtotal.Location = new System.Drawing.Point(431, 179);
            this.txtnewtotal.Margin = new System.Windows.Forms.Padding(4);
            this.txtnewtotal.MaxLength = 6;
            this.txtnewtotal.Name = "txtnewtotal";
            this.txtnewtotal.ReadOnly = true;
            this.txtnewtotal.Size = new System.Drawing.Size(125, 28);
            this.txtnewtotal.TabIndex = 86;
            this.txtnewtotal.Text = "0";
            // 
            // POSEditLine
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(579, 231);
            this.Controls.Add(this.txtnewtotal);
            this.Controls.Add(this.labelControl6);
            this.Controls.Add(this.labelControl5);
            this.Controls.Add(this.percentagedisc);
            this.Controls.Add(this.txtspecialprice);
            this.Controls.Add(this.labelControl4);
            this.Controls.Add(this.txtqty1);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.btnsubmit);
            this.Controls.Add(this.txttotal);
            this.Controls.Add(this.txtuprice);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.txtprodname);
            this.Controls.Add(this.labelControl1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "POSEditLine";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "POSEditLine";
            this.Load += new System.EventHandler(this.POSEditLine_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtspecialprice.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.percentagedisc.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.SimpleButton btnsubmit;
        public System.Windows.Forms.TextBox txtprodname;
        public System.Windows.Forms.TextBox txtuprice;
        public System.Windows.Forms.TextBox txttotal;
        private System.Windows.Forms.CheckBox checkBox1;
        public System.Windows.Forms.TextBox txtqty1;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.SpinEdit txtspecialprice;
        private DevExpress.XtraEditors.SpinEdit percentagedisc;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        public System.Windows.Forms.TextBox txtnewtotal;
    }
}