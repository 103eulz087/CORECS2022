namespace SalesInventorySystem.HOFormsDevEx
{
    partial class ViewExpenseDetailsDevEx
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
            DevExpress.XtraGrid.GridLevelNode gridLevelNode1 = new DevExpress.XtraGrid.GridLevelNode();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ViewExpenseDetailsDevEx));
            this.gridView5 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridControl2 = new DevExpress.XtraGrid.GridControl();
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.repositoryItemComboBox1 = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.repositoryItemSearchLookUpEditglcode = new DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit();
            this.repositoryItemSearchLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.repositoryItemSearchLookUpEditOffsetGLCode = new DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.repositoryItemCheckEditStatus = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.repositoryItemSearchLookUpEditOffsetCreditGLCode = new DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit();
            this.gridView4 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.repositoryItemSearchLookUpEditEWTDebitGLCode = new DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit();
            this.gridView6 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.repositoryItemSearchLookUpEditEWTCreditGLCode = new DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit();
            this.gridView7 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.txtsuppid = new DevExpress.XtraEditors.TextEdit();
            this.txtinvoiceno = new DevExpress.XtraEditors.TextEdit();
            this.txtrefno = new DevExpress.XtraEditors.TextEdit();
            this.btncancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnApproved = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.gridView5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSearchLookUpEditglcode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSearchLookUpEdit1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSearchLookUpEditOffsetGLCode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEditStatus)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSearchLookUpEditOffsetCreditGLCode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSearchLookUpEditEWTDebitGLCode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSearchLookUpEditEWTCreditGLCode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtsuppid.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtinvoiceno.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtrefno.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // gridView5
            // 
            this.gridView5.DetailHeight = 322;
            this.gridView5.GridControl = this.gridControl2;
            this.gridView5.LevelIndent = 0;
            this.gridView5.Name = "gridView5";
            this.gridView5.PreviewIndent = 0;
            // 
            // gridControl2
            // 
            this.gridControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl2.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            gridLevelNode1.LevelTemplate = this.gridView5;
            gridLevelNode1.RelationName = "Level1";
            this.gridControl2.LevelTree.Nodes.AddRange(new DevExpress.XtraGrid.GridLevelNode[] {
            gridLevelNode1});
            this.gridControl2.Location = new System.Drawing.Point(3, 39);
            this.gridControl2.MainView = this.gridView2;
            this.gridControl2.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.gridControl2.Name = "gridControl2";
            this.gridControl2.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemComboBox1,
            this.repositoryItemSearchLookUpEditglcode,
            this.repositoryItemSearchLookUpEditOffsetGLCode,
            this.repositoryItemCheckEditStatus,
            this.repositoryItemSearchLookUpEditOffsetCreditGLCode,
            this.repositoryItemSearchLookUpEditEWTDebitGLCode,
            this.repositoryItemSearchLookUpEditEWTCreditGLCode});
            this.gridControl2.Size = new System.Drawing.Size(2075, 1257);
            this.gridControl2.TabIndex = 8;
            this.gridControl2.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView2,
            this.gridView5});
            // 
            // gridView2
            // 
            this.gridView2.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 9.25F, System.Drawing.FontStyle.Bold);
            this.gridView2.Appearance.HeaderPanel.Options.UseFont = true;
            this.gridView2.Appearance.Row.Font = new System.Drawing.Font("Tahoma", 9.25F);
            this.gridView2.Appearance.Row.Options.UseFont = true;
            this.gridView2.DetailHeight = 322;
            this.gridView2.GridControl = this.gridControl2;
            this.gridView2.LevelIndent = 0;
            this.gridView2.Name = "gridView2";
            this.gridView2.OptionsBehavior.Editable = false;
            this.gridView2.OptionsBehavior.ReadOnly = true;
            this.gridView2.OptionsView.ColumnAutoWidth = false;
            this.gridView2.OptionsView.RowAutoHeight = true;
            this.gridView2.OptionsView.ShowFooter = true;
            this.gridView2.PreviewIndent = 0;
            // 
            // repositoryItemComboBox1
            // 
            this.repositoryItemComboBox1.AutoHeight = false;
            this.repositoryItemComboBox1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemComboBox1.Items.AddRange(new object[] {
            "NONE",
            "PAY"});
            this.repositoryItemComboBox1.Name = "repositoryItemComboBox1";
            // 
            // repositoryItemSearchLookUpEditglcode
            // 
            this.repositoryItemSearchLookUpEditglcode.AutoHeight = false;
            this.repositoryItemSearchLookUpEditglcode.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemSearchLookUpEditglcode.Name = "repositoryItemSearchLookUpEditglcode";
            this.repositoryItemSearchLookUpEditglcode.PopupView = this.repositoryItemSearchLookUpEdit1View;
            // 
            // repositoryItemSearchLookUpEdit1View
            // 
            this.repositoryItemSearchLookUpEdit1View.DetailHeight = 503;
            this.repositoryItemSearchLookUpEdit1View.FixedLineWidth = 3;
            this.repositoryItemSearchLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.repositoryItemSearchLookUpEdit1View.Name = "repositoryItemSearchLookUpEdit1View";
            this.repositoryItemSearchLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.repositoryItemSearchLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            // 
            // repositoryItemSearchLookUpEditOffsetGLCode
            // 
            this.repositoryItemSearchLookUpEditOffsetGLCode.AutoHeight = false;
            this.repositoryItemSearchLookUpEditOffsetGLCode.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemSearchLookUpEditOffsetGLCode.Name = "repositoryItemSearchLookUpEditOffsetGLCode";
            this.repositoryItemSearchLookUpEditOffsetGLCode.PopupView = this.gridView1;
            // 
            // gridView1
            // 
            this.gridView1.DetailHeight = 503;
            this.gridView1.FixedLineWidth = 3;
            this.gridView1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // repositoryItemCheckEditStatus
            // 
            this.repositoryItemCheckEditStatus.AutoHeight = false;
            this.repositoryItemCheckEditStatus.Name = "repositoryItemCheckEditStatus";
            this.repositoryItemCheckEditStatus.ValueChecked = "True";
            this.repositoryItemCheckEditStatus.ValueUnchecked = "False";
            // 
            // repositoryItemSearchLookUpEditOffsetCreditGLCode
            // 
            this.repositoryItemSearchLookUpEditOffsetCreditGLCode.AutoHeight = false;
            this.repositoryItemSearchLookUpEditOffsetCreditGLCode.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemSearchLookUpEditOffsetCreditGLCode.Name = "repositoryItemSearchLookUpEditOffsetCreditGLCode";
            this.repositoryItemSearchLookUpEditOffsetCreditGLCode.PopupView = this.gridView4;
            // 
            // gridView4
            // 
            this.gridView4.DetailHeight = 503;
            this.gridView4.FixedLineWidth = 3;
            this.gridView4.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridView4.Name = "gridView4";
            this.gridView4.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView4.OptionsView.ShowGroupPanel = false;
            // 
            // repositoryItemSearchLookUpEditEWTDebitGLCode
            // 
            this.repositoryItemSearchLookUpEditEWTDebitGLCode.AutoHeight = false;
            this.repositoryItemSearchLookUpEditEWTDebitGLCode.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemSearchLookUpEditEWTDebitGLCode.Name = "repositoryItemSearchLookUpEditEWTDebitGLCode";
            this.repositoryItemSearchLookUpEditEWTDebitGLCode.PopupView = this.gridView6;
            // 
            // gridView6
            // 
            this.gridView6.DetailHeight = 503;
            this.gridView6.FixedLineWidth = 3;
            this.gridView6.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridView6.Name = "gridView6";
            this.gridView6.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView6.OptionsView.ShowGroupPanel = false;
            // 
            // repositoryItemSearchLookUpEditEWTCreditGLCode
            // 
            this.repositoryItemSearchLookUpEditEWTCreditGLCode.AutoHeight = false;
            this.repositoryItemSearchLookUpEditEWTCreditGLCode.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemSearchLookUpEditEWTCreditGLCode.Name = "repositoryItemSearchLookUpEditEWTCreditGLCode";
            this.repositoryItemSearchLookUpEditEWTCreditGLCode.PopupView = this.gridView7;
            // 
            // gridView7
            // 
            this.gridView7.DetailHeight = 503;
            this.gridView7.FixedLineWidth = 3;
            this.gridView7.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridView7.Name = "gridView7";
            this.gridView7.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView7.OptionsView.ShowGroupPanel = false;
            // 
            // groupControl2
            // 
            this.groupControl2.Controls.Add(this.gridControl2);
            this.groupControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl2.Location = new System.Drawing.Point(0, 118);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(2081, 1299);
            this.groupControl2.TabIndex = 3;
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.txtsuppid);
            this.groupControl1.Controls.Add(this.txtinvoiceno);
            this.groupControl1.Controls.Add(this.txtrefno);
            this.groupControl1.Controls.Add(this.btncancel);
            this.groupControl1.Controls.Add(this.btnApproved);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(2081, 118);
            this.groupControl1.TabIndex = 2;
            // 
            // txtsuppid
            // 
            this.txtsuppid.Location = new System.Drawing.Point(1088, 49);
            this.txtsuppid.Name = "txtsuppid";
            this.txtsuppid.Size = new System.Drawing.Size(202, 36);
            this.txtsuppid.TabIndex = 9;
            this.txtsuppid.Visible = false;
            // 
            // txtinvoiceno
            // 
            this.txtinvoiceno.Location = new System.Drawing.Point(853, 49);
            this.txtinvoiceno.Name = "txtinvoiceno";
            this.txtinvoiceno.Size = new System.Drawing.Size(202, 36);
            this.txtinvoiceno.TabIndex = 8;
            this.txtinvoiceno.Visible = false;
            // 
            // txtrefno
            // 
            this.txtrefno.Location = new System.Drawing.Point(619, 49);
            this.txtrefno.Name = "txtrefno";
            this.txtrefno.Size = new System.Drawing.Size(202, 36);
            this.txtrefno.TabIndex = 7;
            this.txtrefno.Visible = false;
            // 
            // btncancel
            // 
            this.btncancel.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btncancel.ImageOptions.Image")));
            this.btncancel.Location = new System.Drawing.Point(172, 54);
            this.btncancel.Name = "btncancel";
            this.btncancel.Size = new System.Drawing.Size(182, 49);
            this.btncancel.TabIndex = 6;
            this.btncancel.Text = "DisApproved";
            this.btncancel.Visible = false;
            this.btncancel.Click += new System.EventHandler(this.simpleButton3_Click);
            // 
            // btnApproved
            // 
            this.btnApproved.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnApproved.ImageOptions.Image")));
            this.btnApproved.Location = new System.Drawing.Point(12, 54);
            this.btnApproved.Name = "btnApproved";
            this.btnApproved.Size = new System.Drawing.Size(153, 49);
            this.btnApproved.TabIndex = 5;
            this.btnApproved.Text = "Approved";
            this.btnApproved.Visible = false;
            this.btnApproved.Click += new System.EventHandler(this.simpleButton2_Click);
            // 
            // ViewExpenseDetailsDevEx
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(2081, 1417);
            this.Controls.Add(this.groupControl2);
            this.Controls.Add(this.groupControl1);
            this.Name = "ViewExpenseDetailsDevEx";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ViewExpenseDetailsDevEx";
            this.Load += new System.EventHandler(this.ViewExpenseDetailsDevEx_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridView5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSearchLookUpEditglcode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSearchLookUpEdit1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSearchLookUpEditOffsetGLCode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEditStatus)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSearchLookUpEditOffsetCreditGLCode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSearchLookUpEditEWTDebitGLCode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSearchLookUpEditEWTCreditGLCode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtsuppid.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtinvoiceno.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtrefno.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControl2;
        public DevExpress.XtraGrid.GridControl gridControl2;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView5;
        public DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox repositoryItemComboBox1;
        private DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit repositoryItemSearchLookUpEditglcode;
        private DevExpress.XtraGrid.Views.Grid.GridView repositoryItemSearchLookUpEdit1View;
        private DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit repositoryItemSearchLookUpEditOffsetGLCode;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEditStatus;
        private DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit repositoryItemSearchLookUpEditOffsetCreditGLCode;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView4;
        private DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit repositoryItemSearchLookUpEditEWTDebitGLCode;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView6;
        private DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit repositoryItemSearchLookUpEditEWTCreditGLCode;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView7;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        public DevExpress.XtraEditors.SimpleButton btncancel;
        public DevExpress.XtraEditors.SimpleButton btnApproved;
        public DevExpress.XtraEditors.TextEdit txtinvoiceno;
        public DevExpress.XtraEditors.TextEdit txtrefno;
        public DevExpress.XtraEditors.TextEdit txtsuppid;
    }
}