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
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.txtsrchprodcat = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.searchLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.txtpono = new DevExpress.XtraEditors.TextEdit();
            this.btnsave = new DevExpress.XtraEditors.SimpleButton();
            this.txtsearchprod = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.gridControlMain = new DevExpress.XtraGrid.GridControl();
            this.gridViewMain = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.repoMetrics = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtsrchprodcat.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtpono.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtsearchprod.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repoMetrics)).BeginInit();
            this.SuspendLayout();
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
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(1132, 79);
            this.groupControl1.TabIndex = 7;
            this.groupControl1.Text = "Press Extract Button to show All Items";
            // 
            // txtsrchprodcat
            // 
            this.txtsrchprodcat.Location = new System.Drawing.Point(117, 38);
            this.txtsrchprodcat.Name = "txtsrchprodcat";
            this.txtsrchprodcat.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.8F);
            this.txtsrchprodcat.Properties.Appearance.Options.UseFont = true;
            this.txtsrchprodcat.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtsrchprodcat.Properties.NullText = "";
            this.txtsrchprodcat.Properties.PopupView = this.searchLookUpEdit1View;
            this.txtsrchprodcat.Size = new System.Drawing.Size(213, 26);
            this.txtsrchprodcat.TabIndex = 108;
            this.txtsrchprodcat.EditValueChanged += new System.EventHandler(this.txtsrchprodcat_EditValueChanged);
            // 
            // searchLookUpEdit1View
            // 
            this.searchLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.searchLookUpEdit1View.Name = "searchLookUpEdit1View";
            this.searchLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.searchLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl2.Appearance.Options.UseFont = true;
            this.labelControl2.Location = new System.Drawing.Point(9, 44);
            this.labelControl2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(107, 18);
            this.labelControl2.TabIndex = 107;
            this.labelControl2.Text = "Select Category:";
            // 
            // txtpono
            // 
            this.txtpono.Location = new System.Drawing.Point(9, 95);
            this.txtpono.Name = "txtpono";
            this.txtpono.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.8F);
            this.txtpono.Properties.Appearance.Options.UseFont = true;
            this.txtpono.Size = new System.Drawing.Size(89, 26);
            this.txtpono.TabIndex = 5;
            this.txtpono.Visible = false;
            // 
            // btnsave
            // 
            this.btnsave.Location = new System.Drawing.Point(337, 38);
            this.btnsave.Name = "btnsave";
            this.btnsave.Size = new System.Drawing.Size(79, 29);
            this.btnsave.TabIndex = 4;
            this.btnsave.Text = "Save";
            this.btnsave.Click += new System.EventHandler(this.btnsave_Click);
            // 
            // txtsearchprod
            // 
            this.txtsearchprod.Location = new System.Drawing.Point(132, 73);
            this.txtsearchprod.Name = "txtsearchprod";
            this.txtsearchprod.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.8F);
            this.txtsearchprod.Properties.Appearance.Options.UseFont = true;
            this.txtsearchprod.Size = new System.Drawing.Size(978, 26);
            this.txtsearchprod.TabIndex = 2;
            this.txtsearchprod.Visible = false;
            this.txtsearchprod.EditValueChanged += new System.EventHandler(this.txtsearchprod_EditValueChanged);
            this.txtsearchprod.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtsearchprod_KeyDown);
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Location = new System.Drawing.Point(9, 76);
            this.labelControl1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(108, 21);
            this.labelControl1.TabIndex = 1;
            this.labelControl1.Text = "Search Product:";
            this.labelControl1.Visible = false;
            // 
            // gridControlMain
            // 
            this.gridControlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControlMain.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gridControlMain.Location = new System.Drawing.Point(0, 79);
            this.gridControlMain.MainView = this.gridViewMain;
            this.gridControlMain.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gridControlMain.Name = "gridControlMain";
            this.gridControlMain.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repoMetrics});
            this.gridControlMain.Size = new System.Drawing.Size(1132, 519);
            this.gridControlMain.TabIndex = 109;
            this.gridControlMain.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewMain});
            // 
            // gridViewMain
            // 
            this.gridViewMain.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridViewMain.Appearance.HeaderPanel.Options.UseFont = true;
            this.gridViewMain.Appearance.Row.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridViewMain.Appearance.Row.Options.UseFont = true;
            this.gridViewMain.DetailHeight = 431;
            this.gridViewMain.GridControl = this.gridControlMain;
            this.gridViewMain.Name = "gridViewMain";
            this.gridViewMain.OptionsFind.Behavior = DevExpress.XtraEditors.FindPanelBehavior.Search;
            this.gridViewMain.OptionsSelection.MultiSelect = true;
            this.gridViewMain.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            this.gridViewMain.OptionsView.ColumnAutoWidth = false;
            this.gridViewMain.OptionsView.RowAutoHeight = true;
            this.gridViewMain.OptionsView.ShowIndicator = false;
            this.gridViewMain.RowCellStyle += new DevExpress.XtraGrid.Views.Grid.RowCellStyleEventHandler(this.gridViewMain_RowCellStyle);
            this.gridViewMain.CustomRowCellEdit += new DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventHandler(this.gridViewMain_CustomRowCellEdit);
            this.gridViewMain.SelectionChanged += new DevExpress.Data.SelectionChangedEventHandler(this.gridViewMain_SelectionChanged);
            this.gridViewMain.ShowingEditor += new System.ComponentModel.CancelEventHandler(this.gridViewMain_ShowingEditor);
            this.gridViewMain.ValidatingEditor += new DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventHandler(this.gridViewMain_ValidatingEditor);
            // 
            // repoMetrics
            // 
            this.repoMetrics.AutoHeight = false;
            this.repoMetrics.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repoMetrics.Name = "repoMetrics";
            // 
            // SearchProductBatchMode
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1132, 598);
            this.Controls.Add(this.gridControlMain);
            this.Controls.Add(this.groupControl1);
            this.Name = "SearchProductBatchMode";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SearchProductBatchMode";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.SearchProductBatchMode_Load);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtsrchprodcat.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtpono.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtsearchprod.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repoMetrics)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraEditors.GroupControl groupControl1;
        public DevExpress.XtraEditors.TextEdit txtpono;
        private DevExpress.XtraEditors.SimpleButton btnsave;
        private DevExpress.XtraEditors.TextEdit txtsearchprod;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.SearchLookUpEdit txtsrchprodcat;
        private DevExpress.XtraGrid.Views.Grid.GridView searchLookUpEdit1View;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        public DevExpress.XtraGrid.GridControl gridControlMain;
        public DevExpress.XtraGrid.Views.Grid.GridView gridViewMain;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox repoMetrics;
    }
}