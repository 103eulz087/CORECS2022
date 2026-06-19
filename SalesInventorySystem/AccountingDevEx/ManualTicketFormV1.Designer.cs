namespace SalesInventorySystem.AccountingDevEx
{
    partial class ManualTicketFormV1
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
            this.lblBannerTitle = new DevExpress.XtraEditors.LabelControl();
            this.lblBannerSub = new DevExpress.XtraEditors.LabelControl();
            this.panelHeader = new System.Windows.Forms.Panel();
            this.flowLayoutPanelHeader = new System.Windows.Forms.FlowLayoutPanel();
            this.radSourceType = new DevExpress.XtraEditors.RadioGroup();
            this.cmbAdjType = new DevExpress.XtraEditors.ComboBoxEdit();
            this.cmbSupplier = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.cmbSupplierView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.cmbInvoice = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.cmbInvoiceView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.lblInvoiceBalance = new DevExpress.XtraEditors.LabelControl();
            this.txtAdjAmount = new DevExpress.XtraEditors.TextEdit();
            this.cmbAPImpact = new DevExpress.XtraEditors.ComboBoxEdit();
            this.txtOrigTicket = new DevExpress.XtraEditors.TextEdit();
            this.txtDocRef = new DevExpress.XtraEditors.TextEdit();
            this.txtRemarks = new DevExpress.XtraEditors.TextEdit();
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.tableLayoutPanelLeft = new System.Windows.Forms.TableLayoutPanel();
            this.lblLegs = new DevExpress.XtraEditors.LabelControl();
            this.flowLayoutPanelLegBar = new System.Windows.Forms.FlowLayoutPanel();
            this.cmbLegAccount = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.cmbLegAccountView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.cmbLegDC = new DevExpress.XtraEditors.ComboBoxEdit();
            this.txtLegAmount = new DevExpress.XtraEditors.TextEdit();
            this.txtLegDesc = new DevExpress.XtraEditors.TextEdit();
            this.btnAddLeg = new DevExpress.XtraEditors.SimpleButton();
            this.btnRemoveLeg = new DevExpress.XtraEditors.SimpleButton();
            this.gridLegs = new DevExpress.XtraGrid.GridControl();
            this.viewLegs = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.tableLayoutPanelRight = new System.Windows.Forms.TableLayoutPanel();
            this.lblPend = new DevExpress.XtraEditors.LabelControl();
            this.gridPending = new DevExpress.XtraGrid.GridControl();
            this.viewPending = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.panelToolbar = new System.Windows.Forms.Panel();
            this.lblStatus = new DevExpress.XtraEditors.LabelControl();
            this.flowLayoutPanelToolbar = new System.Windows.Forms.FlowLayoutPanel();
            this.btnPost = new DevExpress.XtraEditors.SimpleButton();
            this.btnApprove = new DevExpress.XtraEditors.SimpleButton();
            this.btnReject = new DevExpress.XtraEditors.SimpleButton();
            this.btnRefresh = new DevExpress.XtraEditors.SimpleButton();
            this.tableLayoutPanelMain.SuspendLayout();
            this.panelBanner.SuspendLayout();
            this.panelHeader.SuspendLayout();
            this.flowLayoutPanelHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radSourceType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbAdjType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbSupplier.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbSupplierView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbInvoice.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbInvoiceView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAdjAmount.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbAPImpact.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtOrigTicket.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDocRef.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRemarks.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            this.splitContainerControl1.SuspendLayout();
            this.tableLayoutPanelLeft.SuspendLayout();
            this.flowLayoutPanelLegBar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbLegAccount.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbLegAccountView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbLegDC.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLegAmount.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLegDesc.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLegs)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.viewLegs)).BeginInit();
            this.tableLayoutPanelRight.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridPending)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.viewPending)).BeginInit();
            this.panelToolbar.SuspendLayout();
            this.flowLayoutPanelToolbar.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanelMain
            // 
            this.tableLayoutPanelMain.ColumnCount = 1;
            this.tableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelMain.Controls.Add(this.panelBanner, 0, 0);
            this.tableLayoutPanelMain.Controls.Add(this.panelHeader, 0, 1);
            this.tableLayoutPanelMain.Controls.Add(this.splitContainerControl1, 0, 2);
            this.tableLayoutPanelMain.Controls.Add(this.panelToolbar, 0, 3);
            this.tableLayoutPanelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelMain.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanelMain.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tableLayoutPanelMain.Name = "tableLayoutPanelMain";
            this.tableLayoutPanelMain.RowCount = 4;
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 64F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 241F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 54F));
            this.tableLayoutPanelMain.Size = new System.Drawing.Size(1493, 985);
            this.tableLayoutPanelMain.TabIndex = 0;
            // 
            // panelBanner
            // 
            this.panelBanner.Controls.Add(this.lblBannerTitle);
            this.panelBanner.Controls.Add(this.lblBannerSub);
            this.panelBanner.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelBanner.Location = new System.Drawing.Point(3, 4);
            this.panelBanner.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panelBanner.Name = "panelBanner";
            this.panelBanner.Size = new System.Drawing.Size(1487, 56);
            this.panelBanner.TabIndex = 0;
            // 
            // lblBannerTitle
            // 
            this.lblBannerTitle.Location = new System.Drawing.Point(16, 15);
            this.lblBannerTitle.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lblBannerTitle.Name = "lblBannerTitle";
            this.lblBannerTitle.Size = new System.Drawing.Size(96, 16);
            this.lblBannerTitle.TabIndex = 0;
            this.lblBannerTitle.Text = "Manual Ticketing";
            // 
            // lblBannerSub
            // 
            this.lblBannerSub.Location = new System.Drawing.Point(16, 36);
            this.lblBannerSub.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lblBannerSub.Name = "lblBannerSub";
            this.lblBannerSub.Size = new System.Drawing.Size(456, 16);
            this.lblBannerSub.TabIndex = 1;
            this.lblBannerSub.Text = "Purchase Adjustments (APACCOUNTS) · Expense Adjustments (ExpenseMaster)";
            // 
            // panelHeader
            // 
            this.panelHeader.Controls.Add(this.flowLayoutPanelHeader);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelHeader.Location = new System.Drawing.Point(3, 68);
            this.panelHeader.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(1487, 233);
            this.panelHeader.TabIndex = 1;
            // 
            // flowLayoutPanelHeader
            // 
            this.flowLayoutPanelHeader.Controls.Add(this.radSourceType);
            this.flowLayoutPanelHeader.Controls.Add(this.cmbAdjType);
            this.flowLayoutPanelHeader.Controls.Add(this.cmbSupplier);
            this.flowLayoutPanelHeader.Controls.Add(this.cmbInvoice);
            this.flowLayoutPanelHeader.Controls.Add(this.lblInvoiceBalance);
            this.flowLayoutPanelHeader.Controls.Add(this.txtAdjAmount);
            this.flowLayoutPanelHeader.Controls.Add(this.cmbAPImpact);
            this.flowLayoutPanelHeader.Controls.Add(this.txtOrigTicket);
            this.flowLayoutPanelHeader.Controls.Add(this.txtDocRef);
            this.flowLayoutPanelHeader.Controls.Add(this.txtRemarks);
            this.flowLayoutPanelHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanelHeader.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanelHeader.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.flowLayoutPanelHeader.Name = "flowLayoutPanelHeader";
            this.flowLayoutPanelHeader.Padding = new System.Windows.Forms.Padding(12);
            this.flowLayoutPanelHeader.Size = new System.Drawing.Size(1487, 233);
            this.flowLayoutPanelHeader.TabIndex = 0;
            // 
            // radSourceType
            // 
            this.radSourceType.Location = new System.Drawing.Point(15, 16);
            this.radSourceType.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.radSourceType.Name = "radSourceType";
            this.radSourceType.Size = new System.Drawing.Size(175, 49);
            this.radSourceType.TabIndex = 0;
            // 
            // cmbAdjType
            // 
            this.cmbAdjType.Location = new System.Drawing.Point(196, 16);
            this.cmbAdjType.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cmbAdjType.Name = "cmbAdjType";
            this.cmbAdjType.Size = new System.Drawing.Size(163, 22);
            this.cmbAdjType.TabIndex = 1;
            // 
            // cmbSupplier
            // 
            this.cmbSupplier.Location = new System.Drawing.Point(365, 16);
            this.cmbSupplier.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cmbSupplier.Name = "cmbSupplier";
            this.cmbSupplier.Properties.PopupView = this.cmbSupplierView;
            this.cmbSupplier.Size = new System.Drawing.Size(233, 22);
            this.cmbSupplier.TabIndex = 2;
            // 
            // cmbSupplierView
            // 
            this.cmbSupplierView.DetailHeight = 431;
            this.cmbSupplierView.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.cmbSupplierView.Name = "cmbSupplierView";
            this.cmbSupplierView.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.cmbSupplierView.OptionsView.ShowGroupPanel = false;
            // 
            // cmbInvoice
            // 
            this.cmbInvoice.Location = new System.Drawing.Point(604, 16);
            this.cmbInvoice.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cmbInvoice.Name = "cmbInvoice";
            this.cmbInvoice.Properties.PopupView = this.cmbInvoiceView;
            this.cmbInvoice.Size = new System.Drawing.Size(233, 22);
            this.cmbInvoice.TabIndex = 3;
            // 
            // cmbInvoiceView
            // 
            this.cmbInvoiceView.DetailHeight = 431;
            this.cmbInvoiceView.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.cmbInvoiceView.Name = "cmbInvoiceView";
            this.cmbInvoiceView.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.cmbInvoiceView.OptionsView.ShowGroupPanel = false;
            // 
            // lblInvoiceBalance
            // 
            this.lblInvoiceBalance.Location = new System.Drawing.Point(843, 16);
            this.lblInvoiceBalance.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lblInvoiceBalance.Name = "lblInvoiceBalance";
            this.lblInvoiceBalance.Size = new System.Drawing.Size(0, 16);
            this.lblInvoiceBalance.TabIndex = 4;
            // 
            // txtAdjAmount
            // 
            this.txtAdjAmount.Location = new System.Drawing.Point(849, 16);
            this.txtAdjAmount.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtAdjAmount.Name = "txtAdjAmount";
            this.txtAdjAmount.Size = new System.Drawing.Size(117, 22);
            this.txtAdjAmount.TabIndex = 5;
            // 
            // cmbAPImpact
            // 
            this.cmbAPImpact.Location = new System.Drawing.Point(972, 16);
            this.cmbAPImpact.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cmbAPImpact.Name = "cmbAPImpact";
            this.cmbAPImpact.Size = new System.Drawing.Size(117, 22);
            this.cmbAPImpact.TabIndex = 6;
            // 
            // txtOrigTicket
            // 
            this.txtOrigTicket.Location = new System.Drawing.Point(1095, 16);
            this.txtOrigTicket.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtOrigTicket.Name = "txtOrigTicket";
            this.txtOrigTicket.Size = new System.Drawing.Size(140, 22);
            this.txtOrigTicket.TabIndex = 7;
            // 
            // txtDocRef
            // 
            this.txtDocRef.Location = new System.Drawing.Point(1241, 16);
            this.txtDocRef.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtDocRef.Name = "txtDocRef";
            this.txtDocRef.Size = new System.Drawing.Size(140, 22);
            this.txtDocRef.TabIndex = 8;
            // 
            // txtRemarks
            // 
            this.txtRemarks.Location = new System.Drawing.Point(15, 73);
            this.txtRemarks.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtRemarks.Name = "txtRemarks";
            this.txtRemarks.Size = new System.Drawing.Size(350, 22);
            this.txtRemarks.TabIndex = 9;
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.Location = new System.Drawing.Point(3, 309);
            this.splitContainerControl1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.splitContainerControl1.Name = "splitContainerControl1";
            this.splitContainerControl1.Panel1.Controls.Add(this.tableLayoutPanelLeft);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            this.splitContainerControl1.Panel2.Controls.Add(this.tableLayoutPanelRight);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(1487, 618);
            this.splitContainerControl1.SplitterPosition = 700;
            this.splitContainerControl1.TabIndex = 2;
            // 
            // tableLayoutPanelLeft
            // 
            this.tableLayoutPanelLeft.ColumnCount = 1;
            this.tableLayoutPanelLeft.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelLeft.Controls.Add(this.lblLegs, 0, 0);
            this.tableLayoutPanelLeft.Controls.Add(this.flowLayoutPanelLegBar, 0, 1);
            this.tableLayoutPanelLeft.Controls.Add(this.gridLegs, 0, 2);
            this.tableLayoutPanelLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelLeft.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanelLeft.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tableLayoutPanelLeft.Name = "tableLayoutPanelLeft";
            this.tableLayoutPanelLeft.RowCount = 3;
            this.tableLayoutPanelLeft.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanelLeft.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 85F));
            this.tableLayoutPanelLeft.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelLeft.Size = new System.Drawing.Size(700, 618);
            this.tableLayoutPanelLeft.TabIndex = 0;
            // 
            // lblLegs
            // 
            this.lblLegs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblLegs.Location = new System.Drawing.Point(3, 4);
            this.lblLegs.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lblLegs.Name = "lblLegs";
            this.lblLegs.Size = new System.Drawing.Size(694, 17);
            this.lblLegs.TabIndex = 0;
            this.lblLegs.Text = "GL LEGS";
            // 
            // flowLayoutPanelLegBar
            // 
            this.flowLayoutPanelLegBar.Controls.Add(this.cmbLegAccount);
            this.flowLayoutPanelLegBar.Controls.Add(this.cmbLegDC);
            this.flowLayoutPanelLegBar.Controls.Add(this.txtLegAmount);
            this.flowLayoutPanelLegBar.Controls.Add(this.txtLegDesc);
            this.flowLayoutPanelLegBar.Controls.Add(this.btnAddLeg);
            this.flowLayoutPanelLegBar.Controls.Add(this.btnRemoveLeg);
            this.flowLayoutPanelLegBar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanelLegBar.Location = new System.Drawing.Point(3, 29);
            this.flowLayoutPanelLegBar.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.flowLayoutPanelLegBar.Name = "flowLayoutPanelLegBar";
            this.flowLayoutPanelLegBar.Size = new System.Drawing.Size(694, 77);
            this.flowLayoutPanelLegBar.TabIndex = 1;
            // 
            // cmbLegAccount
            // 
            this.cmbLegAccount.Location = new System.Drawing.Point(3, 4);
            this.cmbLegAccount.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cmbLegAccount.Name = "cmbLegAccount";
            this.cmbLegAccount.Properties.PopupView = this.cmbLegAccountView;
            this.cmbLegAccount.Size = new System.Drawing.Size(175, 22);
            this.cmbLegAccount.TabIndex = 0;
            // 
            // cmbLegAccountView
            // 
            this.cmbLegAccountView.DetailHeight = 431;
            this.cmbLegAccountView.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.cmbLegAccountView.Name = "cmbLegAccountView";
            this.cmbLegAccountView.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.cmbLegAccountView.OptionsView.ShowGroupPanel = false;
            // 
            // cmbLegDC
            // 
            this.cmbLegDC.Location = new System.Drawing.Point(184, 4);
            this.cmbLegDC.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cmbLegDC.Name = "cmbLegDC";
            this.cmbLegDC.Size = new System.Drawing.Size(93, 22);
            this.cmbLegDC.TabIndex = 1;
            // 
            // txtLegAmount
            // 
            this.txtLegAmount.Location = new System.Drawing.Point(283, 4);
            this.txtLegAmount.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtLegAmount.Name = "txtLegAmount";
            this.txtLegAmount.Size = new System.Drawing.Size(117, 22);
            this.txtLegAmount.TabIndex = 2;
            // 
            // txtLegDesc
            // 
            this.txtLegDesc.Location = new System.Drawing.Point(406, 4);
            this.txtLegDesc.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtLegDesc.Name = "txtLegDesc";
            this.txtLegDesc.Size = new System.Drawing.Size(175, 22);
            this.txtLegDesc.TabIndex = 3;
            // 
            // btnAddLeg
            // 
            this.btnAddLeg.Location = new System.Drawing.Point(3, 34);
            this.btnAddLeg.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnAddLeg.Name = "btnAddLeg";
            this.btnAddLeg.Size = new System.Drawing.Size(110, 36);
            this.btnAddLeg.TabIndex = 4;
            this.btnAddLeg.Text = "Add";
            // 
            // btnRemoveLeg
            // 
            this.btnRemoveLeg.Location = new System.Drawing.Point(119, 34);
            this.btnRemoveLeg.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnRemoveLeg.Name = "btnRemoveLeg";
            this.btnRemoveLeg.Size = new System.Drawing.Size(110, 36);
            this.btnRemoveLeg.TabIndex = 5;
            this.btnRemoveLeg.Text = "Remove";
            // 
            // gridLegs
            // 
            this.gridLegs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridLegs.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gridLegs.Location = new System.Drawing.Point(3, 114);
            this.gridLegs.MainView = this.viewLegs;
            this.gridLegs.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gridLegs.Name = "gridLegs";
            this.gridLegs.Size = new System.Drawing.Size(694, 500);
            this.gridLegs.TabIndex = 2;
            this.gridLegs.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.viewLegs});
            // 
            // viewLegs
            // 
            this.viewLegs.DetailHeight = 431;
            this.viewLegs.GridControl = this.gridLegs;
            this.viewLegs.Name = "viewLegs";
            // 
            // tableLayoutPanelRight
            // 
            this.tableLayoutPanelRight.ColumnCount = 1;
            this.tableLayoutPanelRight.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelRight.Controls.Add(this.lblPend, 0, 0);
            this.tableLayoutPanelRight.Controls.Add(this.gridPending, 0, 1);
            this.tableLayoutPanelRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelRight.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanelRight.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tableLayoutPanelRight.Name = "tableLayoutPanelRight";
            this.tableLayoutPanelRight.RowCount = 2;
            this.tableLayoutPanelRight.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanelRight.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelRight.Size = new System.Drawing.Size(775, 618);
            this.tableLayoutPanelRight.TabIndex = 0;
            // 
            // lblPend
            // 
            this.lblPend.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblPend.Location = new System.Drawing.Point(3, 4);
            this.lblPend.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lblPend.Name = "lblPend";
            this.lblPend.Size = new System.Drawing.Size(769, 17);
            this.lblPend.TabIndex = 0;
            this.lblPend.Text = "PENDING APPROVAL";
            // 
            // gridPending
            // 
            this.gridPending.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridPending.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gridPending.Location = new System.Drawing.Point(3, 29);
            this.gridPending.MainView = this.viewPending;
            this.gridPending.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gridPending.Name = "gridPending";
            this.gridPending.Size = new System.Drawing.Size(769, 585);
            this.gridPending.TabIndex = 1;
            this.gridPending.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.viewPending});
            // 
            // viewPending
            // 
            this.viewPending.DetailHeight = 431;
            this.viewPending.GridControl = this.gridPending;
            this.viewPending.Name = "viewPending";
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
            this.panelToolbar.TabIndex = 3;
            // 
            // lblStatus
            // 
            this.lblStatus.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblStatus.Location = new System.Drawing.Point(1451, 0);
            this.lblStatus.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(36, 16);
            this.lblStatus.TabIndex = 0;
            this.lblStatus.Text = "Status";
            // 
            // flowLayoutPanelToolbar
            // 
            this.flowLayoutPanelToolbar.AutoSize = true;
            this.flowLayoutPanelToolbar.Controls.Add(this.btnPost);
            this.flowLayoutPanelToolbar.Controls.Add(this.btnApprove);
            this.flowLayoutPanelToolbar.Controls.Add(this.btnReject);
            this.flowLayoutPanelToolbar.Controls.Add(this.btnRefresh);
            this.flowLayoutPanelToolbar.Dock = System.Windows.Forms.DockStyle.Left;
            this.flowLayoutPanelToolbar.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanelToolbar.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.flowLayoutPanelToolbar.Name = "flowLayoutPanelToolbar";
            this.flowLayoutPanelToolbar.Size = new System.Drawing.Size(116, 46);
            this.flowLayoutPanelToolbar.TabIndex = 1;
            // 
            // btnPost
            // 
            this.btnPost.Location = new System.Drawing.Point(3, 4);
            this.btnPost.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnPost.Name = "btnPost";
            this.btnPost.Size = new System.Drawing.Size(110, 36);
            this.btnPost.TabIndex = 0;
            this.btnPost.Text = "Post Ticket";
            // 
            // btnApprove
            // 
            this.btnApprove.Location = new System.Drawing.Point(3, 48);
            this.btnApprove.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnApprove.Name = "btnApprove";
            this.btnApprove.Size = new System.Drawing.Size(110, 36);
            this.btnApprove.TabIndex = 1;
            this.btnApprove.Text = "Approve";
            // 
            // btnReject
            // 
            this.btnReject.Location = new System.Drawing.Point(3, 92);
            this.btnReject.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnReject.Name = "btnReject";
            this.btnReject.Size = new System.Drawing.Size(110, 36);
            this.btnReject.TabIndex = 2;
            this.btnReject.Text = "Reject";
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(3, 136);
            this.btnRefresh.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(110, 36);
            this.btnRefresh.TabIndex = 3;
            this.btnRefresh.Text = "Refresh";
            // 
            // ManualTicketFormV1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1493, 985);
            this.Controls.Add(this.tableLayoutPanelMain);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "ManualTicketFormV1";
            this.Text = "Manual Ticketing";
            this.tableLayoutPanelMain.ResumeLayout(false);
            this.panelBanner.ResumeLayout(false);
            this.panelBanner.PerformLayout();
            this.panelHeader.ResumeLayout(false);
            this.flowLayoutPanelHeader.ResumeLayout(false);
            this.flowLayoutPanelHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radSourceType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbAdjType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbSupplier.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbSupplierView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbInvoice.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbInvoiceView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAdjAmount.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbAPImpact.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtOrigTicket.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDocRef.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRemarks.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            this.tableLayoutPanelLeft.ResumeLayout(false);
            this.tableLayoutPanelLeft.PerformLayout();
            this.flowLayoutPanelLegBar.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cmbLegAccount.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbLegAccountView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbLegDC.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLegAmount.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLegDesc.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLegs)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.viewLegs)).EndInit();
            this.tableLayoutPanelRight.ResumeLayout(false);
            this.tableLayoutPanelRight.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridPending)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.viewPending)).EndInit();
            this.panelToolbar.ResumeLayout(false);
            this.panelToolbar.PerformLayout();
            this.flowLayoutPanelToolbar.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelMain;
        private System.Windows.Forms.Panel panelBanner;
        private DevExpress.XtraEditors.LabelControl lblBannerTitle;
        private DevExpress.XtraEditors.LabelControl lblBannerSub;
        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelHeader;

        // Header Controls
        private DevExpress.XtraEditors.RadioGroup radSourceType;
        private DevExpress.XtraEditors.ComboBoxEdit cmbAdjType;
        private DevExpress.XtraEditors.SearchLookUpEdit cmbSupplier;
        private DevExpress.XtraGrid.Views.Grid.GridView viewSupplier;
        private DevExpress.XtraEditors.SearchLookUpEdit cmbInvoice;
        private DevExpress.XtraGrid.Views.Grid.GridView viewInvoice;
        private DevExpress.XtraEditors.LabelControl lblInvoiceBalance;
        private DevExpress.XtraEditors.TextEdit txtAdjAmount;
        private DevExpress.XtraEditors.ComboBoxEdit cmbAPImpact;
        private DevExpress.XtraEditors.TextEdit txtOrigTicket;
        private DevExpress.XtraEditors.TextEdit txtDocRef;
        private DevExpress.XtraEditors.TextEdit txtRemarks;

        // Body
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelLeft;
        private DevExpress.XtraEditors.LabelControl lblLegs;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelLegBar;

        // Leg controls
        private DevExpress.XtraEditors.SearchLookUpEdit cmbLegAccount;
        private DevExpress.XtraGrid.Views.Grid.GridView viewLegAccount;
        private DevExpress.XtraEditors.ComboBoxEdit cmbLegDC;
        private DevExpress.XtraEditors.TextEdit txtLegAmount;
        private DevExpress.XtraEditors.TextEdit txtLegDesc;
        private DevExpress.XtraEditors.SimpleButton btnAddLeg;
        private DevExpress.XtraEditors.SimpleButton btnRemoveLeg;
        private DevExpress.XtraGrid.GridControl gridLegs;
        private DevExpress.XtraGrid.Views.Grid.GridView viewLegs;

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelRight;
        private DevExpress.XtraEditors.LabelControl lblPend;
        private DevExpress.XtraGrid.GridControl gridPending;
        private DevExpress.XtraGrid.Views.Grid.GridView viewPending;

        // Toolbar
        private System.Windows.Forms.Panel panelToolbar;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelToolbar;
        private DevExpress.XtraEditors.LabelControl lblStatus;
        private DevExpress.XtraEditors.SimpleButton btnPost;
        private DevExpress.XtraEditors.SimpleButton btnApprove;
        private DevExpress.XtraEditors.SimpleButton btnReject;
        private DevExpress.XtraEditors.SimpleButton btnRefresh;
        private DevExpress.XtraGrid.Views.Grid.GridView cmbSupplierView;
        private DevExpress.XtraGrid.Views.Grid.GridView cmbInvoiceView;
        private DevExpress.XtraGrid.Views.Grid.GridView cmbLegAccountView;
    }
}