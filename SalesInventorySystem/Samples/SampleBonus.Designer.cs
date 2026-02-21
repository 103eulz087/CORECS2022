namespace SalesInventorySystem.Samples
{
    partial class SampleBonus
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
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.txtamount = new DevExpress.XtraEditors.TextEdit();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.gridControlMaster = new DevExpress.XtraGrid.GridControl();
            this.gridViewMaster = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.repositoryItemComboBox5 = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.repositoryItemSearchLookUpEdit3 = new DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit();
            this.gridView5 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.repositoryItemCheckEditStat = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.repositoryItemComboBoxVarianceType = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.repositoryItemSearchLookUpEditoffsetdebitglcode = new DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit();
            this.repositoryItemSearchLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.repositoryItemSearchLookUpEditdiscountglcode = new DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.repositoryItemSearchLookUpEditoffsetCreditGLCode = new DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit();
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtamount.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlMaster)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewMaster)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBox5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSearchLookUpEdit3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEditStat)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBoxVarianceType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSearchLookUpEditoffsetdebitglcode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSearchLookUpEdit1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSearchLookUpEditdiscountglcode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSearchLookUpEditoffsetCreditGLCode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            this.SuspendLayout();
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.btnSave);
            this.groupControl1.Controls.Add(this.labelControl3);
            this.groupControl1.Controls.Add(this.txtamount);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(1834, 199);
            this.groupControl1.TabIndex = 0;
            this.groupControl1.Text = "Send Bonus";
            this.groupControl1.Paint += new System.Windows.Forms.PaintEventHandler(this.groupControl1_Paint);
            // 
            // btnSave
            // 
            this.btnSave.ImageOptions.Image = global::SalesInventorySystem.Properties.Resources.Save_16x16__5_;
            this.btnSave.Location = new System.Drawing.Point(474, 91);
            this.btnSave.Margin = new System.Windows.Forms.Padding(5, 7, 5, 7);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(235, 57);
            this.btnSave.TabIndex = 444;
            this.btnSave.Text = "Send";
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Tahoma", 12.25F);
            this.labelControl3.Appearance.Options.UseFont = true;
            this.labelControl3.Location = new System.Drawing.Point(35, 100);
            this.labelControl3.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(223, 40);
            this.labelControl3.TabIndex = 3;
            this.labelControl3.Text = "Bonus Amount:";
            // 
            // txtamount
            // 
            this.txtamount.Location = new System.Drawing.Point(266, 92);
            this.txtamount.Name = "txtamount";
            this.txtamount.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12.875F);
            this.txtamount.Properties.Appearance.Options.UseFont = true;
            this.txtamount.Size = new System.Drawing.Size(200, 56);
            this.txtamount.TabIndex = 0;
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.gridControlMaster);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(0, 199);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(1834, 985);
            this.panelControl1.TabIndex = 1;
            // 
            // gridControlMaster
            // 
            this.gridControlMaster.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControlMaster.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(6, 8, 6, 8);
            this.gridControlMaster.Location = new System.Drawing.Point(3, 3);
            this.gridControlMaster.MainView = this.gridViewMaster;
            this.gridControlMaster.Margin = new System.Windows.Forms.Padding(6, 8, 6, 8);
            this.gridControlMaster.Name = "gridControlMaster";
            this.gridControlMaster.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemComboBox5,
            this.repositoryItemSearchLookUpEdit3,
            this.repositoryItemCheckEdit1,
            this.repositoryItemCheckEditStat,
            this.repositoryItemComboBoxVarianceType,
            this.repositoryItemSearchLookUpEditoffsetdebitglcode,
            this.repositoryItemSearchLookUpEditdiscountglcode,
            this.repositoryItemSearchLookUpEditoffsetCreditGLCode});
            this.gridControlMaster.Size = new System.Drawing.Size(1828, 979);
            this.gridControlMaster.TabIndex = 9;
            this.gridControlMaster.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewMaster});
            // 
            // gridViewMaster
            // 
            this.gridViewMaster.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridViewMaster.Appearance.HeaderPanel.Options.UseFont = true;
            this.gridViewMaster.Appearance.Row.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridViewMaster.Appearance.Row.Options.UseFont = true;
            this.gridViewMaster.DetailHeight = 673;
            this.gridViewMaster.FixedLineWidth = 4;
            this.gridViewMaster.GridControl = this.gridControlMaster;
            this.gridViewMaster.LevelIndent = 0;
            this.gridViewMaster.Name = "gridViewMaster";
            this.gridViewMaster.OptionsView.ColumnAutoWidth = false;
            this.gridViewMaster.OptionsView.RowAutoHeight = true;
            this.gridViewMaster.OptionsView.ShowFooter = true;
            this.gridViewMaster.PreviewIndent = 0;
            this.gridViewMaster.CustomRowCellEdit += new DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventHandler(this.gridViewMaster_CustomRowCellEdit);
            // 
            // repositoryItemComboBox5
            // 
            this.repositoryItemComboBox5.AutoHeight = false;
            this.repositoryItemComboBox5.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemComboBox5.Items.AddRange(new object[] {
            "NONE",
            "PAY"});
            this.repositoryItemComboBox5.Name = "repositoryItemComboBox5";
            // 
            // repositoryItemSearchLookUpEdit3
            // 
            this.repositoryItemSearchLookUpEdit3.AutoHeight = false;
            this.repositoryItemSearchLookUpEdit3.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemSearchLookUpEdit3.Name = "repositoryItemSearchLookUpEdit3";
            this.repositoryItemSearchLookUpEdit3.PopupView = this.gridView5;
            // 
            // gridView5
            // 
            this.gridView5.DetailHeight = 546;
            this.gridView5.FixedLineWidth = 4;
            this.gridView5.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridView5.LevelIndent = 0;
            this.gridView5.Name = "gridView5";
            this.gridView5.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView5.OptionsView.ShowGroupPanel = false;
            this.gridView5.PreviewIndent = 0;
            // 
            // repositoryItemCheckEdit1
            // 
            this.repositoryItemCheckEdit1.AutoHeight = false;
            this.repositoryItemCheckEdit1.Name = "repositoryItemCheckEdit1";
            // 
            // repositoryItemCheckEditStat
            // 
            this.repositoryItemCheckEditStat.AutoHeight = false;
            this.repositoryItemCheckEditStat.Name = "repositoryItemCheckEditStat";
            this.repositoryItemCheckEditStat.ValueChecked = "True";
            this.repositoryItemCheckEditStat.ValueUnchecked = "False";
            // 
            // repositoryItemComboBoxVarianceType
            // 
            this.repositoryItemComboBoxVarianceType.AutoHeight = false;
            this.repositoryItemComboBoxVarianceType.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemComboBoxVarianceType.Items.AddRange(new object[] {
            "DISCOUNT",
            "OFFSET"});
            this.repositoryItemComboBoxVarianceType.Name = "repositoryItemComboBoxVarianceType";
            // 
            // repositoryItemSearchLookUpEditoffsetdebitglcode
            // 
            this.repositoryItemSearchLookUpEditoffsetdebitglcode.AutoHeight = false;
            this.repositoryItemSearchLookUpEditoffsetdebitglcode.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemSearchLookUpEditoffsetdebitglcode.Name = "repositoryItemSearchLookUpEditoffsetdebitglcode";
            this.repositoryItemSearchLookUpEditoffsetdebitglcode.PopupView = this.repositoryItemSearchLookUpEdit1View;
            // 
            // repositoryItemSearchLookUpEdit1View
            // 
            this.repositoryItemSearchLookUpEdit1View.DetailHeight = 546;
            this.repositoryItemSearchLookUpEdit1View.FixedLineWidth = 4;
            this.repositoryItemSearchLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.repositoryItemSearchLookUpEdit1View.LevelIndent = 0;
            this.repositoryItemSearchLookUpEdit1View.Name = "repositoryItemSearchLookUpEdit1View";
            this.repositoryItemSearchLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.repositoryItemSearchLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            this.repositoryItemSearchLookUpEdit1View.PreviewIndent = 0;
            // 
            // repositoryItemSearchLookUpEditdiscountglcode
            // 
            this.repositoryItemSearchLookUpEditdiscountglcode.AutoHeight = false;
            this.repositoryItemSearchLookUpEditdiscountglcode.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemSearchLookUpEditdiscountglcode.Name = "repositoryItemSearchLookUpEditdiscountglcode";
            this.repositoryItemSearchLookUpEditdiscountglcode.PopupView = this.gridView1;
            // 
            // gridView1
            // 
            this.gridView1.DetailHeight = 546;
            this.gridView1.FixedLineWidth = 4;
            this.gridView1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridView1.LevelIndent = 0;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            this.gridView1.PreviewIndent = 0;
            // 
            // repositoryItemSearchLookUpEditoffsetCreditGLCode
            // 
            this.repositoryItemSearchLookUpEditoffsetCreditGLCode.AutoHeight = false;
            this.repositoryItemSearchLookUpEditoffsetCreditGLCode.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemSearchLookUpEditoffsetCreditGLCode.Name = "repositoryItemSearchLookUpEditoffsetCreditGLCode";
            this.repositoryItemSearchLookUpEditoffsetCreditGLCode.PopupView = this.gridView2;
            // 
            // gridView2
            // 
            this.gridView2.DetailHeight = 546;
            this.gridView2.FixedLineWidth = 4;
            this.gridView2.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridView2.LevelIndent = 0;
            this.gridView2.Name = "gridView2";
            this.gridView2.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView2.OptionsView.ShowGroupPanel = false;
            this.gridView2.PreviewIndent = 0;
            // 
            // SampleBonus
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1834, 1184);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.groupControl1);
            this.Name = "SampleBonus";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SampleBonus";
            this.Load += new System.EventHandler(this.SampleBonus_Load);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtamount.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControlMaster)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewMaster)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBox5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSearchLookUpEdit3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEditStat)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBoxVarianceType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSearchLookUpEditoffsetdebitglcode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSearchLookUpEdit1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSearchLookUpEditdiscountglcode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSearchLookUpEditoffsetCreditGLCode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.TextEdit txtamount;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraGrid.GridControl gridControlMaster;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewMaster;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox repositoryItemComboBox5;
        private DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit repositoryItemSearchLookUpEdit3;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView5;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEditStat;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox repositoryItemComboBoxVarianceType;
        private DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit repositoryItemSearchLookUpEditoffsetdebitglcode;
        private DevExpress.XtraGrid.Views.Grid.GridView repositoryItemSearchLookUpEdit1View;
        private DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit repositoryItemSearchLookUpEditdiscountglcode;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit repositoryItemSearchLookUpEditoffsetCreditGLCode;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        public DevExpress.XtraEditors.SimpleButton btnSave;
    }
}