namespace SalesInventorySystem.RFID
{
    partial class RFIDDashboard
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
            this.lblCount = new DevExpress.XtraEditors.LabelControl();
            this.txtScannerInput = new DevExpress.XtraEditors.TextEdit();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.tablePanel1 = new DevExpress.Utils.Layout.TablePanel();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.lblfound = new DevExpress.XtraEditors.LabelControl();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.lblnotfound = new DevExpress.XtraEditors.LabelControl();
            this.lbltotcount = new DevExpress.XtraEditors.LabelControl();
            this.labelControl9 = new DevExpress.XtraEditors.LabelControl();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.btnRetrieve = new DevExpress.XtraEditors.SimpleButton();
            this.panelControl3 = new DevExpress.XtraEditors.PanelControl();
            this.tablePanel3 = new DevExpress.Utils.Layout.TablePanel();
            this.gridControlFound = new DevExpress.XtraGrid.GridControl();
            this.gridViewFound = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.labelControl8 = new DevExpress.XtraEditors.LabelControl();
            this.gridControlGather = new DevExpress.XtraGrid.GridControl();
            this.gridViewGather = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.gridControlnotfound = new DevExpress.XtraGrid.GridControl();
            this.gridViewnotfound = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.txtScannerInput.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tablePanel1)).BeginInit();
            this.tablePanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).BeginInit();
            this.panelControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tablePanel3)).BeginInit();
            this.tablePanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlFound)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewFound)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlGather)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewGather)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlnotfound)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewnotfound)).BeginInit();
            this.SuspendLayout();
            // 
            // lblCount
            // 
            this.lblCount.Appearance.Font = new System.Drawing.Font("Tahoma", 9.8F);
            this.lblCount.Appearance.Options.UseFont = true;
            this.lblCount.Location = new System.Drawing.Point(12, 21);
            this.lblCount.Name = "lblCount";
            this.lblCount.Size = new System.Drawing.Size(62, 19);
            this.lblCount.TabIndex = 1;
            this.lblCount.Text = "Barcode:";
            // 
            // txtScannerInput
            // 
            this.txtScannerInput.Location = new System.Drawing.Point(80, 16);
            this.txtScannerInput.Name = "txtScannerInput";
            this.txtScannerInput.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10.8F);
            this.txtScannerInput.Properties.Appearance.Options.UseFont = true;
            this.txtScannerInput.Size = new System.Drawing.Size(278, 28);
            this.txtScannerInput.TabIndex = 2;
            this.txtScannerInput.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtScannerInput_KeyDown);
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.tablePanel1);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(1115, 171);
            this.panelControl1.TabIndex = 3;
            // 
            // tablePanel1
            // 
            this.tablePanel1.Appearance.BorderColor = System.Drawing.Color.Black;
            this.tablePanel1.Appearance.Options.UseBorderColor = true;
            this.tablePanel1.Columns.AddRange(new DevExpress.Utils.Layout.TablePanelColumn[] {
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 55F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 50F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 50F)});
            this.tablePanel1.Controls.Add(this.labelControl4);
            this.tablePanel1.Controls.Add(this.lblfound);
            this.tablePanel1.Controls.Add(this.labelControl6);
            this.tablePanel1.Controls.Add(this.lblnotfound);
            this.tablePanel1.Controls.Add(this.lbltotcount);
            this.tablePanel1.Controls.Add(this.labelControl9);
            this.tablePanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tablePanel1.Location = new System.Drawing.Point(2, 2);
            this.tablePanel1.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.tablePanel1.Name = "tablePanel1";
            this.tablePanel1.Rows.AddRange(new DevExpress.Utils.Layout.TablePanelRow[] {
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 66.5F),
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 26F)});
            this.tablePanel1.ShowGrid = DevExpress.Utils.DefaultBoolean.False;
            this.tablePanel1.Size = new System.Drawing.Size(1111, 167);
            this.tablePanel1.TabIndex = 2;
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.BackColor = System.Drawing.Color.Black;
            this.labelControl4.Appearance.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Bold);
            this.labelControl4.Appearance.ForeColor = System.Drawing.Color.Lime;
            this.labelControl4.Appearance.Options.UseBackColor = true;
            this.labelControl4.Appearance.Options.UseFont = true;
            this.labelControl4.Appearance.Options.UseForeColor = true;
            this.labelControl4.Appearance.Options.UseTextOptions = true;
            this.labelControl4.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labelControl4.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.labelControl4.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.labelControl4.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.tablePanel1.SetColumn(this.labelControl4, 1);
            this.labelControl4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelControl4.Location = new System.Drawing.Point(395, 2);
            this.labelControl4.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.labelControl4.Name = "labelControl4";
            this.tablePanel1.SetRow(this.labelControl4, 0);
            this.labelControl4.Size = new System.Drawing.Size(356, 63);
            this.labelControl4.TabIndex = 17;
            this.labelControl4.Text = "NOT FOUND";
            // 
            // lblfound
            // 
            this.lblfound.Appearance.BackColor = System.Drawing.Color.Black;
            this.lblfound.Appearance.Font = new System.Drawing.Font("Tahoma", 40F);
            this.lblfound.Appearance.ForeColor = System.Drawing.Color.Lime;
            this.lblfound.Appearance.Options.UseBackColor = true;
            this.lblfound.Appearance.Options.UseFont = true;
            this.lblfound.Appearance.Options.UseForeColor = true;
            this.lblfound.Appearance.Options.UseTextOptions = true;
            this.lblfound.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.lblfound.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.lblfound.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.lblfound.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.tablePanel1.SetColumn(this.lblfound, 2);
            this.lblfound.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblfound.Location = new System.Drawing.Point(754, 69);
            this.lblfound.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.lblfound.Name = "lblfound";
            this.tablePanel1.SetRow(this.lblfound, 1);
            this.lblfound.Size = new System.Drawing.Size(356, 96);
            this.lblfound.TabIndex = 17;
            this.lblfound.Text = "0";
            // 
            // labelControl6
            // 
            this.labelControl6.Appearance.BackColor = System.Drawing.Color.Black;
            this.labelControl6.Appearance.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Bold);
            this.labelControl6.Appearance.ForeColor = System.Drawing.Color.Lime;
            this.labelControl6.Appearance.Options.UseBackColor = true;
            this.labelControl6.Appearance.Options.UseFont = true;
            this.labelControl6.Appearance.Options.UseForeColor = true;
            this.labelControl6.Appearance.Options.UseTextOptions = true;
            this.labelControl6.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labelControl6.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.labelControl6.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.labelControl6.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.tablePanel1.SetColumn(this.labelControl6, 2);
            this.labelControl6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelControl6.Location = new System.Drawing.Point(754, 2);
            this.labelControl6.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.labelControl6.Name = "labelControl6";
            this.tablePanel1.SetRow(this.labelControl6, 0);
            this.labelControl6.Size = new System.Drawing.Size(356, 63);
            this.labelControl6.TabIndex = 18;
            this.labelControl6.Text = "RETRIEVE";
            // 
            // lblnotfound
            // 
            this.lblnotfound.Appearance.BackColor = System.Drawing.Color.Black;
            this.lblnotfound.Appearance.Font = new System.Drawing.Font("Tahoma", 40F);
            this.lblnotfound.Appearance.ForeColor = System.Drawing.Color.Lime;
            this.lblnotfound.Appearance.Options.UseBackColor = true;
            this.lblnotfound.Appearance.Options.UseFont = true;
            this.lblnotfound.Appearance.Options.UseForeColor = true;
            this.lblnotfound.Appearance.Options.UseTextOptions = true;
            this.lblnotfound.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.lblnotfound.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.lblnotfound.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.lblnotfound.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.tablePanel1.SetColumn(this.lblnotfound, 1);
            this.lblnotfound.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblnotfound.Location = new System.Drawing.Point(395, 69);
            this.lblnotfound.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.lblnotfound.Name = "lblnotfound";
            this.tablePanel1.SetRow(this.lblnotfound, 1);
            this.lblnotfound.Size = new System.Drawing.Size(356, 96);
            this.lblnotfound.TabIndex = 16;
            this.lblnotfound.Text = "0";
            // 
            // lbltotcount
            // 
            this.lbltotcount.Appearance.BackColor = System.Drawing.Color.Black;
            this.lbltotcount.Appearance.Font = new System.Drawing.Font("Tahoma", 40F);
            this.lbltotcount.Appearance.ForeColor = System.Drawing.Color.Lime;
            this.lbltotcount.Appearance.Options.UseBackColor = true;
            this.lbltotcount.Appearance.Options.UseFont = true;
            this.lbltotcount.Appearance.Options.UseForeColor = true;
            this.lbltotcount.Appearance.Options.UseTextOptions = true;
            this.lbltotcount.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.lbltotcount.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.lbltotcount.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.lbltotcount.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.tablePanel1.SetColumn(this.lbltotcount, 0);
            this.lbltotcount.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbltotcount.Location = new System.Drawing.Point(1, 69);
            this.lbltotcount.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.lbltotcount.Name = "lbltotcount";
            this.tablePanel1.SetRow(this.lbltotcount, 1);
            this.lbltotcount.Size = new System.Drawing.Size(392, 96);
            this.lbltotcount.TabIndex = 15;
            this.lbltotcount.Text = "0";
            // 
            // labelControl9
            // 
            this.labelControl9.Appearance.BackColor = System.Drawing.Color.Black;
            this.labelControl9.Appearance.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Bold);
            this.labelControl9.Appearance.ForeColor = System.Drawing.Color.Lime;
            this.labelControl9.Appearance.Options.UseBackColor = true;
            this.labelControl9.Appearance.Options.UseFont = true;
            this.labelControl9.Appearance.Options.UseForeColor = true;
            this.labelControl9.Appearance.Options.UseTextOptions = true;
            this.labelControl9.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labelControl9.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.labelControl9.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.labelControl9.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.tablePanel1.SetColumn(this.labelControl9, 0);
            this.labelControl9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelControl9.Location = new System.Drawing.Point(1, 2);
            this.labelControl9.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.labelControl9.Name = "labelControl9";
            this.tablePanel1.SetRow(this.labelControl9, 0);
            this.labelControl9.Size = new System.Drawing.Size(392, 63);
            this.labelControl9.TabIndex = 16;
            this.labelControl9.Text = "TOTAL COUNTS";
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.btnRetrieve);
            this.panelControl2.Controls.Add(this.lblCount);
            this.panelControl2.Controls.Add(this.txtScannerInput);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl2.Location = new System.Drawing.Point(0, 171);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(1115, 58);
            this.panelControl2.TabIndex = 4;
            // 
            // btnRetrieve
            // 
            this.btnRetrieve.Location = new System.Drawing.Point(364, 15);
            this.btnRetrieve.Name = "btnRetrieve";
            this.btnRetrieve.Size = new System.Drawing.Size(94, 29);
            this.btnRetrieve.TabIndex = 3;
            this.btnRetrieve.Text = "RETRIEVE";
            this.btnRetrieve.Click += new System.EventHandler(this.btnRetrieve_Click);
            // 
            // panelControl3
            // 
            this.panelControl3.Controls.Add(this.tablePanel3);
            this.panelControl3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl3.Location = new System.Drawing.Point(0, 229);
            this.panelControl3.Name = "panelControl3";
            this.panelControl3.Size = new System.Drawing.Size(1115, 458);
            this.panelControl3.TabIndex = 5;
            // 
            // tablePanel3
            // 
            this.tablePanel3.Appearance.Options.UseBorderColor = true;
            this.tablePanel3.Columns.AddRange(new DevExpress.Utils.Layout.TablePanelColumn[] {
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 37.37F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 33.83F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 33.94F)});
            this.tablePanel3.Controls.Add(this.gridControlFound);
            this.tablePanel3.Controls.Add(this.labelControl8);
            this.tablePanel3.Controls.Add(this.gridControlGather);
            this.tablePanel3.Controls.Add(this.labelControl7);
            this.tablePanel3.Controls.Add(this.labelControl5);
            this.tablePanel3.Controls.Add(this.gridControlnotfound);
            this.tablePanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tablePanel3.Location = new System.Drawing.Point(2, 2);
            this.tablePanel3.Name = "tablePanel3";
            this.tablePanel3.Rows.AddRange(new DevExpress.Utils.Layout.TablePanelRow[] {
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 60.3996F),
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 26F)});
            this.tablePanel3.ShowGrid = DevExpress.Utils.DefaultBoolean.False;
            this.tablePanel3.Size = new System.Drawing.Size(1111, 454);
            this.tablePanel3.TabIndex = 1;
            // 
            // gridControlFound
            // 
            this.tablePanel3.SetColumn(this.gridControlFound, 2);
            this.gridControlFound.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControlFound.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.gridControlFound.Font = new System.Drawing.Font("Tahoma", 7.8F);
            this.gridControlFound.Location = new System.Drawing.Point(754, 64);
            this.gridControlFound.MainView = this.gridViewFound;
            this.gridControlFound.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.gridControlFound.Name = "gridControlFound";
            this.tablePanel3.SetRow(this.gridControlFound, 1);
            this.gridControlFound.Size = new System.Drawing.Size(355, 386);
            this.gridControlFound.TabIndex = 20;
            this.gridControlFound.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewFound});
            // 
            // gridViewFound
            // 
            this.gridViewFound.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 8.75F, System.Drawing.FontStyle.Bold);
            this.gridViewFound.Appearance.HeaderPanel.Options.UseFont = true;
            this.gridViewFound.Appearance.Row.Font = new System.Drawing.Font("Tahoma", 8.75F);
            this.gridViewFound.Appearance.Row.Options.UseFont = true;
            this.gridViewFound.DetailHeight = 431;
            this.gridViewFound.GridControl = this.gridControlFound;
            this.gridViewFound.Name = "gridViewFound";
            this.gridViewFound.OptionsBehavior.Editable = false;
            this.gridViewFound.OptionsBehavior.ReadOnly = true;
            this.gridViewFound.OptionsPrint.AutoWidth = false;
            this.gridViewFound.OptionsView.ColumnAutoWidth = false;
            this.gridViewFound.OptionsView.RowAutoHeight = true;
            this.gridViewFound.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;
            this.gridViewFound.OptionsView.ShowGroupExpandCollapseButtons = false;
            this.gridViewFound.OptionsView.ShowGroupPanel = false;
            this.gridViewFound.OptionsView.ShowIndicator = false;
            // 
            // labelControl8
            // 
            this.labelControl8.Appearance.BackColor = System.Drawing.Color.Black;
            this.labelControl8.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.labelControl8.Appearance.ForeColor = System.Drawing.Color.Red;
            this.labelControl8.Appearance.Options.UseBackColor = true;
            this.labelControl8.Appearance.Options.UseFont = true;
            this.labelControl8.Appearance.Options.UseForeColor = true;
            this.labelControl8.Appearance.Options.UseTextOptions = true;
            this.labelControl8.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labelControl8.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.labelControl8.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.labelControl8.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.tablePanel3.SetColumn(this.labelControl8, 2);
            this.labelControl8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelControl8.Location = new System.Drawing.Point(753, 2);
            this.labelControl8.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.labelControl8.Name = "labelControl8";
            this.tablePanel3.SetRow(this.labelControl8, 0);
            this.labelControl8.Size = new System.Drawing.Size(357, 56);
            this.labelControl8.TabIndex = 19;
            this.labelControl8.Text = "FOUND / RETRIEVED";
            // 
            // gridControlGather
            // 
            this.tablePanel3.SetColumn(this.gridControlGather, 0);
            this.gridControlGather.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControlGather.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.gridControlGather.Font = new System.Drawing.Font("Tahoma", 10.8F);
            this.gridControlGather.Location = new System.Drawing.Point(2, 64);
            this.gridControlGather.MainView = this.gridViewGather;
            this.gridControlGather.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.gridControlGather.Name = "gridControlGather";
            this.tablePanel3.SetRow(this.gridControlGather, 1);
            this.gridControlGather.Size = new System.Drawing.Size(391, 386);
            this.gridControlGather.TabIndex = 6;
            this.gridControlGather.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewGather});
            // 
            // gridViewGather
            // 
            this.gridViewGather.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 11.75F, System.Drawing.FontStyle.Bold);
            this.gridViewGather.Appearance.HeaderPanel.Options.UseFont = true;
            this.gridViewGather.Appearance.Row.Font = new System.Drawing.Font("Tahoma", 11.75F);
            this.gridViewGather.Appearance.Row.Options.UseFont = true;
            this.gridViewGather.DetailHeight = 431;
            this.gridViewGather.GridControl = this.gridControlGather;
            this.gridViewGather.Name = "gridViewGather";
            this.gridViewGather.OptionsBehavior.Editable = false;
            this.gridViewGather.OptionsBehavior.ReadOnly = true;
            this.gridViewGather.OptionsPrint.AutoWidth = false;
            this.gridViewGather.OptionsView.ColumnAutoWidth = false;
            this.gridViewGather.OptionsView.RowAutoHeight = true;
            this.gridViewGather.OptionsView.ShowGroupPanel = false;
            this.gridViewGather.OptionsView.ShowIndicator = false;
            // 
            // labelControl7
            // 
            this.labelControl7.Appearance.BackColor = System.Drawing.Color.Black;
            this.labelControl7.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.labelControl7.Appearance.ForeColor = System.Drawing.Color.Red;
            this.labelControl7.Appearance.Options.UseBackColor = true;
            this.labelControl7.Appearance.Options.UseFont = true;
            this.labelControl7.Appearance.Options.UseForeColor = true;
            this.labelControl7.Appearance.Options.UseTextOptions = true;
            this.labelControl7.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labelControl7.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.labelControl7.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.labelControl7.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.tablePanel3.SetColumn(this.labelControl7, 0);
            this.labelControl7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelControl7.Location = new System.Drawing.Point(1, 2);
            this.labelControl7.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.labelControl7.Name = "labelControl7";
            this.tablePanel3.SetRow(this.labelControl7, 0);
            this.labelControl7.Size = new System.Drawing.Size(393, 56);
            this.labelControl7.TabIndex = 18;
            this.labelControl7.Text = "BARCODES GATHER";
            // 
            // labelControl5
            // 
            this.labelControl5.Appearance.BackColor = System.Drawing.Color.Black;
            this.labelControl5.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.labelControl5.Appearance.ForeColor = System.Drawing.Color.Red;
            this.labelControl5.Appearance.Options.UseBackColor = true;
            this.labelControl5.Appearance.Options.UseFont = true;
            this.labelControl5.Appearance.Options.UseForeColor = true;
            this.labelControl5.Appearance.Options.UseTextOptions = true;
            this.labelControl5.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labelControl5.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.labelControl5.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.labelControl5.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.tablePanel3.SetColumn(this.labelControl5, 1);
            this.labelControl5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelControl5.Location = new System.Drawing.Point(396, 2);
            this.labelControl5.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.labelControl5.Name = "labelControl5";
            this.tablePanel3.SetRow(this.labelControl5, 0);
            this.labelControl5.Size = new System.Drawing.Size(355, 56);
            this.labelControl5.TabIndex = 17;
            this.labelControl5.Text = "NOT IN RECORDS";
            // 
            // gridControlnotfound
            // 
            this.tablePanel3.SetColumn(this.gridControlnotfound, 1);
            this.gridControlnotfound.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControlnotfound.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.gridControlnotfound.Font = new System.Drawing.Font("Tahoma", 12.8F);
            this.gridControlnotfound.Location = new System.Drawing.Point(397, 64);
            this.gridControlnotfound.MainView = this.gridViewnotfound;
            this.gridControlnotfound.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.gridControlnotfound.Name = "gridControlnotfound";
            this.tablePanel3.SetRow(this.gridControlnotfound, 1);
            this.gridControlnotfound.Size = new System.Drawing.Size(353, 386);
            this.gridControlnotfound.TabIndex = 7;
            this.gridControlnotfound.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewnotfound});
            // 
            // gridViewnotfound
            // 
            this.gridViewnotfound.Appearance.GroupRow.Font = new System.Drawing.Font("Tahoma", 12.8F);
            this.gridViewnotfound.Appearance.GroupRow.Options.UseFont = true;
            this.gridViewnotfound.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 11.75F, System.Drawing.FontStyle.Bold);
            this.gridViewnotfound.Appearance.HeaderPanel.Options.UseFont = true;
            this.gridViewnotfound.Appearance.Row.Font = new System.Drawing.Font("Tahoma", 11.75F);
            this.gridViewnotfound.Appearance.Row.Options.UseFont = true;
            this.gridViewnotfound.DetailHeight = 431;
            this.gridViewnotfound.GridControl = this.gridControlnotfound;
            this.gridViewnotfound.Name = "gridViewnotfound";
            this.gridViewnotfound.OptionsBehavior.Editable = false;
            this.gridViewnotfound.OptionsBehavior.ReadOnly = true;
            this.gridViewnotfound.OptionsPrint.AutoWidth = false;
            this.gridViewnotfound.OptionsView.ColumnAutoWidth = false;
            this.gridViewnotfound.OptionsView.RowAutoHeight = true;
            this.gridViewnotfound.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;
            this.gridViewnotfound.OptionsView.ShowGroupExpandCollapseButtons = false;
            this.gridViewnotfound.OptionsView.ShowGroupPanel = false;
            this.gridViewnotfound.OptionsView.ShowIndicator = false;
            // 
            // RFIDDashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1115, 687);
            this.Controls.Add(this.panelControl3);
            this.Controls.Add(this.panelControl2);
            this.Controls.Add(this.panelControl1);
            this.Name = "RFIDDashboard";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "RFIDDashboard";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.RFIDDashboard_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtScannerInput.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tablePanel1)).EndInit();
            this.tablePanel1.ResumeLayout(false);
            this.tablePanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            this.panelControl2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).EndInit();
            this.panelControl3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tablePanel3)).EndInit();
            this.tablePanel3.ResumeLayout(false);
            this.tablePanel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlFound)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewFound)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlGather)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewGather)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlnotfound)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewnotfound)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraEditors.LabelControl lblCount;
        private DevExpress.XtraEditors.TextEdit txtScannerInput;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.PanelControl panelControl3;
        private DevExpress.Utils.Layout.TablePanel tablePanel1;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl lblfound;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.LabelControl lblnotfound;
        private DevExpress.XtraEditors.LabelControl lbltotcount;
        private DevExpress.XtraEditors.LabelControl labelControl9;
        private DevExpress.Utils.Layout.TablePanel tablePanel3;
        private DevExpress.XtraGrid.GridControl gridControlFound;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewFound;
        private DevExpress.XtraEditors.LabelControl labelControl8;
        private DevExpress.XtraGrid.GridControl gridControlGather;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewGather;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraGrid.GridControl gridControlnotfound;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewnotfound;
        private DevExpress.XtraEditors.SimpleButton btnRetrieve;
    }
}