namespace SalesInventorySystem.HOFormsDevEx
{
    partial class AddNonTradeOrdersDevEx
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
            this.components = new System.ComponentModel.Container();
            this.searchLookUpEdit1 = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.gridview = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.txtbranch = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txtmetrics = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.btnsave = new DevExpress.XtraEditors.SimpleButton();
            this.txtcost = new DevExpress.XtraEditors.TextEdit();
            this.label3 = new System.Windows.Forms.Label();
            this.txtdate = new DevExpress.XtraEditors.DateEdit();
            this.label2 = new System.Windows.Forms.Label();
            this.txtqty = new DevExpress.XtraEditors.TextEdit();
            this.label7 = new System.Windows.Forms.Label();
            this.txtremarks = new System.Windows.Forms.RichTextBox();
            this.addbtn = new DevExpress.XtraEditors.SimpleButton();
            this.txtshipmentno = new DevExpress.XtraEditors.TextEdit();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.txtproductcategory = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.txtproduct = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.label9 = new System.Windows.Forms.Label();
            this.txtinvoiceno = new DevExpress.XtraEditors.TextEdit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridview)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtcost.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtdate.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtdate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtqty.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtshipmentno.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtproduct.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtinvoiceno.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // searchLookUpEdit1
            // 
            this.searchLookUpEdit1.Location = new System.Drawing.Point(247, 107);
            this.searchLookUpEdit1.Margin = new System.Windows.Forms.Padding(7, 7, 7, 7);
            this.searchLookUpEdit1.Name = "searchLookUpEdit1";
            this.searchLookUpEdit1.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10.25F);
            this.searchLookUpEdit1.Properties.Appearance.Options.UseFont = true;
            this.searchLookUpEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.searchLookUpEdit1.Properties.DisplayMember = "SupplierName";
            this.searchLookUpEdit1.Properties.NullText = "";
            this.searchLookUpEdit1.Properties.ValueMember = "SupplierName";
            this.searchLookUpEdit1.Properties.View = this.gridview;
            this.searchLookUpEdit1.Size = new System.Drawing.Size(531, 44);
            this.searchLookUpEdit1.TabIndex = 69;
            // 
            // gridview
            // 
            this.gridview.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridview.Name = "gridview";
            this.gridview.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridview.OptionsView.ShowGroupPanel = false;
            // 
            // txtbranch
            // 
            this.txtbranch.Font = new System.Drawing.Font("Tahoma", 10.25F);
            this.txtbranch.FormattingEnabled = true;
            this.txtbranch.Location = new System.Drawing.Point(1389, 103);
            this.txtbranch.Margin = new System.Windows.Forms.Padding(7, 7, 7, 7);
            this.txtbranch.Name = "txtbranch";
            this.txtbranch.Size = new System.Drawing.Size(258, 45);
            this.txtbranch.TabIndex = 68;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Tahoma", 10.25F);
            this.label12.Location = new System.Drawing.Point(1255, 114);
            this.label12.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(121, 37);
            this.label12.TabIndex = 67;
            this.label12.Text = "Branch:";
            // 
            // txtmetrics
            // 
            this.txtmetrics.Font = new System.Drawing.Font("Tahoma", 10.25F);
            this.txtmetrics.FormattingEnabled = true;
            this.txtmetrics.Location = new System.Drawing.Point(1389, 172);
            this.txtmetrics.Margin = new System.Windows.Forms.Padding(7, 7, 7, 7);
            this.txtmetrics.Name = "txtmetrics";
            this.txtmetrics.Size = new System.Drawing.Size(258, 45);
            this.txtmetrics.TabIndex = 64;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Tahoma", 10.25F);
            this.label8.Location = new System.Drawing.Point(1255, 181);
            this.label8.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(96, 37);
            this.label8.TabIndex = 63;
            this.label8.Text = "Units:";
            // 
            // btnsave
            // 
            this.btnsave.Appearance.BackColor = System.Drawing.Color.RoyalBlue;
            this.btnsave.Appearance.ForeColor = System.Drawing.Color.White;
            this.btnsave.Appearance.Options.UseBackColor = true;
            this.btnsave.Appearance.Options.UseForeColor = true;
            this.btnsave.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.btnsave.Location = new System.Drawing.Point(423, 442);
            this.btnsave.Margin = new System.Windows.Forms.Padding(7, 7, 7, 7);
            this.btnsave.Name = "btnsave";
            this.btnsave.Size = new System.Drawing.Size(163, 51);
            this.btnsave.TabIndex = 62;
            this.btnsave.Text = "Save";
            this.btnsave.Click += new System.EventHandler(this.btnsave_Click);
            // 
            // txtcost
            // 
            this.txtcost.Location = new System.Drawing.Point(979, 241);
            this.txtcost.Margin = new System.Windows.Forms.Padding(7, 7, 7, 7);
            this.txtcost.Name = "txtcost";
            this.txtcost.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10.25F);
            this.txtcost.Properties.Appearance.Options.UseFont = true;
            this.txtcost.Size = new System.Drawing.Size(262, 44);
            this.txtcost.TabIndex = 61;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 10.25F);
            this.label3.Location = new System.Drawing.Point(791, 248);
            this.label3.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(88, 37);
            this.label3.TabIndex = 60;
            this.label3.Text = "Cost:";
            // 
            // txtdate
            // 
            this.txtdate.EditValue = null;
            this.txtdate.Location = new System.Drawing.Point(979, 107);
            this.txtdate.Margin = new System.Windows.Forms.Padding(7, 7, 7, 7);
            this.txtdate.Name = "txtdate";
            this.txtdate.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10.25F);
            this.txtdate.Properties.Appearance.Options.UseFont = true;
            this.txtdate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtdate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtdate.Size = new System.Drawing.Size(262, 44);
            this.txtdate.TabIndex = 59;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 10.25F);
            this.label2.Location = new System.Drawing.Point(791, 114);
            this.label2.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(189, 37);
            this.label2.TabIndex = 58;
            this.label2.Text = "Target Date:";
            // 
            // txtqty
            // 
            this.txtqty.Location = new System.Drawing.Point(979, 174);
            this.txtqty.Margin = new System.Windows.Forms.Padding(7, 7, 7, 7);
            this.txtqty.Name = "txtqty";
            this.txtqty.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10.25F);
            this.txtqty.Properties.Appearance.Options.UseFont = true;
            this.txtqty.Size = new System.Drawing.Size(262, 44);
            this.txtqty.TabIndex = 57;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Tahoma", 10.25F);
            this.label7.Location = new System.Drawing.Point(791, 181);
            this.label7.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(142, 37);
            this.label7.TabIndex = 56;
            this.label7.Text = "Quantity:";
            // 
            // txtremarks
            // 
            this.txtremarks.Location = new System.Drawing.Point(247, 312);
            this.txtremarks.Margin = new System.Windows.Forms.Padding(7, 7, 7, 7);
            this.txtremarks.Name = "txtremarks";
            this.txtremarks.Size = new System.Drawing.Size(1399, 111);
            this.txtremarks.TabIndex = 53;
            this.txtremarks.Text = "";
            // 
            // addbtn
            // 
            this.addbtn.Appearance.BackColor = System.Drawing.Color.RoyalBlue;
            this.addbtn.Appearance.ForeColor = System.Drawing.Color.White;
            this.addbtn.Appearance.Options.UseBackColor = true;
            this.addbtn.Appearance.Options.UseForeColor = true;
            this.addbtn.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.addbtn.Location = new System.Drawing.Point(247, 442);
            this.addbtn.Margin = new System.Windows.Forms.Padding(7, 7, 7, 7);
            this.addbtn.Name = "addbtn";
            this.addbtn.Size = new System.Drawing.Size(163, 51);
            this.addbtn.TabIndex = 50;
            this.addbtn.Text = "Add";
            this.addbtn.Click += new System.EventHandler(this.addbtn_Click);
            // 
            // txtshipmentno
            // 
            this.txtshipmentno.Enabled = false;
            this.txtshipmentno.Location = new System.Drawing.Point(247, 40);
            this.txtshipmentno.Margin = new System.Windows.Forms.Padding(7, 7, 7, 7);
            this.txtshipmentno.Name = "txtshipmentno";
            this.txtshipmentno.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10.25F);
            this.txtshipmentno.Properties.Appearance.Options.UseFont = true;
            this.txtshipmentno.Properties.ReadOnly = true;
            this.txtshipmentno.Size = new System.Drawing.Size(228, 44);
            this.txtshipmentno.TabIndex = 48;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Tahoma", 10.25F);
            this.label5.Location = new System.Drawing.Point(26, 344);
            this.label5.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(145, 37);
            this.label5.TabIndex = 47;
            this.label5.Text = "Remarks:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 10.25F);
            this.label4.Location = new System.Drawing.Point(26, 114);
            this.label4.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(137, 37);
            this.label4.TabIndex = 46;
            this.label4.Text = "Supplier:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 10.25F);
            this.label1.Location = new System.Drawing.Point(26, 47);
            this.label1.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(162, 37);
            this.label1.TabIndex = 44;
            this.label1.Text = "Order No.:";
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
            this.dataGridView1.Location = new System.Drawing.Point(2, 506);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(7, 7, 7, 7);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(1859, 870);
            this.dataGridView1.TabIndex = 70;
            this.dataGridView1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.dataGridView1_MouseUp);
            // 
            // txtproductcategory
            // 
            this.txtproductcategory.Font = new System.Drawing.Font("Tahoma", 10.25F);
            this.txtproductcategory.FormattingEnabled = true;
            this.txtproductcategory.Location = new System.Drawing.Point(247, 174);
            this.txtproductcategory.Margin = new System.Windows.Forms.Padding(7, 7, 7, 7);
            this.txtproductcategory.Name = "txtproductcategory";
            this.txtproductcategory.Size = new System.Drawing.Size(526, 45);
            this.txtproductcategory.TabIndex = 74;
            this.txtproductcategory.SelectedIndexChanged += new System.EventHandler(this.txtproductcategory_SelectedIndexChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Tahoma", 10.25F);
            this.label11.Location = new System.Drawing.Point(26, 181);
            this.label11.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(149, 37);
            this.label11.TabIndex = 73;
            this.label11.Text = "Category:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Tahoma", 10.25F);
            this.label6.Location = new System.Drawing.Point(26, 250);
            this.label6.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(179, 37);
            this.label6.TabIndex = 71;
            this.label6.Text = "Description:";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(36, 36);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deleteToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(172, 46);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(171, 42);
            this.deleteToolStripMenuItem.Text = "Delete";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
            // 
            // txtproduct
            // 
            this.txtproduct.Location = new System.Drawing.Point(247, 241);
            this.txtproduct.Margin = new System.Windows.Forms.Padding(7, 7, 7, 7);
            this.txtproduct.Name = "txtproduct";
            this.txtproduct.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10.25F);
            this.txtproduct.Properties.Appearance.Options.UseFont = true;
            this.txtproduct.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtproduct.Properties.DisplayMember = "SupplierName";
            this.txtproduct.Properties.NullText = "";
            this.txtproduct.Properties.ValueMember = "SupplierName";
            this.txtproduct.Properties.View = this.gridView1;
            this.txtproduct.Size = new System.Drawing.Size(531, 44);
            this.txtproduct.TabIndex = 75;
            // 
            // gridView1
            // 
            this.gridView1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Tahoma", 10.25F);
            this.label9.Location = new System.Drawing.Point(1255, 248);
            this.label9.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(183, 37);
            this.label9.TabIndex = 76;
            this.label9.Text = "Invoice No.:";
            // 
            // txtinvoiceno
            // 
            this.txtinvoiceno.Location = new System.Drawing.Point(1450, 241);
            this.txtinvoiceno.Margin = new System.Windows.Forms.Padding(7, 7, 7, 7);
            this.txtinvoiceno.Name = "txtinvoiceno";
            this.txtinvoiceno.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10.25F);
            this.txtinvoiceno.Properties.Appearance.Options.UseFont = true;
            this.txtinvoiceno.Size = new System.Drawing.Size(202, 44);
            this.txtinvoiceno.TabIndex = 77;
            // 
            // AddNonTradeOrdersDevEx
            // 
            this.Appearance.BackColor = System.Drawing.Color.White;
            this.Appearance.Options.UseBackColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 29F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1842, 1379);
            this.Controls.Add(this.txtinvoiceno);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.txtproduct);
            this.Controls.Add(this.txtproductcategory);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.searchLookUpEdit1);
            this.Controls.Add(this.txtbranch);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.txtmetrics);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.btnsave);
            this.Controls.Add(this.txtcost);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtdate);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtqty);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtremarks);
            this.Controls.Add(this.addbtn);
            this.Controls.Add(this.txtshipmentno);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(7, 7, 7, 7);
            this.Name = "AddNonTradeOrdersDevEx";
            this.Text = "AddNonTradeOrdersDevEx";
            this.Load += new System.EventHandler(this.AddNonTradeOrdersDevEx_Load);
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridview)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtcost.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtdate.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtdate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtqty.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtshipmentno.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtproduct.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtinvoiceno.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private DevExpress.XtraEditors.SearchLookUpEdit searchLookUpEdit1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridview;
        private System.Windows.Forms.ComboBox txtbranch;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.ComboBox txtmetrics;
        private System.Windows.Forms.Label label8;
        private DevExpress.XtraEditors.SimpleButton btnsave;
        private DevExpress.XtraEditors.TextEdit txtcost;
        private System.Windows.Forms.Label label3;
        private DevExpress.XtraEditors.DateEdit txtdate;
        private System.Windows.Forms.Label label2;
        private DevExpress.XtraEditors.TextEdit txtqty;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.RichTextBox txtremarks;
        private DevExpress.XtraEditors.SimpleButton addbtn;
        private DevExpress.XtraEditors.TextEdit txtshipmentno;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.ComboBox txtproductcategory;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private DevExpress.XtraEditors.SearchLookUpEdit txtproduct;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private System.Windows.Forms.Label label9;
        private DevExpress.XtraEditors.TextEdit txtinvoiceno;
    }
}