namespace SalesInventorySystem
{
    partial class ViewPrimalCutsInventory
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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.transferInventoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bigBlueToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.expandAllGroupsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.expandAllGroupsToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.hideGroupsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reprintBarcodeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.refreshDisplayToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(3, 17);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(976, 645);
            this.gridControl1.TabIndex = 0;
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
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsBehavior.ReadOnly = true;
            this.gridView1.OptionsSelection.ShowCheckBoxSelectorInColumnHeader = DevExpress.Utils.DefaultBoolean.True;
            this.gridView1.OptionsView.ColumnAutoWidth = false;
            this.gridView1.OptionsView.RowAutoHeight = true;
            this.gridView1.OptionsView.ShowFooter = true;
            this.gridView1.RowCellStyle += new DevExpress.XtraGrid.Views.Grid.RowCellStyleEventHandler(this.gridView1_RowCellStyle);
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.White;
            this.groupBox2.Controls.Add(this.gridControl1);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(982, 665);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.transferInventoryToolStripMenuItem,
            this.expandAllGroupsToolStripMenuItem,
            this.reprintBarcodeToolStripMenuItem,
            this.refreshDisplayToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(171, 114);
            // 
            // transferInventoryToolStripMenuItem
            // 
            this.transferInventoryToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bigBlueToolStripMenuItem});
            this.transferInventoryToolStripMenuItem.Name = "transferInventoryToolStripMenuItem";
            this.transferInventoryToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.transferInventoryToolStripMenuItem.Text = "Transfer Inventory";
            // 
            // bigBlueToolStripMenuItem
            // 
            this.bigBlueToolStripMenuItem.Name = "bigBlueToolStripMenuItem";
            this.bigBlueToolStripMenuItem.Size = new System.Drawing.Size(114, 22);
            this.bigBlueToolStripMenuItem.Text = "BigBlue";
            this.bigBlueToolStripMenuItem.Click += new System.EventHandler(this.bigBlueToolStripMenuItem_Click);
            // 
            // expandAllGroupsToolStripMenuItem
            // 
            this.expandAllGroupsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.expandAllGroupsToolStripMenuItem1,
            this.hideGroupsToolStripMenuItem});
            this.expandAllGroupsToolStripMenuItem.Name = "expandAllGroupsToolStripMenuItem";
            this.expandAllGroupsToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.expandAllGroupsToolStripMenuItem.Text = "Expand All Groups";
            this.expandAllGroupsToolStripMenuItem.Click += new System.EventHandler(this.expandAllGroupsToolStripMenuItem_Click);
            // 
            // expandAllGroupsToolStripMenuItem1
            // 
            this.expandAllGroupsToolStripMenuItem1.Name = "expandAllGroupsToolStripMenuItem1";
            this.expandAllGroupsToolStripMenuItem1.Size = new System.Drawing.Size(170, 22);
            this.expandAllGroupsToolStripMenuItem1.Text = "Expand All Groups";
            this.expandAllGroupsToolStripMenuItem1.Click += new System.EventHandler(this.expandAllGroupsToolStripMenuItem1_Click);
            // 
            // hideGroupsToolStripMenuItem
            // 
            this.hideGroupsToolStripMenuItem.Name = "hideGroupsToolStripMenuItem";
            this.hideGroupsToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.hideGroupsToolStripMenuItem.Text = "Hide Groups";
            this.hideGroupsToolStripMenuItem.Click += new System.EventHandler(this.hideGroupsToolStripMenuItem_Click);
            // 
            // reprintBarcodeToolStripMenuItem
            // 
            this.reprintBarcodeToolStripMenuItem.Name = "reprintBarcodeToolStripMenuItem";
            this.reprintBarcodeToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.reprintBarcodeToolStripMenuItem.Text = "Reprint Barcode";
            this.reprintBarcodeToolStripMenuItem.Click += new System.EventHandler(this.reprintBarcodeToolStripMenuItem_Click);
            // 
            // refreshDisplayToolStripMenuItem
            // 
            this.refreshDisplayToolStripMenuItem.Name = "refreshDisplayToolStripMenuItem";
            this.refreshDisplayToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.refreshDisplayToolStripMenuItem.Text = "Refresh Display";
            this.refreshDisplayToolStripMenuItem.Click += new System.EventHandler(this.refreshDisplayToolStripMenuItem_Click);
            // 
            // ViewPrimalCutsInventory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(982, 665);
            this.Controls.Add(this.groupBox2);
            this.Name = "ViewPrimalCutsInventory";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Commisary Inventory";
            this.Load += new System.EventHandler(this.ViewProductsInventory_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem transferInventoryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bigBlueToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem expandAllGroupsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem expandAllGroupsToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem hideGroupsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reprintBarcodeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem refreshDisplayToolStripMenuItem;
    }
}