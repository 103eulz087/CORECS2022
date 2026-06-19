namespace SalesInventorySystem.AccountingDevEx
{
    partial class BankReconFormV1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.tableLayoutPanelMain = new System.Windows.Forms.TableLayoutPanel();
            this.panelBanner = new System.Windows.Forms.Panel();
            this.lblTitle = new DevExpress.XtraEditors.LabelControl();
            this.lblBranch = new DevExpress.XtraEditors.LabelControl();
            this.panelFilter = new System.Windows.Forms.Panel();
            this.flowLayoutPanelFilter = new System.Windows.Forms.FlowLayoutPanel();
            this.cmbBranch = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.cmbBranchView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.cmbAccount = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.cmbAccountView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.dtPeriod = new DevExpress.XtraEditors.DateEdit();
            this.btnLoad = new DevExpress.XtraEditors.SimpleButton();
            this.panelHeader = new System.Windows.Forms.Panel();
            this.flowLayoutPanelHeader = new System.Windows.Forms.FlowLayoutPanel();
            this.lblBookBal = new DevExpress.XtraEditors.LabelControl();
            this.txtBankBal = new DevExpress.XtraEditors.TextEdit();
            this.btnSaveHeader = new DevExpress.XtraEditors.SimpleButton();
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.gridItems = new DevExpress.XtraGrid.GridControl();
            this.viewItems = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.tableLayoutPanelSummary = new System.Windows.Forms.TableLayoutPanel();
            this.lblBankStmt = new DevExpress.XtraEditors.LabelControl();
            this.lblDIT = new DevExpress.XtraEditors.LabelControl();
            this.lblOC = new DevExpress.XtraEditors.LabelControl();
            this.lblAdjBank = new DevExpress.XtraEditors.LabelControl();
            this.lblBookSide = new DevExpress.XtraEditors.LabelControl();
            this.lblBCM = new DevExpress.XtraEditors.LabelControl();
            this.lblBDM = new DevExpress.XtraEditors.LabelControl();
            this.lblAdjBook = new DevExpress.XtraEditors.LabelControl();
            this.lblDiff = new DevExpress.XtraEditors.LabelControl();
            this.panelToolbar = new System.Windows.Forms.Panel();
            this.lblStatus = new DevExpress.XtraEditors.LabelControl();
            this.flowLayoutPanelToolbar = new System.Windows.Forms.FlowLayoutPanel();
            this.btnAdd = new DevExpress.XtraEditors.SimpleButton();
            this.btnEdit = new DevExpress.XtraEditors.SimpleButton();
            this.btnResolve = new DevExpress.XtraEditors.SimpleButton();
            this.btnDelete = new DevExpress.XtraEditors.SimpleButton();
            this.btnAutoMatch = new DevExpress.XtraEditors.SimpleButton();
            this.btnPrint = new DevExpress.XtraEditors.SimpleButton();
            this.tableLayoutPanelMain.SuspendLayout();
            this.panelBanner.SuspendLayout();
            this.panelFilter.SuspendLayout();
            this.flowLayoutPanelFilter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbBranch.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbBranchView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbAccount.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbAccountView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtPeriod.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtPeriod.Properties)).BeginInit();
            this.panelHeader.SuspendLayout();
            this.flowLayoutPanelHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtBankBal.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            this.splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridItems)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.viewItems)).BeginInit();
            this.tableLayoutPanelSummary.SuspendLayout();
            this.panelToolbar.SuspendLayout();
            this.flowLayoutPanelToolbar.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanelMain
            // 
            this.tableLayoutPanelMain.ColumnCount = 1;
            this.tableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelMain.Controls.Add(this.panelBanner, 0, 0);
            this.tableLayoutPanelMain.Controls.Add(this.panelFilter, 0, 1);
            this.tableLayoutPanelMain.Controls.Add(this.panelHeader, 0, 2);
            this.tableLayoutPanelMain.Controls.Add(this.splitContainerControl1, 0, 3);
            this.tableLayoutPanelMain.Controls.Add(this.panelToolbar, 0, 4);
            this.tableLayoutPanelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelMain.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanelMain.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tableLayoutPanelMain.Name = "tableLayoutPanelMain";
            this.tableLayoutPanelMain.RowCount = 5;
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 64F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 79F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 79F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 54F));
            this.tableLayoutPanelMain.Size = new System.Drawing.Size(1493, 985);
            this.tableLayoutPanelMain.TabIndex = 0;
            // 
            // panelBanner
            // 
            this.panelBanner.Controls.Add(this.lblTitle);
            this.panelBanner.Controls.Add(this.lblBranch);
            this.panelBanner.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelBanner.Location = new System.Drawing.Point(3, 4);
            this.panelBanner.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panelBanner.Name = "panelBanner";
            this.panelBanner.Size = new System.Drawing.Size(1487, 56);
            this.panelBanner.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.Location = new System.Drawing.Point(16, 16);
            this.lblTitle.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(109, 16);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Bank Reconciliation";
            // 
            // lblBranch
            // 
            this.lblBranch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblBranch.Location = new System.Drawing.Point(2537, 21);
            this.lblBranch.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lblBranch.Name = "lblBranch";
            this.lblBranch.Size = new System.Drawing.Size(48, 16);
            this.lblBranch.TabIndex = 1;
            this.lblBranch.Text = "Branch: ";
            // 
            // panelFilter
            // 
            this.panelFilter.Controls.Add(this.flowLayoutPanelFilter);
            this.panelFilter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelFilter.Location = new System.Drawing.Point(3, 68);
            this.panelFilter.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panelFilter.Name = "panelFilter";
            this.panelFilter.Padding = new System.Windows.Forms.Padding(12);
            this.panelFilter.Size = new System.Drawing.Size(1487, 71);
            this.panelFilter.TabIndex = 1;
            // 
            // flowLayoutPanelFilter
            // 
            this.flowLayoutPanelFilter.Controls.Add(this.cmbBranch);
            this.flowLayoutPanelFilter.Controls.Add(this.cmbAccount);
            this.flowLayoutPanelFilter.Controls.Add(this.dtPeriod);
            this.flowLayoutPanelFilter.Controls.Add(this.btnLoad);
            this.flowLayoutPanelFilter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanelFilter.Location = new System.Drawing.Point(12, 12);
            this.flowLayoutPanelFilter.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.flowLayoutPanelFilter.Name = "flowLayoutPanelFilter";
            this.flowLayoutPanelFilter.Size = new System.Drawing.Size(1463, 47);
            this.flowLayoutPanelFilter.TabIndex = 0;
            // 
            // cmbBranch
            // 
            this.cmbBranch.Location = new System.Drawing.Point(3, 4);
            this.cmbBranch.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cmbBranch.Name = "cmbBranch";
            this.cmbBranch.Properties.PopupView = this.cmbBranchView;
            this.cmbBranch.Size = new System.Drawing.Size(140, 22);
            this.cmbBranch.TabIndex = 0;
            // 
            // cmbBranchView
            // 
            this.cmbBranchView.DetailHeight = 431;
            this.cmbBranchView.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.cmbBranchView.Name = "cmbBranchView";
            this.cmbBranchView.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.cmbBranchView.OptionsView.ShowGroupPanel = false;
            // 
            // cmbAccount
            // 
            this.cmbAccount.Location = new System.Drawing.Point(149, 4);
            this.cmbAccount.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cmbAccount.Name = "cmbAccount";
            this.cmbAccount.Properties.PopupView = this.cmbAccountView;
            this.cmbAccount.Size = new System.Drawing.Size(327, 22);
            this.cmbAccount.TabIndex = 1;
            // 
            // cmbAccountView
            // 
            this.cmbAccountView.DetailHeight = 431;
            this.cmbAccountView.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.cmbAccountView.Name = "cmbAccountView";
            this.cmbAccountView.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.cmbAccountView.OptionsView.ShowGroupPanel = false;
            // 
            // dtPeriod
            // 
            this.dtPeriod.EditValue = new System.DateTime(2026, 6, 11, 0, 0, 0, 0);
            this.dtPeriod.Location = new System.Drawing.Point(482, 4);
            this.dtPeriod.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dtPeriod.Name = "dtPeriod";
            this.dtPeriod.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtPeriod.Size = new System.Drawing.Size(163, 22);
            this.dtPeriod.TabIndex = 2;
            // 
            // btnLoad
            // 
            this.btnLoad.Location = new System.Drawing.Point(651, 4);
            this.btnLoad.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(105, 30);
            this.btnLoad.TabIndex = 3;
            this.btnLoad.Text = "▶ Load";
            // 
            // panelHeader
            // 
            this.panelHeader.Controls.Add(this.flowLayoutPanelHeader);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelHeader.Location = new System.Drawing.Point(3, 147);
            this.panelHeader.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Padding = new System.Windows.Forms.Padding(12);
            this.panelHeader.Size = new System.Drawing.Size(1487, 71);
            this.panelHeader.TabIndex = 2;
            // 
            // flowLayoutPanelHeader
            // 
            this.flowLayoutPanelHeader.Controls.Add(this.lblBookBal);
            this.flowLayoutPanelHeader.Controls.Add(this.txtBankBal);
            this.flowLayoutPanelHeader.Controls.Add(this.btnSaveHeader);
            this.flowLayoutPanelHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanelHeader.Location = new System.Drawing.Point(12, 12);
            this.flowLayoutPanelHeader.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.flowLayoutPanelHeader.Name = "flowLayoutPanelHeader";
            this.flowLayoutPanelHeader.Size = new System.Drawing.Size(1463, 47);
            this.flowLayoutPanelHeader.TabIndex = 0;
            // 
            // lblBookBal
            // 
            this.lblBookBal.Location = new System.Drawing.Point(3, 4);
            this.lblBookBal.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lblBookBal.Name = "lblBookBal";
            this.lblBookBal.Size = new System.Drawing.Size(25, 16);
            this.lblBookBal.TabIndex = 0;
            this.lblBookBal.Text = "0.00";
            // 
            // txtBankBal
            // 
            this.txtBankBal.EditValue = "0.00";
            this.txtBankBal.Location = new System.Drawing.Point(34, 4);
            this.txtBankBal.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtBankBal.Name = "txtBankBal";
            this.txtBankBal.Size = new System.Drawing.Size(187, 22);
            this.txtBankBal.TabIndex = 1;
            // 
            // btnSaveHeader
            // 
            this.btnSaveHeader.Location = new System.Drawing.Point(227, 4);
            this.btnSaveHeader.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnSaveHeader.Name = "btnSaveHeader";
            this.btnSaveHeader.Size = new System.Drawing.Size(140, 30);
            this.btnSaveHeader.TabIndex = 2;
            this.btnSaveHeader.Text = "💾 Save Balance";
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.Location = new System.Drawing.Point(3, 226);
            this.splitContainerControl1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.splitContainerControl1.Name = "splitContainerControl1";
            this.splitContainerControl1.Panel1.Controls.Add(this.gridItems);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            this.splitContainerControl1.Panel2.Controls.Add(this.tableLayoutPanelSummary);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(1487, 701);
            this.splitContainerControl1.SplitterPosition = 957;
            this.splitContainerControl1.TabIndex = 3;
            // 
            // gridItems
            // 
            this.gridItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridItems.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gridItems.Location = new System.Drawing.Point(0, 0);
            this.gridItems.MainView = this.viewItems;
            this.gridItems.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gridItems.Name = "gridItems";
            this.gridItems.Size = new System.Drawing.Size(957, 701);
            this.gridItems.TabIndex = 0;
            this.gridItems.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.viewItems});
            // 
            // viewItems
            // 
            this.viewItems.DetailHeight = 431;
            this.viewItems.GridControl = this.gridItems;
            this.viewItems.Name = "viewItems";
            // 
            // tableLayoutPanelSummary
            // 
            this.tableLayoutPanelSummary.ColumnCount = 2;
            this.tableLayoutPanelSummary.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tableLayoutPanelSummary.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanelSummary.Controls.Add(this.lblBankStmt, 1, 0);
            this.tableLayoutPanelSummary.Controls.Add(this.lblDIT, 1, 1);
            this.tableLayoutPanelSummary.Controls.Add(this.lblOC, 1, 2);
            this.tableLayoutPanelSummary.Controls.Add(this.lblAdjBank, 1, 3);
            this.tableLayoutPanelSummary.Controls.Add(this.lblBookSide, 1, 4);
            this.tableLayoutPanelSummary.Controls.Add(this.lblBCM, 1, 5);
            this.tableLayoutPanelSummary.Controls.Add(this.lblBDM, 1, 6);
            this.tableLayoutPanelSummary.Controls.Add(this.lblAdjBook, 1, 7);
            this.tableLayoutPanelSummary.Controls.Add(this.lblDiff, 1, 8);
            this.tableLayoutPanelSummary.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelSummary.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanelSummary.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tableLayoutPanelSummary.Name = "tableLayoutPanelSummary";
            this.tableLayoutPanelSummary.RowCount = 10;
            this.tableLayoutPanelSummary.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 37F));
            this.tableLayoutPanelSummary.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 37F));
            this.tableLayoutPanelSummary.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 37F));
            this.tableLayoutPanelSummary.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 49F));
            this.tableLayoutPanelSummary.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 37F));
            this.tableLayoutPanelSummary.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 37F));
            this.tableLayoutPanelSummary.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 37F));
            this.tableLayoutPanelSummary.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 49F));
            this.tableLayoutPanelSummary.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 49F));
            this.tableLayoutPanelSummary.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelSummary.Size = new System.Drawing.Size(518, 701);
            this.tableLayoutPanelSummary.TabIndex = 0;
            // 
            // lblBankStmt
            // 
            this.lblBankStmt.Location = new System.Drawing.Point(313, 4);
            this.lblBankStmt.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lblBankStmt.Name = "lblBankStmt";
            this.lblBankStmt.Size = new System.Drawing.Size(25, 16);
            this.lblBankStmt.TabIndex = 0;
            this.lblBankStmt.Text = "0.00";
            // 
            // lblDIT
            // 
            this.lblDIT.Location = new System.Drawing.Point(313, 41);
            this.lblDIT.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lblDIT.Name = "lblDIT";
            this.lblDIT.Size = new System.Drawing.Size(25, 16);
            this.lblDIT.TabIndex = 1;
            this.lblDIT.Text = "0.00";
            // 
            // lblOC
            // 
            this.lblOC.Location = new System.Drawing.Point(313, 78);
            this.lblOC.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lblOC.Name = "lblOC";
            this.lblOC.Size = new System.Drawing.Size(25, 16);
            this.lblOC.TabIndex = 2;
            this.lblOC.Text = "0.00";
            // 
            // lblAdjBank
            // 
            this.lblAdjBank.Location = new System.Drawing.Point(313, 115);
            this.lblAdjBank.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lblAdjBank.Name = "lblAdjBank";
            this.lblAdjBank.Size = new System.Drawing.Size(25, 16);
            this.lblAdjBank.TabIndex = 3;
            this.lblAdjBank.Text = "0.00";
            // 
            // lblBookSide
            // 
            this.lblBookSide.Location = new System.Drawing.Point(313, 164);
            this.lblBookSide.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lblBookSide.Name = "lblBookSide";
            this.lblBookSide.Size = new System.Drawing.Size(25, 16);
            this.lblBookSide.TabIndex = 4;
            this.lblBookSide.Text = "0.00";
            // 
            // lblBCM
            // 
            this.lblBCM.Location = new System.Drawing.Point(313, 201);
            this.lblBCM.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lblBCM.Name = "lblBCM";
            this.lblBCM.Size = new System.Drawing.Size(25, 16);
            this.lblBCM.TabIndex = 5;
            this.lblBCM.Text = "0.00";
            // 
            // lblBDM
            // 
            this.lblBDM.Location = new System.Drawing.Point(313, 238);
            this.lblBDM.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lblBDM.Name = "lblBDM";
            this.lblBDM.Size = new System.Drawing.Size(25, 16);
            this.lblBDM.TabIndex = 6;
            this.lblBDM.Text = "0.00";
            // 
            // lblAdjBook
            // 
            this.lblAdjBook.Location = new System.Drawing.Point(313, 275);
            this.lblAdjBook.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lblAdjBook.Name = "lblAdjBook";
            this.lblAdjBook.Size = new System.Drawing.Size(25, 16);
            this.lblAdjBook.TabIndex = 7;
            this.lblAdjBook.Text = "0.00";
            // 
            // lblDiff
            // 
            this.lblDiff.Location = new System.Drawing.Point(313, 324);
            this.lblDiff.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lblDiff.Name = "lblDiff";
            this.lblDiff.Size = new System.Drawing.Size(25, 16);
            this.lblDiff.TabIndex = 8;
            this.lblDiff.Text = "0.00";
            // 
            // panelToolbar
            // 
            this.panelToolbar.Controls.Add(this.lblStatus);
            this.panelToolbar.Controls.Add(this.flowLayoutPanelToolbar);
            this.panelToolbar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelToolbar.Location = new System.Drawing.Point(3, 935);
            this.panelToolbar.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panelToolbar.Name = "panelToolbar";
            this.panelToolbar.Size = new System.Drawing.Size(1487, 46);
            this.panelToolbar.TabIndex = 4;
            // 
            // lblStatus
            // 
            this.lblStatus.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblStatus.Location = new System.Drawing.Point(1274, 0);
            this.lblStatus.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(213, 16);
            this.lblStatus.TabIndex = 0;
            this.lblStatus.Text = "Select a bank account and click Load.";
            // 
            // flowLayoutPanelToolbar
            // 
            this.flowLayoutPanelToolbar.AutoSize = true;
            this.flowLayoutPanelToolbar.Controls.Add(this.btnAdd);
            this.flowLayoutPanelToolbar.Controls.Add(this.btnEdit);
            this.flowLayoutPanelToolbar.Controls.Add(this.btnResolve);
            this.flowLayoutPanelToolbar.Controls.Add(this.btnDelete);
            this.flowLayoutPanelToolbar.Controls.Add(this.btnAutoMatch);
            this.flowLayoutPanelToolbar.Controls.Add(this.btnPrint);
            this.flowLayoutPanelToolbar.Dock = System.Windows.Forms.DockStyle.Left;
            this.flowLayoutPanelToolbar.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanelToolbar.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.flowLayoutPanelToolbar.Name = "flowLayoutPanelToolbar";
            this.flowLayoutPanelToolbar.Size = new System.Drawing.Size(116, 46);
            this.flowLayoutPanelToolbar.TabIndex = 1;
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(3, 4);
            this.btnAdd.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(110, 36);
            this.btnAdd.TabIndex = 0;
            this.btnAdd.Text = "＋ Add Item";
            // 
            // btnEdit
            // 
            this.btnEdit.Location = new System.Drawing.Point(3, 48);
            this.btnEdit.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(110, 36);
            this.btnEdit.TabIndex = 1;
            this.btnEdit.Text = "✎ Edit";
            // 
            // btnResolve
            // 
            this.btnResolve.Location = new System.Drawing.Point(3, 92);
            this.btnResolve.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnResolve.Name = "btnResolve";
            this.btnResolve.Size = new System.Drawing.Size(110, 36);
            this.btnResolve.TabIndex = 2;
            this.btnResolve.Text = "✔ Resolve";
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(3, 136);
            this.btnDelete.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(110, 36);
            this.btnDelete.TabIndex = 3;
            this.btnDelete.Text = "✖ Delete";
            // 
            // btnAutoMatch
            // 
            this.btnAutoMatch.Location = new System.Drawing.Point(3, 180);
            this.btnAutoMatch.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnAutoMatch.Name = "btnAutoMatch";
            this.btnAutoMatch.Size = new System.Drawing.Size(110, 36);
            this.btnAutoMatch.TabIndex = 4;
            this.btnAutoMatch.Text = "⚡ Auto-Match";
            // 
            // btnPrint
            // 
            this.btnPrint.Location = new System.Drawing.Point(3, 224);
            this.btnPrint.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(110, 36);
            this.btnPrint.TabIndex = 5;
            this.btnPrint.Text = "🖨 Print";
            // 
            // BankReconFormV1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1493, 985);
            this.Controls.Add(this.tableLayoutPanelMain);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "BankReconFormV1";
            this.Text = "Bank Reconciliation";
            this.tableLayoutPanelMain.ResumeLayout(false);
            this.panelBanner.ResumeLayout(false);
            this.panelBanner.PerformLayout();
            this.panelFilter.ResumeLayout(false);
            this.flowLayoutPanelFilter.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cmbBranch.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbBranchView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbAccount.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbAccountView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtPeriod.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtPeriod.Properties)).EndInit();
            this.panelHeader.ResumeLayout(false);
            this.flowLayoutPanelHeader.ResumeLayout(false);
            this.flowLayoutPanelHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtBankBal.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridItems)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.viewItems)).EndInit();
            this.tableLayoutPanelSummary.ResumeLayout(false);
            this.tableLayoutPanelSummary.PerformLayout();
            this.panelToolbar.ResumeLayout(false);
            this.panelToolbar.PerformLayout();
            this.flowLayoutPanelToolbar.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelMain;
        private System.Windows.Forms.Panel panelBanner;
        private DevExpress.XtraEditors.LabelControl lblTitle;
        private DevExpress.XtraEditors.LabelControl lblBranch;

        private System.Windows.Forms.Panel panelFilter;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelFilter;
        private DevExpress.XtraEditors.SearchLookUpEdit cmbBranch;
        private DevExpress.XtraGrid.Views.Grid.GridView viewBranch;
        private DevExpress.XtraEditors.SearchLookUpEdit cmbAccount;
        private DevExpress.XtraGrid.Views.Grid.GridView viewAccount;
        private DevExpress.XtraEditors.DateEdit dtPeriod;
        private DevExpress.XtraEditors.SimpleButton btnLoad;

        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelHeader;
        private DevExpress.XtraEditors.LabelControl lblBookBal;
        private DevExpress.XtraEditors.TextEdit txtBankBal;
        private DevExpress.XtraEditors.SimpleButton btnSaveHeader;

        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private DevExpress.XtraGrid.GridControl gridItems;
        private DevExpress.XtraGrid.Views.Grid.GridView viewItems;

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelSummary;
        private DevExpress.XtraEditors.LabelControl lblBankStmt;
        private DevExpress.XtraEditors.LabelControl lblDIT;
        private DevExpress.XtraEditors.LabelControl lblOC;
        private DevExpress.XtraEditors.LabelControl lblAdjBank;
        private DevExpress.XtraEditors.LabelControl lblBookSide;
        private DevExpress.XtraEditors.LabelControl lblBCM;
        private DevExpress.XtraEditors.LabelControl lblBDM;
        private DevExpress.XtraEditors.LabelControl lblAdjBook;
        private DevExpress.XtraEditors.LabelControl lblDiff;

        private System.Windows.Forms.Panel panelToolbar;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelToolbar;
        private DevExpress.XtraEditors.LabelControl lblStatus;
        private DevExpress.XtraEditors.SimpleButton btnAdd;
        private DevExpress.XtraEditors.SimpleButton btnEdit;
        private DevExpress.XtraEditors.SimpleButton btnResolve;
        private DevExpress.XtraEditors.SimpleButton btnDelete;
        private DevExpress.XtraEditors.SimpleButton btnAutoMatch;
        private DevExpress.XtraEditors.SimpleButton btnPrint;
        private DevExpress.XtraGrid.Views.Grid.GridView cmbBranchView;
        private DevExpress.XtraGrid.Views.Grid.GridView cmbAccountView;
    }
}