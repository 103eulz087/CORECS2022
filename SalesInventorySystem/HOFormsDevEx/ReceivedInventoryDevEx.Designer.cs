namespace SalesInventorySystem.HOFormsDevEx
{
    partial class ReceivedInventoryDevEx
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
            this.NewShipment = new System.Windows.Forms.TabPage();
            this.gridControl2 = new DevExpress.XtraGrid.GridControl();
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.ExistingShipment = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.txtdateto = new DevExpress.XtraEditors.DateEdit();
            this.txtdatefrom = new DevExpress.XtraEditors.DateEdit();
            this.label1 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.manualInventoryEntryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.uploadBatchInventoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.localConnectionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.liveConnectionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fromLocalConnectionWithoutPOToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.singleModeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.batchModeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControl1.SuspendLayout();
            this.NewShipment.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            this.ExistingShipment.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtdateto.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtdateto.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtdatefrom.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtdatefrom.Properties)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.NewShipment);
            this.tabControl1.Controls.Add(this.ExistingShipment);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1402, 822);
            this.tabControl1.TabIndex = 6;
            // 
            // NewShipment
            // 
            this.NewShipment.Controls.Add(this.gridControl2);
            this.NewShipment.Location = new System.Drawing.Point(4, 27);
            this.NewShipment.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.NewShipment.Name = "NewShipment";
            this.NewShipment.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.NewShipment.Size = new System.Drawing.Size(1394, 791);
            this.NewShipment.TabIndex = 0;
            this.NewShipment.Text = "For Delivery";
            this.NewShipment.UseVisualStyleBackColor = true;
            // 
            // gridControl2
            // 
            this.gridControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl2.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gridControl2.Location = new System.Drawing.Point(3, 4);
            this.gridControl2.MainView = this.gridView2;
            this.gridControl2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gridControl2.Name = "gridControl2";
            this.gridControl2.Size = new System.Drawing.Size(1388, 783);
            this.gridControl2.TabIndex = 4;
            this.gridControl2.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView2});
            this.gridControl2.MouseUp += new System.Windows.Forms.MouseEventHandler(this.gridControl2_MouseUp);
            // 
            // gridView2
            // 
            this.gridView2.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridView2.Appearance.HeaderPanel.Options.UseFont = true;
            this.gridView2.Appearance.Row.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridView2.Appearance.Row.Options.UseFont = true;
            this.gridView2.DetailHeight = 431;
            this.gridView2.GridControl = this.gridControl2;
            this.gridView2.Name = "gridView2";
            this.gridView2.OptionsBehavior.Editable = false;
            this.gridView2.OptionsBehavior.ReadOnly = true;
            this.gridView2.OptionsView.ColumnAutoWidth = false;
            this.gridView2.OptionsView.RowAutoHeight = true;
            this.gridView2.DoubleClick += new System.EventHandler(this.gridView2_DoubleClick);
            // 
            // ExistingShipment
            // 
            this.ExistingShipment.Controls.Add(this.groupBox2);
            this.ExistingShipment.Controls.Add(this.groupBox1);
            this.ExistingShipment.Location = new System.Drawing.Point(4, 27);
            this.ExistingShipment.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ExistingShipment.Name = "ExistingShipment";
            this.ExistingShipment.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ExistingShipment.Size = new System.Drawing.Size(1394, 791);
            this.ExistingShipment.TabIndex = 1;
            this.ExistingShipment.Text = "ExistingShipment";
            this.ExistingShipment.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.gridControl1);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(3, 104);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox2.Size = new System.Drawing.Size(1388, 683);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gridControl1.Location = new System.Drawing.Point(3, 23);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(1382, 656);
            this.gridControl1.TabIndex = 5;
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
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Controls.Add(this.checkBox1);
            this.groupBox1.Controls.Add(this.txtdateto);
            this.groupBox1.Controls.Add(this.txtdatefrom);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(3, 4);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Size = new System.Drawing.Size(1388, 100);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(164, 57);
            this.button1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(92, 33);
            this.button1.TabIndex = 441;
            this.button1.Text = "Clear";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(65, 57);
            this.button2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(92, 33);
            this.button2.TabIndex = 439;
            this.button2.Text = "Search";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.checkBox1.Location = new System.Drawing.Point(400, 25);
            this.checkBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(151, 25);
            this.checkBox1.TabIndex = 435;
            this.checkBox1.Text = "All Transactions";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // txtdateto
            // 
            this.txtdateto.EditValue = null;
            this.txtdateto.Location = new System.Drawing.Point(252, 20);
            this.txtdateto.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtdateto.Name = "txtdateto";
            this.txtdateto.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10.25F);
            this.txtdateto.Properties.Appearance.Options.UseFont = true;
            this.txtdateto.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtdateto.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtdateto.Size = new System.Drawing.Size(141, 28);
            this.txtdateto.TabIndex = 434;
            // 
            // txtdatefrom
            // 
            this.txtdatefrom.EditValue = null;
            this.txtdatefrom.Location = new System.Drawing.Point(65, 20);
            this.txtdatefrom.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtdatefrom.Name = "txtdatefrom";
            this.txtdatefrom.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10.25F);
            this.txtdatefrom.Properties.Appearance.Options.UseFont = true;
            this.txtdatefrom.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtdatefrom.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtdatefrom.Size = new System.Drawing.Size(141, 28);
            this.txtdatefrom.TabIndex = 433;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label1.Location = new System.Drawing.Point(213, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 23);
            this.label1.TabIndex = 432;
            this.label1.Text = "To:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label8.Location = new System.Drawing.Point(7, 23);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(53, 23);
            this.label8.TabIndex = 431;
            this.label8.Text = "From:";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.manualInventoryEntryToolStripMenuItem,
            this.uploadBatchInventoryToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(234, 80);
            // 
            // manualInventoryEntryToolStripMenuItem
            // 
            this.manualInventoryEntryToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.singleModeToolStripMenuItem,
            this.batchModeToolStripMenuItem});
            this.manualInventoryEntryToolStripMenuItem.Name = "manualInventoryEntryToolStripMenuItem";
            this.manualInventoryEntryToolStripMenuItem.Size = new System.Drawing.Size(233, 24);
            this.manualInventoryEntryToolStripMenuItem.Text = "Manual Inventory Entry";
            this.manualInventoryEntryToolStripMenuItem.Click += new System.EventHandler(this.manualInventoryEntryToolStripMenuItem_Click);
            // 
            // uploadBatchInventoryToolStripMenuItem
            // 
            this.uploadBatchInventoryToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.localConnectionToolStripMenuItem,
            this.liveConnectionToolStripMenuItem,
            this.fromLocalConnectionWithoutPOToolStripMenuItem});
            this.uploadBatchInventoryToolStripMenuItem.Name = "uploadBatchInventoryToolStripMenuItem";
            this.uploadBatchInventoryToolStripMenuItem.Size = new System.Drawing.Size(233, 24);
            this.uploadBatchInventoryToolStripMenuItem.Text = "Upload Batch Inventory";
            // 
            // localConnectionToolStripMenuItem
            // 
            this.localConnectionToolStripMenuItem.Name = "localConnectionToolStripMenuItem";
            this.localConnectionToolStripMenuItem.Size = new System.Drawing.Size(316, 26);
            this.localConnectionToolStripMenuItem.Text = "From Local Connection";
            this.localConnectionToolStripMenuItem.Click += new System.EventHandler(this.localConnectionToolStripMenuItem_Click);
            // 
            // liveConnectionToolStripMenuItem
            // 
            this.liveConnectionToolStripMenuItem.Name = "liveConnectionToolStripMenuItem";
            this.liveConnectionToolStripMenuItem.Size = new System.Drawing.Size(316, 26);
            this.liveConnectionToolStripMenuItem.Text = "From Live Connection";
            this.liveConnectionToolStripMenuItem.Click += new System.EventHandler(this.liveConnectionToolStripMenuItem_Click);
            // 
            // fromLocalConnectionWithoutPOToolStripMenuItem
            // 
            this.fromLocalConnectionWithoutPOToolStripMenuItem.Name = "fromLocalConnectionWithoutPOToolStripMenuItem";
            this.fromLocalConnectionWithoutPOToolStripMenuItem.Size = new System.Drawing.Size(316, 26);
            this.fromLocalConnectionWithoutPOToolStripMenuItem.Text = "From Local Connection Without PO";
            this.fromLocalConnectionWithoutPOToolStripMenuItem.Click += new System.EventHandler(this.fromLocalConnectionWithoutPOToolStripMenuItem_Click);
            // 
            // singleModeToolStripMenuItem
            // 
            this.singleModeToolStripMenuItem.Name = "singleModeToolStripMenuItem";
            this.singleModeToolStripMenuItem.Size = new System.Drawing.Size(216, 26);
            this.singleModeToolStripMenuItem.Text = "Single Mode";
            this.singleModeToolStripMenuItem.Click += new System.EventHandler(this.singleModeToolStripMenuItem_Click);
            // 
            // batchModeToolStripMenuItem
            // 
            this.batchModeToolStripMenuItem.Name = "batchModeToolStripMenuItem";
            this.batchModeToolStripMenuItem.Size = new System.Drawing.Size(216, 26);
            this.batchModeToolStripMenuItem.Text = "Batch Mode";
            this.batchModeToolStripMenuItem.Click += new System.EventHandler(this.batchModeToolStripMenuItem_Click);
            // 
            // ReceivedInventoryDevEx
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1402, 822);
            this.Controls.Add(this.tabControl1);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "ReceivedInventoryDevEx";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ReceivedInventoryDevEx";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.ReceivedInventoryDevEx_Load);
            this.tabControl1.ResumeLayout(false);
            this.NewShipment.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            this.ExistingShipment.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtdateto.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtdateto.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtdatefrom.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtdatefrom.Properties)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage NewShipment;
        private DevExpress.XtraGrid.GridControl gridControl2;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private System.Windows.Forms.TabPage ExistingShipment;
        private System.Windows.Forms.GroupBox groupBox2;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.CheckBox checkBox1;
        private DevExpress.XtraEditors.DateEdit txtdateto;
        private DevExpress.XtraEditors.DateEdit txtdatefrom;
        internal System.Windows.Forms.Label label1;
        internal System.Windows.Forms.Label label8;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem manualInventoryEntryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem uploadBatchInventoryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem localConnectionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem liveConnectionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fromLocalConnectionWithoutPOToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem singleModeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem batchModeToolStripMenuItem;
    }
}