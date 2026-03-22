namespace SalesInventorySystem.Orders
{
    partial class AddOrderSTS
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddOrderSTS));
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.txtpname = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.gridView3 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.simpleButton9 = new DevExpress.XtraEditors.SimpleButton();
            this.btncancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnsave = new DevExpress.XtraEditors.SimpleButton();
            this.btnadd = new DevExpress.XtraEditors.SimpleButton();
            this.btnnew = new DevExpress.XtraEditors.SimpleButton();
            this.txtqty = new System.Windows.Forms.TextBox();
            this.ordertype = new System.Windows.Forms.ComboBox();
            this.labelControl11 = new DevExpress.XtraEditors.LabelControl();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.labelControl10 = new DevExpress.XtraEditors.LabelControl();
            this.panel2 = new System.Windows.Forms.Panel();
            this.labelControl9 = new DevExpress.XtraEditors.LabelControl();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.txteffectivedate = new System.Windows.Forms.DateTimePicker();
            this.labelControl8 = new DevExpress.XtraEditors.LabelControl();
            this.txtprodcat = new System.Windows.Forms.ComboBox();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.txtrefno = new DevExpress.XtraEditors.TextEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.textEdit1 = new DevExpress.XtraEditors.TextEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.tabMain = new DevExpress.XtraTab.XtraTabControl();
            this.tabProducts = new DevExpress.XtraTab.XtraTabPage();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.txtpcode = new System.Windows.Forms.TextBox();
            this.txtpcat = new System.Windows.Forms.TextBox();
            this.simpleButton3 = new DevExpress.XtraEditors.SimpleButton();
            this.txtgroup = new System.Windows.Forms.ComboBox();
            this.labelControl12 = new DevExpress.XtraEditors.LabelControl();
            this.radothers = new System.Windows.Forms.RadioButton();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.txtbranch = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.searchLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.radho = new System.Windows.Forms.RadioButton();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtpname.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView3)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtrefno.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabMain)).BeginInit();
            this.tabMain.SuspendLayout();
            this.tabProducts.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtbranch.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.White;
            this.groupBox2.Controls.Add(this.gridControl1);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 236);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox2.Size = new System.Drawing.Size(1281, 407);
            this.groupBox2.TabIndex = 11;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "List of Orders";
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.gridControl1.Location = new System.Drawing.Point(4, 20);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(1273, 383);
            this.gridControl1.TabIndex = 2;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridView1.Appearance.HeaderPanel.Options.UseFont = true;
            this.gridView1.Appearance.Row.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridView1.Appearance.Row.Options.UseFont = true;
            this.gridView1.DetailHeight = 431;
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsBehavior.ReadOnly = true;
            this.gridView1.OptionsView.ColumnAutoWidth = false;
            this.gridView1.OptionsView.RowAutoHeight = true;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            this.gridView1.OptionsView.ShowIndicator = false;
            this.gridView1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.gridView1_KeyDown);
            // 
            // txtpname
            // 
            this.txtpname.Enabled = false;
            this.txtpname.Location = new System.Drawing.Point(186, 296);
            this.txtpname.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtpname.Name = "txtpname";
            this.txtpname.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10.25F);
            this.txtpname.Properties.Appearance.Options.UseFont = true;
            this.txtpname.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtpname.Properties.NullText = "";
            this.txtpname.Properties.PopupView = this.gridView3;
            this.txtpname.Size = new System.Drawing.Size(316, 28);
            this.txtpname.TabIndex = 73;
            this.txtpname.EditValueChanged += new System.EventHandler(this.txtpname_EditValueChanged);
            // 
            // gridView3
            // 
            this.gridView3.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridView3.Name = "gridView3";
            this.gridView3.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView3.OptionsView.ShowGroupPanel = false;
            // 
            // simpleButton9
            // 
            this.simpleButton9.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("simpleButton9.ImageOptions.Image")));
            this.simpleButton9.Location = new System.Drawing.Point(664, 192);
            this.simpleButton9.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.simpleButton9.Name = "simpleButton9";
            this.simpleButton9.Size = new System.Drawing.Size(109, 32);
            this.simpleButton9.TabIndex = 72;
            this.simpleButton9.Text = "Close (Esc)";
            this.simpleButton9.Click += new System.EventHandler(this.simpleButton9_Click);
            // 
            // btncancel
            // 
            this.btncancel.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btncancel.ImageOptions.Image")));
            this.btncancel.Location = new System.Drawing.Point(501, 192);
            this.btncancel.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btncancel.Name = "btncancel";
            this.btncancel.Size = new System.Drawing.Size(155, 32);
            this.btncancel.TabIndex = 71;
            this.btncancel.Text = "Cancel Line (Del)";
            this.btncancel.Click += new System.EventHandler(this.btncancel_Click);
            // 
            // btnsave
            // 
            this.btnsave.ImageOptions.Image = global::SalesInventorySystem.Properties.Resources.Save_16x16__5_;
            this.btnsave.Location = new System.Drawing.Point(396, 192);
            this.btnsave.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnsave.Name = "btnsave";
            this.btnsave.Size = new System.Drawing.Size(97, 32);
            this.btnsave.TabIndex = 70;
            this.btnsave.Text = "Save (F5)";
            this.btnsave.Click += new System.EventHandler(this.btnsave_Click);
            // 
            // btnadd
            // 
            this.btnadd.ImageOptions.Image = global::SalesInventorySystem.Properties.Resources.Add_16x16__2_;
            this.btnadd.Location = new System.Drawing.Point(275, 192);
            this.btnadd.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnadd.Name = "btnadd";
            this.btnadd.Size = new System.Drawing.Size(112, 32);
            this.btnadd.TabIndex = 69;
            this.btnadd.Text = "Add (Enter)";
            this.btnadd.Click += new System.EventHandler(this.btnadd_Click);
            // 
            // btnnew
            // 
            this.btnnew.ImageOptions.Image = global::SalesInventorySystem.Properties.Resources.New_16x16__5_;
            this.btnnew.Location = new System.Drawing.Point(187, 192);
            this.btnnew.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnnew.Name = "btnnew";
            this.btnnew.Size = new System.Drawing.Size(80, 32);
            this.btnnew.TabIndex = 68;
            this.btnnew.Text = "New";
            this.btnnew.Click += new System.EventHandler(this.btnnew_Click);
            // 
            // txtqty
            // 
            this.txtqty.Font = new System.Drawing.Font("Tahoma", 9.25F);
            this.txtqty.Location = new System.Drawing.Point(681, 60);
            this.txtqty.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtqty.Name = "txtqty";
            this.txtqty.Size = new System.Drawing.Size(147, 26);
            this.txtqty.TabIndex = 37;
            this.txtqty.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtqty_KeyDown);
            // 
            // ordertype
            // 
            this.ordertype.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ordertype.FormattingEnabled = true;
            this.ordertype.Items.AddRange(new object[] {
            "MAIN",
            "ADD-ONS"});
            this.ordertype.Location = new System.Drawing.Point(680, 135);
            this.ordertype.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ordertype.Name = "ordertype";
            this.ordertype.Size = new System.Drawing.Size(148, 27);
            this.ordertype.TabIndex = 35;
            // 
            // labelControl11
            // 
            this.labelControl11.Appearance.Font = new System.Drawing.Font("Tahoma", 9.25F, System.Drawing.FontStyle.Bold);
            this.labelControl11.Appearance.Options.UseFont = true;
            this.labelControl11.Location = new System.Drawing.Point(575, 140);
            this.labelControl11.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.labelControl11.Name = "labelControl11";
            this.labelControl11.Size = new System.Drawing.Size(89, 18);
            this.labelControl11.TabIndex = 34;
            this.labelControl11.Text = "Order Type:";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(505, 166);
            this.checkBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(149, 21);
            this.checkBox1.TabIndex = 33;
            this.checkBox1.Text = "Used this Remarks?";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.Visible = false;
            // 
            // comboBox1
            // 
            this.comboBox1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(680, 97);
            this.comboBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(148, 27);
            this.comboBox1.TabIndex = 31;
            // 
            // labelControl10
            // 
            this.labelControl10.Appearance.Font = new System.Drawing.Font("Tahoma", 9.25F, System.Drawing.FontStyle.Bold);
            this.labelControl10.Appearance.Options.UseFont = true;
            this.labelControl10.Location = new System.Drawing.Point(607, 102);
            this.labelControl10.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.labelControl10.Name = "labelControl10";
            this.labelControl10.Size = new System.Drawing.Size(61, 18);
            this.labelControl10.TabIndex = 30;
            this.labelControl10.Text = "Metrics:";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.labelControl9);
            this.panel2.Controls.Add(this.richTextBox1);
            this.panel2.Location = new System.Drawing.Point(16, 132);
            this.panel2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(484, 53);
            this.panel2.TabIndex = 29;
            // 
            // labelControl9
            // 
            this.labelControl9.Appearance.Font = new System.Drawing.Font("Tahoma", 9.25F, System.Drawing.FontStyle.Bold);
            this.labelControl9.Appearance.Options.UseFont = true;
            this.labelControl9.Location = new System.Drawing.Point(5, 14);
            this.labelControl9.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.labelControl9.Name = "labelControl9";
            this.labelControl9.Size = new System.Drawing.Size(118, 18);
            this.labelControl9.TabIndex = 27;
            this.labelControl9.Text = "Items Remarks:";
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(168, 5);
            this.richTextBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(303, 42);
            this.richTextBox1.TabIndex = 28;
            this.richTextBox1.Text = "";
            // 
            // txteffectivedate
            // 
            this.txteffectivedate.Font = new System.Drawing.Font("Tahoma", 10.25F);
            this.txteffectivedate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.txteffectivedate.Location = new System.Drawing.Point(680, 21);
            this.txteffectivedate.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txteffectivedate.Name = "txteffectivedate";
            this.txteffectivedate.Size = new System.Drawing.Size(148, 28);
            this.txteffectivedate.TabIndex = 26;
            // 
            // labelControl8
            // 
            this.labelControl8.Appearance.Font = new System.Drawing.Font("Tahoma", 9.25F, System.Drawing.FontStyle.Bold);
            this.labelControl8.Appearance.Options.UseFont = true;
            this.labelControl8.Location = new System.Drawing.Point(536, 26);
            this.labelControl8.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.labelControl8.Name = "labelControl8";
            this.labelControl8.Size = new System.Drawing.Size(122, 18);
            this.labelControl8.TabIndex = 25;
            this.labelControl8.Text = "Effectivity Date:";
            // 
            // txtprodcat
            // 
            this.txtprodcat.Enabled = false;
            this.txtprodcat.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtprodcat.FormattingEnabled = true;
            this.txtprodcat.Location = new System.Drawing.Point(187, 257);
            this.txtprodcat.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtprodcat.Name = "txtprodcat";
            this.txtprodcat.Size = new System.Drawing.Size(313, 27);
            this.txtprodcat.TabIndex = 20;
            this.txtprodcat.SelectedIndexChanged += new System.EventHandler(this.txtprodcat_SelectedIndexChanged);
            this.txtprodcat.Click += new System.EventHandler(this.txtprodcat_Click);
            // 
            // labelControl7
            // 
            this.labelControl7.Appearance.Font = new System.Drawing.Font("Tahoma", 9.25F, System.Drawing.FontStyle.Bold);
            this.labelControl7.Appearance.Options.UseFont = true;
            this.labelControl7.Location = new System.Drawing.Point(21, 57);
            this.labelControl7.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(135, 18);
            this.labelControl7.TabIndex = 19;
            this.labelControl7.Text = "Product Category:";
            // 
            // txtrefno
            // 
            this.txtrefno.Location = new System.Drawing.Point(1033, 201);
            this.txtrefno.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtrefno.Name = "txtrefno";
            this.txtrefno.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtrefno.Properties.Appearance.Options.UseFont = true;
            this.txtrefno.Properties.ReadOnly = true;
            this.txtrefno.Size = new System.Drawing.Size(116, 26);
            this.txtrefno.TabIndex = 14;
            this.txtrefno.Visible = false;
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl4.Appearance.Options.UseFont = true;
            this.labelControl4.Location = new System.Drawing.Point(912, 204);
            this.labelControl4.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(107, 19);
            this.labelControl4.TabIndex = 13;
            this.labelControl4.Text = "Reference #:";
            this.labelControl4.Visible = false;
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 9.25F, System.Drawing.FontStyle.Bold);
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Location = new System.Drawing.Point(21, 98);
            this.labelControl1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(114, 18);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "Select Product:";
            // 
            // textEdit1
            // 
            this.textEdit1.Location = new System.Drawing.Point(185, 15);
            this.textEdit1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textEdit1.Name = "textEdit1";
            this.textEdit1.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textEdit1.Properties.Appearance.Options.UseFont = true;
            this.textEdit1.Properties.ReadOnly = true;
            this.textEdit1.Size = new System.Drawing.Size(128, 26);
            this.textEdit1.TabIndex = 9;
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Tahoma", 9.25F, System.Drawing.FontStyle.Bold);
            this.labelControl2.Appearance.Options.UseFont = true;
            this.labelControl2.Location = new System.Drawing.Point(596, 64);
            this.labelControl2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(70, 18);
            this.labelControl2.TabIndex = 1;
            this.labelControl2.Text = "Quantity:";
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Tahoma", 9.25F, System.Drawing.FontStyle.Bold);
            this.labelControl3.Appearance.Options.UseFont = true;
            this.labelControl3.Location = new System.Drawing.Point(21, 18);
            this.labelControl3.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(50, 18);
            this.labelControl3.TabIndex = 8;
            this.labelControl3.Text = "Req #:";
            // 
            // tabMain
            // 
            this.tabMain.AppearancePage.Header.Font = new System.Drawing.Font("Tahoma", 9.25F);
            this.tabMain.AppearancePage.Header.Options.UseFont = true;
            this.tabMain.AppearancePage.HeaderActive.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabMain.AppearancePage.HeaderActive.Options.UseFont = true;
            this.tabMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabMain.Location = new System.Drawing.Point(0, 0);
            this.tabMain.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabMain.Name = "tabMain";
            this.tabMain.SelectedTabPage = this.tabProducts;
            this.tabMain.Size = new System.Drawing.Size(1283, 675);
            this.tabMain.TabIndex = 14;
            this.tabMain.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.tabProducts});
            // 
            // tabProducts
            // 
            this.tabProducts.Controls.Add(this.groupBox2);
            this.tabProducts.Controls.Add(this.panelControl1);
            this.tabProducts.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("tabProducts.ImageOptions.Image")));
            this.tabProducts.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabProducts.Name = "tabProducts";
            this.tabProducts.Size = new System.Drawing.Size(1281, 643);
            this.tabProducts.Text = "STS Products";
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.txtpcode);
            this.panelControl1.Controls.Add(this.txtpcat);
            this.panelControl1.Controls.Add(this.simpleButton3);
            this.panelControl1.Controls.Add(this.txtgroup);
            this.panelControl1.Controls.Add(this.labelControl12);
            this.panelControl1.Controls.Add(this.radothers);
            this.panelControl1.Controls.Add(this.panelControl2);
            this.panelControl1.Controls.Add(this.radho);
            this.panelControl1.Controls.Add(this.txtpname);
            this.panelControl1.Controls.Add(this.labelControl5);
            this.panelControl1.Controls.Add(this.labelControl3);
            this.panelControl1.Controls.Add(this.simpleButton9);
            this.panelControl1.Controls.Add(this.labelControl2);
            this.panelControl1.Controls.Add(this.btncancel);
            this.panelControl1.Controls.Add(this.textEdit1);
            this.panelControl1.Controls.Add(this.btnsave);
            this.panelControl1.Controls.Add(this.labelControl1);
            this.panelControl1.Controls.Add(this.btnadd);
            this.panelControl1.Controls.Add(this.labelControl4);
            this.panelControl1.Controls.Add(this.btnnew);
            this.panelControl1.Controls.Add(this.txtrefno);
            this.panelControl1.Controls.Add(this.txtqty);
            this.panelControl1.Controls.Add(this.labelControl7);
            this.panelControl1.Controls.Add(this.ordertype);
            this.panelControl1.Controls.Add(this.txtprodcat);
            this.panelControl1.Controls.Add(this.labelControl11);
            this.panelControl1.Controls.Add(this.labelControl8);
            this.panelControl1.Controls.Add(this.checkBox1);
            this.panelControl1.Controls.Add(this.txteffectivedate);
            this.panelControl1.Controls.Add(this.comboBox1);
            this.panelControl1.Controls.Add(this.panel2);
            this.panelControl1.Controls.Add(this.labelControl10);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(1281, 236);
            this.panelControl1.TabIndex = 0;
            // 
            // txtpcode
            // 
            this.txtpcode.Font = new System.Drawing.Font("Tahoma", 9.25F);
            this.txtpcode.Location = new System.Drawing.Point(184, 95);
            this.txtpcode.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtpcode.Name = "txtpcode";
            this.txtpcode.ReadOnly = true;
            this.txtpcode.Size = new System.Drawing.Size(316, 26);
            this.txtpcode.TabIndex = 85;
            // 
            // txtpcat
            // 
            this.txtpcat.Font = new System.Drawing.Font("Tahoma", 9.25F);
            this.txtpcat.Location = new System.Drawing.Point(184, 54);
            this.txtpcat.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtpcat.Name = "txtpcat";
            this.txtpcat.ReadOnly = true;
            this.txtpcat.Size = new System.Drawing.Size(147, 26);
            this.txtpcat.TabIndex = 84;
            // 
            // simpleButton3
            // 
            this.simpleButton3.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("simpleButton3.ImageOptions.Image")));
            this.simpleButton3.Location = new System.Drawing.Point(379, 52);
            this.simpleButton3.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.simpleButton3.Name = "simpleButton3";
            this.simpleButton3.Size = new System.Drawing.Size(121, 32);
            this.simpleButton3.TabIndex = 83;
            this.simpleButton3.Text = "Find (F1)";
            this.simpleButton3.Click += new System.EventHandler(this.simpleButton3_Click_1);
            // 
            // txtgroup
            // 
            this.txtgroup.Enabled = false;
            this.txtgroup.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtgroup.FormattingEnabled = true;
            this.txtgroup.Location = new System.Drawing.Point(379, 14);
            this.txtgroup.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtgroup.Name = "txtgroup";
            this.txtgroup.Size = new System.Drawing.Size(121, 27);
            this.txtgroup.TabIndex = 76;
            // 
            // labelControl12
            // 
            this.labelControl12.Appearance.Font = new System.Drawing.Font("Tahoma", 9.25F, System.Drawing.FontStyle.Bold);
            this.labelControl12.Appearance.Options.UseFont = true;
            this.labelControl12.Location = new System.Drawing.Point(321, 18);
            this.labelControl12.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.labelControl12.Name = "labelControl12";
            this.labelControl12.Size = new System.Drawing.Size(50, 18);
            this.labelControl12.TabIndex = 75;
            this.labelControl12.Text = "Group:";
            // 
            // radothers
            // 
            this.radothers.AutoSize = true;
            this.radothers.Location = new System.Drawing.Point(1115, 25);
            this.radothers.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.radothers.Name = "radothers";
            this.radothers.Size = new System.Drawing.Size(111, 21);
            this.radothers.TabIndex = 28;
            this.radothers.Text = "Other Branch";
            this.radothers.UseVisualStyleBackColor = true;
            this.radothers.CheckedChanged += new System.EventHandler(this.radothers_CheckedChanged);
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.txtbranch);
            this.panelControl2.Controls.Add(this.labelControl6);
            this.panelControl2.Location = new System.Drawing.Point(837, 53);
            this.panelControl2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(412, 112);
            this.panelControl2.TabIndex = 74;
            // 
            // txtbranch
            // 
            this.txtbranch.Location = new System.Drawing.Point(136, 7);
            this.txtbranch.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtbranch.Name = "txtbranch";
            this.txtbranch.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.25F);
            this.txtbranch.Properties.Appearance.Options.UseFont = true;
            this.txtbranch.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtbranch.Properties.NullText = "";
            this.txtbranch.Properties.PopupView = this.searchLookUpEdit1View;
            this.txtbranch.Size = new System.Drawing.Size(260, 24);
            this.txtbranch.TabIndex = 28;
            // 
            // searchLookUpEdit1View
            // 
            this.searchLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.searchLookUpEdit1View.Name = "searchLookUpEdit1View";
            this.searchLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.searchLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            // 
            // labelControl6
            // 
            this.labelControl6.Appearance.Font = new System.Drawing.Font("Tahoma", 9.25F, System.Drawing.FontStyle.Bold);
            this.labelControl6.Appearance.Options.UseFont = true;
            this.labelControl6.Location = new System.Drawing.Point(7, 11);
            this.labelControl6.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(108, 18);
            this.labelControl6.TabIndex = 27;
            this.labelControl6.Text = "Select Branch:";
            // 
            // radho
            // 
            this.radho.AutoSize = true;
            this.radho.Checked = true;
            this.radho.Location = new System.Drawing.Point(997, 25);
            this.radho.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.radho.Name = "radho";
            this.radho.Size = new System.Drawing.Size(98, 21);
            this.radho.TabIndex = 27;
            this.radho.TabStop = true;
            this.radho.Text = "Head Office";
            this.radho.UseVisualStyleBackColor = true;
            this.radho.CheckedChanged += new System.EventHandler(this.radho_CheckedChanged);
            // 
            // labelControl5
            // 
            this.labelControl5.Appearance.Font = new System.Drawing.Font("Tahoma", 9.25F, System.Drawing.FontStyle.Bold);
            this.labelControl5.Appearance.Options.UseFont = true;
            this.labelControl5.Location = new System.Drawing.Point(837, 26);
            this.labelControl5.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(141, 18);
            this.labelControl5.TabIndex = 26;
            this.labelControl5.Text = "Request to Branch:";
            // 
            // AddOrderSTS
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1283, 675);
            this.Controls.Add(this.tabMain);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "AddOrderSTS";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AddOrderSTS";
            this.Load += new System.EventHandler(this.AddOrderSTS_Load);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtpname.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView3)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtrefno.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabMain)).EndInit();
            this.tabMain.ResumeLayout(false);
            this.tabProducts.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            this.panelControl2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtbranch.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        public DevExpress.XtraGrid.GridControl gridControl1;
        public DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private System.Windows.Forms.TextBox txtqty;
        private System.Windows.Forms.ComboBox ordertype;
        private DevExpress.XtraEditors.LabelControl labelControl11;
        public System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.ComboBox comboBox1;
        private DevExpress.XtraEditors.LabelControl labelControl10;
        private System.Windows.Forms.Panel panel2;
        private DevExpress.XtraEditors.LabelControl labelControl9;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.DateTimePicker txteffectivedate;
        private DevExpress.XtraEditors.LabelControl labelControl8;
        private System.Windows.Forms.ComboBox txtprodcat;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        private DevExpress.XtraEditors.TextEdit txtrefno;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit textEdit1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.SimpleButton simpleButton9;
        private DevExpress.XtraEditors.SimpleButton btncancel;
        private DevExpress.XtraEditors.SimpleButton btnsave;
        private DevExpress.XtraEditors.SimpleButton btnadd;
        private DevExpress.XtraEditors.SimpleButton btnnew;
        private DevExpress.XtraEditors.SearchLookUpEdit txtpname;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView3;
        private DevExpress.XtraTab.XtraTabControl tabMain;
        private DevExpress.XtraTab.XtraTabPage tabProducts;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private System.Windows.Forms.RadioButton radothers;
        private System.Windows.Forms.RadioButton radho;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.SearchLookUpEdit txtbranch;
        private DevExpress.XtraGrid.Views.Grid.GridView searchLookUpEdit1View;
        private DevExpress.XtraEditors.LabelControl labelControl12;
        private System.Windows.Forms.ComboBox txtgroup;
        private DevExpress.XtraEditors.SimpleButton simpleButton3;
        private System.Windows.Forms.TextBox txtpcode;
        private System.Windows.Forms.TextBox txtpcat;
    }
}