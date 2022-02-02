namespace SalesInventorySystem.HOFormsDevEx
{
    partial class InventoryCostAdjustment
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.radlinktosupplier = new System.Windows.Forms.RadioButton();
            this.txtsupplier = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.searchLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.txtproduct = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.gridView3 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.txtshipmentno = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.ShipmentNo = new DevExpress.XtraEditors.LabelControl();
            this.lblsupplier = new DevExpress.XtraEditors.LabelControl();
            this.txtorigcost = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.txtorigqty = new DevExpress.XtraEditors.TextEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.txtnewcostvalue = new DevExpress.XtraEditors.TextEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtsupplier.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtproduct.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtshipmentno.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtorigcost.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtorigqty.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtnewcostvalue.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.radlinktosupplier);
            this.panel1.Location = new System.Drawing.Point(127, 15);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(240, 34);
            this.panel1.TabIndex = 92;
            // 
            // radlinktosupplier
            // 
            this.radlinktosupplier.AutoSize = true;
            this.radlinktosupplier.Font = new System.Drawing.Font("Tahoma", 9.25F);
            this.radlinktosupplier.Location = new System.Drawing.Point(3, 6);
            this.radlinktosupplier.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.radlinktosupplier.Name = "radlinktosupplier";
            this.radlinktosupplier.Size = new System.Drawing.Size(141, 23);
            this.radlinktosupplier.TabIndex = 85;
            this.radlinktosupplier.TabStop = true;
            this.radlinktosupplier.Text = "Link to Supplier";
            this.radlinktosupplier.UseVisualStyleBackColor = true;
            this.radlinktosupplier.CheckedChanged += new System.EventHandler(this.radlinktosupplier_CheckedChanged);
            // 
            // txtsupplier
            // 
            this.txtsupplier.Location = new System.Drawing.Point(131, 57);
            this.txtsupplier.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtsupplier.Name = "txtsupplier";
            this.txtsupplier.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtsupplier.Properties.Appearance.Options.UseFont = true;
            this.txtsupplier.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtsupplier.Properties.NullText = "";
            this.txtsupplier.Properties.PopupView = this.searchLookUpEdit1View;
            this.txtsupplier.Size = new System.Drawing.Size(284, 28);
            this.txtsupplier.TabIndex = 91;
            this.txtsupplier.EditValueChanged += new System.EventHandler(this.txtsupplier_EditValueChanged);
            // 
            // searchLookUpEdit1View
            // 
            this.searchLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.searchLookUpEdit1View.Name = "searchLookUpEdit1View";
            this.searchLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.searchLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            // 
            // txtproduct
            // 
            this.txtproduct.Location = new System.Drawing.Point(131, 126);
            this.txtproduct.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtproduct.Name = "txtproduct";
            this.txtproduct.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtproduct.Properties.Appearance.Options.UseFont = true;
            this.txtproduct.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtproduct.Properties.NullText = "";
            this.txtproduct.Properties.PopupView = this.gridView3;
            this.txtproduct.Size = new System.Drawing.Size(284, 28);
            this.txtproduct.TabIndex = 97;
            this.txtproduct.EditValueChanged += new System.EventHandler(this.txtproduct_EditValueChanged);
            // 
            // gridView3
            // 
            this.gridView3.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridView3.Name = "gridView3";
            this.gridView3.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView3.OptionsView.ShowGroupPanel = false;
            // 
            // labelControl7
            // 
            this.labelControl7.Appearance.Font = new System.Drawing.Font("Tahoma", 10F);
            this.labelControl7.Appearance.Options.UseFont = true;
            this.labelControl7.Location = new System.Drawing.Point(12, 129);
            this.labelControl7.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(119, 21);
            this.labelControl7.TabIndex = 96;
            this.labelControl7.Text = "Product Details:";
            // 
            // txtshipmentno
            // 
            this.txtshipmentno.Location = new System.Drawing.Point(131, 91);
            this.txtshipmentno.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtshipmentno.Name = "txtshipmentno";
            this.txtshipmentno.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtshipmentno.Properties.Appearance.Options.UseFont = true;
            this.txtshipmentno.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtshipmentno.Properties.NullText = "";
            this.txtshipmentno.Properties.PopupView = this.gridView2;
            this.txtshipmentno.Size = new System.Drawing.Size(284, 28);
            this.txtshipmentno.TabIndex = 95;
            this.txtshipmentno.EditValueChanged += new System.EventHandler(this.txtshipmentno_EditValueChanged);
            // 
            // gridView2
            // 
            this.gridView2.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridView2.Name = "gridView2";
            this.gridView2.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView2.OptionsView.ShowGroupPanel = false;
            // 
            // ShipmentNo
            // 
            this.ShipmentNo.Appearance.Font = new System.Drawing.Font("Tahoma", 10F);
            this.ShipmentNo.Appearance.Options.UseFont = true;
            this.ShipmentNo.Location = new System.Drawing.Point(30, 95);
            this.ShipmentNo.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ShipmentNo.Name = "ShipmentNo";
            this.ShipmentNo.Size = new System.Drawing.Size(95, 21);
            this.ShipmentNo.TabIndex = 94;
            this.ShipmentNo.Text = "ShipmentNo:";
            // 
            // lblsupplier
            // 
            this.lblsupplier.Appearance.Font = new System.Drawing.Font("Tahoma", 10F);
            this.lblsupplier.Appearance.Options.UseFont = true;
            this.lblsupplier.Location = new System.Drawing.Point(56, 60);
            this.lblsupplier.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lblsupplier.Name = "lblsupplier";
            this.lblsupplier.Size = new System.Drawing.Size(65, 21);
            this.lblsupplier.TabIndex = 93;
            this.lblsupplier.Text = "Supplier:";
            // 
            // txtorigcost
            // 
            this.txtorigcost.EditValue = "";
            this.txtorigcost.Location = new System.Drawing.Point(131, 194);
            this.txtorigcost.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtorigcost.Name = "txtorigcost";
            this.txtorigcost.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtorigcost.Properties.Appearance.Options.UseFont = true;
            this.txtorigcost.Properties.ReadOnly = true;
            this.txtorigcost.Size = new System.Drawing.Size(122, 28);
            this.txtorigcost.TabIndex = 99;
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 10F);
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Location = new System.Drawing.Point(36, 198);
            this.labelControl1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(94, 21);
            this.labelControl1.TabIndex = 98;
            this.labelControl1.Text = "Orig CostKg:";
            // 
            // txtorigqty
            // 
            this.txtorigqty.EditValue = "";
            this.txtorigqty.Location = new System.Drawing.Point(131, 160);
            this.txtorigqty.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtorigqty.Name = "txtorigqty";
            this.txtorigqty.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtorigqty.Properties.Appearance.Options.UseFont = true;
            this.txtorigqty.Properties.ReadOnly = true;
            this.txtorigqty.Size = new System.Drawing.Size(122, 28);
            this.txtorigqty.TabIndex = 101;
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Tahoma", 10F);
            this.labelControl2.Appearance.Options.UseFont = true;
            this.labelControl2.Location = new System.Drawing.Point(17, 164);
            this.labelControl2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(111, 21);
            this.labelControl2.TabIndex = 100;
            this.labelControl2.Text = "Orig Total Qty:";
            // 
            // txtnewcostvalue
            // 
            this.txtnewcostvalue.EditValue = "0";
            this.txtnewcostvalue.Location = new System.Drawing.Point(131, 229);
            this.txtnewcostvalue.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtnewcostvalue.Name = "txtnewcostvalue";
            this.txtnewcostvalue.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtnewcostvalue.Properties.Appearance.Options.UseFont = true;
            this.txtnewcostvalue.Size = new System.Drawing.Size(122, 28);
            this.txtnewcostvalue.TabIndex = 103;
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Tahoma", 10F);
            this.labelControl3.Appearance.Options.UseFont = true;
            this.labelControl3.Location = new System.Drawing.Point(48, 233);
            this.labelControl3.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(77, 21);
            this.labelControl3.TabIndex = 102;
            this.labelControl3.Text = "New Cost:";
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Tahoma", 10F);
            this.button2.Location = new System.Drawing.Point(220, 263);
            this.button2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(87, 36);
            this.button2.TabIndex = 105;
            this.button2.Text = "Cancel";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Tahoma", 10F);
            this.button1.Location = new System.Drawing.Point(127, 263);
            this.button1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(87, 36);
            this.button1.TabIndex = 104;
            this.button1.Text = "Save";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // InventoryCostAdjustment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(436, 314);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txtnewcostvalue);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.txtorigqty);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.txtorigcost);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.txtproduct);
            this.Controls.Add(this.labelControl7);
            this.Controls.Add(this.txtshipmentno);
            this.Controls.Add(this.ShipmentNo);
            this.Controls.Add(this.lblsupplier);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.txtsupplier);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "InventoryCostAdjustment";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "InventoryCostAdjustment";
            this.Load += new System.EventHandler(this.InventoryCostAdjustment_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtsupplier.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtproduct.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtshipmentno.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtorigcost.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtorigqty.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtnewcostvalue.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton radlinktosupplier;
        private DevExpress.XtraEditors.SearchLookUpEdit txtsupplier;
        private DevExpress.XtraGrid.Views.Grid.GridView searchLookUpEdit1View;
        private DevExpress.XtraEditors.SearchLookUpEdit txtproduct;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView3;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        private DevExpress.XtraEditors.SearchLookUpEdit txtshipmentno;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private DevExpress.XtraEditors.LabelControl ShipmentNo;
        private DevExpress.XtraEditors.LabelControl lblsupplier;
        public DevExpress.XtraEditors.TextEdit txtorigcost;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        public DevExpress.XtraEditors.TextEdit txtorigqty;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        public DevExpress.XtraEditors.TextEdit txtnewcostvalue;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
    }
}