namespace SalesInventorySystem.HOFormsDevEx
{
    partial class TransferPerPalletDevEx
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TransferPerPalletDevEx));
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.txtshipmentno = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.btnsave = new DevExpress.XtraEditors.SimpleButton();
            this.txtproduct = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.searchLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.btnadd = new DevExpress.XtraEditors.SimpleButton();
            this.txtdispatchno = new DevExpress.XtraEditors.TextEdit();
            this.txtbatchno = new DevExpress.XtraEditors.TextEdit();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtpalletno = new DevExpress.XtraEditors.ComboBoxEdit();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.radtocomm = new System.Windows.Forms.RadioButton();
            this.radtobigblue = new System.Windows.Forms.RadioButton();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtshipmentno.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtproduct.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtdispatchno.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtbatchno.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtpalletno.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.txtshipmentno);
            this.groupControl1.Controls.Add(this.btnsave);
            this.groupControl1.Controls.Add(this.txtproduct);
            this.groupControl1.Controls.Add(this.btnadd);
            this.groupControl1.Controls.Add(this.txtdispatchno);
            this.groupControl1.Controls.Add(this.txtbatchno);
            this.groupControl1.Controls.Add(this.label5);
            this.groupControl1.Controls.Add(this.label3);
            this.groupControl1.Controls.Add(this.txtpalletno);
            this.groupControl1.Controls.Add(this.label2);
            this.groupControl1.Controls.Add(this.label1);
            this.groupControl1.Controls.Add(this.label4);
            this.groupControl1.Controls.Add(this.radtocomm);
            this.groupControl1.Controls.Add(this.radtobigblue);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(2138, 306);
            this.groupControl1.TabIndex = 0;
            // 
            // txtshipmentno
            // 
            this.txtshipmentno.Location = new System.Drawing.Point(288, 120);
            this.txtshipmentno.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.txtshipmentno.Name = "txtshipmentno";
            this.txtshipmentno.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.8F);
            this.txtshipmentno.Properties.Appearance.Options.UseFont = true;
            this.txtshipmentno.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtshipmentno.Properties.NullText = "";
            this.txtshipmentno.Properties.PopupView = this.gridView2;
            this.txtshipmentno.Size = new System.Drawing.Size(397, 50);
            this.txtshipmentno.TabIndex = 53;
            this.txtshipmentno.EditValueChanged += new System.EventHandler(this.txtshipmentno_EditValueChanged);
            // 
            // gridView2
            // 
            this.gridView2.DetailHeight = 634;
            this.gridView2.FixedLineWidth = 4;
            this.gridView2.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridView2.Name = "gridView2";
            this.gridView2.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView2.OptionsView.ShowGroupPanel = false;
            // 
            // btnsave
            // 
            this.btnsave.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnsave.ImageOptions.Image")));
            this.btnsave.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btnsave.Location = new System.Drawing.Point(1311, 121);
            this.btnsave.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.btnsave.Name = "btnsave";
            this.btnsave.Size = new System.Drawing.Size(201, 109);
            this.btnsave.TabIndex = 52;
            this.btnsave.Text = "Save";
            this.btnsave.Click += new System.EventHandler(this.btnsave_Click);
            // 
            // txtproduct
            // 
            this.txtproduct.Location = new System.Drawing.Point(288, 176);
            this.txtproduct.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.txtproduct.Name = "txtproduct";
            this.txtproduct.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.8F);
            this.txtproduct.Properties.Appearance.Options.UseFont = true;
            this.txtproduct.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtproduct.Properties.NullText = "";
            this.txtproduct.Properties.PopupView = this.searchLookUpEdit1View;
            this.txtproduct.Size = new System.Drawing.Size(397, 50);
            this.txtproduct.TabIndex = 51;
            this.txtproduct.EditValueChanged += new System.EventHandler(this.txtproduct_EditValueChanged);
            // 
            // searchLookUpEdit1View
            // 
            this.searchLookUpEdit1View.DetailHeight = 634;
            this.searchLookUpEdit1View.FixedLineWidth = 4;
            this.searchLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.searchLookUpEdit1View.Name = "searchLookUpEdit1View";
            this.searchLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.searchLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            // 
            // btnadd
            // 
            this.btnadd.ImageOptions.Image = global::SalesInventorySystem.Properties.Resources.Add_32x32__2_;
            this.btnadd.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btnadd.Location = new System.Drawing.Point(1099, 121);
            this.btnadd.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.btnadd.Name = "btnadd";
            this.btnadd.Size = new System.Drawing.Size(201, 109);
            this.btnadd.TabIndex = 50;
            this.btnadd.Text = "Add Items";
            this.btnadd.Click += new System.EventHandler(this.btnadd_Click);
            // 
            // txtdispatchno
            // 
            this.txtdispatchno.Location = new System.Drawing.Point(884, 183);
            this.txtdispatchno.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.txtdispatchno.Name = "txtdispatchno";
            this.txtdispatchno.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.8F);
            this.txtdispatchno.Properties.Appearance.Options.UseFont = true;
            this.txtdispatchno.Size = new System.Drawing.Size(204, 50);
            this.txtdispatchno.TabIndex = 49;
            // 
            // txtbatchno
            // 
            this.txtbatchno.Location = new System.Drawing.Point(884, 123);
            this.txtbatchno.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.txtbatchno.Name = "txtbatchno";
            this.txtbatchno.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.8F);
            this.txtbatchno.Properties.Appearance.Options.UseFont = true;
            this.txtbatchno.Properties.ReadOnly = true;
            this.txtbatchno.Size = new System.Drawing.Size(204, 50);
            this.txtbatchno.TabIndex = 48;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(695, 190);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(175, 33);
            this.label5.TabIndex = 47;
            this.label5.Text = "Dispatch No.:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(695, 129);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(140, 33);
            this.label3.TabIndex = 46;
            this.label3.Text = "Batch No.:";
            // 
            // txtpalletno
            // 
            this.txtpalletno.Location = new System.Drawing.Point(288, 234);
            this.txtpalletno.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.txtpalletno.Name = "txtpalletno";
            this.txtpalletno.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.8F);
            this.txtpalletno.Properties.Appearance.Options.UseFont = true;
            this.txtpalletno.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtpalletno.Size = new System.Drawing.Size(397, 50);
            this.txtpalletno.TabIndex = 45;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(33, 239);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(195, 33);
            this.label2.TabIndex = 42;
            this.label2.Text = "Select Pallet #:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(33, 183);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(194, 33);
            this.label1.TabIndex = 41;
            this.label1.Text = "Select Product:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(33, 125);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(243, 33);
            this.label4.TabIndex = 40;
            this.label4.Text = "Select Shipment #:";
            // 
            // radtocomm
            // 
            this.radtocomm.AutoSize = true;
            this.radtocomm.Font = new System.Drawing.Font("Tahoma", 8.8F);
            this.radtocomm.Location = new System.Drawing.Point(336, 67);
            this.radtocomm.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.radtocomm.Name = "radtocomm";
            this.radtocomm.Size = new System.Drawing.Size(330, 37);
            this.radtocomm.TabIndex = 1;
            this.radtocomm.Text = "Transfer to Commissary";
            this.radtocomm.UseVisualStyleBackColor = true;
            this.radtocomm.CheckedChanged += new System.EventHandler(this.radtocomm_CheckedChanged);
            // 
            // radtobigblue
            // 
            this.radtobigblue.AutoSize = true;
            this.radtobigblue.Checked = true;
            this.radtobigblue.Font = new System.Drawing.Font("Tahoma", 8.8F);
            this.radtobigblue.Location = new System.Drawing.Point(39, 67);
            this.radtobigblue.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.radtobigblue.Name = "radtobigblue";
            this.radtobigblue.Size = new System.Drawing.Size(274, 37);
            this.radtobigblue.TabIndex = 0;
            this.radtobigblue.TabStop = true;
            this.radtobigblue.Text = "Transfer to BigBlue";
            this.radtobigblue.UseVisualStyleBackColor = true;
            this.radtobigblue.CheckedChanged += new System.EventHandler(this.radtobigblue_CheckedChanged);
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.gridControl1);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(0, 306);
            this.panelControl1.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(2138, 1023);
            this.panelControl1.TabIndex = 1;
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(7, 7, 7, 7);
            this.gridControl1.Location = new System.Drawing.Point(3, 3);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Margin = new System.Windows.Forms.Padding(7, 7, 7, 7);
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(2132, 1017);
            this.gridControl1.TabIndex = 20;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
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
            this.gridView1.OptionsView.ColumnAutoWidth = false;
            this.gridView1.OptionsView.RowAutoHeight = true;
            this.gridView1.OptionsView.ShowFooter = true;
            // 
            // TransferPerPalletDevEx
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 29F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(2138, 1329);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.groupControl1);
            this.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.Name = "TransferPerPalletDevEx";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TransferPerPalletDevEx";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.TransferPerPalletDevEx_Load);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtshipmentno.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtproduct.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtdispatchno.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtbatchno.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtpalletno.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private System.Windows.Forms.RadioButton radtobigblue;
        private System.Windows.Forms.RadioButton radtocomm;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private DevExpress.XtraEditors.ComboBoxEdit txtpalletno;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private DevExpress.XtraEditors.TextEdit txtdispatchno;
        private DevExpress.XtraEditors.TextEdit txtbatchno;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraEditors.SimpleButton btnadd;
        private DevExpress.XtraEditors.SearchLookUpEdit txtproduct;
        private DevExpress.XtraGrid.Views.Grid.GridView searchLookUpEdit1View;
        private DevExpress.XtraEditors.SimpleButton btnsave;
        private DevExpress.XtraEditors.SearchLookUpEdit txtshipmentno;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
    }
}