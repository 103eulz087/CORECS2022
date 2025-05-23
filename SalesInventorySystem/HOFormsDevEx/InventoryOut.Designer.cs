namespace SalesInventorySystem.HOFormsDevEx
{
    partial class InventoryOut
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
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.txtbrcode = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.searchLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.txtproduct = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.txtdatein = new DevExpress.XtraEditors.DateEdit();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.txtbatchid = new DevExpress.XtraEditors.TextEdit();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.txtcategory = new System.Windows.Forms.ComboBox();
            this.labelControl8 = new DevExpress.XtraEditors.LabelControl();
            this.txtremarks = new System.Windows.Forms.RichTextBox();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.btnconfirm = new DevExpress.XtraEditors.SimpleButton();
            this.btnrecover = new DevExpress.XtraEditors.SimpleButton();
            this.txtavailable = new DevExpress.XtraEditors.SpinEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.btnprint = new DevExpress.XtraEditors.SimpleButton();
            this.btnnew = new DevExpress.XtraEditors.SimpleButton();
            this.txtqty = new DevExpress.XtraEditors.SpinEdit();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.btncancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnadd = new DevExpress.XtraEditors.SimpleButton();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.bandedGridView1 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridView();
            this.advBandedGridView1 = new DevExpress.XtraGrid.Views.BandedGrid.AdvBandedGridView();
            this.gridBand1 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.gridView3 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cancelLineToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.txtbrcode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtproduct.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtdatein.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtdatein.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtbatchid.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtavailable.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtqty.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bandedGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.advBandedGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView3)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 9.8F);
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Location = new System.Drawing.Point(15, 86);
            this.labelControl1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(44, 16);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "Branch:";
            // 
            // txtbrcode
            // 
            this.txtbrcode.Location = new System.Drawing.Point(99, 84);
            this.txtbrcode.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtbrcode.Name = "txtbrcode";
            this.txtbrcode.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.8F);
            this.txtbrcode.Properties.Appearance.Options.UseFont = true;
            this.txtbrcode.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtbrcode.Properties.NullText = "";
            this.txtbrcode.Properties.PopupView = this.searchLookUpEdit1View;
            this.txtbrcode.Size = new System.Drawing.Size(107, 22);
            this.txtbrcode.TabIndex = 1;
            // 
            // searchLookUpEdit1View
            // 
            this.searchLookUpEdit1View.DetailHeight = 284;
            this.searchLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.searchLookUpEdit1View.LevelIndent = 0;
            this.searchLookUpEdit1View.Name = "searchLookUpEdit1View";
            this.searchLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.searchLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            this.searchLookUpEdit1View.PreviewIndent = 0;
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Tahoma", 9.8F);
            this.labelControl2.Appearance.Options.UseFont = true;
            this.labelControl2.Location = new System.Drawing.Point(15, 112);
            this.labelControl2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(48, 16);
            this.labelControl2.TabIndex = 2;
            this.labelControl2.Text = "Product:";
            // 
            // txtproduct
            // 
            this.txtproduct.Location = new System.Drawing.Point(99, 110);
            this.txtproduct.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtproduct.Name = "txtproduct";
            this.txtproduct.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.8F);
            this.txtproduct.Properties.Appearance.Options.UseFont = true;
            this.txtproduct.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtproduct.Properties.NullText = "";
            this.txtproduct.Properties.PopupView = this.gridView1;
            this.txtproduct.Size = new System.Drawing.Size(332, 22);
            this.txtproduct.TabIndex = 3;
            this.txtproduct.EditValueChanged += new System.EventHandler(this.txtproduct_EditValueChanged);
            // 
            // gridView1
            // 
            this.gridView1.DetailHeight = 284;
            this.gridView1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridView1.LevelIndent = 0;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            this.gridView1.PreviewIndent = 0;
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.Font = new System.Drawing.Font("Tahoma", 9.8F);
            this.labelControl4.Appearance.Options.UseFont = true;
            this.labelControl4.Location = new System.Drawing.Point(15, 60);
            this.labelControl4.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(55, 16);
            this.labelControl4.TabIndex = 6;
            this.labelControl4.Text = "Date Out:";
            // 
            // txtdatein
            // 
            this.txtdatein.EditValue = null;
            this.txtdatein.Location = new System.Drawing.Point(99, 58);
            this.txtdatein.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtdatein.Name = "txtdatein";
            this.txtdatein.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.8F);
            this.txtdatein.Properties.Appearance.Options.UseFont = true;
            this.txtdatein.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtdatein.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtdatein.Size = new System.Drawing.Size(107, 22);
            this.txtdatein.TabIndex = 9;
            // 
            // labelControl6
            // 
            this.labelControl6.Appearance.Font = new System.Drawing.Font("Tahoma", 9.8F);
            this.labelControl6.Appearance.Options.UseFont = true;
            this.labelControl6.Location = new System.Drawing.Point(15, 34);
            this.labelControl6.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(52, 16);
            this.labelControl6.TabIndex = 10;
            this.labelControl6.Text = "Batch ID:";
            // 
            // txtbatchid
            // 
            this.txtbatchid.Location = new System.Drawing.Point(99, 32);
            this.txtbatchid.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtbatchid.Name = "txtbatchid";
            this.txtbatchid.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.8F);
            this.txtbatchid.Properties.Appearance.Options.UseFont = true;
            this.txtbatchid.Properties.ReadOnly = true;
            this.txtbatchid.Size = new System.Drawing.Size(107, 22);
            this.txtbatchid.TabIndex = 11;
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.txtcategory);
            this.groupControl1.Controls.Add(this.labelControl8);
            this.groupControl1.Controls.Add(this.txtremarks);
            this.groupControl1.Controls.Add(this.labelControl5);
            this.groupControl1.Controls.Add(this.btnconfirm);
            this.groupControl1.Controls.Add(this.btnrecover);
            this.groupControl1.Controls.Add(this.txtavailable);
            this.groupControl1.Controls.Add(this.labelControl3);
            this.groupControl1.Controls.Add(this.btnprint);
            this.groupControl1.Controls.Add(this.btnnew);
            this.groupControl1.Controls.Add(this.txtqty);
            this.groupControl1.Controls.Add(this.labelControl7);
            this.groupControl1.Controls.Add(this.btncancel);
            this.groupControl1.Controls.Add(this.btnadd);
            this.groupControl1.Controls.Add(this.labelControl6);
            this.groupControl1.Controls.Add(this.txtbatchid);
            this.groupControl1.Controls.Add(this.labelControl1);
            this.groupControl1.Controls.Add(this.txtbrcode);
            this.groupControl1.Controls.Add(this.txtdatein);
            this.groupControl1.Controls.Add(this.labelControl2);
            this.groupControl1.Controls.Add(this.txtproduct);
            this.groupControl1.Controls.Add(this.labelControl4);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(1248, 324);
            this.groupControl1.TabIndex = 12;
            // 
            // txtcategory
            // 
            this.txtcategory.Font = new System.Drawing.Font("Tahoma", 11.14286F);
            this.txtcategory.FormattingEnabled = true;
            this.txtcategory.Location = new System.Drawing.Point(99, 138);
            this.txtcategory.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtcategory.Name = "txtcategory";
            this.txtcategory.Size = new System.Drawing.Size(187, 26);
            this.txtcategory.TabIndex = 79;
            // 
            // labelControl8
            // 
            this.labelControl8.Appearance.Font = new System.Drawing.Font("Tahoma", 9.8F);
            this.labelControl8.Appearance.Options.UseFont = true;
            this.labelControl8.Location = new System.Drawing.Point(15, 138);
            this.labelControl8.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.labelControl8.Name = "labelControl8";
            this.labelControl8.Size = new System.Drawing.Size(56, 16);
            this.labelControl8.TabIndex = 78;
            this.labelControl8.Text = "Category:";
            // 
            // txtremarks
            // 
            this.txtremarks.Location = new System.Drawing.Point(99, 218);
            this.txtremarks.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtremarks.Name = "txtremarks";
            this.txtremarks.Size = new System.Drawing.Size(334, 56);
            this.txtremarks.TabIndex = 77;
            this.txtremarks.Text = "";
            // 
            // labelControl5
            // 
            this.labelControl5.Appearance.Font = new System.Drawing.Font("Tahoma", 9.8F);
            this.labelControl5.Appearance.Options.UseFont = true;
            this.labelControl5.Location = new System.Drawing.Point(15, 240);
            this.labelControl5.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(55, 16);
            this.labelControl5.TabIndex = 76;
            this.labelControl5.Text = "Remarks:";
            // 
            // btnconfirm
            // 
            this.btnconfirm.Location = new System.Drawing.Point(248, 288);
            this.btnconfirm.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnconfirm.Name = "btnconfirm";
            this.btnconfirm.Size = new System.Drawing.Size(93, 26);
            this.btnconfirm.TabIndex = 75;
            this.btnconfirm.Text = "Confirm";
            this.btnconfirm.Click += new System.EventHandler(this.simpleButton2_Click);
            // 
            // btnrecover
            // 
            this.btnrecover.Location = new System.Drawing.Point(442, 288);
            this.btnrecover.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnrecover.Name = "btnrecover";
            this.btnrecover.Size = new System.Drawing.Size(93, 26);
            this.btnrecover.TabIndex = 74;
            this.btnrecover.Text = "Recover";
            this.btnrecover.Click += new System.EventHandler(this.simpleButton1_Click_1);
            // 
            // txtavailable
            // 
            this.txtavailable.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtavailable.Location = new System.Drawing.Point(99, 165);
            this.txtavailable.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtavailable.Name = "txtavailable";
            this.txtavailable.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.8F);
            this.txtavailable.Properties.Appearance.Options.UseFont = true;
            this.txtavailable.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtavailable.Properties.ReadOnly = true;
            this.txtavailable.Size = new System.Drawing.Size(107, 22);
            this.txtavailable.TabIndex = 73;
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Tahoma", 9.8F);
            this.labelControl3.Appearance.Options.UseFont = true;
            this.labelControl3.Location = new System.Drawing.Point(15, 167);
            this.labelControl3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(56, 16);
            this.labelControl3.TabIndex = 72;
            this.labelControl3.Text = "Available:";
            // 
            // btnprint
            // 
            this.btnprint.Location = new System.Drawing.Point(723, 178);
            this.btnprint.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnprint.Name = "btnprint";
            this.btnprint.Size = new System.Drawing.Size(93, 26);
            this.btnprint.TabIndex = 71;
            this.btnprint.Text = "Print";
            this.btnprint.Visible = false;
            this.btnprint.Click += new System.EventHandler(this.btnprint_Click);
            // 
            // btnnew
            // 
            this.btnnew.ImageOptions.Image = global::SalesInventorySystem.Properties.Resources.New_16x16__5_;
            this.btnnew.Location = new System.Drawing.Point(99, 288);
            this.btnnew.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnnew.Name = "btnnew";
            this.btnnew.Size = new System.Drawing.Size(60, 26);
            this.btnnew.TabIndex = 69;
            this.btnnew.Text = "New";
            this.btnnew.Click += new System.EventHandler(this.btnnew_Click);
            // 
            // txtqty
            // 
            this.txtqty.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtqty.Location = new System.Drawing.Point(99, 191);
            this.txtqty.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtqty.Name = "txtqty";
            this.txtqty.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.8F);
            this.txtqty.Properties.Appearance.Options.UseFont = true;
            this.txtqty.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtqty.Size = new System.Drawing.Size(107, 22);
            this.txtqty.TabIndex = 67;
            // 
            // labelControl7
            // 
            this.labelControl7.Appearance.Font = new System.Drawing.Font("Tahoma", 9.8F);
            this.labelControl7.Appearance.Options.UseFont = true;
            this.labelControl7.Location = new System.Drawing.Point(15, 195);
            this.labelControl7.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(52, 16);
            this.labelControl7.TabIndex = 66;
            this.labelControl7.Text = "Quantity:";
            this.labelControl7.Click += new System.EventHandler(this.labelControl7_Click);
            // 
            // btncancel
            // 
            this.btncancel.ImageOptions.Image = global::SalesInventorySystem.Properties.Resources.Cancel_16x16__2_;
            this.btncancel.Location = new System.Drawing.Point(348, 288);
            this.btncancel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btncancel.Name = "btncancel";
            this.btncancel.Size = new System.Drawing.Size(89, 26);
            this.btncancel.TabIndex = 65;
            this.btncancel.Text = "Close";
            this.btncancel.Click += new System.EventHandler(this.btncancel_Click);
            // 
            // btnadd
            // 
            this.btnadd.ImageOptions.Image = global::SalesInventorySystem.Properties.Resources.Add_16x16__2_;
            this.btnadd.Location = new System.Drawing.Point(164, 288);
            this.btnadd.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnadd.Name = "btnadd";
            this.btnadd.Size = new System.Drawing.Size(78, 26);
            this.btnadd.TabIndex = 63;
            this.btnadd.Text = "Add";
            this.btnadd.Click += new System.EventHandler(this.btnadd_Click);
            // 
            // groupControl2
            // 
            this.groupControl2.Controls.Add(this.gridControl1);
            this.groupControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl2.Location = new System.Drawing.Point(0, 324);
            this.groupControl2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(1248, 231);
            this.groupControl2.TabIndex = 13;
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gridControl1.Location = new System.Drawing.Point(2, 23);
            this.gridControl1.MainView = this.gridView2;
            this.gridControl1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(1244, 206);
            this.gridControl1.TabIndex = 5;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView2,
            this.bandedGridView1,
            this.advBandedGridView1,
            this.gridView3});
            this.gridControl1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.gridControl1_MouseUp);
            // 
            // gridView2
            // 
            this.gridView2.Appearance.GroupRow.BackColor = System.Drawing.Color.ForestGreen;
            this.gridView2.Appearance.GroupRow.ForeColor = System.Drawing.Color.Gold;
            this.gridView2.Appearance.GroupRow.Options.UseBackColor = true;
            this.gridView2.Appearance.GroupRow.Options.UseForeColor = true;
            this.gridView2.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridView2.Appearance.HeaderPanel.Options.UseFont = true;
            this.gridView2.Appearance.Row.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridView2.Appearance.Row.Options.UseFont = true;
            this.gridView2.DetailHeight = 189;
            this.gridView2.FixedLineWidth = 1;
            this.gridView2.GridControl = this.gridControl1;
            this.gridView2.LevelIndent = 0;
            this.gridView2.Name = "gridView2";
            this.gridView2.OptionsBehavior.Editable = false;
            this.gridView2.OptionsBehavior.ReadOnly = true;
            this.gridView2.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
            this.gridView2.OptionsView.ColumnAutoWidth = false;
            this.gridView2.OptionsView.RowAutoHeight = true;
            this.gridView2.OptionsView.ShowFooter = true;
            this.gridView2.PreviewIndent = 0;
            // 
            // bandedGridView1
            // 
            this.bandedGridView1.DetailHeight = 189;
            this.bandedGridView1.FixedLineWidth = 1;
            this.bandedGridView1.GridControl = this.gridControl1;
            this.bandedGridView1.LevelIndent = 0;
            this.bandedGridView1.Name = "bandedGridView1";
            this.bandedGridView1.PreviewIndent = 0;
            // 
            // advBandedGridView1
            // 
            this.advBandedGridView1.Bands.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] {
            this.gridBand1});
            this.advBandedGridView1.DetailHeight = 189;
            this.advBandedGridView1.FixedLineWidth = 1;
            this.advBandedGridView1.GridControl = this.gridControl1;
            this.advBandedGridView1.LevelIndent = 0;
            this.advBandedGridView1.Name = "advBandedGridView1";
            this.advBandedGridView1.PreviewIndent = 0;
            // 
            // gridBand1
            // 
            this.gridBand1.Caption = "gridBand1";
            this.gridBand1.MinWidth = 9;
            this.gridBand1.Name = "gridBand1";
            this.gridBand1.VisibleIndex = 0;
            this.gridBand1.Width = 44;
            // 
            // gridView3
            // 
            this.gridView3.DetailHeight = 189;
            this.gridView3.FixedLineWidth = 1;
            this.gridView3.GridControl = this.gridControl1;
            this.gridView3.LevelIndent = 0;
            this.gridView3.Name = "gridView3";
            this.gridView3.PreviewIndent = 0;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(28, 28);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cancelLineToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(136, 26);
            // 
            // cancelLineToolStripMenuItem
            // 
            this.cancelLineToolStripMenuItem.Name = "cancelLineToolStripMenuItem";
            this.cancelLineToolStripMenuItem.Size = new System.Drawing.Size(135, 22);
            this.cancelLineToolStripMenuItem.Text = "Cancel Line";
            this.cancelLineToolStripMenuItem.Click += new System.EventHandler(this.cancelLineToolStripMenuItem_Click);
            // 
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.ImageScalingSize = new System.Drawing.Size(28, 28);
            this.contextMenuStrip2.Name = "contextMenuStrip2";
            this.contextMenuStrip2.Size = new System.Drawing.Size(61, 4);
            // 
            // InventoryOut
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1248, 555);
            this.Controls.Add(this.groupControl2);
            this.Controls.Add(this.groupControl1);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "InventoryOut";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "InventoryOut";
            this.Load += new System.EventHandler(this.POSInventoryIN_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtbrcode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtproduct.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtdatein.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtdatein.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtbatchid.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtavailable.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtqty.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bandedGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.advBandedGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView3)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.SearchLookUpEdit txtbrcode;
        private DevExpress.XtraGrid.Views.Grid.GridView searchLookUpEdit1View;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.SearchLookUpEdit txtproduct;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.DateEdit txtdatein;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.TextEdit txtbatchid;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private DevExpress.XtraEditors.SimpleButton btncancel;
        private DevExpress.XtraEditors.SimpleButton btnadd;
        private DevExpress.XtraEditors.SpinEdit txtqty;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        public DevExpress.XtraGrid.GridControl gridControl1;
        public DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridView bandedGridView1;
        private DevExpress.XtraGrid.Views.BandedGrid.AdvBandedGridView advBandedGridView1;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView3;
        private DevExpress.XtraEditors.SimpleButton btnnew;
        private DevExpress.XtraEditors.SimpleButton btnprint;
        private DevExpress.XtraEditors.SpinEdit txtavailable;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.SimpleButton btnrecover;
        private DevExpress.XtraEditors.SimpleButton btnconfirm;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        public System.Windows.Forms.RichTextBox txtremarks;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
        private System.Windows.Forms.ToolStripMenuItem cancelLineToolStripMenuItem;
        private System.Windows.Forms.ComboBox txtcategory;
        private DevExpress.XtraEditors.LabelControl labelControl8;
    }
}