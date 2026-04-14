namespace SalesInventorySystem.LiveTrends
{
    partial class STSMonitoring
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
            this.tablePanel1 = new DevExpress.Utils.Layout.TablePanel();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.lblcompleted = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.lblreceiving = new DevExpress.XtraEditors.LabelControl();
            this.lblpending = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.gridControlForRcvng = new DevExpress.XtraGrid.GridControl();
            this.gridViewForRcvng = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.timerLiveSync = new System.Windows.Forms.Timer(this.components);
            this.tablePanel2 = new DevExpress.Utils.Layout.TablePanel();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.lblapproved = new DevExpress.XtraEditors.LabelControl();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.lblrejected = new DevExpress.XtraEditors.LabelControl();
            this.lblforapproval = new DevExpress.XtraEditors.LabelControl();
            this.labelControl9 = new DevExpress.XtraEditors.LabelControl();
            this.gridControlSalesNotDeducted = new DevExpress.XtraGrid.GridControl();
            this.gridViewSalesNotDeducted = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.panelControl3 = new DevExpress.XtraEditors.PanelControl();
            this.tablePanel3 = new DevExpress.Utils.Layout.TablePanel();
            this.gridControlUploadedSales = new DevExpress.XtraGrid.GridControl();
            this.gridViewUploadedSales = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.labelControl8 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.tablePanel1)).BeginInit();
            this.tablePanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlForRcvng)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewForRcvng)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tablePanel2)).BeginInit();
            this.tablePanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlSalesNotDeducted)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewSalesNotDeducted)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).BeginInit();
            this.panelControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tablePanel3)).BeginInit();
            this.tablePanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlUploadedSales)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewUploadedSales)).BeginInit();
            this.SuspendLayout();
            // 
            // tablePanel1
            // 
            this.tablePanel1.Appearance.BackColor = System.Drawing.Color.Black;
            this.tablePanel1.Appearance.BorderColor = System.Drawing.Color.Black;
            this.tablePanel1.Appearance.Options.UseBackColor = true;
            this.tablePanel1.Appearance.Options.UseBorderColor = true;
            this.tablePanel1.Columns.AddRange(new DevExpress.Utils.Layout.TablePanelColumn[] {
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 55F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 50F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 50F)});
            this.tablePanel1.Controls.Add(this.labelControl2);
            this.tablePanel1.Controls.Add(this.lblcompleted);
            this.tablePanel1.Controls.Add(this.labelControl3);
            this.tablePanel1.Controls.Add(this.lblreceiving);
            this.tablePanel1.Controls.Add(this.lblpending);
            this.tablePanel1.Controls.Add(this.labelControl1);
            this.tablePanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tablePanel1.Location = new System.Drawing.Point(0, 0);
            this.tablePanel1.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.tablePanel1.Name = "tablePanel1";
            this.tablePanel1.Rows.AddRange(new DevExpress.Utils.Layout.TablePanelRow[] {
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 66.5F),
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 26F)});
            this.tablePanel1.ShowGrid = DevExpress.Utils.DefaultBoolean.False;
            this.tablePanel1.Size = new System.Drawing.Size(1105, 173);
            this.tablePanel1.TabIndex = 0;
            this.tablePanel1.Paint += new System.Windows.Forms.PaintEventHandler(this.tablePanel1_Paint);
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.BackColor = System.Drawing.Color.Black;
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Bold);
            this.labelControl2.Appearance.ForeColor = System.Drawing.Color.Yellow;
            this.labelControl2.Appearance.Options.UseBackColor = true;
            this.labelControl2.Appearance.Options.UseFont = true;
            this.labelControl2.Appearance.Options.UseForeColor = true;
            this.labelControl2.Appearance.Options.UseTextOptions = true;
            this.labelControl2.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labelControl2.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.labelControl2.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.labelControl2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.tablePanel1.SetColumn(this.labelControl2, 1);
            this.labelControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelControl2.Location = new System.Drawing.Point(393, 2);
            this.labelControl2.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.labelControl2.Name = "labelControl2";
            this.tablePanel1.SetRow(this.labelControl2, 0);
            this.labelControl2.Size = new System.Drawing.Size(354, 63);
            this.labelControl2.TabIndex = 17;
            this.labelControl2.Text = "FOR RECEIVING";
            // 
            // lblcompleted
            // 
            this.lblcompleted.Appearance.BackColor = System.Drawing.Color.Black;
            this.lblcompleted.Appearance.Font = new System.Drawing.Font("Tahoma", 40F);
            this.lblcompleted.Appearance.ForeColor = System.Drawing.Color.Yellow;
            this.lblcompleted.Appearance.Options.UseBackColor = true;
            this.lblcompleted.Appearance.Options.UseFont = true;
            this.lblcompleted.Appearance.Options.UseForeColor = true;
            this.lblcompleted.Appearance.Options.UseTextOptions = true;
            this.lblcompleted.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.lblcompleted.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.lblcompleted.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.lblcompleted.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.tablePanel1.SetColumn(this.lblcompleted, 2);
            this.lblcompleted.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblcompleted.Location = new System.Drawing.Point(750, 69);
            this.lblcompleted.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.lblcompleted.Name = "lblcompleted";
            this.tablePanel1.SetRow(this.lblcompleted, 1);
            this.lblcompleted.Size = new System.Drawing.Size(354, 102);
            this.lblcompleted.TabIndex = 17;
            this.lblcompleted.Text = "0";
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.BackColor = System.Drawing.Color.Black;
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Bold);
            this.labelControl3.Appearance.ForeColor = System.Drawing.Color.Yellow;
            this.labelControl3.Appearance.Options.UseBackColor = true;
            this.labelControl3.Appearance.Options.UseFont = true;
            this.labelControl3.Appearance.Options.UseForeColor = true;
            this.labelControl3.Appearance.Options.UseTextOptions = true;
            this.labelControl3.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labelControl3.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.labelControl3.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.labelControl3.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.tablePanel1.SetColumn(this.labelControl3, 2);
            this.labelControl3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelControl3.Location = new System.Drawing.Point(750, 2);
            this.labelControl3.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.labelControl3.Name = "labelControl3";
            this.tablePanel1.SetRow(this.labelControl3, 0);
            this.labelControl3.Size = new System.Drawing.Size(354, 63);
            this.labelControl3.TabIndex = 18;
            this.labelControl3.Text = "COMPLETED";
            // 
            // lblreceiving
            // 
            this.lblreceiving.Appearance.BackColor = System.Drawing.Color.Black;
            this.lblreceiving.Appearance.Font = new System.Drawing.Font("Tahoma", 40F);
            this.lblreceiving.Appearance.ForeColor = System.Drawing.Color.Yellow;
            this.lblreceiving.Appearance.Options.UseBackColor = true;
            this.lblreceiving.Appearance.Options.UseFont = true;
            this.lblreceiving.Appearance.Options.UseForeColor = true;
            this.lblreceiving.Appearance.Options.UseTextOptions = true;
            this.lblreceiving.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.lblreceiving.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.lblreceiving.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.lblreceiving.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.tablePanel1.SetColumn(this.lblreceiving, 1);
            this.lblreceiving.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblreceiving.Location = new System.Drawing.Point(393, 69);
            this.lblreceiving.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.lblreceiving.Name = "lblreceiving";
            this.tablePanel1.SetRow(this.lblreceiving, 1);
            this.lblreceiving.Size = new System.Drawing.Size(354, 102);
            this.lblreceiving.TabIndex = 16;
            this.lblreceiving.Text = "0";
            // 
            // lblpending
            // 
            this.lblpending.Appearance.BackColor = System.Drawing.Color.Black;
            this.lblpending.Appearance.Font = new System.Drawing.Font("Tahoma", 40F);
            this.lblpending.Appearance.ForeColor = System.Drawing.Color.Yellow;
            this.lblpending.Appearance.Options.UseBackColor = true;
            this.lblpending.Appearance.Options.UseFont = true;
            this.lblpending.Appearance.Options.UseForeColor = true;
            this.lblpending.Appearance.Options.UseTextOptions = true;
            this.lblpending.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.lblpending.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.lblpending.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.lblpending.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.tablePanel1.SetColumn(this.lblpending, 0);
            this.lblpending.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblpending.Location = new System.Drawing.Point(1, 69);
            this.lblpending.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.lblpending.Name = "lblpending";
            this.tablePanel1.SetRow(this.lblpending, 1);
            this.lblpending.Size = new System.Drawing.Size(390, 102);
            this.lblpending.TabIndex = 15;
            this.lblpending.Text = "0";
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.BackColor = System.Drawing.Color.Black;
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Bold);
            this.labelControl1.Appearance.ForeColor = System.Drawing.Color.Yellow;
            this.labelControl1.Appearance.Options.UseBackColor = true;
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Appearance.Options.UseForeColor = true;
            this.labelControl1.Appearance.Options.UseTextOptions = true;
            this.labelControl1.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labelControl1.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.labelControl1.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.labelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.tablePanel1.SetColumn(this.labelControl1, 0);
            this.labelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelControl1.Location = new System.Drawing.Point(1, 2);
            this.labelControl1.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.labelControl1.Name = "labelControl1";
            this.tablePanel1.SetRow(this.labelControl1, 0);
            this.labelControl1.Size = new System.Drawing.Size(390, 63);
            this.labelControl1.TabIndex = 16;
            this.labelControl1.Text = "PENDING";
            // 
            // gridControlForRcvng
            // 
            this.tablePanel3.SetColumn(this.gridControlForRcvng, 0);
            this.gridControlForRcvng.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControlForRcvng.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.gridControlForRcvng.Font = new System.Drawing.Font("Tahoma", 12.8F);
            this.gridControlForRcvng.Location = new System.Drawing.Point(2, 64);
            this.gridControlForRcvng.MainView = this.gridViewForRcvng;
            this.gridControlForRcvng.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.gridControlForRcvng.Name = "gridControlForRcvng";
            this.tablePanel3.SetRow(this.gridControlForRcvng, 1);
            this.gridControlForRcvng.Size = new System.Drawing.Size(389, 318);
            this.gridControlForRcvng.TabIndex = 6;
            this.gridControlForRcvng.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewForRcvng});
            // 
            // gridViewForRcvng
            // 
            this.gridViewForRcvng.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 16.75F, System.Drawing.FontStyle.Bold);
            this.gridViewForRcvng.Appearance.HeaderPanel.Options.UseFont = true;
            this.gridViewForRcvng.Appearance.Row.Font = new System.Drawing.Font("Tahoma", 16.75F);
            this.gridViewForRcvng.Appearance.Row.Options.UseFont = true;
            this.gridViewForRcvng.DetailHeight = 431;
            this.gridViewForRcvng.GridControl = this.gridControlForRcvng;
            this.gridViewForRcvng.Name = "gridViewForRcvng";
            this.gridViewForRcvng.OptionsBehavior.Editable = false;
            this.gridViewForRcvng.OptionsBehavior.ReadOnly = true;
            this.gridViewForRcvng.OptionsPrint.AutoWidth = false;
            this.gridViewForRcvng.OptionsView.ColumnAutoWidth = false;
            this.gridViewForRcvng.OptionsView.RowAutoHeight = true;
            this.gridViewForRcvng.OptionsView.ShowGroupPanel = false;
            this.gridViewForRcvng.OptionsView.ShowIndicator = false;
            // 
            // timerLiveSync
            // 
            this.timerLiveSync.Tick += new System.EventHandler(this.timerLiveSync_Tick);
            // 
            // tablePanel2
            // 
            this.tablePanel2.Appearance.BorderColor = System.Drawing.Color.Black;
            this.tablePanel2.Appearance.Options.UseBorderColor = true;
            this.tablePanel2.Columns.AddRange(new DevExpress.Utils.Layout.TablePanelColumn[] {
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 55F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 50F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 50F)});
            this.tablePanel2.Controls.Add(this.labelControl4);
            this.tablePanel2.Controls.Add(this.lblapproved);
            this.tablePanel2.Controls.Add(this.labelControl6);
            this.tablePanel2.Controls.Add(this.lblrejected);
            this.tablePanel2.Controls.Add(this.lblforapproval);
            this.tablePanel2.Controls.Add(this.labelControl9);
            this.tablePanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tablePanel2.Location = new System.Drawing.Point(0, 0);
            this.tablePanel2.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.tablePanel2.Name = "tablePanel2";
            this.tablePanel2.Rows.AddRange(new DevExpress.Utils.Layout.TablePanelRow[] {
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 66.5F),
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 26F)});
            this.tablePanel2.ShowGrid = DevExpress.Utils.DefaultBoolean.False;
            this.tablePanel2.Size = new System.Drawing.Size(1105, 172);
            this.tablePanel2.TabIndex = 1;
            this.tablePanel2.Paint += new System.Windows.Forms.PaintEventHandler(this.tablePanel2_Paint);
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
            this.tablePanel2.SetColumn(this.labelControl4, 1);
            this.labelControl4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelControl4.Location = new System.Drawing.Point(393, 2);
            this.labelControl4.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.labelControl4.Name = "labelControl4";
            this.tablePanel2.SetRow(this.labelControl4, 0);
            this.labelControl4.Size = new System.Drawing.Size(354, 63);
            this.labelControl4.TabIndex = 17;
            this.labelControl4.Text = "REJECTED";
            // 
            // lblapproved
            // 
            this.lblapproved.Appearance.BackColor = System.Drawing.Color.Black;
            this.lblapproved.Appearance.Font = new System.Drawing.Font("Tahoma", 40F);
            this.lblapproved.Appearance.ForeColor = System.Drawing.Color.Lime;
            this.lblapproved.Appearance.Options.UseBackColor = true;
            this.lblapproved.Appearance.Options.UseFont = true;
            this.lblapproved.Appearance.Options.UseForeColor = true;
            this.lblapproved.Appearance.Options.UseTextOptions = true;
            this.lblapproved.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.lblapproved.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.lblapproved.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.lblapproved.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.tablePanel2.SetColumn(this.lblapproved, 2);
            this.lblapproved.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblapproved.Location = new System.Drawing.Point(750, 69);
            this.lblapproved.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.lblapproved.Name = "lblapproved";
            this.tablePanel2.SetRow(this.lblapproved, 1);
            this.lblapproved.Size = new System.Drawing.Size(354, 101);
            this.lblapproved.TabIndex = 17;
            this.lblapproved.Text = "0";
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
            this.tablePanel2.SetColumn(this.labelControl6, 2);
            this.labelControl6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelControl6.Location = new System.Drawing.Point(750, 2);
            this.labelControl6.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.labelControl6.Name = "labelControl6";
            this.tablePanel2.SetRow(this.labelControl6, 0);
            this.labelControl6.Size = new System.Drawing.Size(354, 63);
            this.labelControl6.TabIndex = 18;
            this.labelControl6.Text = "APPROVED";
            // 
            // lblrejected
            // 
            this.lblrejected.Appearance.BackColor = System.Drawing.Color.Black;
            this.lblrejected.Appearance.Font = new System.Drawing.Font("Tahoma", 40F);
            this.lblrejected.Appearance.ForeColor = System.Drawing.Color.Lime;
            this.lblrejected.Appearance.Options.UseBackColor = true;
            this.lblrejected.Appearance.Options.UseFont = true;
            this.lblrejected.Appearance.Options.UseForeColor = true;
            this.lblrejected.Appearance.Options.UseTextOptions = true;
            this.lblrejected.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.lblrejected.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.lblrejected.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.lblrejected.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.tablePanel2.SetColumn(this.lblrejected, 1);
            this.lblrejected.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblrejected.Location = new System.Drawing.Point(393, 69);
            this.lblrejected.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.lblrejected.Name = "lblrejected";
            this.tablePanel2.SetRow(this.lblrejected, 1);
            this.lblrejected.Size = new System.Drawing.Size(354, 101);
            this.lblrejected.TabIndex = 16;
            this.lblrejected.Text = "0";
            // 
            // lblforapproval
            // 
            this.lblforapproval.Appearance.BackColor = System.Drawing.Color.Black;
            this.lblforapproval.Appearance.Font = new System.Drawing.Font("Tahoma", 40F);
            this.lblforapproval.Appearance.ForeColor = System.Drawing.Color.Lime;
            this.lblforapproval.Appearance.Options.UseBackColor = true;
            this.lblforapproval.Appearance.Options.UseFont = true;
            this.lblforapproval.Appearance.Options.UseForeColor = true;
            this.lblforapproval.Appearance.Options.UseTextOptions = true;
            this.lblforapproval.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.lblforapproval.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.lblforapproval.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.lblforapproval.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.tablePanel2.SetColumn(this.lblforapproval, 0);
            this.lblforapproval.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblforapproval.Location = new System.Drawing.Point(1, 69);
            this.lblforapproval.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.lblforapproval.Name = "lblforapproval";
            this.tablePanel2.SetRow(this.lblforapproval, 1);
            this.lblforapproval.Size = new System.Drawing.Size(390, 101);
            this.lblforapproval.TabIndex = 15;
            this.lblforapproval.Text = "0";
            this.lblforapproval.Paint += new System.Windows.Forms.PaintEventHandler(this.lblforapproval_Paint);
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
            this.tablePanel2.SetColumn(this.labelControl9, 0);
            this.labelControl9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelControl9.Location = new System.Drawing.Point(1, 2);
            this.labelControl9.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.labelControl9.Name = "labelControl9";
            this.tablePanel2.SetRow(this.labelControl9, 0);
            this.labelControl9.Size = new System.Drawing.Size(390, 63);
            this.labelControl9.TabIndex = 16;
            this.labelControl9.Text = "FOR APPROVAL";
            // 
            // gridControlSalesNotDeducted
            // 
            this.tablePanel3.SetColumn(this.gridControlSalesNotDeducted, 1);
            this.gridControlSalesNotDeducted.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControlSalesNotDeducted.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.gridControlSalesNotDeducted.Font = new System.Drawing.Font("Tahoma", 12.8F);
            this.gridControlSalesNotDeducted.Location = new System.Drawing.Point(395, 64);
            this.gridControlSalesNotDeducted.MainView = this.gridViewSalesNotDeducted;
            this.gridControlSalesNotDeducted.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.gridControlSalesNotDeducted.Name = "gridControlSalesNotDeducted";
            this.tablePanel3.SetRow(this.gridControlSalesNotDeducted, 1);
            this.gridControlSalesNotDeducted.Size = new System.Drawing.Size(352, 318);
            this.gridControlSalesNotDeducted.TabIndex = 7;
            this.gridControlSalesNotDeducted.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewSalesNotDeducted});
            // 
            // gridViewSalesNotDeducted
            // 
            this.gridViewSalesNotDeducted.Appearance.GroupRow.Font = new System.Drawing.Font("Tahoma", 12.8F);
            this.gridViewSalesNotDeducted.Appearance.GroupRow.Options.UseFont = true;
            this.gridViewSalesNotDeducted.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 16.75F, System.Drawing.FontStyle.Bold);
            this.gridViewSalesNotDeducted.Appearance.HeaderPanel.Options.UseFont = true;
            this.gridViewSalesNotDeducted.Appearance.Row.Font = new System.Drawing.Font("Tahoma", 16.75F);
            this.gridViewSalesNotDeducted.Appearance.Row.Options.UseFont = true;
            this.gridViewSalesNotDeducted.DetailHeight = 431;
            this.gridViewSalesNotDeducted.GridControl = this.gridControlSalesNotDeducted;
            this.gridViewSalesNotDeducted.Name = "gridViewSalesNotDeducted";
            this.gridViewSalesNotDeducted.OptionsBehavior.Editable = false;
            this.gridViewSalesNotDeducted.OptionsBehavior.ReadOnly = true;
            this.gridViewSalesNotDeducted.OptionsPrint.AutoWidth = false;
            this.gridViewSalesNotDeducted.OptionsView.ColumnAutoWidth = false;
            this.gridViewSalesNotDeducted.OptionsView.RowAutoHeight = true;
            this.gridViewSalesNotDeducted.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;
            this.gridViewSalesNotDeducted.OptionsView.ShowGroupExpandCollapseButtons = false;
            this.gridViewSalesNotDeducted.OptionsView.ShowGroupPanel = false;
            this.gridViewSalesNotDeducted.OptionsView.ShowIndicator = false;
            // 
            // panelControl1
            // 
            this.panelControl1.Appearance.BackColor = System.Drawing.Color.Black;
            this.panelControl1.Appearance.Options.UseBackColor = true;
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl1.Controls.Add(this.tablePanel2);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(1105, 172);
            this.panelControl1.TabIndex = 18;
            // 
            // panelControl2
            // 
            this.panelControl2.Appearance.BackColor = System.Drawing.Color.Black;
            this.panelControl2.Appearance.Options.UseBackColor = true;
            this.panelControl2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl2.Controls.Add(this.tablePanel1);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl2.Location = new System.Drawing.Point(0, 172);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(1105, 173);
            this.panelControl2.TabIndex = 19;
            // 
            // panelControl3
            // 
            this.panelControl3.Appearance.BackColor = System.Drawing.Color.Black;
            this.panelControl3.Appearance.Options.UseBackColor = true;
            this.panelControl3.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl3.Controls.Add(this.tablePanel3);
            this.panelControl3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl3.Location = new System.Drawing.Point(0, 345);
            this.panelControl3.Name = "panelControl3";
            this.panelControl3.Size = new System.Drawing.Size(1105, 386);
            this.panelControl3.TabIndex = 20;
            // 
            // tablePanel3
            // 
            this.tablePanel3.Appearance.Options.UseBorderColor = true;
            this.tablePanel3.Columns.AddRange(new DevExpress.Utils.Layout.TablePanelColumn[] {
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 37.37F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 33.83F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 33.94F)});
            this.tablePanel3.Controls.Add(this.gridControlUploadedSales);
            this.tablePanel3.Controls.Add(this.labelControl8);
            this.tablePanel3.Controls.Add(this.gridControlForRcvng);
            this.tablePanel3.Controls.Add(this.labelControl7);
            this.tablePanel3.Controls.Add(this.labelControl5);
            this.tablePanel3.Controls.Add(this.gridControlSalesNotDeducted);
            this.tablePanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tablePanel3.Location = new System.Drawing.Point(0, 0);
            this.tablePanel3.Name = "tablePanel3";
            this.tablePanel3.Rows.AddRange(new DevExpress.Utils.Layout.TablePanelRow[] {
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 60.3996F),
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 26F)});
            this.tablePanel3.ShowGrid = DevExpress.Utils.DefaultBoolean.False;
            this.tablePanel3.Size = new System.Drawing.Size(1105, 386);
            this.tablePanel3.TabIndex = 0;
            this.tablePanel3.Paint += new System.Windows.Forms.PaintEventHandler(this.tablePanel3_Paint);
            // 
            // gridControlUploadedSales
            // 
            this.tablePanel3.SetColumn(this.gridControlUploadedSales, 2);
            this.gridControlUploadedSales.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControlUploadedSales.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.gridControlUploadedSales.Font = new System.Drawing.Font("Tahoma", 12.8F);
            this.gridControlUploadedSales.Location = new System.Drawing.Point(750, 64);
            this.gridControlUploadedSales.MainView = this.gridViewUploadedSales;
            this.gridControlUploadedSales.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.gridControlUploadedSales.Name = "gridControlUploadedSales";
            this.tablePanel3.SetRow(this.gridControlUploadedSales, 1);
            this.gridControlUploadedSales.Size = new System.Drawing.Size(353, 318);
            this.gridControlUploadedSales.TabIndex = 20;
            this.gridControlUploadedSales.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewUploadedSales});
            // 
            // gridViewUploadedSales
            // 
            this.gridViewUploadedSales.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 16.75F, System.Drawing.FontStyle.Bold);
            this.gridViewUploadedSales.Appearance.HeaderPanel.Options.UseFont = true;
            this.gridViewUploadedSales.Appearance.Row.Font = new System.Drawing.Font("Tahoma", 16.75F);
            this.gridViewUploadedSales.Appearance.Row.Options.UseFont = true;
            this.gridViewUploadedSales.DetailHeight = 431;
            this.gridViewUploadedSales.GridControl = this.gridControlUploadedSales;
            this.gridViewUploadedSales.Name = "gridViewUploadedSales";
            this.gridViewUploadedSales.OptionsBehavior.Editable = false;
            this.gridViewUploadedSales.OptionsBehavior.ReadOnly = true;
            this.gridViewUploadedSales.OptionsPrint.AutoWidth = false;
            this.gridViewUploadedSales.OptionsView.ColumnAutoWidth = false;
            this.gridViewUploadedSales.OptionsView.RowAutoHeight = true;
            this.gridViewUploadedSales.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;
            this.gridViewUploadedSales.OptionsView.ShowGroupExpandCollapseButtons = false;
            this.gridViewUploadedSales.OptionsView.ShowGroupPanel = false;
            this.gridViewUploadedSales.OptionsView.ShowIndicator = false;
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
            this.labelControl8.Location = new System.Drawing.Point(749, 2);
            this.labelControl8.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.labelControl8.Name = "labelControl8";
            this.tablePanel3.SetRow(this.labelControl8, 0);
            this.labelControl8.Size = new System.Drawing.Size(355, 56);
            this.labelControl8.TabIndex = 19;
            this.labelControl8.Text = "BRANCH POS UPLOADED SALES";
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
            this.labelControl7.Size = new System.Drawing.Size(391, 56);
            this.labelControl7.TabIndex = 18;
            this.labelControl7.Text = "BRANCH FOR RECEIVING PO";
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
            this.labelControl5.Location = new System.Drawing.Point(394, 2);
            this.labelControl5.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.labelControl5.Name = "labelControl5";
            this.tablePanel3.SetRow(this.labelControl5, 0);
            this.labelControl5.Size = new System.Drawing.Size(354, 56);
            this.labelControl5.TabIndex = 17;
            this.labelControl5.Text = "SALES  NOT DEDUCTED TO INV";
            // 
            // STSMonitoring
            // 
            this.Appearance.BackColor = System.Drawing.Color.Black;
            this.Appearance.Options.UseBackColor = true;
            this.Appearance.Options.UseBorderColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1105, 731);
            this.Controls.Add(this.panelControl3);
            this.Controls.Add(this.panelControl2);
            this.Controls.Add(this.panelControl1);
            this.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.Name = "STSMonitoring";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "STSMonitoring";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.STSMonitoring_FormClosing);
            this.Load += new System.EventHandler(this.STSMonitoring_Load);
            ((System.ComponentModel.ISupportInitialize)(this.tablePanel1)).EndInit();
            this.tablePanel1.ResumeLayout(false);
            this.tablePanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlForRcvng)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewForRcvng)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tablePanel2)).EndInit();
            this.tablePanel2.ResumeLayout(false);
            this.tablePanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlSalesNotDeducted)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewSalesNotDeducted)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).EndInit();
            this.panelControl3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tablePanel3)).EndInit();
            this.tablePanel3.ResumeLayout(false);
            this.tablePanel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlUploadedSales)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewUploadedSales)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.Utils.Layout.TablePanel tablePanel1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl lblcompleted;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl lblreceiving;
        private DevExpress.XtraEditors.LabelControl lblpending;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraGrid.GridControl gridControlForRcvng;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewForRcvng;
        private System.Windows.Forms.Timer timerLiveSync;
        private DevExpress.Utils.Layout.TablePanel tablePanel2;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl lblapproved;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.LabelControl lblrejected;
        private DevExpress.XtraEditors.LabelControl lblforapproval;
        private DevExpress.XtraEditors.LabelControl labelControl9;
        private DevExpress.XtraGrid.GridControl gridControlSalesNotDeducted;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewSalesNotDeducted;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.PanelControl panelControl3;
        private DevExpress.Utils.Layout.TablePanel tablePanel3;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        private DevExpress.XtraEditors.LabelControl labelControl8;
        private DevExpress.XtraGrid.GridControl gridControlUploadedSales;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewUploadedSales;
    }
}