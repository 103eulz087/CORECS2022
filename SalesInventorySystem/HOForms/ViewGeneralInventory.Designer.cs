namespace SalesInventorySystem
{
    partial class ViewGeneralInventory
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
            this.button3 = new System.Windows.Forms.Button();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.Commissary = new System.Windows.Forms.RadioButton();
            this.bigblue = new System.Windows.Forms.RadioButton();
            this.button1 = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.inventoryAdjustmentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.inTransitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.otherIncomeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addToSupplierToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deductToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.inTransitToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.otherExpenseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deductToSupplierToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.inventoryCostAdjustmentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.deductToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.dateEdit1 = new DevExpress.XtraEditors.DateEdit();
            this.dateEdit2 = new DevExpress.XtraEditors.DateEdit();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.panel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit1.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit2.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.SeaGreen;
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button3.ForeColor = System.Drawing.Color.White;
            this.button3.Location = new System.Drawing.Point(1081, 78);
            this.button3.Margin = new System.Windows.Forms.Padding(7, 7, 7, 7);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(215, 96);
            this.button3.TabIndex = 6;
            this.button3.Text = "Save Edit Changes";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBox1.Location = new System.Drawing.Point(469, 181);
            this.checkBox1.Margin = new System.Windows.Forms.Padding(7, 7, 7, 7);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(391, 37);
            this.checkBox1.TabIndex = 4;
            this.checkBox1.Text = "Show Conversion Items Only";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.Commissary);
            this.panel1.Controls.Add(this.bigblue);
            this.panel1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel1.Location = new System.Drawing.Point(522, 64);
            this.panel1.Margin = new System.Windows.Forms.Padding(7, 7, 7, 7);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(359, 58);
            this.panel1.TabIndex = 3;
            this.panel1.Visible = false;
            // 
            // Commissary
            // 
            this.Commissary.AutoSize = true;
            this.Commissary.Checked = true;
            this.Commissary.Location = new System.Drawing.Point(163, 7);
            this.Commissary.Margin = new System.Windows.Forms.Padding(7, 7, 7, 7);
            this.Commissary.Name = "Commissary";
            this.Commissary.Size = new System.Drawing.Size(193, 37);
            this.Commissary.TabIndex = 1;
            this.Commissary.TabStop = true;
            this.Commissary.Text = "Commissary";
            this.Commissary.UseVisualStyleBackColor = true;
            // 
            // bigblue
            // 
            this.bigblue.AutoSize = true;
            this.bigblue.Location = new System.Drawing.Point(7, 7);
            this.bigblue.Margin = new System.Windows.Forms.Padding(7, 7, 7, 7);
            this.bigblue.Name = "bigblue";
            this.bigblue.Size = new System.Drawing.Size(137, 37);
            this.bigblue.TabIndex = 0;
            this.bigblue.Text = "BigBlue";
            this.bigblue.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.SeaGreen;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(892, 78);
            this.button1.Margin = new System.Windows.Forms.Padding(7, 7, 7, 7);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(175, 94);
            this.button1.TabIndex = 2;
            this.button1.Text = "Get Query";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(254, 71);
            this.comboBox1.Margin = new System.Windows.Forms.Padding(7, 7, 7, 7);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(248, 41);
            this.comboBox1.TabIndex = 1;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(28, 78);
            this.label1.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(185, 33);
            this.label1.TabIndex = 0;
            this.label1.Text = "Select Branch:";
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.White;
            this.groupBox2.Controls.Add(this.gridControl1);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 247);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(7, 7, 7, 7);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(7, 7, 7, 7);
            this.groupBox2.Size = new System.Drawing.Size(2977, 1299);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(7, 7, 7, 7);
            this.gridControl1.Location = new System.Drawing.Point(7, 35);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Margin = new System.Windows.Forms.Padding(7, 7, 7, 7);
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(2963, 1257);
            this.gridControl1.TabIndex = 1;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            this.gridControl1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.gridControl1_MouseUp);
            // 
            // gridView1
            // 
            this.gridView1.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 9.25F, System.Drawing.FontStyle.Bold);
            this.gridView1.Appearance.HeaderPanel.Options.UseFont = true;
            this.gridView1.Appearance.Row.Font = new System.Drawing.Font("Tahoma", 9.25F);
            this.gridView1.Appearance.Row.Options.UseFont = true;
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsSelection.MultiSelect = true;
            this.gridView1.OptionsView.ColumnAutoWidth = false;
            this.gridView1.OptionsView.RowAutoHeight = true;
            this.gridView1.OptionsView.ShowFooter = true;
            this.gridView1.RowCellStyle += new DevExpress.XtraGrid.Views.Grid.RowCellStyleEventHandler(this.gridView1_RowCellStyle);
            this.gridView1.ShowingEditor += new System.ComponentModel.CancelEventHandler(this.gridView1_ShowingEditor);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(36, 36);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.inventoryAdjustmentToolStripMenuItem,
            this.inventoryCostAdjustmentToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(409, 88);
            // 
            // inventoryAdjustmentToolStripMenuItem
            // 
            this.inventoryAdjustmentToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addToolStripMenuItem,
            this.deductToolStripMenuItem});
            this.inventoryAdjustmentToolStripMenuItem.Name = "inventoryAdjustmentToolStripMenuItem";
            this.inventoryAdjustmentToolStripMenuItem.Size = new System.Drawing.Size(408, 42);
            this.inventoryAdjustmentToolStripMenuItem.Text = "Inventory Qty Adjustment";
            // 
            // addToolStripMenuItem
            // 
            this.addToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.inTransitToolStripMenuItem,
            this.otherIncomeToolStripMenuItem,
            this.addToSupplierToolStripMenuItem});
            this.addToolStripMenuItem.Name = "addToolStripMenuItem";
            this.addToolStripMenuItem.Size = new System.Drawing.Size(209, 42);
            this.addToolStripMenuItem.Text = "Add";
            this.addToolStripMenuItem.Click += new System.EventHandler(this.addToolStripMenuItem_Click);
            // 
            // inTransitToolStripMenuItem
            // 
            this.inTransitToolStripMenuItem.Name = "inTransitToolStripMenuItem";
            this.inTransitToolStripMenuItem.Size = new System.Drawing.Size(310, 42);
            this.inTransitToolStripMenuItem.Text = "In Transit";
            this.inTransitToolStripMenuItem.Click += new System.EventHandler(this.inTransitToolStripMenuItem_Click);
            // 
            // otherIncomeToolStripMenuItem
            // 
            this.otherIncomeToolStripMenuItem.Name = "otherIncomeToolStripMenuItem";
            this.otherIncomeToolStripMenuItem.Size = new System.Drawing.Size(310, 42);
            this.otherIncomeToolStripMenuItem.Text = "Other Income";
            this.otherIncomeToolStripMenuItem.Click += new System.EventHandler(this.otherIncomeToolStripMenuItem_Click);
            // 
            // addToSupplierToolStripMenuItem
            // 
            this.addToSupplierToolStripMenuItem.Name = "addToSupplierToolStripMenuItem";
            this.addToSupplierToolStripMenuItem.Size = new System.Drawing.Size(310, 42);
            this.addToSupplierToolStripMenuItem.Text = "Add to Supplier";
            this.addToSupplierToolStripMenuItem.Click += new System.EventHandler(this.addToSupplierToolStripMenuItem_Click);
            // 
            // deductToolStripMenuItem
            // 
            this.deductToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.inTransitToolStripMenuItem1,
            this.otherExpenseToolStripMenuItem,
            this.deductToSupplierToolStripMenuItem});
            this.deductToolStripMenuItem.Name = "deductToolStripMenuItem";
            this.deductToolStripMenuItem.Size = new System.Drawing.Size(209, 42);
            this.deductToolStripMenuItem.Text = "Deduct";
            this.deductToolStripMenuItem.Click += new System.EventHandler(this.deductToolStripMenuItem_Click);
            // 
            // inTransitToolStripMenuItem1
            // 
            this.inTransitToolStripMenuItem1.Name = "inTransitToolStripMenuItem1";
            this.inTransitToolStripMenuItem1.Size = new System.Drawing.Size(346, 42);
            this.inTransitToolStripMenuItem1.Text = "In Transit";
            this.inTransitToolStripMenuItem1.Click += new System.EventHandler(this.inTransitToolStripMenuItem1_Click);
            // 
            // otherExpenseToolStripMenuItem
            // 
            this.otherExpenseToolStripMenuItem.Name = "otherExpenseToolStripMenuItem";
            this.otherExpenseToolStripMenuItem.Size = new System.Drawing.Size(346, 42);
            this.otherExpenseToolStripMenuItem.Text = "Other Expense";
            this.otherExpenseToolStripMenuItem.Click += new System.EventHandler(this.otherExpenseToolStripMenuItem_Click);
            // 
            // deductToSupplierToolStripMenuItem
            // 
            this.deductToSupplierToolStripMenuItem.Name = "deductToSupplierToolStripMenuItem";
            this.deductToSupplierToolStripMenuItem.Size = new System.Drawing.Size(346, 42);
            this.deductToSupplierToolStripMenuItem.Text = "Deduct to Supplier";
            this.deductToSupplierToolStripMenuItem.Click += new System.EventHandler(this.deductToSupplierToolStripMenuItem_Click);
            // 
            // inventoryCostAdjustmentToolStripMenuItem
            // 
            this.inventoryCostAdjustmentToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addToolStripMenuItem1,
            this.deductToolStripMenuItem1});
            this.inventoryCostAdjustmentToolStripMenuItem.Name = "inventoryCostAdjustmentToolStripMenuItem";
            this.inventoryCostAdjustmentToolStripMenuItem.Size = new System.Drawing.Size(408, 42);
            this.inventoryCostAdjustmentToolStripMenuItem.Text = "Inventory Cost Adjustment";
            // 
            // addToolStripMenuItem1
            // 
            this.addToolStripMenuItem1.Name = "addToolStripMenuItem1";
            this.addToolStripMenuItem1.Size = new System.Drawing.Size(209, 42);
            this.addToolStripMenuItem1.Text = "Add";
            this.addToolStripMenuItem1.Click += new System.EventHandler(this.addToolStripMenuItem1_Click);
            // 
            // deductToolStripMenuItem1
            // 
            this.deductToolStripMenuItem1.Name = "deductToolStripMenuItem1";
            this.deductToolStripMenuItem1.Size = new System.Drawing.Size(209, 42);
            this.deductToolStripMenuItem1.Text = "Deduct";
            this.deductToolStripMenuItem1.Click += new System.EventHandler(this.deductToolStripMenuItem1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(126, 130);
            this.label2.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(87, 33);
            this.label2.TabIndex = 7;
            this.label2.Text = "From:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(516, 129);
            this.label3.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 33);
            this.label3.TabIndex = 8;
            this.label3.Text = "To:";
            // 
            // dateEdit1
            // 
            this.dateEdit1.EditValue = null;
            this.dateEdit1.Enabled = false;
            this.dateEdit1.Location = new System.Drawing.Point(255, 126);
            this.dateEdit1.Margin = new System.Windows.Forms.Padding(7, 7, 7, 7);
            this.dateEdit1.Name = "dateEdit1";
            this.dateEdit1.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateEdit1.Properties.Appearance.Options.UseFont = true;
            this.dateEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEdit1.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEdit1.Size = new System.Drawing.Size(247, 40);
            this.dateEdit1.TabIndex = 9;
            // 
            // dateEdit2
            // 
            this.dateEdit2.EditValue = null;
            this.dateEdit2.Enabled = false;
            this.dateEdit2.Location = new System.Drawing.Point(586, 126);
            this.dateEdit2.Margin = new System.Windows.Forms.Padding(7, 7, 7, 7);
            this.dateEdit2.Name = "dateEdit2";
            this.dateEdit2.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateEdit2.Properties.Appearance.Options.UseFont = true;
            this.dateEdit2.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEdit2.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEdit2.Size = new System.Drawing.Size(247, 40);
            this.dateEdit2.TabIndex = 10;
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBox2.Location = new System.Drawing.Point(255, 180);
            this.checkBox2.Margin = new System.Windows.Forms.Padding(7, 7, 7, 7);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(156, 37);
            this.checkBox2.TabIndex = 11;
            this.checkBox2.Text = "w/ Date?";
            this.checkBox2.UseVisualStyleBackColor = true;
            this.checkBox2.CheckedChanged += new System.EventHandler(this.checkBox2_CheckedChanged);
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.label1);
            this.groupControl1.Controls.Add(this.checkBox2);
            this.groupControl1.Controls.Add(this.panel1);
            this.groupControl1.Controls.Add(this.checkBox1);
            this.groupControl1.Controls.Add(this.dateEdit2);
            this.groupControl1.Controls.Add(this.button1);
            this.groupControl1.Controls.Add(this.button3);
            this.groupControl1.Controls.Add(this.dateEdit1);
            this.groupControl1.Controls.Add(this.comboBox1);
            this.groupControl1.Controls.Add(this.label3);
            this.groupControl1.Controls.Add(this.label2);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Margin = new System.Windows.Forms.Padding(7, 7, 7, 7);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(2977, 247);
            this.groupControl1.TabIndex = 12;
            // 
            // ViewGeneralInventory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(14F, 29F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(2977, 1546);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupControl1);
            this.Margin = new System.Windows.Forms.Padding(7, 7, 7, 7);
            this.Name = "ViewGeneralInventory";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ViewGeneralInventory";
            this.Load += new System.EventHandler(this.ViewGeneralInventory_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit1.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit2.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem inventoryAdjustmentToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deductToolStripMenuItem;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton Commissary;
        private System.Windows.Forms.RadioButton bigblue;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private DevExpress.XtraEditors.DateEdit dateEdit2;
        private DevExpress.XtraEditors.DateEdit dateEdit1;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.ToolStripMenuItem inTransitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem otherIncomeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem inTransitToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem otherExpenseToolStripMenuItem;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private System.Windows.Forms.ToolStripMenuItem inventoryCostAdjustmentToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem deductToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem addToSupplierToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deductToSupplierToolStripMenuItem;
    }
}