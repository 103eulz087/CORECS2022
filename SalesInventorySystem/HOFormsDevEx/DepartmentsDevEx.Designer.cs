﻿namespace SalesInventorySystem.HOFormsDevEx
{
    partial class DepartmentsDevEx
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DepartmentsDevEx));
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.btncancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnupdate = new DevExpress.XtraEditors.SimpleButton();
            this.btnadd = new DevExpress.XtraEditors.SimpleButton();
            this.btnnew = new DevExpress.XtraEditors.SimpleButton();
            this.txtdeptname = new DevExpress.XtraEditors.TextEdit();
            this.txtdeptid = new DevExpress.XtraEditors.TextEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.editItemsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtdeptname.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtdeptid.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.btncancel);
            this.groupControl1.Controls.Add(this.btnupdate);
            this.groupControl1.Controls.Add(this.btnadd);
            this.groupControl1.Controls.Add(this.btnnew);
            this.groupControl1.Controls.Add(this.txtdeptname);
            this.groupControl1.Controls.Add(this.txtdeptid);
            this.groupControl1.Controls.Add(this.labelControl2);
            this.groupControl1.Controls.Add(this.labelControl1);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(802, 286);
            this.groupControl1.TabIndex = 0;
            // 
            // btncancel
            // 
            this.btncancel.ImageOptions.Image = global::SalesInventorySystem.Properties.Resources.Cancel_16x16__2_;
            this.btncancel.Location = new System.Drawing.Point(485, 201);
            this.btncancel.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.btncancel.Name = "btncancel";
            this.btncancel.Size = new System.Drawing.Size(130, 58);
            this.btncancel.TabIndex = 61;
            this.btncancel.Text = "Cancel";
            this.btncancel.Click += new System.EventHandler(this.btncancel_Click);
            // 
            // btnupdate
            // 
            this.btnupdate.ImageOptions.Image = global::SalesInventorySystem.Properties.Resources.Refresh_16x16;
            this.btnupdate.Location = new System.Drawing.Point(314, 201);
            this.btnupdate.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.btnupdate.Name = "btnupdate";
            this.btnupdate.Size = new System.Drawing.Size(158, 58);
            this.btnupdate.TabIndex = 60;
            this.btnupdate.Text = "Update";
            this.btnupdate.Click += new System.EventHandler(this.btnupdate_Click);
            // 
            // btnadd
            // 
            this.btnadd.ImageOptions.Image = global::SalesInventorySystem.Properties.Resources.Add_16x16__2_;
            this.btnadd.Location = new System.Drawing.Point(171, 201);
            this.btnadd.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.btnadd.Name = "btnadd";
            this.btnadd.Size = new System.Drawing.Size(130, 58);
            this.btnadd.TabIndex = 59;
            this.btnadd.Text = "Add";
            this.btnadd.Click += new System.EventHandler(this.btnadd_Click);
            // 
            // btnnew
            // 
            this.btnnew.ImageOptions.Image = global::SalesInventorySystem.Properties.Resources.New_16x16__5_;
            this.btnnew.Location = new System.Drawing.Point(28, 201);
            this.btnnew.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.btnnew.Name = "btnnew";
            this.btnnew.Size = new System.Drawing.Size(130, 58);
            this.btnnew.TabIndex = 58;
            this.btnnew.Text = "New";
            this.btnnew.Click += new System.EventHandler(this.btnnew_Click);
            // 
            // txtdeptname
            // 
            this.txtdeptname.Location = new System.Drawing.Point(266, 129);
            this.txtdeptname.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.txtdeptname.Name = "txtdeptname";
            this.txtdeptname.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.25F);
            this.txtdeptname.Properties.Appearance.Options.UseFont = true;
            this.txtdeptname.Size = new System.Drawing.Size(479, 48);
            this.txtdeptname.TabIndex = 3;
            // 
            // txtdeptid
            // 
            this.txtdeptid.Location = new System.Drawing.Point(269, 65);
            this.txtdeptid.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.txtdeptid.Name = "txtdeptid";
            this.txtdeptid.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.25F);
            this.txtdeptid.Properties.Appearance.Options.UseFont = true;
            this.txtdeptid.Size = new System.Drawing.Size(479, 48);
            this.txtdeptid.TabIndex = 2;
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Tahoma", 9.25F);
            this.labelControl2.Appearance.Options.UseFont = true;
            this.labelControl2.Location = new System.Drawing.Point(26, 136);
            this.labelControl2.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(239, 34);
            this.labelControl2.TabIndex = 1;
            this.labelControl2.Text = "Department Name:";
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 9.25F);
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Location = new System.Drawing.Point(26, 71);
            this.labelControl1.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(139, 34);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "Dept Code:";
            // 
            // groupControl2
            // 
            this.groupControl2.Controls.Add(this.gridControl1);
            this.groupControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl2.Location = new System.Drawing.Point(0, 286);
            this.groupControl2.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(802, 642);
            this.groupControl2.TabIndex = 1;
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.gridControl1.Location = new System.Drawing.Point(3, 50);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(796, 589);
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
            this.gridView1.DetailHeight = 634;
            this.gridView1.FixedLineWidth = 4;
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsBehavior.ReadOnly = true;
            this.gridView1.OptionsSelection.ShowCheckBoxSelectorInColumnHeader = DevExpress.Utils.DefaultBoolean.True;
            this.gridView1.OptionsView.ColumnAutoWidth = false;
            this.gridView1.OptionsView.RowAutoHeight = true;
            this.gridView1.OptionsView.ShowFooter = true;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editItemsToolStripMenuItem,
            this.deleteToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(335, 138);
            // 
            // editItemsToolStripMenuItem
            // 
            this.editItemsToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("editItemsToolStripMenuItem.Image")));
            this.editItemsToolStripMenuItem.Name = "editItemsToolStripMenuItem";
            this.editItemsToolStripMenuItem.Size = new System.Drawing.Size(334, 42);
            this.editItemsToolStripMenuItem.Text = "Edit Items";
            this.editItemsToolStripMenuItem.Click += new System.EventHandler(this.editItemsToolStripMenuItem_Click);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("deleteToolStripMenuItem.Image")));
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(334, 42);
            this.deleteToolStripMenuItem.Text = "Delete";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
            // 
            // DepartmentsDevEx
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 29F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(802, 928);
            this.Controls.Add(this.groupControl2);
            this.Controls.Add(this.groupControl1);
            this.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.Name = "DepartmentsDevEx";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DepartmentsDevEx";
            this.Load += new System.EventHandler(this.DepartmentsDevEx_Load);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtdeptname.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtdeptid.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private DevExpress.XtraEditors.TextEdit txtdeptid;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit txtdeptname;
        private DevExpress.XtraEditors.SimpleButton btncancel;
        private DevExpress.XtraEditors.SimpleButton btnupdate;
        private DevExpress.XtraEditors.SimpleButton btnadd;
        private DevExpress.XtraEditors.SimpleButton btnnew;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem editItemsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
    }
}