namespace SalesInventorySystem.LiveTrends
{
    partial class STSMonitoring
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tablePanel1 = new DevExpress.Utils.Layout.TablePanel();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.lblcompleted = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.lblreceiving = new DevExpress.XtraEditors.LabelControl();
            this.lblpending = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.gridControl3 = new DevExpress.XtraGrid.GridControl();
            this.gridView3 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.timerLiveSync = new System.Windows.Forms.Timer(this.components);
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.tablePanel2 = new DevExpress.Utils.Layout.TablePanel();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.lblapproved = new DevExpress.XtraEditors.LabelControl();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.lblrejected = new DevExpress.XtraEditors.LabelControl();
            this.lblforapproval = new DevExpress.XtraEditors.LabelControl();
            this.labelControl9 = new DevExpress.XtraEditors.LabelControl();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tablePanel1)).BeginInit();
            this.tablePanel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView3)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tablePanel2)).BeginInit();
            this.tablePanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tablePanel1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 206);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(1105, 256);
            this.groupBox1.TabIndex = 15;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "DISPATCH";
            // 
            // tablePanel1
            // 
            this.tablePanel1.Appearance.BorderColor = System.Drawing.Color.Black;
            this.tablePanel1.Appearance.Options.UseBorderColor = true;
            this.tablePanel1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.tablePanel1.Columns.AddRange(new DevExpress.Utils.Layout.TablePanelColumn[] {
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 55F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 50F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 50F)});
            this.tablePanel1.Controls.Add(this.labelControl2);
            this.tablePanel1.Controls.Add(this.lblcompleted);
            this.tablePanel1.Controls.Add(this.labelControl3);
            this.tablePanel1.Controls.Add(this.lblreceiving);
            this.tablePanel1.Controls.Add(this.lblpending);
            this.tablePanel1.Controls.Add(this.labelControl1);
            this.tablePanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tablePanel1.Location = new System.Drawing.Point(2, 18);
            this.tablePanel1.Margin = new System.Windows.Forms.Padding(2);
            this.tablePanel1.Name = "tablePanel1";
            this.tablePanel1.Rows.AddRange(new DevExpress.Utils.Layout.TablePanelRow[] {
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 66.5F),
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 26F)});
            this.tablePanel1.ShowGrid = DevExpress.Utils.DefaultBoolean.False;
            this.tablePanel1.Size = new System.Drawing.Size(1101, 236);
            this.tablePanel1.TabIndex = 0;
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.BackColor = System.Drawing.Color.Black;
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Bold);
            this.labelControl2.Appearance.ForeColor = System.Drawing.Color.Aqua;
            this.labelControl2.Appearance.Options.UseBackColor = true;
            this.labelControl2.Appearance.Options.UseFont = true;
            this.labelControl2.Appearance.Options.UseForeColor = true;
            this.labelControl2.Appearance.Options.UseTextOptions = true;
            this.labelControl2.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labelControl2.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.labelControl2.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.labelControl2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.tablePanel1.SetColumn(this.labelControl2, 1);
            this.labelControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelControl2.Location = new System.Drawing.Point(393, 2);
            this.labelControl2.Margin = new System.Windows.Forms.Padding(2);
            this.labelControl2.Name = "labelControl2";
            this.tablePanel1.SetRow(this.labelControl2, 0);
            this.labelControl2.Size = new System.Drawing.Size(351, 63);
            this.labelControl2.TabIndex = 17;
            this.labelControl2.Text = "FOR RECEIVING";
            // 
            // lblcompleted
            // 
            this.lblcompleted.Appearance.BackColor = System.Drawing.Color.Black;
            this.lblcompleted.Appearance.Font = new System.Drawing.Font("Tahoma", 40F);
            this.lblcompleted.Appearance.ForeColor = System.Drawing.Color.Aqua;
            this.lblcompleted.Appearance.Options.UseBackColor = true;
            this.lblcompleted.Appearance.Options.UseFont = true;
            this.lblcompleted.Appearance.Options.UseForeColor = true;
            this.lblcompleted.Appearance.Options.UseTextOptions = true;
            this.lblcompleted.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.lblcompleted.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.lblcompleted.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.lblcompleted.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.tablePanel1.SetColumn(this.lblcompleted, 2);
            this.lblcompleted.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblcompleted.Location = new System.Drawing.Point(748, 69);
            this.lblcompleted.Margin = new System.Windows.Forms.Padding(2);
            this.lblcompleted.Name = "lblcompleted";
            this.tablePanel1.SetRow(this.lblcompleted, 1);
            this.lblcompleted.Size = new System.Drawing.Size(351, 165);
            this.lblcompleted.TabIndex = 17;
            this.lblcompleted.Text = "0";
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.BackColor = System.Drawing.Color.Black;
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Bold);
            this.labelControl3.Appearance.ForeColor = System.Drawing.Color.Aqua;
            this.labelControl3.Appearance.Options.UseBackColor = true;
            this.labelControl3.Appearance.Options.UseFont = true;
            this.labelControl3.Appearance.Options.UseForeColor = true;
            this.labelControl3.Appearance.Options.UseTextOptions = true;
            this.labelControl3.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labelControl3.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.labelControl3.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.labelControl3.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.tablePanel1.SetColumn(this.labelControl3, 2);
            this.labelControl3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelControl3.Location = new System.Drawing.Point(748, 2);
            this.labelControl3.Margin = new System.Windows.Forms.Padding(2);
            this.labelControl3.Name = "labelControl3";
            this.tablePanel1.SetRow(this.labelControl3, 0);
            this.labelControl3.Size = new System.Drawing.Size(351, 63);
            this.labelControl3.TabIndex = 18;
            this.labelControl3.Text = "COMPLETED";
            // 
            // lblreceiving
            // 
            this.lblreceiving.Appearance.BackColor = System.Drawing.Color.Black;
            this.lblreceiving.Appearance.Font = new System.Drawing.Font("Tahoma", 40F);
            this.lblreceiving.Appearance.ForeColor = System.Drawing.Color.Aqua;
            this.lblreceiving.Appearance.Options.UseBackColor = true;
            this.lblreceiving.Appearance.Options.UseFont = true;
            this.lblreceiving.Appearance.Options.UseForeColor = true;
            this.lblreceiving.Appearance.Options.UseTextOptions = true;
            this.lblreceiving.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.lblreceiving.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.lblreceiving.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.lblreceiving.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.tablePanel1.SetColumn(this.lblreceiving, 1);
            this.lblreceiving.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblreceiving.Location = new System.Drawing.Point(393, 69);
            this.lblreceiving.Margin = new System.Windows.Forms.Padding(2);
            this.lblreceiving.Name = "lblreceiving";
            this.tablePanel1.SetRow(this.lblreceiving, 1);
            this.lblreceiving.Size = new System.Drawing.Size(351, 165);
            this.lblreceiving.TabIndex = 16;
            this.lblreceiving.Text = "0";
            // 
            // lblpending
            // 
            this.lblpending.Appearance.BackColor = System.Drawing.Color.Black;
            this.lblpending.Appearance.Font = new System.Drawing.Font("Tahoma", 40F);
            this.lblpending.Appearance.ForeColor = System.Drawing.Color.Aqua;
            this.lblpending.Appearance.Options.UseBackColor = true;
            this.lblpending.Appearance.Options.UseFont = true;
            this.lblpending.Appearance.Options.UseForeColor = true;
            this.lblpending.Appearance.Options.UseTextOptions = true;
            this.lblpending.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.lblpending.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.lblpending.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.lblpending.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.tablePanel1.SetColumn(this.lblpending, 0);
            this.lblpending.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblpending.Location = new System.Drawing.Point(2, 69);
            this.lblpending.Margin = new System.Windows.Forms.Padding(2);
            this.lblpending.Name = "lblpending";
            this.tablePanel1.SetRow(this.lblpending, 1);
            this.lblpending.Size = new System.Drawing.Size(387, 165);
            this.lblpending.TabIndex = 15;
            this.lblpending.Text = "0";
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.BackColor = System.Drawing.Color.Black;
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Bold);
            this.labelControl1.Appearance.ForeColor = System.Drawing.Color.Aqua;
            this.labelControl1.Appearance.Options.UseBackColor = true;
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Appearance.Options.UseForeColor = true;
            this.labelControl1.Appearance.Options.UseTextOptions = true;
            this.labelControl1.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labelControl1.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.labelControl1.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.labelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.tablePanel1.SetColumn(this.labelControl1, 0);
            this.labelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelControl1.Location = new System.Drawing.Point(2, 2);
            this.labelControl1.Margin = new System.Windows.Forms.Padding(2);
            this.labelControl1.Name = "labelControl1";
            this.tablePanel1.SetRow(this.labelControl1, 0);
            this.labelControl1.Size = new System.Drawing.Size(387, 63);
            this.labelControl1.TabIndex = 16;
            this.labelControl1.Text = "PENDING";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.gridControl3);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 462);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox2.Size = new System.Drawing.Size(1105, 269);
            this.groupBox2.TabIndex = 16;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "FOR RECEIVING";
            // 
            // gridControl3
            // 
            this.gridControl3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl3.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gridControl3.Font = new System.Drawing.Font("Tahoma", 12.8F);
            this.gridControl3.Location = new System.Drawing.Point(2, 18);
            this.gridControl3.MainView = this.gridView3;
            this.gridControl3.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gridControl3.Name = "gridControl3";
            this.gridControl3.Size = new System.Drawing.Size(1101, 249);
            this.gridControl3.TabIndex = 6;
            this.gridControl3.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView3});
            // 
            // gridView3
            // 
            this.gridView3.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridView3.Appearance.HeaderPanel.Options.UseFont = true;
            this.gridView3.Appearance.Row.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridView3.Appearance.Row.Options.UseFont = true;
            this.gridView3.DetailHeight = 431;
            this.gridView3.GridControl = this.gridControl3;
            this.gridView3.Name = "gridView3";
            this.gridView3.OptionsBehavior.Editable = false;
            this.gridView3.OptionsBehavior.ReadOnly = true;
            this.gridView3.OptionsPrint.AutoWidth = false;
            this.gridView3.OptionsView.ColumnAutoWidth = false;
            this.gridView3.OptionsView.RowAutoHeight = true;
            // 
            // timerLiveSync
            // 
            this.timerLiveSync.Tick += new System.EventHandler(this.timerLiveSync_Tick);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.tablePanel2);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox3.Location = new System.Drawing.Point(0, 0);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(1105, 206);
            this.groupBox3.TabIndex = 17;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "ORDERS";
            // 
            // tablePanel2
            // 
            this.tablePanel2.Appearance.BorderColor = System.Drawing.Color.Black;
            this.tablePanel2.Appearance.Options.UseBorderColor = true;
            this.tablePanel2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.tablePanel2.Columns.AddRange(new DevExpress.Utils.Layout.TablePanelColumn[] {
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 55F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 50F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 50F)});
            this.tablePanel2.Controls.Add(this.labelControl4);
            this.tablePanel2.Controls.Add(this.lblapproved);
            this.tablePanel2.Controls.Add(this.labelControl6);
            this.tablePanel2.Controls.Add(this.lblrejected);
            this.tablePanel2.Controls.Add(this.lblforapproval);
            this.tablePanel2.Controls.Add(this.labelControl9);
            this.tablePanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tablePanel2.Location = new System.Drawing.Point(3, 19);
            this.tablePanel2.Margin = new System.Windows.Forms.Padding(2);
            this.tablePanel2.Name = "tablePanel2";
            this.tablePanel2.Rows.AddRange(new DevExpress.Utils.Layout.TablePanelRow[] {
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 66.5F),
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 26F)});
            this.tablePanel2.ShowGrid = DevExpress.Utils.DefaultBoolean.False;
            this.tablePanel2.Size = new System.Drawing.Size(1099, 184);
            this.tablePanel2.TabIndex = 1;
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.BackColor = System.Drawing.Color.Black;
            this.labelControl4.Appearance.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Bold);
            this.labelControl4.Appearance.ForeColor = System.Drawing.Color.Lime;
            this.labelControl4.Appearance.Options.UseBackColor = true;
            this.labelControl4.Appearance.Options.UseFont = true;
            this.labelControl4.Appearance.Options.UseForeColor = true;
            this.labelControl4.Appearance.Options.UseTextOptions = true;
            this.labelControl4.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labelControl4.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.labelControl4.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.labelControl4.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.tablePanel2.SetColumn(this.labelControl4, 1);
            this.labelControl4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelControl4.Location = new System.Drawing.Point(392, 2);
            this.labelControl4.Margin = new System.Windows.Forms.Padding(2);
            this.labelControl4.Name = "labelControl4";
            this.tablePanel2.SetRow(this.labelControl4, 0);
            this.labelControl4.Size = new System.Drawing.Size(351, 63);
            this.labelControl4.TabIndex = 17;
            this.labelControl4.Text = "REJECTED";
            // 
            // lblapproved
            // 
            this.lblapproved.Appearance.BackColor = System.Drawing.Color.Black;
            this.lblapproved.Appearance.Font = new System.Drawing.Font("Tahoma", 40F);
            this.lblapproved.Appearance.ForeColor = System.Drawing.Color.Lime;
            this.lblapproved.Appearance.Options.UseBackColor = true;
            this.lblapproved.Appearance.Options.UseFont = true;
            this.lblapproved.Appearance.Options.UseForeColor = true;
            this.lblapproved.Appearance.Options.UseTextOptions = true;
            this.lblapproved.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.lblapproved.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.lblapproved.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.lblapproved.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.tablePanel2.SetColumn(this.lblapproved, 2);
            this.lblapproved.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblapproved.Location = new System.Drawing.Point(746, 69);
            this.lblapproved.Margin = new System.Windows.Forms.Padding(2);
            this.lblapproved.Name = "lblapproved";
            this.tablePanel2.SetRow(this.lblapproved, 1);
            this.lblapproved.Size = new System.Drawing.Size(351, 113);
            this.lblapproved.TabIndex = 17;
            this.lblapproved.Text = "0";
            // 
            // labelControl6
            // 
            this.labelControl6.Appearance.BackColor = System.Drawing.Color.Black;
            this.labelControl6.Appearance.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Bold);
            this.labelControl6.Appearance.ForeColor = System.Drawing.Color.Lime;
            this.labelControl6.Appearance.Options.UseBackColor = true;
            this.labelControl6.Appearance.Options.UseFont = true;
            this.labelControl6.Appearance.Options.UseForeColor = true;
            this.labelControl6.Appearance.Options.UseTextOptions = true;
            this.labelControl6.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labelControl6.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.labelControl6.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.labelControl6.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.tablePanel2.SetColumn(this.labelControl6, 2);
            this.labelControl6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelControl6.Location = new System.Drawing.Point(746, 2);
            this.labelControl6.Margin = new System.Windows.Forms.Padding(2);
            this.labelControl6.Name = "labelControl6";
            this.tablePanel2.SetRow(this.labelControl6, 0);
            this.labelControl6.Size = new System.Drawing.Size(351, 63);
            this.labelControl6.TabIndex = 18;
            this.labelControl6.Text = "APPROVED";
            // 
            // lblrejected
            // 
            this.lblrejected.Appearance.BackColor = System.Drawing.Color.Black;
            this.lblrejected.Appearance.Font = new System.Drawing.Font("Tahoma", 40F);
            this.lblrejected.Appearance.ForeColor = System.Drawing.Color.Lime;
            this.lblrejected.Appearance.Options.UseBackColor = true;
            this.lblrejected.Appearance.Options.UseFont = true;
            this.lblrejected.Appearance.Options.UseForeColor = true;
            this.lblrejected.Appearance.Options.UseTextOptions = true;
            this.lblrejected.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.lblrejected.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.lblrejected.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.lblrejected.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.tablePanel2.SetColumn(this.lblrejected, 1);
            this.lblrejected.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblrejected.Location = new System.Drawing.Point(392, 69);
            this.lblrejected.Margin = new System.Windows.Forms.Padding(2);
            this.lblrejected.Name = "lblrejected";
            this.tablePanel2.SetRow(this.lblrejected, 1);
            this.lblrejected.Size = new System.Drawing.Size(351, 113);
            this.lblrejected.TabIndex = 16;
            this.lblrejected.Text = "0";
            // 
            // lblforapproval
            // 
            this.lblforapproval.Appearance.BackColor = System.Drawing.Color.Black;
            this.lblforapproval.Appearance.Font = new System.Drawing.Font("Tahoma", 40F);
            this.lblforapproval.Appearance.ForeColor = System.Drawing.Color.Lime;
            this.lblforapproval.Appearance.Options.UseBackColor = true;
            this.lblforapproval.Appearance.Options.UseFont = true;
            this.lblforapproval.Appearance.Options.UseForeColor = true;
            this.lblforapproval.Appearance.Options.UseTextOptions = true;
            this.lblforapproval.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.lblforapproval.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.lblforapproval.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.lblforapproval.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.tablePanel2.SetColumn(this.lblforapproval, 0);
            this.lblforapproval.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblforapproval.Location = new System.Drawing.Point(2, 69);
            this.lblforapproval.Margin = new System.Windows.Forms.Padding(2);
            this.lblforapproval.Name = "lblforapproval";
            this.tablePanel2.SetRow(this.lblforapproval, 1);
            this.lblforapproval.Size = new System.Drawing.Size(386, 113);
            this.lblforapproval.TabIndex = 15;
            this.lblforapproval.Text = "0";
            // 
            // labelControl9
            // 
            this.labelControl9.Appearance.BackColor = System.Drawing.Color.Black;
            this.labelControl9.Appearance.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Bold);
            this.labelControl9.Appearance.ForeColor = System.Drawing.Color.Lime;
            this.labelControl9.Appearance.Options.UseBackColor = true;
            this.labelControl9.Appearance.Options.UseFont = true;
            this.labelControl9.Appearance.Options.UseForeColor = true;
            this.labelControl9.Appearance.Options.UseTextOptions = true;
            this.labelControl9.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labelControl9.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.labelControl9.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.labelControl9.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.tablePanel2.SetColumn(this.labelControl9, 0);
            this.labelControl9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelControl9.Location = new System.Drawing.Point(2, 2);
            this.labelControl9.Margin = new System.Windows.Forms.Padding(2);
            this.labelControl9.Name = "labelControl9";
            this.tablePanel2.SetRow(this.labelControl9, 0);
            this.labelControl9.Size = new System.Drawing.Size(386, 63);
            this.labelControl9.TabIndex = 16;
            this.labelControl9.Text = "FOR APPROVAL";
            // 
            // STSMonitoring
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1105, 731);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox3);
            this.Name = "STSMonitoring";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "STSMonitoring";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.STSMonitoring_FormClosing);
            this.Load += new System.EventHandler(this.STSMonitoring_Load);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tablePanel1)).EndInit();
            this.tablePanel1.ResumeLayout(false);
            this.tablePanel1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView3)).EndInit();
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tablePanel2)).EndInit();
            this.tablePanel2.ResumeLayout(false);
            this.tablePanel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private DevExpress.Utils.Layout.TablePanel tablePanel1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl lblcompleted;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl lblreceiving;
        private DevExpress.XtraEditors.LabelControl lblpending;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private System.Windows.Forms.GroupBox groupBox2;
        private DevExpress.XtraGrid.GridControl gridControl3;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView3;
        private System.Windows.Forms.Timer timerLiveSync;
        private System.Windows.Forms.GroupBox groupBox3;
        private DevExpress.Utils.Layout.TablePanel tablePanel2;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl lblapproved;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.LabelControl lblrejected;
        private DevExpress.XtraEditors.LabelControl lblforapproval;
        private DevExpress.XtraEditors.LabelControl labelControl9;
    }
}