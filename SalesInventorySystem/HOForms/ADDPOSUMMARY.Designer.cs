namespace SalesInventorySystem.HOForms
{
    partial class ADDPOSUMMARY
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
            this.searchLookUpEdit1 = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.gridview = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.txtshipmentno = new DevExpress.XtraEditors.TextEdit();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtproductcategory = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtproduct = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.isProd = new System.Windows.Forms.RadioButton();
            this.isServ = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.txtservices = new System.Windows.Forms.ComboBox();
            this.txtcost = new DevExpress.XtraEditors.TextEdit();
            this.label3 = new System.Windows.Forms.Label();
            this.txtqty = new DevExpress.XtraEditors.TextEdit();
            this.label7 = new System.Windows.Forms.Label();
            this.txtbranch = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txtdate = new DevExpress.XtraEditors.DateEdit();
            this.label5 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.btnsave = new DevExpress.XtraEditors.SimpleButton();
            this.btncancel = new DevExpress.XtraEditors.SimpleButton();
            this.updatebtn = new DevExpress.XtraEditors.SimpleButton();
            this.addbtn = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridview)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtshipmentno.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtcost.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtqty.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtdate.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtdate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // searchLookUpEdit1
            // 
            this.searchLookUpEdit1.Location = new System.Drawing.Point(184, 74);
            this.searchLookUpEdit1.Margin = new System.Windows.Forms.Padding(5);
            this.searchLookUpEdit1.Name = "searchLookUpEdit1";
            this.searchLookUpEdit1.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10.25F);
            this.searchLookUpEdit1.Properties.Appearance.Options.UseFont = true;
            this.searchLookUpEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.searchLookUpEdit1.Properties.DisplayMember = "SupplierName";
            this.searchLookUpEdit1.Properties.ValueMember = "SupplierName";
            this.searchLookUpEdit1.Properties.View = this.gridview;
            this.searchLookUpEdit1.Size = new System.Drawing.Size(408, 36);
            this.searchLookUpEdit1.TabIndex = 41;
            // 
            // gridview
            // 
            this.gridview.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridview.Name = "gridview";
            this.gridview.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridview.OptionsView.ShowGroupPanel = false;
            // 
            // txtshipmentno
            // 
            this.txtshipmentno.Enabled = false;
            this.txtshipmentno.Location = new System.Drawing.Point(184, 21);
            this.txtshipmentno.Margin = new System.Windows.Forms.Padding(5);
            this.txtshipmentno.Name = "txtshipmentno";
            this.txtshipmentno.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10.25F);
            this.txtshipmentno.Properties.Appearance.Options.UseFont = true;
            this.txtshipmentno.Properties.ReadOnly = true;
            this.txtshipmentno.Size = new System.Drawing.Size(198, 36);
            this.txtshipmentno.TabIndex = 40;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 10.25F);
            this.label4.Location = new System.Drawing.Point(14, 80);
            this.label4.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(105, 29);
            this.label4.TabIndex = 39;
            this.label4.Text = "Supplier:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 10.25F);
            this.label1.Location = new System.Drawing.Point(14, 27);
            this.label1.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(163, 29);
            this.label1.TabIndex = 38;
            this.label1.Text = "Shipment No.:";
            // 
            // txtproductcategory
            // 
            this.txtproductcategory.Font = new System.Drawing.Font("Tahoma", 10.25F);
            this.txtproductcategory.FormattingEnabled = true;
            this.txtproductcategory.Location = new System.Drawing.Point(184, 173);
            this.txtproductcategory.Margin = new System.Windows.Forms.Padding(5);
            this.txtproductcategory.Name = "txtproductcategory";
            this.txtproductcategory.Size = new System.Drawing.Size(406, 37);
            this.txtproductcategory.TabIndex = 49;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Tahoma", 10.25F);
            this.label11.Location = new System.Drawing.Point(14, 179);
            this.label11.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(116, 29);
            this.label11.TabIndex = 48;
            this.label11.Text = "Category:";
            // 
            // txtproduct
            // 
            this.txtproduct.Font = new System.Drawing.Font("Tahoma", 10.25F);
            this.txtproduct.FormattingEnabled = true;
            this.txtproduct.Location = new System.Drawing.Point(184, 228);
            this.txtproduct.Margin = new System.Windows.Forms.Padding(5);
            this.txtproduct.Name = "txtproduct";
            this.txtproduct.Size = new System.Drawing.Size(406, 37);
            this.txtproduct.TabIndex = 47;
            this.txtproduct.SelectedIndexChanged += new System.EventHandler(this.txtproduct_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Tahoma", 10.25F);
            this.label6.Location = new System.Drawing.Point(14, 234);
            this.label6.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(101, 29);
            this.label6.TabIndex = 46;
            this.label6.Text = "Product:";
            // 
            // isProd
            // 
            this.isProd.AutoSize = true;
            this.isProd.Checked = true;
            this.isProd.Location = new System.Drawing.Point(184, 128);
            this.isProd.Name = "isProd";
            this.isProd.Size = new System.Drawing.Size(134, 28);
            this.isProd.TabIndex = 50;
            this.isProd.TabStop = true;
            this.isProd.Text = "isProducts?";
            this.isProd.UseVisualStyleBackColor = true;
            this.isProd.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // isServ
            // 
            this.isServ.AutoSize = true;
            this.isServ.Location = new System.Drawing.Point(361, 128);
            this.isServ.Name = "isServ";
            this.isServ.Size = new System.Drawing.Size(131, 28);
            this.isServ.TabIndex = 51;
            this.isServ.Text = "isServices?";
            this.isServ.UseVisualStyleBackColor = true;
            this.isServ.CheckedChanged += new System.EventHandler(this.isServ_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 10.25F);
            this.label2.Location = new System.Drawing.Point(14, 283);
            this.label2.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(108, 29);
            this.label2.TabIndex = 52;
            this.label2.Text = "Services:";
            // 
            // txtservices
            // 
            this.txtservices.Font = new System.Drawing.Font("Tahoma", 10.25F);
            this.txtservices.FormattingEnabled = true;
            this.txtservices.Location = new System.Drawing.Point(184, 280);
            this.txtservices.Margin = new System.Windows.Forms.Padding(5);
            this.txtservices.Name = "txtservices";
            this.txtservices.Size = new System.Drawing.Size(406, 37);
            this.txtservices.TabIndex = 53;
            this.txtservices.SelectedIndexChanged += new System.EventHandler(this.txtservices_SelectedIndexChanged);
            // 
            // txtcost
            // 
            this.txtcost.Location = new System.Drawing.Point(778, 221);
            this.txtcost.Margin = new System.Windows.Forms.Padding(5);
            this.txtcost.Name = "txtcost";
            this.txtcost.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10.25F);
            this.txtcost.Properties.Appearance.Options.UseFont = true;
            this.txtcost.Size = new System.Drawing.Size(202, 36);
            this.txtcost.TabIndex = 57;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 10.25F);
            this.label3.Location = new System.Drawing.Point(619, 232);
            this.label3.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 29);
            this.label3.TabIndex = 56;
            this.label3.Text = "Cost:";
            // 
            // txtqty
            // 
            this.txtqty.Enabled = false;
            this.txtqty.Location = new System.Drawing.Point(778, 173);
            this.txtqty.Margin = new System.Windows.Forms.Padding(5);
            this.txtqty.Name = "txtqty";
            this.txtqty.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10.25F);
            this.txtqty.Properties.Appearance.Options.UseFont = true;
            this.txtqty.Size = new System.Drawing.Size(202, 36);
            this.txtqty.TabIndex = 55;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Tahoma", 10.25F);
            this.label7.Location = new System.Drawing.Point(619, 177);
            this.label7.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(110, 29);
            this.label7.TabIndex = 54;
            this.label7.Text = "Quantity:";
            // 
            // txtbranch
            // 
            this.txtbranch.Font = new System.Drawing.Font("Tahoma", 10.25F);
            this.txtbranch.FormattingEnabled = true;
            this.txtbranch.Location = new System.Drawing.Point(778, 69);
            this.txtbranch.Margin = new System.Windows.Forms.Padding(5);
            this.txtbranch.Name = "txtbranch";
            this.txtbranch.Size = new System.Drawing.Size(199, 37);
            this.txtbranch.TabIndex = 61;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Tahoma", 10.25F);
            this.label12.Location = new System.Drawing.Point(619, 76);
            this.label12.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(94, 29);
            this.label12.TabIndex = 60;
            this.label12.Text = "Branch:";
            // 
            // txtdate
            // 
            this.txtdate.EditValue = null;
            this.txtdate.Enabled = false;
            this.txtdate.Location = new System.Drawing.Point(778, 24);
            this.txtdate.Margin = new System.Windows.Forms.Padding(5);
            this.txtdate.Name = "txtdate";
            this.txtdate.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10.25F);
            this.txtdate.Properties.Appearance.Options.UseFont = true;
            this.txtdate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtdate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtdate.Size = new System.Drawing.Size(202, 36);
            this.txtdate.TabIndex = 59;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Tahoma", 10.25F);
            this.label5.Location = new System.Drawing.Point(619, 28);
            this.label5.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(149, 29);
            this.label5.TabIndex = 58;
            this.label5.Text = "Target Date:";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView1.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dataGridView1.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(19, 339);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(5);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(1884, 632);
            this.dataGridView1.TabIndex = 62;
            // 
            // btnsave
            // 
            this.btnsave.Appearance.BackColor = System.Drawing.Color.RoyalBlue;
            this.btnsave.Appearance.ForeColor = System.Drawing.Color.White;
            this.btnsave.Appearance.Options.UseBackColor = true;
            this.btnsave.Appearance.Options.UseForeColor = true;
            this.btnsave.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.btnsave.Enabled = false;
            this.btnsave.Location = new System.Drawing.Point(1164, 276);
            this.btnsave.Margin = new System.Windows.Forms.Padding(5);
            this.btnsave.Name = "btnsave";
            this.btnsave.Size = new System.Drawing.Size(125, 41);
            this.btnsave.TabIndex = 67;
            this.btnsave.Text = "Save";
            this.btnsave.Click += new System.EventHandler(this.btnsave_Click);
            // 
            // btncancel
            // 
            this.btncancel.Appearance.BackColor = System.Drawing.Color.RoyalBlue;
            this.btncancel.Appearance.ForeColor = System.Drawing.Color.White;
            this.btncancel.Appearance.Options.UseBackColor = true;
            this.btncancel.Appearance.Options.UseForeColor = true;
            this.btncancel.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.btncancel.Enabled = false;
            this.btncancel.Location = new System.Drawing.Point(1029, 276);
            this.btncancel.Margin = new System.Windows.Forms.Padding(5);
            this.btncancel.Name = "btncancel";
            this.btncancel.Size = new System.Drawing.Size(125, 41);
            this.btncancel.TabIndex = 66;
            this.btncancel.Text = "Cancel";
            // 
            // updatebtn
            // 
            this.updatebtn.Appearance.BackColor = System.Drawing.Color.RoyalBlue;
            this.updatebtn.Appearance.ForeColor = System.Drawing.Color.White;
            this.updatebtn.Appearance.Options.UseBackColor = true;
            this.updatebtn.Appearance.Options.UseForeColor = true;
            this.updatebtn.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.updatebtn.Enabled = false;
            this.updatebtn.Location = new System.Drawing.Point(894, 276);
            this.updatebtn.Margin = new System.Windows.Forms.Padding(5);
            this.updatebtn.Name = "updatebtn";
            this.updatebtn.Size = new System.Drawing.Size(125, 41);
            this.updatebtn.TabIndex = 65;
            this.updatebtn.Text = "Update";
            // 
            // addbtn
            // 
            this.addbtn.Appearance.BackColor = System.Drawing.Color.RoyalBlue;
            this.addbtn.Appearance.ForeColor = System.Drawing.Color.White;
            this.addbtn.Appearance.Options.UseBackColor = true;
            this.addbtn.Appearance.Options.UseForeColor = true;
            this.addbtn.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.addbtn.Enabled = false;
            this.addbtn.Location = new System.Drawing.Point(759, 276);
            this.addbtn.Margin = new System.Windows.Forms.Padding(5);
            this.addbtn.Name = "addbtn";
            this.addbtn.Size = new System.Drawing.Size(125, 41);
            this.addbtn.TabIndex = 64;
            this.addbtn.Text = "Add";
            this.addbtn.Click += new System.EventHandler(this.addbtn_Click);
            // 
            // simpleButton1
            // 
            this.simpleButton1.Appearance.BackColor = System.Drawing.Color.RoyalBlue;
            this.simpleButton1.Appearance.ForeColor = System.Drawing.Color.White;
            this.simpleButton1.Appearance.Options.UseBackColor = true;
            this.simpleButton1.Appearance.Options.UseForeColor = true;
            this.simpleButton1.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.simpleButton1.Location = new System.Drawing.Point(624, 276);
            this.simpleButton1.Margin = new System.Windows.Forms.Padding(5);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(125, 41);
            this.simpleButton1.TabIndex = 63;
            this.simpleButton1.Text = "New";
            // 
            // ADDPOSUMMARY
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1923, 990);
            this.Controls.Add(this.btnsave);
            this.Controls.Add(this.btncancel);
            this.Controls.Add(this.updatebtn);
            this.Controls.Add(this.addbtn);
            this.Controls.Add(this.simpleButton1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.txtbranch);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.txtdate);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtcost);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtqty);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtservices);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.isServ);
            this.Controls.Add(this.isProd);
            this.Controls.Add(this.txtproductcategory);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.txtproduct);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.searchLookUpEdit1);
            this.Controls.Add(this.txtshipmentno);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label1);
            this.Name = "ADDPOSUMMARY";
            this.Text = "ADDPOSUMMARY";
            this.Load += new System.EventHandler(this.ADDPOSUMMARY_Load);
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridview)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtshipmentno.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtcost.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtqty.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtdate.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtdate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.SearchLookUpEdit searchLookUpEdit1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridview;
        private DevExpress.XtraEditors.TextEdit txtshipmentno;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox txtproductcategory;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox txtproduct;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.RadioButton isProd;
        private System.Windows.Forms.RadioButton isServ;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox txtservices;
        private DevExpress.XtraEditors.TextEdit txtcost;
        private System.Windows.Forms.Label label3;
        private DevExpress.XtraEditors.TextEdit txtqty;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox txtbranch;
        private System.Windows.Forms.Label label12;
        private DevExpress.XtraEditors.DateEdit txtdate;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridView dataGridView1;
        private DevExpress.XtraEditors.SimpleButton btnsave;
        private DevExpress.XtraEditors.SimpleButton btncancel;
        private DevExpress.XtraEditors.SimpleButton updatebtn;
        private DevExpress.XtraEditors.SimpleButton addbtn;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
    }
}