namespace SalesInventorySystem.HOFormsDevEx
{
    partial class AccountReconDevEx
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
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.bandedGridView1 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridView();
            this.advBandedGridView1 = new DevExpress.XtraGrid.Views.BandedGrid.AdvBandedGridView();
            this.gridBand1 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.button2 = new System.Windows.Forms.Button();
            this.clearedcred = new System.Windows.Forms.TextBox();
            this.cleareddeb = new System.Windows.Forms.TextBox();
            this.unclearedcred = new System.Windows.Forms.TextBox();
            this.uncleareddeb = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.clea = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.dateTo = new DevExpress.XtraEditors.DateEdit();
            this.label3 = new System.Windows.Forms.Label();
            this.dateFrom = new DevExpress.XtraEditors.DateEdit();
            this.label2 = new System.Windows.Forms.Label();
            this.searchLookUpEdit1 = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.gridview = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.taggedAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.unclearedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewTicketDetailsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtglbalance = new System.Windows.Forms.TextBox();
            this.txtunclearedchecks = new System.Windows.Forms.TextBox();
            this.txtoutstandingchecks = new System.Windows.Forms.TextBox();
            this.txtdepositintransit = new System.Windows.Forms.TextBox();
            this.txtbalance = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bandedGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.advBandedGridView1)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dateTo.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateTo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateFrom.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateFrom.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridview)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(4);
            this.gridControl1.Location = new System.Drawing.Point(0, 106);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Margin = new System.Windows.Forms.Padding(4);
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(1348, 600);
            this.gridControl1.TabIndex = 5;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1,
            this.bandedGridView1,
            this.advBandedGridView1});
            this.gridControl1.Click += new System.EventHandler(this.gridControl1_Click);
            this.gridControl1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.gridControl1_MouseUp);
            // 
            // gridView1
            // 
            this.gridView1.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridView1.Appearance.HeaderPanel.Options.UseFont = true;
            this.gridView1.Appearance.Row.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridView1.Appearance.Row.Options.UseFont = true;
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsBehavior.ReadOnly = true;
            this.gridView1.OptionsView.ColumnAutoWidth = false;
            this.gridView1.OptionsView.RowAutoHeight = true;
            this.gridView1.OptionsView.ShowFooter = true;
            this.gridView1.RowStyle += new DevExpress.XtraGrid.Views.Grid.RowStyleEventHandler(this.gridView1_RowStyle);
            // 
            // bandedGridView1
            // 
            this.bandedGridView1.GridControl = this.gridControl1;
            this.bandedGridView1.Name = "bandedGridView1";
            // 
            // advBandedGridView1
            // 
            this.advBandedGridView1.Bands.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] {
            this.gridBand1});
            this.advBandedGridView1.GridControl = this.gridControl1;
            this.advBandedGridView1.Name = "advBandedGridView1";
            // 
            // gridBand1
            // 
            this.gridBand1.Caption = "gridBand1";
            this.gridBand1.Name = "gridBand1";
            this.gridBand1.VisibleIndex = 0;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.button3);
            this.groupBox3.Controls.Add(this.button2);
            this.groupBox3.Controls.Add(this.clearedcred);
            this.groupBox3.Controls.Add(this.cleareddeb);
            this.groupBox3.Controls.Add(this.unclearedcred);
            this.groupBox3.Controls.Add(this.uncleareddeb);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.clea);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.dateTo);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.dateFrom);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.searchLookUpEdit1);
            this.groupBox3.Controls.Add(this.button1);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox3.Font = new System.Drawing.Font("Tahoma", 7.75F);
            this.groupBox3.Location = new System.Drawing.Point(0, 0);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox3.Size = new System.Drawing.Size(1348, 106);
            this.groupBox3.TabIndex = 6;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Filter Date";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(592, 25);
            this.button2.Margin = new System.Windows.Forms.Padding(4);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(145, 63);
            this.button2.TabIndex = 454;
            this.button2.Text = "Export to Excel";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // clearedcred
            // 
            this.clearedcred.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.clearedcred.Location = new System.Drawing.Point(1390, 58);
            this.clearedcred.Margin = new System.Windows.Forms.Padding(4);
            this.clearedcred.MaxLength = 10;
            this.clearedcred.Name = "clearedcred";
            this.clearedcred.Size = new System.Drawing.Size(116, 27);
            this.clearedcred.TabIndex = 453;
            this.clearedcred.Text = "0";
            this.clearedcred.Visible = false;
            // 
            // cleareddeb
            // 
            this.cleareddeb.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.cleareddeb.Location = new System.Drawing.Point(1390, 23);
            this.cleareddeb.Margin = new System.Windows.Forms.Padding(4);
            this.cleareddeb.MaxLength = 10;
            this.cleareddeb.Name = "cleareddeb";
            this.cleareddeb.Size = new System.Drawing.Size(116, 27);
            this.cleareddeb.TabIndex = 452;
            this.cleareddeb.Text = "0";
            this.cleareddeb.Visible = false;
            // 
            // unclearedcred
            // 
            this.unclearedcred.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.unclearedcred.Location = new System.Drawing.Point(1037, 58);
            this.unclearedcred.Margin = new System.Windows.Forms.Padding(4);
            this.unclearedcred.MaxLength = 10;
            this.unclearedcred.Name = "unclearedcred";
            this.unclearedcred.Size = new System.Drawing.Size(117, 27);
            this.unclearedcred.TabIndex = 451;
            this.unclearedcred.Text = "0";
            this.unclearedcred.Visible = false;
            // 
            // uncleareddeb
            // 
            this.uncleareddeb.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.uncleareddeb.Location = new System.Drawing.Point(1037, 23);
            this.uncleareddeb.Margin = new System.Windows.Forms.Padding(4);
            this.uncleareddeb.MaxLength = 10;
            this.uncleareddeb.Name = "uncleareddeb";
            this.uncleareddeb.Size = new System.Drawing.Size(117, 27);
            this.uncleareddeb.TabIndex = 450;
            this.uncleareddeb.Text = "0";
            this.uncleareddeb.Visible = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Tahoma", 11.25F);
            this.label7.Location = new System.Drawing.Point(1161, 60);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(246, 23);
            this.label7.TabIndex = 449;
            this.label7.Text = "Cleared DepositBankCredits:";
            this.label7.Visible = false;
            // 
            // clea
            // 
            this.clea.AutoSize = true;
            this.clea.Font = new System.Drawing.Font("Tahoma", 11.25F);
            this.clea.Location = new System.Drawing.Point(1161, 27);
            this.clea.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.clea.Name = "clea";
            this.clea.Size = new System.Drawing.Size(229, 23);
            this.clea.TabIndex = 448;
            this.clea.Text = "Cleared CheckBankDebits:";
            this.clea.Visible = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Tahoma", 11.25F);
            this.label5.Location = new System.Drawing.Point(799, 59);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(267, 23);
            this.label5.TabIndex = 447;
            this.label5.Text = "Uncleared DepositBankCredits:";
            this.label5.Visible = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 11.25F);
            this.label4.Location = new System.Drawing.Point(799, 26);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(250, 23);
            this.label4.TabIndex = 446;
            this.label4.Text = "Uncleared CheckBankDebits:";
            this.label4.Visible = false;
            // 
            // dateTo
            // 
            this.dateTo.EditValue = null;
            this.dateTo.Location = new System.Drawing.Point(419, 23);
            this.dateTo.Margin = new System.Windows.Forms.Padding(4);
            this.dateTo.Name = "dateTo";
            this.dateTo.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.dateTo.Properties.Appearance.Options.UseFont = true;
            this.dateTo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateTo.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateTo.Size = new System.Drawing.Size(166, 28);
            this.dateTo.TabIndex = 445;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 11.25F);
            this.label3.Location = new System.Drawing.Point(376, 27);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 23);
            this.label3.TabIndex = 444;
            this.label3.Text = "To:";
            // 
            // dateFrom
            // 
            this.dateFrom.EditValue = null;
            this.dateFrom.Location = new System.Drawing.Point(203, 25);
            this.dateFrom.Margin = new System.Windows.Forms.Padding(4);
            this.dateFrom.Name = "dateFrom";
            this.dateFrom.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.dateFrom.Properties.Appearance.Options.UseFont = true;
            this.dateFrom.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateFrom.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateFrom.Size = new System.Drawing.Size(166, 28);
            this.dateFrom.TabIndex = 443;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 11.25F);
            this.label2.Location = new System.Drawing.Point(14, 28);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(197, 23);
            this.label2.TabIndex = 442;
            this.label2.Text = "Statement Date From:";
            // 
            // searchLookUpEdit1
            // 
            this.searchLookUpEdit1.Location = new System.Drawing.Point(203, 58);
            this.searchLookUpEdit1.Margin = new System.Windows.Forms.Padding(4);
            this.searchLookUpEdit1.Name = "searchLookUpEdit1";
            this.searchLookUpEdit1.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10.25F);
            this.searchLookUpEdit1.Properties.Appearance.Options.UseFont = true;
            this.searchLookUpEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.searchLookUpEdit1.Properties.DisplayMember = "SupplierName";
            this.searchLookUpEdit1.Properties.ValueMember = "SupplierName";
            this.searchLookUpEdit1.Properties.View = this.gridview;
            this.searchLookUpEdit1.Size = new System.Drawing.Size(166, 28);
            this.searchLookUpEdit1.TabIndex = 441;
            // 
            // gridview
            // 
            this.gridview.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridview.Name = "gridview";
            this.gridview.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridview.OptionsView.ShowGroupPanel = false;
            // 
            // button1
            // 
            this.button1.Image = global::SalesInventorySystem.Properties.Resources.GenerateData_32x32;
            this.button1.Location = new System.Drawing.Point(419, 57);
            this.button1.Margin = new System.Windows.Forms.Padding(4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(166, 31);
            this.button1.TabIndex = 4;
            this.button1.Text = "Show";
            this.button1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 11.25F);
            this.label1.Location = new System.Drawing.Point(14, 62);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(191, 23);
            this.label1.TabIndex = 0;
            this.label1.Text = "Account to Reconcile:";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(40, 40);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.taggedAsToolStripMenuItem,
            this.viewTicketDetailsToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(200, 52);
            // 
            // taggedAsToolStripMenuItem
            // 
            this.taggedAsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.clearedToolStripMenuItem,
            this.unclearedToolStripMenuItem});
            this.taggedAsToolStripMenuItem.Name = "taggedAsToolStripMenuItem";
            this.taggedAsToolStripMenuItem.Size = new System.Drawing.Size(199, 24);
            this.taggedAsToolStripMenuItem.Text = "Tagged as";
            // 
            // clearedToolStripMenuItem
            // 
            this.clearedToolStripMenuItem.Name = "clearedToolStripMenuItem";
            this.clearedToolStripMenuItem.Size = new System.Drawing.Size(181, 26);
            this.clearedToolStripMenuItem.Text = "Cleared";
            this.clearedToolStripMenuItem.Click += new System.EventHandler(this.clearedToolStripMenuItem_Click);
            // 
            // unclearedToolStripMenuItem
            // 
            this.unclearedToolStripMenuItem.Name = "unclearedToolStripMenuItem";
            this.unclearedToolStripMenuItem.Size = new System.Drawing.Size(181, 26);
            this.unclearedToolStripMenuItem.Text = "Uncleared";
            this.unclearedToolStripMenuItem.Click += new System.EventHandler(this.unclearedToolStripMenuItem_Click);
            // 
            // viewTicketDetailsToolStripMenuItem
            // 
            this.viewTicketDetailsToolStripMenuItem.Name = "viewTicketDetailsToolStripMenuItem";
            this.viewTicketDetailsToolStripMenuItem.Size = new System.Drawing.Size(199, 24);
            this.viewTicketDetailsToolStripMenuItem.Text = "View TicketDetails";
            this.viewTicketDetailsToolStripMenuItem.Click += new System.EventHandler(this.viewTicketDetailsToolStripMenuItem_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtglbalance);
            this.groupBox1.Controls.Add(this.txtunclearedchecks);
            this.groupBox1.Controls.Add(this.txtoutstandingchecks);
            this.groupBox1.Controls.Add(this.txtdepositintransit);
            this.groupBox1.Controls.Add(this.txtbalance);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox1.Location = new System.Drawing.Point(0, 706);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(1348, 71);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            // 
            // txtglbalance
            // 
            this.txtglbalance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.txtglbalance.Location = new System.Drawing.Point(1199, 28);
            this.txtglbalance.Margin = new System.Windows.Forms.Padding(4);
            this.txtglbalance.MaxLength = 10;
            this.txtglbalance.Name = "txtglbalance";
            this.txtglbalance.Size = new System.Drawing.Size(98, 27);
            this.txtglbalance.TabIndex = 459;
            this.txtglbalance.Text = "0";
            // 
            // txtunclearedchecks
            // 
            this.txtunclearedchecks.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.txtunclearedchecks.Location = new System.Drawing.Point(976, 30);
            this.txtunclearedchecks.Margin = new System.Windows.Forms.Padding(4);
            this.txtunclearedchecks.MaxLength = 10;
            this.txtunclearedchecks.Name = "txtunclearedchecks";
            this.txtunclearedchecks.Size = new System.Drawing.Size(98, 27);
            this.txtunclearedchecks.TabIndex = 458;
            this.txtunclearedchecks.Text = "0";
            // 
            // txtoutstandingchecks
            // 
            this.txtoutstandingchecks.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.txtoutstandingchecks.Location = new System.Drawing.Point(680, 28);
            this.txtoutstandingchecks.Margin = new System.Windows.Forms.Padding(4);
            this.txtoutstandingchecks.MaxLength = 10;
            this.txtoutstandingchecks.Name = "txtoutstandingchecks";
            this.txtoutstandingchecks.Size = new System.Drawing.Size(98, 27);
            this.txtoutstandingchecks.TabIndex = 457;
            this.txtoutstandingchecks.Text = "0";
            // 
            // txtdepositintransit
            // 
            this.txtdepositintransit.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.txtdepositintransit.Location = new System.Drawing.Point(379, 30);
            this.txtdepositintransit.Margin = new System.Windows.Forms.Padding(4);
            this.txtdepositintransit.MaxLength = 10;
            this.txtdepositintransit.Name = "txtdepositintransit";
            this.txtdepositintransit.Size = new System.Drawing.Size(98, 27);
            this.txtdepositintransit.TabIndex = 456;
            this.txtdepositintransit.Text = "0";
            // 
            // txtbalance
            // 
            this.txtbalance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.txtbalance.Location = new System.Drawing.Point(105, 30);
            this.txtbalance.Margin = new System.Windows.Forms.Padding(4);
            this.txtbalance.MaxLength = 10;
            this.txtbalance.Name = "txtbalance";
            this.txtbalance.Size = new System.Drawing.Size(98, 27);
            this.txtbalance.TabIndex = 455;
            this.txtbalance.Text = "0";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold);
            this.label11.Location = new System.Drawing.Point(1083, 31);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(122, 23);
            this.label11.TabIndex = 454;
            this.label11.Text = "GL Balance:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold);
            this.label10.Location = new System.Drawing.Point(799, 31);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(185, 23);
            this.label10.TabIndex = 453;
            this.label10.Text = "Uncleared Checks:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold);
            this.label9.Location = new System.Drawing.Point(485, 31);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(208, 23);
            this.label9.TabIndex = 452;
            this.label9.Text = "Outstanding Checks:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold);
            this.label8.Location = new System.Drawing.Point(211, 31);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(190, 23);
            this.label8.TabIndex = 451;
            this.label8.Text = "Deposit In Transit:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold);
            this.label6.Location = new System.Drawing.Point(14, 31);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(91, 23);
            this.label6.TabIndex = 450;
            this.label6.Text = "Balance:";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(745, 26);
            this.button3.Margin = new System.Windows.Forms.Padding(4);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(53, 63);
            this.button3.TabIndex = 455;
            this.button3.Text = "Lock";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // AccountReconDevEx
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1348, 777);
            this.Controls.Add(this.gridControl1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox3);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "AccountReconDevEx";
            this.Text = "AccountReconDevEx";
            this.Load += new System.EventHandler(this.AccountReconDevEx_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bandedGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.advBandedGridView1)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dateTo.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateTo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateFrom.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateFrom.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridview)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridView bandedGridView1;
        private DevExpress.XtraGrid.Views.BandedGrid.AdvBandedGridView advBandedGridView1;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraEditors.SearchLookUpEdit searchLookUpEdit1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridview;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem taggedAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearedToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem unclearedToolStripMenuItem;
        private DevExpress.XtraEditors.DateEdit dateFrom;
        private DevExpress.XtraEditors.DateEdit dateTo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label clea;
        private System.Windows.Forms.TextBox clearedcred;
        private System.Windows.Forms.TextBox cleareddeb;
        private System.Windows.Forms.TextBox unclearedcred;
        private System.Windows.Forms.TextBox uncleareddeb;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtglbalance;
        private System.Windows.Forms.TextBox txtunclearedchecks;
        private System.Windows.Forms.TextBox txtoutstandingchecks;
        private System.Windows.Forms.TextBox txtdepositintransit;
        private System.Windows.Forms.TextBox txtbalance;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ToolStripMenuItem viewTicketDetailsToolStripMenuItem;
        private System.Windows.Forms.Button button3;
    }
}