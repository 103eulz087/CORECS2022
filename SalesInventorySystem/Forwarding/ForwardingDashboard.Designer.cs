namespace SalesInventorySystem.Forwarding
{
    partial class ForwardingDashboard
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.Pending = new System.Windows.Forms.TabPage();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.datetopending = new System.Windows.Forms.DateTimePicker();
            this.datefrompending = new System.Windows.Forms.DateTimePicker();
            this.delivered = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.gridControlDelivered = new DevExpress.XtraGrid.GridControl();
            this.gridViewDelivered = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.button2 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.datetodelivered = new System.Windows.Forms.DateTimePicker();
            this.datefromdelivered = new System.Windows.Forms.DateTimePicker();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.updateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.xtraTabControl1 = new DevExpress.XtraTab.XtraTabControl();
            this.xtraTabPage1 = new DevExpress.XtraTab.XtraTabPage();
            this.xtraTabPage2 = new DevExpress.XtraTab.XtraTabPage();
            this.tabControl2 = new System.Windows.Forms.TabControl();
            this.pendingtrip = new System.Windows.Forms.TabPage();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.gridControlpendingtrip = new DevExpress.XtraGrid.GridControl();
            this.gridViewpendingtrip = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.button3 = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.datetopendingtrip = new System.Windows.Forms.DateTimePicker();
            this.datefrompendingtrip = new System.Windows.Forms.DateTimePicker();
            this.deliveredtrip = new System.Windows.Forms.TabPage();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.gridControldelivtrip = new DevExpress.XtraGrid.GridControl();
            this.gridViewdelivtrip = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.button4 = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.datetodelivtrip = new System.Windows.Forms.DateTimePicker();
            this.datefromdelivtrip = new System.Windows.Forms.DateTimePicker();
            this.tabControl1.SuspendLayout();
            this.Pending.SuspendLayout();
            this.groupBox7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.delivered.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlDelivered)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewDelivered)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).BeginInit();
            this.xtraTabControl1.SuspendLayout();
            this.xtraTabPage1.SuspendLayout();
            this.xtraTabPage2.SuspendLayout();
            this.tabControl2.SuspendLayout();
            this.pendingtrip.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlpendingtrip)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewpendingtrip)).BeginInit();
            this.groupBox5.SuspendLayout();
            this.deliveredtrip.SuspendLayout();
            this.groupBox6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControldelivtrip)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewdelivtrip)).BeginInit();
            this.groupBox8.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.Pending);
            this.tabControl1.Controls.Add(this.delivered);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1235, 750);
            this.tabControl1.TabIndex = 3;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // Pending
            // 
            this.Pending.Controls.Add(this.groupBox7);
            this.Pending.Controls.Add(this.groupBox3);
            this.Pending.Location = new System.Drawing.Point(4, 28);
            this.Pending.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Pending.Name = "Pending";
            this.Pending.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Pending.Size = new System.Drawing.Size(1227, 718);
            this.Pending.TabIndex = 0;
            this.Pending.Text = "Pending";
            this.Pending.UseVisualStyleBackColor = true;
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.gridControl1);
            this.groupBox7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox7.Location = new System.Drawing.Point(3, 68);
            this.groupBox7.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox7.Size = new System.Drawing.Size(1221, 646);
            this.groupBox7.TabIndex = 4;
            this.groupBox7.TabStop = false;
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gridControl1.Location = new System.Drawing.Point(3, 24);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(1215, 618);
            this.gridControl1.TabIndex = 0;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            this.gridControl1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.gridControl1_MouseUp);
            // 
            // gridView1
            // 
            this.gridView1.Appearance.GroupRow.BackColor = System.Drawing.Color.ForestGreen;
            this.gridView1.Appearance.GroupRow.BackColor2 = System.Drawing.Color.LimeGreen;
            this.gridView1.Appearance.GroupRow.ForeColor = System.Drawing.Color.Gold;
            this.gridView1.Appearance.GroupRow.Options.UseBackColor = true;
            this.gridView1.Appearance.GroupRow.Options.UseForeColor = true;
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
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.button1);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.datetopending);
            this.groupBox3.Controls.Add(this.datefrompending);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox3.Font = new System.Drawing.Font("Tahoma", 7.75F);
            this.groupBox3.Location = new System.Drawing.Point(3, 4);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox3.Size = new System.Drawing.Size(1221, 64);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Filter Date";
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Tahoma", 8.75F);
            this.button1.Location = new System.Drawing.Point(409, 21);
            this.button1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(87, 28);
            this.button1.TabIndex = 4;
            this.button1.Text = "Show";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(14, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 21);
            this.label1.TabIndex = 0;
            this.label1.Text = "From:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(225, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 21);
            this.label2.TabIndex = 1;
            this.label2.Text = "To:";
            // 
            // datetopending
            // 
            this.datetopending.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.datetopending.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.datetopending.Location = new System.Drawing.Point(265, 21);
            this.datetopending.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.datetopending.Name = "datetopending";
            this.datetopending.Size = new System.Drawing.Size(137, 27);
            this.datetopending.TabIndex = 3;
            // 
            // datefrompending
            // 
            this.datefrompending.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.datefrompending.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.datefrompending.Location = new System.Drawing.Point(80, 21);
            this.datefrompending.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.datefrompending.Name = "datefrompending";
            this.datefrompending.Size = new System.Drawing.Size(137, 27);
            this.datefrompending.TabIndex = 2;
            // 
            // delivered
            // 
            this.delivered.Controls.Add(this.groupBox1);
            this.delivered.Controls.Add(this.groupBox2);
            this.delivered.Location = new System.Drawing.Point(4, 28);
            this.delivered.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.delivered.Name = "delivered";
            this.delivered.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.delivered.Size = new System.Drawing.Size(1227, 718);
            this.delivered.TabIndex = 5;
            this.delivered.Text = "Delivered";
            this.delivered.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.gridControlDelivered);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(3, 68);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Size = new System.Drawing.Size(1221, 646);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            // 
            // gridControlDelivered
            // 
            this.gridControlDelivered.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControlDelivered.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gridControlDelivered.Location = new System.Drawing.Point(3, 24);
            this.gridControlDelivered.MainView = this.gridViewDelivered;
            this.gridControlDelivered.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gridControlDelivered.Name = "gridControlDelivered";
            this.gridControlDelivered.Size = new System.Drawing.Size(1215, 618);
            this.gridControlDelivered.TabIndex = 0;
            this.gridControlDelivered.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewDelivered});
            // 
            // gridViewDelivered
            // 
            this.gridViewDelivered.Appearance.GroupRow.BackColor = System.Drawing.Color.ForestGreen;
            this.gridViewDelivered.Appearance.GroupRow.BackColor2 = System.Drawing.Color.LimeGreen;
            this.gridViewDelivered.Appearance.GroupRow.ForeColor = System.Drawing.Color.Gold;
            this.gridViewDelivered.Appearance.GroupRow.Options.UseBackColor = true;
            this.gridViewDelivered.Appearance.GroupRow.Options.UseForeColor = true;
            this.gridViewDelivered.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridViewDelivered.Appearance.HeaderPanel.Options.UseFont = true;
            this.gridViewDelivered.Appearance.Row.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridViewDelivered.Appearance.Row.Options.UseFont = true;
            this.gridViewDelivered.GridControl = this.gridControlDelivered;
            this.gridViewDelivered.Name = "gridViewDelivered";
            this.gridViewDelivered.OptionsBehavior.Editable = false;
            this.gridViewDelivered.OptionsBehavior.ReadOnly = true;
            this.gridViewDelivered.OptionsView.ColumnAutoWidth = false;
            this.gridViewDelivered.OptionsView.RowAutoHeight = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.button2);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.datetodelivered);
            this.groupBox2.Controls.Add(this.datefromdelivered);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox2.Font = new System.Drawing.Font("Tahoma", 7.75F);
            this.groupBox2.Location = new System.Drawing.Point(3, 4);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox2.Size = new System.Drawing.Size(1221, 64);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Filter Date";
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Tahoma", 8.75F);
            this.button2.Location = new System.Drawing.Point(409, 21);
            this.button2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(87, 28);
            this.button2.TabIndex = 4;
            this.button2.Text = "Show";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.label3.Location = new System.Drawing.Point(14, 27);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 21);
            this.label3.TabIndex = 0;
            this.label3.Text = "From:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.label4.Location = new System.Drawing.Point(225, 27);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(37, 21);
            this.label4.TabIndex = 1;
            this.label4.Text = "To:";
            // 
            // datetodelivered
            // 
            this.datetodelivered.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.datetodelivered.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.datetodelivered.Location = new System.Drawing.Point(265, 21);
            this.datetodelivered.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.datetodelivered.Name = "datetodelivered";
            this.datetodelivered.Size = new System.Drawing.Size(137, 27);
            this.datetodelivered.TabIndex = 3;
            // 
            // datefromdelivered
            // 
            this.datefromdelivered.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.datefromdelivered.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.datefromdelivered.Location = new System.Drawing.Point(80, 21);
            this.datefromdelivered.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.datefromdelivered.Name = "datefromdelivered";
            this.datefromdelivered.Size = new System.Drawing.Size(137, 27);
            this.datefromdelivered.TabIndex = 2;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.updateToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(128, 28);
            // 
            // updateToolStripMenuItem
            // 
            this.updateToolStripMenuItem.Name = "updateToolStripMenuItem";
            this.updateToolStripMenuItem.Size = new System.Drawing.Size(127, 24);
            this.updateToolStripMenuItem.Text = "Update";
            this.updateToolStripMenuItem.Click += new System.EventHandler(this.updateToolStripMenuItem_Click);
            // 
            // xtraTabControl1
            // 
            this.xtraTabControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 9.8F);
            this.xtraTabControl1.Appearance.Options.UseFont = true;
            this.xtraTabControl1.AppearancePage.Header.Font = new System.Drawing.Font("Tahoma", 15.8F, System.Drawing.FontStyle.Bold);
            this.xtraTabControl1.AppearancePage.Header.Options.UseFont = true;
            this.xtraTabControl1.AppearancePage.HeaderActive.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.xtraTabControl1.AppearancePage.HeaderActive.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.xtraTabControl1.AppearancePage.HeaderActive.Options.UseBackColor = true;
            this.xtraTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtraTabControl1.Location = new System.Drawing.Point(0, 0);
            this.xtraTabControl1.Name = "xtraTabControl1";
            this.xtraTabControl1.SelectedTabPage = this.xtraTabPage1;
            this.xtraTabControl1.Size = new System.Drawing.Size(1242, 799);
            this.xtraTabControl1.TabIndex = 4;
            this.xtraTabControl1.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtraTabPage1,
            this.xtraTabPage2});
            // 
            // xtraTabPage1
            // 
            this.xtraTabPage1.Controls.Add(this.tabControl1);
            this.xtraTabPage1.Name = "xtraTabPage1";
            this.xtraTabPage1.Size = new System.Drawing.Size(1235, 750);
            this.xtraTabPage1.Text = "PRIME OVER";
            // 
            // xtraTabPage2
            // 
            this.xtraTabPage2.Controls.Add(this.tabControl2);
            this.xtraTabPage2.Name = "xtraTabPage2";
            this.xtraTabPage2.Size = new System.Drawing.Size(1235, 750);
            this.xtraTabPage2.Text = "TRIP TICKETS";
            // 
            // tabControl2
            // 
            this.tabControl2.Controls.Add(this.pendingtrip);
            this.tabControl2.Controls.Add(this.deliveredtrip);
            this.tabControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl2.Location = new System.Drawing.Point(0, 0);
            this.tabControl2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabControl2.Name = "tabControl2";
            this.tabControl2.SelectedIndex = 0;
            this.tabControl2.Size = new System.Drawing.Size(1235, 750);
            this.tabControl2.TabIndex = 4;
            this.tabControl2.SelectedIndexChanged += new System.EventHandler(this.tabControl2_SelectedIndexChanged);
            // 
            // pendingtrip
            // 
            this.pendingtrip.Controls.Add(this.groupBox4);
            this.pendingtrip.Controls.Add(this.groupBox5);
            this.pendingtrip.Location = new System.Drawing.Point(4, 28);
            this.pendingtrip.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pendingtrip.Name = "pendingtrip";
            this.pendingtrip.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pendingtrip.Size = new System.Drawing.Size(1227, 718);
            this.pendingtrip.TabIndex = 0;
            this.pendingtrip.Text = "Pending";
            this.pendingtrip.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.gridControlpendingtrip);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox4.Location = new System.Drawing.Point(3, 68);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox4.Size = new System.Drawing.Size(1221, 646);
            this.groupBox4.TabIndex = 4;
            this.groupBox4.TabStop = false;
            // 
            // gridControlpendingtrip
            // 
            this.gridControlpendingtrip.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControlpendingtrip.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gridControlpendingtrip.Location = new System.Drawing.Point(3, 24);
            this.gridControlpendingtrip.MainView = this.gridViewpendingtrip;
            this.gridControlpendingtrip.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gridControlpendingtrip.Name = "gridControlpendingtrip";
            this.gridControlpendingtrip.Size = new System.Drawing.Size(1215, 618);
            this.gridControlpendingtrip.TabIndex = 0;
            this.gridControlpendingtrip.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewpendingtrip});
            // 
            // gridViewpendingtrip
            // 
            this.gridViewpendingtrip.Appearance.GroupRow.BackColor = System.Drawing.Color.ForestGreen;
            this.gridViewpendingtrip.Appearance.GroupRow.BackColor2 = System.Drawing.Color.LimeGreen;
            this.gridViewpendingtrip.Appearance.GroupRow.ForeColor = System.Drawing.Color.Gold;
            this.gridViewpendingtrip.Appearance.GroupRow.Options.UseBackColor = true;
            this.gridViewpendingtrip.Appearance.GroupRow.Options.UseForeColor = true;
            this.gridViewpendingtrip.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridViewpendingtrip.Appearance.HeaderPanel.Options.UseFont = true;
            this.gridViewpendingtrip.Appearance.Row.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridViewpendingtrip.Appearance.Row.Options.UseFont = true;
            this.gridViewpendingtrip.GridControl = this.gridControlpendingtrip;
            this.gridViewpendingtrip.Name = "gridViewpendingtrip";
            this.gridViewpendingtrip.OptionsBehavior.Editable = false;
            this.gridViewpendingtrip.OptionsBehavior.ReadOnly = true;
            this.gridViewpendingtrip.OptionsView.ColumnAutoWidth = false;
            this.gridViewpendingtrip.OptionsView.RowAutoHeight = true;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.button3);
            this.groupBox5.Controls.Add(this.label5);
            this.groupBox5.Controls.Add(this.label6);
            this.groupBox5.Controls.Add(this.datetopendingtrip);
            this.groupBox5.Controls.Add(this.datefrompendingtrip);
            this.groupBox5.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox5.Font = new System.Drawing.Font("Tahoma", 7.75F);
            this.groupBox5.Location = new System.Drawing.Point(3, 4);
            this.groupBox5.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox5.Size = new System.Drawing.Size(1221, 64);
            this.groupBox5.TabIndex = 3;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Filter Date";
            // 
            // button3
            // 
            this.button3.Font = new System.Drawing.Font("Tahoma", 8.75F);
            this.button3.Location = new System.Drawing.Point(409, 21);
            this.button3.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(87, 28);
            this.button3.TabIndex = 4;
            this.button3.Text = "Show";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.label5.Location = new System.Drawing.Point(14, 27);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(60, 21);
            this.label5.TabIndex = 0;
            this.label5.Text = "From:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.label6.Location = new System.Drawing.Point(225, 27);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(37, 21);
            this.label6.TabIndex = 1;
            this.label6.Text = "To:";
            // 
            // datetopendingtrip
            // 
            this.datetopendingtrip.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.datetopendingtrip.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.datetopendingtrip.Location = new System.Drawing.Point(265, 21);
            this.datetopendingtrip.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.datetopendingtrip.Name = "datetopendingtrip";
            this.datetopendingtrip.Size = new System.Drawing.Size(137, 27);
            this.datetopendingtrip.TabIndex = 3;
            // 
            // datefrompendingtrip
            // 
            this.datefrompendingtrip.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.datefrompendingtrip.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.datefrompendingtrip.Location = new System.Drawing.Point(80, 21);
            this.datefrompendingtrip.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.datefrompendingtrip.Name = "datefrompendingtrip";
            this.datefrompendingtrip.Size = new System.Drawing.Size(137, 27);
            this.datefrompendingtrip.TabIndex = 2;
            // 
            // deliveredtrip
            // 
            this.deliveredtrip.Controls.Add(this.groupBox6);
            this.deliveredtrip.Controls.Add(this.groupBox8);
            this.deliveredtrip.Location = new System.Drawing.Point(4, 28);
            this.deliveredtrip.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.deliveredtrip.Name = "deliveredtrip";
            this.deliveredtrip.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.deliveredtrip.Size = new System.Drawing.Size(1227, 718);
            this.deliveredtrip.TabIndex = 5;
            this.deliveredtrip.Text = "Delivered";
            this.deliveredtrip.UseVisualStyleBackColor = true;
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.gridControldelivtrip);
            this.groupBox6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox6.Location = new System.Drawing.Point(3, 68);
            this.groupBox6.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox6.Size = new System.Drawing.Size(1221, 646);
            this.groupBox6.TabIndex = 6;
            this.groupBox6.TabStop = false;
            // 
            // gridControldelivtrip
            // 
            this.gridControldelivtrip.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControldelivtrip.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gridControldelivtrip.Location = new System.Drawing.Point(3, 24);
            this.gridControldelivtrip.MainView = this.gridViewdelivtrip;
            this.gridControldelivtrip.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gridControldelivtrip.Name = "gridControldelivtrip";
            this.gridControldelivtrip.Size = new System.Drawing.Size(1215, 618);
            this.gridControldelivtrip.TabIndex = 0;
            this.gridControldelivtrip.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewdelivtrip});
            // 
            // gridViewdelivtrip
            // 
            this.gridViewdelivtrip.Appearance.GroupRow.BackColor = System.Drawing.Color.ForestGreen;
            this.gridViewdelivtrip.Appearance.GroupRow.BackColor2 = System.Drawing.Color.LimeGreen;
            this.gridViewdelivtrip.Appearance.GroupRow.ForeColor = System.Drawing.Color.Gold;
            this.gridViewdelivtrip.Appearance.GroupRow.Options.UseBackColor = true;
            this.gridViewdelivtrip.Appearance.GroupRow.Options.UseForeColor = true;
            this.gridViewdelivtrip.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridViewdelivtrip.Appearance.HeaderPanel.Options.UseFont = true;
            this.gridViewdelivtrip.Appearance.Row.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridViewdelivtrip.Appearance.Row.Options.UseFont = true;
            this.gridViewdelivtrip.GridControl = this.gridControldelivtrip;
            this.gridViewdelivtrip.Name = "gridViewdelivtrip";
            this.gridViewdelivtrip.OptionsBehavior.Editable = false;
            this.gridViewdelivtrip.OptionsBehavior.ReadOnly = true;
            this.gridViewdelivtrip.OptionsView.ColumnAutoWidth = false;
            this.gridViewdelivtrip.OptionsView.RowAutoHeight = true;
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.button4);
            this.groupBox8.Controls.Add(this.label7);
            this.groupBox8.Controls.Add(this.label8);
            this.groupBox8.Controls.Add(this.datetodelivtrip);
            this.groupBox8.Controls.Add(this.datefromdelivtrip);
            this.groupBox8.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox8.Font = new System.Drawing.Font("Tahoma", 7.75F);
            this.groupBox8.Location = new System.Drawing.Point(3, 4);
            this.groupBox8.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox8.Size = new System.Drawing.Size(1221, 64);
            this.groupBox8.TabIndex = 5;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "Filter Date";
            // 
            // button4
            // 
            this.button4.Font = new System.Drawing.Font("Tahoma", 8.75F);
            this.button4.Location = new System.Drawing.Point(409, 21);
            this.button4.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(87, 28);
            this.button4.TabIndex = 4;
            this.button4.Text = "Show";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.label7.Location = new System.Drawing.Point(14, 27);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(60, 21);
            this.label7.TabIndex = 0;
            this.label7.Text = "From:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.label8.Location = new System.Drawing.Point(225, 27);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(37, 21);
            this.label8.TabIndex = 1;
            this.label8.Text = "To:";
            // 
            // datetodelivtrip
            // 
            this.datetodelivtrip.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.datetodelivtrip.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.datetodelivtrip.Location = new System.Drawing.Point(265, 21);
            this.datetodelivtrip.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.datetodelivtrip.Name = "datetodelivtrip";
            this.datetodelivtrip.Size = new System.Drawing.Size(137, 27);
            this.datetodelivtrip.TabIndex = 3;
            // 
            // datefromdelivtrip
            // 
            this.datefromdelivtrip.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.datefromdelivtrip.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.datefromdelivtrip.Location = new System.Drawing.Point(80, 21);
            this.datefromdelivtrip.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.datefromdelivtrip.Name = "datefromdelivtrip";
            this.datefromdelivtrip.Size = new System.Drawing.Size(137, 27);
            this.datefromdelivtrip.TabIndex = 2;
            // 
            // ForwardingDashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1242, 799);
            this.Controls.Add(this.xtraTabControl1);
            this.Name = "ForwardingDashboard";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ForwardingDashboard";
            this.Load += new System.EventHandler(this.ForwardingDashboard_Load);
            this.tabControl1.ResumeLayout(false);
            this.Pending.ResumeLayout(false);
            this.groupBox7.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.delivered.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControlDelivered)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewDelivered)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).EndInit();
            this.xtraTabControl1.ResumeLayout(false);
            this.xtraTabPage1.ResumeLayout(false);
            this.xtraTabPage2.ResumeLayout(false);
            this.tabControl2.ResumeLayout(false);
            this.pendingtrip.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControlpendingtrip)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewpendingtrip)).EndInit();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.deliveredtrip.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControldelivtrip)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewdelivtrip)).EndInit();
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage Pending;
        private System.Windows.Forms.TabPage delivered;
        private System.Windows.Forms.GroupBox groupBox7;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker datetopending;
        private System.Windows.Forms.DateTimePicker datefrompending;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem updateToolStripMenuItem;
        private System.Windows.Forms.GroupBox groupBox1;
        private DevExpress.XtraGrid.GridControl gridControlDelivered;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewDelivered;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker datetodelivered;
        private System.Windows.Forms.DateTimePicker datefromdelivered;
        private DevExpress.XtraTab.XtraTabControl xtraTabControl1;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage1;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage2;
        private System.Windows.Forms.TabControl tabControl2;
        private System.Windows.Forms.TabPage pendingtrip;
        private System.Windows.Forms.GroupBox groupBox4;
        private DevExpress.XtraGrid.GridControl gridControlpendingtrip;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewpendingtrip;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DateTimePicker datetopendingtrip;
        private System.Windows.Forms.DateTimePicker datefrompendingtrip;
        private System.Windows.Forms.TabPage deliveredtrip;
        private System.Windows.Forms.GroupBox groupBox6;
        private DevExpress.XtraGrid.GridControl gridControldelivtrip;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewdelivtrip;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.DateTimePicker datetodelivtrip;
        private System.Windows.Forms.DateTimePicker datefromdelivtrip;
    }
}