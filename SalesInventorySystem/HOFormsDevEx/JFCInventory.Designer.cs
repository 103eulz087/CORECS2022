namespace SalesInventorySystem.HOFormsDevEx
{
    partial class JFCInventory
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
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.txtbrcode = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.searchLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.raddetailed = new System.Windows.Forms.RadioButton();
            this.radsum = new System.Windows.Forms.RadioButton();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.refreshDisplayToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showPerBoxItemsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtbrcode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.gridControl1.Location = new System.Drawing.Point(3, 45);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(2499, 1212);
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
            this.gridView1.DetailHeight = 673;
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
            this.groupControl1.Controls.Add(this.checkBox1);
            this.groupControl1.Controls.Add(this.txtbrcode);
            this.groupControl1.Controls.Add(this.labelControl1);
            this.groupControl1.Controls.Add(this.raddetailed);
            this.groupControl1.Controls.Add(this.radsum);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(2505, 181);
            this.groupControl1.TabIndex = 2;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(600, 117);
            this.checkBox1.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(148, 29);
            this.checkBox1.TabIndex = 4;
            this.checkBox1.Text = "Per Branch";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // txtbrcode
            // 
            this.txtbrcode.Enabled = false;
            this.txtbrcode.Location = new System.Drawing.Point(243, 111);
            this.txtbrcode.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.txtbrcode.Name = "txtbrcode";
            this.txtbrcode.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.25F);
            this.txtbrcode.Properties.Appearance.Options.UseFont = true;
            this.txtbrcode.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtbrcode.Properties.NullText = "";
            this.txtbrcode.Properties.PopupView = this.searchLookUpEdit1View;
            this.txtbrcode.Size = new System.Drawing.Size(341, 44);
            this.txtbrcode.TabIndex = 3;
            this.txtbrcode.EditValueChanged += new System.EventHandler(this.txtbrcode_EditValueChanged);
            // 
            // searchLookUpEdit1View
            // 
            this.searchLookUpEdit1View.DetailHeight = 547;
            this.searchLookUpEdit1View.FixedLineWidth = 3;
            this.searchLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.searchLookUpEdit1View.Name = "searchLookUpEdit1View";
            this.searchLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.searchLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 9.25F);
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Location = new System.Drawing.Point(57, 117);
            this.labelControl1.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(183, 30);
            this.labelControl1.TabIndex = 2;
            this.labelControl1.Text = "Filter by Branch:";
            // 
            // raddetailed
            // 
            this.raddetailed.AutoSize = true;
            this.raddetailed.Font = new System.Drawing.Font("Tahoma", 9.25F);
            this.raddetailed.Location = new System.Drawing.Point(254, 58);
            this.raddetailed.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.raddetailed.Name = "raddetailed";
            this.raddetailed.Size = new System.Drawing.Size(134, 34);
            this.raddetailed.TabIndex = 1;
            this.raddetailed.Text = "Detailed";
            this.raddetailed.UseVisualStyleBackColor = true;
            this.raddetailed.CheckedChanged += new System.EventHandler(this.raddetailed_CheckedChanged);
            // 
            // radsum
            // 
            this.radsum.AutoSize = true;
            this.radsum.Checked = true;
            this.radsum.Font = new System.Drawing.Font("Tahoma", 9.25F);
            this.radsum.Location = new System.Drawing.Point(57, 58);
            this.radsum.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.radsum.Name = "radsum";
            this.radsum.Size = new System.Drawing.Size(148, 34);
            this.radsum.TabIndex = 0;
            this.radsum.TabStop = true;
            this.radsum.Text = "Summary";
            this.radsum.UseVisualStyleBackColor = true;
            this.radsum.CheckedChanged += new System.EventHandler(this.radsum_CheckedChanged);
            // 
            // groupControl2
            // 
            this.groupControl2.Controls.Add(this.gridControl1);
            this.groupControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl2.Location = new System.Drawing.Point(0, 181);
            this.groupControl2.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(2505, 1260);
            this.groupControl2.TabIndex = 3;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.refreshDisplayToolStripMenuItem,
            this.showPerBoxItemsToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(304, 76);
            // 
            // refreshDisplayToolStripMenuItem
            // 
            this.refreshDisplayToolStripMenuItem.Image = global::SalesInventorySystem.Properties.Resources.Refresh_16x16;
            this.refreshDisplayToolStripMenuItem.Name = "refreshDisplayToolStripMenuItem";
            this.refreshDisplayToolStripMenuItem.Size = new System.Drawing.Size(303, 36);
            this.refreshDisplayToolStripMenuItem.Text = "Refresh Display";
            this.refreshDisplayToolStripMenuItem.Click += new System.EventHandler(this.refreshDisplayToolStripMenuItem_Click);
            // 
            // showPerBoxItemsToolStripMenuItem
            // 
            this.showPerBoxItemsToolStripMenuItem.Name = "showPerBoxItemsToolStripMenuItem";
            this.showPerBoxItemsToolStripMenuItem.Size = new System.Drawing.Size(303, 36);
            this.showPerBoxItemsToolStripMenuItem.Text = "Show Per Box Items";
            this.showPerBoxItemsToolStripMenuItem.Click += new System.EventHandler(this.showPerBoxItemsToolStripMenuItem_Click);
            // 
            // JFCInventory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(2505, 1441);
            this.Controls.Add(this.groupControl2);
            this.Controls.Add(this.groupControl1);
            this.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.Name = "JFCInventory";
            this.Text = "JFCInventory";
            this.Load += new System.EventHandler(this.JFCInventory_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtbrcode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private System.Windows.Forms.RadioButton raddetailed;
        private System.Windows.Forms.RadioButton radsum;
        private DevExpress.XtraEditors.SearchLookUpEdit txtbrcode;
        private DevExpress.XtraGrid.Views.Grid.GridView searchLookUpEdit1View;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem refreshDisplayToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showPerBoxItemsToolStripMenuItem;
    }
}