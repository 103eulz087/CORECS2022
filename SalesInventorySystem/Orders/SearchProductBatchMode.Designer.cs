namespace SalesInventorySystem.Orders
{
    partial class SearchProductBatchMode
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
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.repoMetrics = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.txtpono = new DevExpress.XtraEditors.TextEdit();
            this.btnsave = new DevExpress.XtraEditors.SimpleButton();
            this.txtsearchprod = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.txtsrchprodcat = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.searchLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repoMetrics)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtpono.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtsearchprod.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtsrchprodcat.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).BeginInit();
            this.SuspendLayout();
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(6);
            this.gridControl1.Location = new System.Drawing.Point(0, 124);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Margin = new System.Windows.Forms.Padding(6);
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repoMetrics});
            this.gridControl1.Size = new System.Drawing.Size(2244, 811);
            this.gridControl1.TabIndex = 8;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridView1.Appearance.HeaderPanel.Options.UseFont = true;
            this.gridView1.Appearance.Row.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridView1.Appearance.Row.Options.UseFont = true;
            this.gridView1.ColumnPanelRowHeight = 0;
            this.gridView1.DetailHeight = 673;
            this.gridView1.FixedLineWidth = 4;
            this.gridView1.FooterPanelHeight = 0;
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.GroupRowHeight = 0;
            this.gridView1.LevelIndent = 0;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsSelection.MultiSelect = true;
            this.gridView1.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            this.gridView1.OptionsView.ColumnAutoWidth = false;
            this.gridView1.OptionsView.RowAutoHeight = true;
            this.gridView1.OptionsView.ShowIndicator = false;
            this.gridView1.PreviewIndent = 0;
            this.gridView1.RowHeight = 0;
            this.gridView1.ViewCaptionHeight = 0;
            this.gridView1.RowCellStyle += new DevExpress.XtraGrid.Views.Grid.RowCellStyleEventHandler(this.gridView1_RowCellStyle);
            this.gridView1.CustomRowCellEdit += new DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventHandler(this.gridView1_CustomRowCellEdit);
            this.gridView1.SelectionChanged += new DevExpress.Data.SelectionChangedEventHandler(this.gridView1_SelectionChanged);
            this.gridView1.ShowingEditor += new System.ComponentModel.CancelEventHandler(this.gridView1_ShowingEditor);
            this.gridView1.ColumnFilterChanged += new System.EventHandler(this.gridView1_ColumnFilterChanged);
            this.gridView1.DataSourceChanged += new System.EventHandler(this.gridView1_DataSourceChanged);
            // 
            // repoMetrics
            // 
            this.repoMetrics.AutoHeight = false;
            this.repoMetrics.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repoMetrics.Name = "repoMetrics";
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.txtsrchprodcat);
            this.groupControl1.Controls.Add(this.labelControl2);
            this.groupControl1.Controls.Add(this.txtpono);
            this.groupControl1.Controls.Add(this.btnsave);
            this.groupControl1.Controls.Add(this.txtsearchprod);
            this.groupControl1.Controls.Add(this.labelControl1);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Margin = new System.Windows.Forms.Padding(6, 4, 6, 4);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(2244, 124);
            this.groupControl1.TabIndex = 7;
            this.groupControl1.Text = "Press Extract Button to show All Items";
            // 
            // txtpono
            // 
            this.txtpono.Location = new System.Drawing.Point(15, 149);
            this.txtpono.Margin = new System.Windows.Forms.Padding(6, 4, 6, 4);
            this.txtpono.Name = "txtpono";
            this.txtpono.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.8F);
            this.txtpono.Properties.Appearance.Options.UseFont = true;
            this.txtpono.Size = new System.Drawing.Size(152, 46);
            this.txtpono.TabIndex = 5;
            this.txtpono.Visible = false;
            // 
            // btnsave
            // 
            this.btnsave.Location = new System.Drawing.Point(577, 59);
            this.btnsave.Margin = new System.Windows.Forms.Padding(6, 4, 6, 4);
            this.btnsave.Name = "btnsave";
            this.btnsave.Size = new System.Drawing.Size(136, 46);
            this.btnsave.TabIndex = 4;
            this.btnsave.Text = "Save";
            this.btnsave.Click += new System.EventHandler(this.btnsave_Click);
            // 
            // txtsearchprod
            // 
            this.txtsearchprod.Location = new System.Drawing.Point(227, 114);
            this.txtsearchprod.Margin = new System.Windows.Forms.Padding(6, 4, 6, 4);
            this.txtsearchprod.Name = "txtsearchprod";
            this.txtsearchprod.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.8F);
            this.txtsearchprod.Properties.Appearance.Options.UseFont = true;
            this.txtsearchprod.Size = new System.Drawing.Size(1676, 46);
            this.txtsearchprod.TabIndex = 2;
            this.txtsearchprod.Visible = false;
            this.txtsearchprod.EditValueChanged += new System.EventHandler(this.txtsearchprod_EditValueChanged);
            this.txtsearchprod.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtsearchprod_KeyDown);
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Location = new System.Drawing.Point(15, 118);
            this.labelControl1.Margin = new System.Windows.Forms.Padding(6);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(180, 36);
            this.labelControl1.TabIndex = 1;
            this.labelControl1.Text = "Search Product:";
            this.labelControl1.Visible = false;
            // 
            // txtsrchprodcat
            // 
            this.txtsrchprodcat.Location = new System.Drawing.Point(200, 59);
            this.txtsrchprodcat.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.txtsrchprodcat.Name = "txtsrchprodcat";
            this.txtsrchprodcat.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.8F);
            this.txtsrchprodcat.Properties.Appearance.Options.UseFont = true;
            this.txtsrchprodcat.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtsrchprodcat.Properties.NullText = "";
            this.txtsrchprodcat.Properties.PopupView = this.searchLookUpEdit1View;
            this.txtsrchprodcat.Size = new System.Drawing.Size(366, 46);
            this.txtsrchprodcat.TabIndex = 108;
            this.txtsrchprodcat.EditValueChanged += new System.EventHandler(this.txtsrchprodcat_EditValueChanged);
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
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl2.Appearance.Options.UseFont = true;
            this.labelControl2.Location = new System.Drawing.Point(15, 68);
            this.labelControl2.Margin = new System.Windows.Forms.Padding(6);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(174, 29);
            this.labelControl2.TabIndex = 107;
            this.labelControl2.Text = "Select Category:";
            // 
            // SearchProductBatchMode
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(2244, 935);
            this.Controls.Add(this.gridControl1);
            this.Controls.Add(this.groupControl1);
            this.Margin = new System.Windows.Forms.Padding(6, 4, 6, 4);
            this.Name = "SearchProductBatchMode";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SearchProductBatchMode";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.SearchProductBatchMode_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repoMetrics)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtpono.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtsearchprod.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtsrchprodcat.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public DevExpress.XtraGrid.GridControl gridControl1;
        public DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        public DevExpress.XtraEditors.TextEdit txtpono;
        private DevExpress.XtraEditors.SimpleButton btnsave;
        private DevExpress.XtraEditors.TextEdit txtsearchprod;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox repoMetrics;
        private DevExpress.XtraEditors.SearchLookUpEdit txtsrchprodcat;
        private DevExpress.XtraGrid.Views.Grid.GridView searchLookUpEdit1View;
        private DevExpress.XtraEditors.LabelControl labelControl2;
    }
}