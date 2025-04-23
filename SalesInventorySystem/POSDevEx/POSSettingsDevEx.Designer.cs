namespace SalesInventorySystem.POSDevEx
{
    partial class POSSettingsDevEx
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(POSSettingsDevEx));
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.txtposname = new DevExpress.XtraEditors.TextEdit();
            this.labelControl9 = new DevExpress.XtraEditors.LabelControl();
            this.txtbrcode = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.searchLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.txtserialno = new DevExpress.XtraEditors.TextEdit();
            this.txtbirpermitno = new DevExpress.XtraEditors.TextEdit();
            this.txtminno = new DevExpress.XtraEditors.TextEdit();
            this.txttinno = new DevExpress.XtraEditors.TextEdit();
            this.labelControl8 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.txtaddress2 = new DevExpress.XtraEditors.TextEdit();
            this.txtaddress1 = new DevExpress.XtraEditors.TextEdit();
            this.btncancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnupdate = new DevExpress.XtraEditors.SimpleButton();
            this.btnadd = new DevExpress.XtraEditors.SimpleButton();
            this.btnnew = new DevExpress.XtraEditors.SimpleButton();
            this.txtcompname = new DevExpress.XtraEditors.TextEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.editItemsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtposname.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtbrcode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtserialno.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtbirpermitno.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtminno.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txttinno.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtaddress2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtaddress1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtcompname.Properties)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupControl2
            // 
            this.groupControl2.Controls.Add(this.gridControl1);
            this.groupControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl2.Location = new System.Drawing.Point(0, 298);
            this.groupControl2.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(2144, 575);
            this.groupControl2.TabIndex = 3;
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.gridControl1.Location = new System.Drawing.Point(3, 39);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(2138, 533);
            this.gridControl1.TabIndex = 1;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            this.gridControl1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.gridControl1_MouseUp);
            // 
            // gridView1
            // 
            this.gridView1.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridView1.Appearance.HeaderPanel.Options.UseFont = true;
            this.gridView1.Appearance.Row.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridView1.Appearance.Row.Options.UseFont = true;
            this.gridView1.DetailHeight = 620;
            this.gridView1.FixedLineWidth = 3;
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsBehavior.ReadOnly = true;
            this.gridView1.OptionsSelection.ShowCheckBoxSelectorInColumnHeader = DevExpress.Utils.DefaultBoolean.True;
            this.gridView1.OptionsView.ColumnAutoWidth = false;
            this.gridView1.OptionsView.RowAutoHeight = true;
            this.gridView1.OptionsView.ShowFooter = true;
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.txtposname);
            this.groupControl1.Controls.Add(this.labelControl9);
            this.groupControl1.Controls.Add(this.txtbrcode);
            this.groupControl1.Controls.Add(this.txtserialno);
            this.groupControl1.Controls.Add(this.txtbirpermitno);
            this.groupControl1.Controls.Add(this.txtminno);
            this.groupControl1.Controls.Add(this.txttinno);
            this.groupControl1.Controls.Add(this.labelControl8);
            this.groupControl1.Controls.Add(this.labelControl7);
            this.groupControl1.Controls.Add(this.labelControl6);
            this.groupControl1.Controls.Add(this.labelControl5);
            this.groupControl1.Controls.Add(this.labelControl4);
            this.groupControl1.Controls.Add(this.labelControl3);
            this.groupControl1.Controls.Add(this.txtaddress2);
            this.groupControl1.Controls.Add(this.txtaddress1);
            this.groupControl1.Controls.Add(this.btncancel);
            this.groupControl1.Controls.Add(this.btnupdate);
            this.groupControl1.Controls.Add(this.btnadd);
            this.groupControl1.Controls.Add(this.btnnew);
            this.groupControl1.Controls.Add(this.txtcompname);
            this.groupControl1.Controls.Add(this.labelControl2);
            this.groupControl1.Controls.Add(this.labelControl1);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(2144, 298);
            this.groupControl1.TabIndex = 2;
            // 
            // txtposname
            // 
            this.txtposname.Location = new System.Drawing.Point(133, 98);
            this.txtposname.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.txtposname.Name = "txtposname";
            this.txtposname.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.25F);
            this.txtposname.Properties.Appearance.Options.UseFont = true;
            this.txtposname.Size = new System.Drawing.Size(369, 40);
            this.txtposname.TabIndex = 76;
            // 
            // labelControl9
            // 
            this.labelControl9.Appearance.Font = new System.Drawing.Font("Tahoma", 9.25F);
            this.labelControl9.Appearance.Options.UseFont = true;
            this.labelControl9.Location = new System.Drawing.Point(20, 102);
            this.labelControl9.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.labelControl9.Name = "labelControl9";
            this.labelControl9.Size = new System.Drawing.Size(112, 27);
            this.labelControl9.TabIndex = 75;
            this.labelControl9.Text = "POS Name:";
            // 
            // txtbrcode
            // 
            this.txtbrcode.Location = new System.Drawing.Point(134, 52);
            this.txtbrcode.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.txtbrcode.Name = "txtbrcode";
            this.txtbrcode.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.25F);
            this.txtbrcode.Properties.Appearance.Options.UseFont = true;
            this.txtbrcode.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtbrcode.Properties.NullText = "";
            this.txtbrcode.Properties.PopupView = this.searchLookUpEdit1View;
            this.txtbrcode.Size = new System.Drawing.Size(369, 40);
            this.txtbrcode.TabIndex = 74;
            // 
            // searchLookUpEdit1View
            // 
            this.searchLookUpEdit1View.DetailHeight = 503;
            this.searchLookUpEdit1View.FixedLineWidth = 3;
            this.searchLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.searchLookUpEdit1View.Name = "searchLookUpEdit1View";
            this.searchLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.searchLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            // 
            // txtserialno
            // 
            this.txtserialno.Location = new System.Drawing.Point(690, 236);
            this.txtserialno.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.txtserialno.Name = "txtserialno";
            this.txtserialno.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.25F);
            this.txtserialno.Properties.Appearance.Options.UseFont = true;
            this.txtserialno.Size = new System.Drawing.Size(369, 40);
            this.txtserialno.TabIndex = 73;
            // 
            // txtbirpermitno
            // 
            this.txtbirpermitno.Location = new System.Drawing.Point(690, 190);
            this.txtbirpermitno.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.txtbirpermitno.Name = "txtbirpermitno";
            this.txtbirpermitno.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.25F);
            this.txtbirpermitno.Properties.Appearance.Options.UseFont = true;
            this.txtbirpermitno.Size = new System.Drawing.Size(369, 40);
            this.txtbirpermitno.TabIndex = 72;
            // 
            // txtminno
            // 
            this.txtminno.Location = new System.Drawing.Point(690, 144);
            this.txtminno.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.txtminno.Name = "txtminno";
            this.txtminno.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.25F);
            this.txtminno.Properties.Appearance.Options.UseFont = true;
            this.txtminno.Size = new System.Drawing.Size(369, 40);
            this.txtminno.TabIndex = 71;
            // 
            // txttinno
            // 
            this.txttinno.Location = new System.Drawing.Point(690, 98);
            this.txttinno.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.txttinno.Name = "txttinno";
            this.txttinno.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.25F);
            this.txttinno.Properties.Appearance.Options.UseFont = true;
            this.txttinno.Size = new System.Drawing.Size(369, 40);
            this.txttinno.TabIndex = 70;
            // 
            // labelControl8
            // 
            this.labelControl8.Appearance.Font = new System.Drawing.Font("Tahoma", 9.25F);
            this.labelControl8.Appearance.Options.UseFont = true;
            this.labelControl8.Location = new System.Drawing.Point(531, 240);
            this.labelControl8.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.labelControl8.Name = "labelControl8";
            this.labelControl8.Size = new System.Drawing.Size(103, 27);
            this.labelControl8.TabIndex = 69;
            this.labelControl8.Text = "Serial No.:";
            // 
            // labelControl7
            // 
            this.labelControl7.Appearance.Font = new System.Drawing.Font("Tahoma", 9.25F);
            this.labelControl7.Appearance.Options.UseFont = true;
            this.labelControl7.Location = new System.Drawing.Point(531, 194);
            this.labelControl7.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(135, 27);
            this.labelControl7.TabIndex = 68;
            this.labelControl7.Text = "BIR Permit #:";
            // 
            // labelControl6
            // 
            this.labelControl6.Appearance.Font = new System.Drawing.Font("Tahoma", 9.25F);
            this.labelControl6.Appearance.Options.UseFont = true;
            this.labelControl6.Location = new System.Drawing.Point(20, 194);
            this.labelControl6.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(104, 27);
            this.labelControl6.TabIndex = 67;
            this.labelControl6.Text = "Address 2:";
            // 
            // labelControl5
            // 
            this.labelControl5.Appearance.Font = new System.Drawing.Font("Tahoma", 9.25F);
            this.labelControl5.Appearance.Options.UseFont = true;
            this.labelControl5.Location = new System.Drawing.Point(21, 148);
            this.labelControl5.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(104, 27);
            this.labelControl5.TabIndex = 66;
            this.labelControl5.Text = "Address 1:";
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.Font = new System.Drawing.Font("Tahoma", 9.25F);
            this.labelControl4.Appearance.Options.UseFont = true;
            this.labelControl4.Location = new System.Drawing.Point(531, 148);
            this.labelControl4.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(89, 27);
            this.labelControl4.TabIndex = 65;
            this.labelControl4.Text = "MIN No.:";
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Tahoma", 9.25F);
            this.labelControl3.Appearance.Options.UseFont = true;
            this.labelControl3.Location = new System.Drawing.Point(531, 102);
            this.labelControl3.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(79, 27);
            this.labelControl3.TabIndex = 64;
            this.labelControl3.Text = "Tin No.:";
            // 
            // txtaddress2
            // 
            this.txtaddress2.Location = new System.Drawing.Point(134, 190);
            this.txtaddress2.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.txtaddress2.Name = "txtaddress2";
            this.txtaddress2.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.25F);
            this.txtaddress2.Properties.Appearance.Options.UseFont = true;
            this.txtaddress2.Size = new System.Drawing.Size(369, 40);
            this.txtaddress2.TabIndex = 63;
            // 
            // txtaddress1
            // 
            this.txtaddress1.Location = new System.Drawing.Point(134, 144);
            this.txtaddress1.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.txtaddress1.Name = "txtaddress1";
            this.txtaddress1.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.25F);
            this.txtaddress1.Properties.Appearance.Options.UseFont = true;
            this.txtaddress1.Size = new System.Drawing.Size(369, 40);
            this.txtaddress1.TabIndex = 62;
            // 
            // btncancel
            // 
            this.btncancel.Location = new System.Drawing.Point(404, 236);
            this.btncancel.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btncancel.Name = "btncancel";
            this.btncancel.Size = new System.Drawing.Size(100, 46);
            this.btncancel.TabIndex = 61;
            this.btncancel.Text = "Cancel";
            this.btncancel.Click += new System.EventHandler(this.btncancel_Click);
            // 
            // btnupdate
            // 
            this.btnupdate.Location = new System.Drawing.Point(294, 236);
            this.btnupdate.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnupdate.Name = "btnupdate";
            this.btnupdate.Size = new System.Drawing.Size(100, 46);
            this.btnupdate.TabIndex = 60;
            this.btnupdate.Text = "Update";
            this.btnupdate.Click += new System.EventHandler(this.btnupdate_Click);
            // 
            // btnadd
            // 
            this.btnadd.Location = new System.Drawing.Point(184, 236);
            this.btnadd.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnadd.Name = "btnadd";
            this.btnadd.Size = new System.Drawing.Size(100, 46);
            this.btnadd.TabIndex = 59;
            this.btnadd.Text = "Add";
            this.btnadd.Click += new System.EventHandler(this.btnadd_Click);
            // 
            // btnnew
            // 
            this.btnnew.Location = new System.Drawing.Point(74, 236);
            this.btnnew.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnnew.Name = "btnnew";
            this.btnnew.Size = new System.Drawing.Size(100, 46);
            this.btnnew.TabIndex = 58;
            this.btnnew.Text = "New";
            this.btnnew.Click += new System.EventHandler(this.btnnew_Click);
            // 
            // txtcompname
            // 
            this.txtcompname.Location = new System.Drawing.Point(690, 52);
            this.txtcompname.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.txtcompname.Name = "txtcompname";
            this.txtcompname.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.25F);
            this.txtcompname.Properties.Appearance.Options.UseFont = true;
            this.txtcompname.Size = new System.Drawing.Size(369, 40);
            this.txtcompname.TabIndex = 3;
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Tahoma", 9.25F);
            this.labelControl2.Appearance.Options.UseFont = true;
            this.labelControl2.Location = new System.Drawing.Point(531, 56);
            this.labelControl2.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(162, 27);
            this.labelControl2.TabIndex = 1;
            this.labelControl2.Text = "Company Name:";
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 9.25F);
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Location = new System.Drawing.Point(20, 56);
            this.labelControl1.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(75, 27);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "Branch:";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editItemsToolStripMenuItem,
            this.deleteToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(179, 72);
            // 
            // editItemsToolStripMenuItem
            // 
            this.editItemsToolStripMenuItem.Name = "editItemsToolStripMenuItem";
            this.editItemsToolStripMenuItem.Size = new System.Drawing.Size(178, 34);
            this.editItemsToolStripMenuItem.Text = "Edit Items";
            this.editItemsToolStripMenuItem.Click += new System.EventHandler(this.editItemsToolStripMenuItem_Click);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(178, 34);
            this.deleteToolStripMenuItem.Text = "Delete";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
            // 
            // POSSettingsDevEx
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(2144, 873);
            this.Controls.Add(this.groupControl2);
            this.Controls.Add(this.groupControl1);
            this.IconOptions.Icon = ((System.Drawing.Icon)(resources.GetObject("POSSettingsDevEx.IconOptions.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.Name = "POSSettingsDevEx";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "POSSettingsDevEx";
            this.Load += new System.EventHandler(this.POSSettingsDevEx_Load);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtposname.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtbrcode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtserialno.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtbirpermitno.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtminno.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txttinno.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtaddress2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtaddress1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtcompname.Properties)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControl2;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.SimpleButton btncancel;
        private DevExpress.XtraEditors.SimpleButton btnupdate;
        private DevExpress.XtraEditors.SimpleButton btnadd;
        private DevExpress.XtraEditors.SimpleButton btnnew;
        private DevExpress.XtraEditors.TextEdit txtcompname;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.TextEdit txtaddress2;
        private DevExpress.XtraEditors.TextEdit txtaddress1;
        private DevExpress.XtraEditors.LabelControl labelControl8;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        private DevExpress.XtraEditors.TextEdit txtserialno;
        private DevExpress.XtraEditors.TextEdit txtbirpermitno;
        private DevExpress.XtraEditors.TextEdit txtminno;
        private DevExpress.XtraEditors.TextEdit txttinno;
        public DevExpress.XtraEditors.SearchLookUpEdit txtbrcode;
        private DevExpress.XtraGrid.Views.Grid.GridView searchLookUpEdit1View;
        private DevExpress.XtraEditors.TextEdit txtposname;
        private DevExpress.XtraEditors.LabelControl labelControl9;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem editItemsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
    }
}